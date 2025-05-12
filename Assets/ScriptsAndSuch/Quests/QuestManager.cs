using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questTextBox;
    [SerializeField] Inventory inventory;
    public QuestList activeQuests;


    void Start()
    {
        activeQuests = new QuestList();
        questTextBox.text = "";
    }

    // Check for completion of each quest
    // Runs after getting/losing an item and on quest start
    public void CheckQuestProgress(string questItem)
    {
        Debug.Log("Checking quest progress");
        QuestNode curr = activeQuests.head;
        while (curr != null)
        {
            if ((curr.quest.questItem == questItem) && 
                (inventory.getWood(questItem) >= curr.quest.questAmount))
            {
                curr.quest.completionStatus = 2;
            }
            else
            {
                curr.quest.completionStatus = 1;
            }
            curr = curr.next;
        }

        Debug.Log("Done checking");
    }

    public void StartQuest(int questid, string questDescription, string questItem, int questAmount)
    {
        activeQuests.AddQuest(new Quest(questid, questDescription, questItem, questAmount));
        UpdateQuestUI();
        Debug.Log("Starting quest: " + questid);
    }

    public void UpdateQuestUI()
    {
        questTextBox.text = "";
        QuestNode curr = activeQuests.head;

        while(curr != null)
        {
            if (curr.quest.completionStatus != 3)
            {
                questTextBox.text += $"{curr.quest.questDescription}\n";
            }
            curr = curr.next;
        }

        if (questTextBox.text == "")
        {
            questTextBox.text = "Talk to townspeople to find quests!";
        }
    }

    public void turnInQuest(string questItem, int questAmount)
    {
        inventory.turnInQuest(questItem, questAmount);
    }
}
