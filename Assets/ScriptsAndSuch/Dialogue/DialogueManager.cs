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

    // For making choices
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    // Other stuff
    public bool dialogueIsPlaying { get; private set; }
    private static DialogueManager instance;
    [SerializeField] public GameObject nextButton;
    public bool isFollowing;

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

        // Get choices
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

    public void EnterDialogue(TextAsset inkJSON, Sprite NPCSprite, string NPCName)
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

        ContinueStory();
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

    public static DialogueManager GetInstance()
    {
        return instance;
    }
}
