using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    // For the dialogue canvas
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private Story currentStory;

    // For name and sprite of NPC
    [SerializeField] private Image dialogueImage;
    [SerializeField] private Text nameText;
    [SerializeField] private int questid;

    // For making choices
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    // For quests
    [SerializeField] QuestManager questManager;

    // Other stuff
    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager instance;
    [SerializeField] public GameObject nextButton;
    public bool isFollowing;
    private Player_Stats playerStats;
    private float sustainability_level;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiple dialogue managers");
        }

        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialogueBox.SetActive(false);

        // Get choice boxes
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    //ADDED parent field - atreyu
    public void EnterDialogue(TextAsset inkJSON, Sprite NPCSprite, string NPCName, GameObject parent)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueBox.SetActive(true);

        // Update sprite
        if (NPCSprite != null)
        {
            dialogueImage.sprite = NPCSprite;
        }

        // Update NPC name
        if (!string.IsNullOrEmpty(NPCName))
        {
            nameText.text = NPCName;
        }

        //NEW
        NPC npc = parent.GetComponent<NPC>();
        if (npc != null)
        {
            UpdateHC(npc);
            UpdateHS(npc);
        }
        //END NEW

        // Update sustainability levels
        UpdateStates();

        ContinueStory();
    }

    private void UpdateStates()
    {
        UpdateSL();
        UpdateTH();
        UpdateSH();
    }

    private void UpdateTH()
    {
        GameObject player = GameObject.Find("Player");
        playerStats = player.GetComponent<Player_Stats>();
        float TH = playerStats.getTH();
        if (currentStory.variablesState["town_health"] != null)
        {
            currentStory.variablesState["town_health"] = TH;
        }
    }

    private void UpdateSH()
    {
        GameObject player = GameObject.Find("Player");
        playerStats = player.GetComponent<Player_Stats>();
        float FH = playerStats.getFH();
        if (currentStory.variablesState["forest_health"] != null)
        {
            currentStory.variablesState["forest_health"] = FH;
        }
    }

    private void UpdateSL()
    {
        // Get + set sustainability level
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            Debug.LogError("Player object could not be found!");
        }
        else
        {
            playerStats = player.GetComponent<Player_Stats>();
            if (playerStats == null)
            {
                Debug.LogError("Player stats script could not be found!");
            }
            else
            {
                sustainability_level = playerStats.getSL();
                if (currentStory.variablesState["sustainability_lvl"] != null)
                {
                    currentStory.variablesState["sustainability_lvl"] = sustainability_level;
                }
            }
        }
    }

    private void UpdateHS(NPC npc)
    {
        int houseBroken = npc.getHouseState();
        if (currentStory.variablesState["houseBroken"] != null)
        {
            currentStory.variablesState["houseBroken"] = houseBroken;
        }
    }

    private void UpdateHC(NPC npc)
    {
        int houseCost = npc.getHouseCost();
        if (currentStory.variablesState["houseCost"] != null)
        {
            currentStory.variablesState["houseCost"] = houseCost;
        }

        string houseType = npc.getHouseType();
        if (currentStory.variablesState["houseType"] != null)
        {
            currentStory.variablesState["houseType"] = houseType;
        }

    }

    // For clicking continue button in dialogue
    public void ContinueStory()
    {
        // Display dialogue if there is more
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            // Display choices if there are any
            DisplayChoices();
        }
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        // Check for specific conditions //

        // Check for dog following
        if (currentStory.variablesState["followPlayer"] != null)
        {
            isFollowing = (bool)currentStory.variablesState["followPlayer"];
        }

        //Check for quest start
        if (currentStory.variablesState["questActive"] != null && 
            (bool)currentStory.variablesState["questActive"] == true)
        {
            HandleQuest();
        }

        dialogueIsPlaying = false;
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // Check to make sure that there aren't too many choices to display
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too many choices given. " +
                "Can only support: " + currentChoices.Count);
        }

        // Display choices
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++; 
        }

        if (index != 0)
        {
            nextButton.SetActive(false);
        }

        // Hide unneeded choices for this dialogue
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    public void OnChoiceSelected(int index)
    {
        currentStory.ChooseChoiceIndex(index);
        nextButton.SetActive(true);
        ContinueStory();
    }

    public void HandleQuest()
    {
        int questid = (int)currentStory.variablesState["questid"];
        string questItem = (string)currentStory.variablesState["questItem"];
        int questAmount = (int)currentStory.variablesState["questAmount"];

        // First, check if they are turning in the quest
        // Active quests dont have descriptions in their ink files
        // Only turninable quests have "turnedIn" states in their ink files
        // Else, start the quest
        if (currentStory.variablesState["questDescription"] == null)
        {
            if (currentStory.variablesState["turnedIn"] != null)
            {
                QuestNode curr = questManager.activeQuests.head;
                while (curr != null)
                {
                    if (curr.quest.questid == questid)
                    {
                        // If they choose to turn in the quest
                        if ((bool)currentStory.variablesState["turnedIn"])
                        {
                            curr.quest.completionStatus = 3;
                            questManager.turnInQuest(questItem, questAmount);
                            questManager.UpdateQuestUI();
                        }
                    }
                    curr = curr.next;
                }
            }
        }
        else
        {
            string questDescription = (string)currentStory.variablesState["questDescription"];
            questManager.StartQuest(questid, questDescription, questItem, questAmount);
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }
}
