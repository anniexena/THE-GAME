using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dog_Script : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string[] dialogue;

    public GameObject nextButton;
    private int index;
    public float wordSpeed;
    public bool playerIsInRange;

    [SerializeField] private Sprite dialogueSprite;
    [SerializeField] private Image dialogueImage;
    [SerializeField] private string characterName;
    [SerializeField] private Text nameText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInRange) {

            if (dialogueSprite != null && dialogueImage != null) {
                dialogueImage.sprite = dialogueSprite;
            }

            if (!string.IsNullOrEmpty(characterName) && nameText != null) {
                nameText.text = characterName;
            }

            if (dialogueBox.activeInHierarchy) {
                resetText();
            }
            else {
                dialogueBox.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index]) {
            nextButton.SetActive(true);
        }
    }

    IEnumerator Typing() {
        foreach(char letter in dialogue[index].ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine() {

        nextButton.SetActive(false);

        if(index < dialogue.Length - 1) {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else {
            resetText();
        }
    }

    // Checks if player is in range
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsInRange = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsInRange = false;
            resetText();
        }
    }

    // Resets textbox to empty
    public void resetText() {
        dialogueText.text = "";
        index = 0;
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
            SceneManager.LoadScene("TreeGame");
    }
}
