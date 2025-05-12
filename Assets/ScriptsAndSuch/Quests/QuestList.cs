using Unity.VisualScripting;
using UnityEngine;

public class QuestList
{
    public QuestNode head;

    // Initialize quest list
    public QuestList()
    {
        head = null;
    }

    // Adds a quest to the quest list
    public void AddQuest(Quest quest)
    {
        QuestNode newQuest = new QuestNode(quest);
        if (head == null)
        {
            head = newQuest;
        }
        else
        {
            QuestNode curr = head;
            while (curr.next != null)
            {
                curr = curr.next;
            }
            curr.next = newQuest;
        }
    }

    // Deletes a quest, im not using this but im keeping it here just in case
    public void CompleteQuest(int questid)
    {
        // Check for null active quests
        if (head == null)
        {
            Debug.Log("Tried to complete quest with no active quests");
            return;
        }
        
        // Check if active quest is the head
        if (head.quest.questid == questid)
        {
            head = head.next;
            Debug.Log("Completed quest: " + questid);
            return;
        }

        // Else, find quest in LL
        QuestNode curr = head;

        while (curr.next != null && curr.next.quest.questid != questid)
        {
            curr = curr.next;
        }

        if (curr.next != null)
        {
            curr.next = curr.next.next;
        }
}
}
