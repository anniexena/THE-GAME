using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // For the interact sprite [E]
    [Header("Interaction Cue")]
    [SerializeField] private GameObject interactionCue;

    // For the INK dialogue
    [Header("Ink JSON")]
    [SerializeField] private TextAsset ink0;
    [SerializeField] private TextAsset ink1;
    [SerializeField] private TextAsset ink2;
    [SerializeField] private TextAsset ink3;

    // Tracks if player can interact with NPC
    private bool playerIsInRange;

    // For NPC name and sprite
    [SerializeField] private Sprite NPCSprite;
    [SerializeField] private string NPCName;
    [SerializeField] private int questid;
    [SerializeField] QuestManager questManager;

    private void Awake()
    {
        playerIsInRange = false;
        interactionCue.SetActive(false);
        interactionCue.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    private void Update()
    {
        if (playerIsInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            interactionCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                // OLD
                // DialogueManager.GetInstance().EnterDialogue(inkJSON, NPCSprite, NPCName);
                DialogueManager.GetInstance().EnterDialogue(SetStory(), NPCSprite, NPCName, gameObject);
            }
        }
        else
        {
            interactionCue.SetActive(false);
        }
    }

    // Checks the quest status and sets the ink json based on that
    private TextAsset SetStory()
    {
        if (questid == 0)
        {
            return ink0;
        }
        QuestNode curr = questManager.activeQuests.head;

        while (curr != null)
        {
            if (curr.quest.questid == questid)
            {
                // Check for status of quest
                switch (curr.quest.completionStatus)
                {
                    case 0:
                        // Initial dialogue
                        return ink0;
                    case 1:
                        // Doesnt have resources
                        return ink1;
                    case 2:
                        // Has resources, could turn in
                        return ink2;
                    case 3:
                        // Turned in
                        return ink3;
                    default:
                        // Error
                        Debug.Log("Quest status invalid: " + curr.quest.completionStatus);
                        break;
                }
            }
        }
        return ink0;
    }

    // Checks if player is in range
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerIsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerIsInRange = false;
        }
    }
}
