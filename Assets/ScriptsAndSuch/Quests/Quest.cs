using UnityEngine;

public class Quest
{
    public int questid { get; set; }
    public string questDescription { get; set; }
    public string questItem { get; set; }
    public int questAmount { get; set; }

    // 0 = initial dialogue
    // 1 = active, but not fulfilled
    // 2 = active, fulfilled, but not turned in
    // 3 = turned in
    public int completionStatus { get; set; } 


    public Quest(int questid, string questDescription, string questItem, int questAmount)
    {
        this.questid = questid;
        this.questDescription = questDescription;
        this.questItem = questItem;
        this.questAmount = questAmount;
        this.completionStatus = 1;
    }
}
