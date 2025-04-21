using UnityEngine;

// Class for all stats of the player like inventory, sustainability level, etc...
public class Player_Stats : MonoBehaviour
{
    public TownHealth TH;
    public ForestHealth FH;

    // ! NOTE: this shouldnt be a serialized field, this is just for testing ! //

    public float getSL() { return (FH.getHealth() + TH.getHealth()) / 2f; } // (0-8)

    public float getTH() { return (TH.getHealth()); } // (0-8)
    public float getFH() { return (FH.getHealth()); } // (0-8)
}
