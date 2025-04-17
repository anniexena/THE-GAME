using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // For the interact sprite [E]
    [Header("Interaction Cue")]
    [SerializeField] private GameObject interactionCue;

    // For the INK dialogue
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    // Tracks if player can interact with NPC
    private bool playerIsInRange;

    // For NPC name and sprite
    [SerializeField] private Sprite NPCSprite;
    [SerializeField] private string NPCName;

    private void Awake()
    {
        playerIsInRange = false;
        interactionCue.SetActive(false);
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
                DialogueManager.GetInstance().EnterDialogue(inkJSON, NPCSprite, NPCName, gameObject);
            }
        }
        else
        {
            interactionCue.SetActive(false);
        }
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
