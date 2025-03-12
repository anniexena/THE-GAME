using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue/Character")]
public class DialogueCharacter : ScriptableObject
{
    [SerializeField] private string myCharacterName;
    public string CharacterName => myCharacterName;
}
