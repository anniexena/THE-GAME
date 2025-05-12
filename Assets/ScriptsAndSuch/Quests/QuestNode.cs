using UnityEngine;

public class QuestNode
{
    public Quest quest {  get; set; }
    public QuestNode next {  get; set; }

    public QuestNode(Quest quest)
    {
        this.quest = quest;
        next = null;
    }
}
