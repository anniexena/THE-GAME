using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dog_Script : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string[] dialogue;

    public GameObject nextButton;
    private int index;
    public float wordSpeed;
    public bool playerIsInRange;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInRange) {
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
    }
}
