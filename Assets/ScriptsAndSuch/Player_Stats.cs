using UnityEngine;

// Class for all stats of the player like inventory, sustainability level, etc...
public class Player_Stats : MonoBehaviour
{
    [SerializeField] int sustainability_level;
    // ! NOTE: this shouldnt be a serialized field, this is just for testing ! //

    public int getSL()
    {
        return sustainability_level;
    }

    public void setSL(int sl)
    {
        sustainability_level = sl;
    }
}
