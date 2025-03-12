using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    public Node[] nodes = new Node[9];
    public TextField[] textFields = new TextField[4];

    void Start() {
        // init textfields with random numbers
        foreach (TextField tf in textFields) {
            tf.randomInt = Random.Range(1,5);
        }

        checkTextFields();
    }

    public void checkTextFields() {
        for (int i = 0; i < textFields.Length; i++) {
            textFields[i].CheckSatisfied();
        }
    }

    public void setNode(int nodeId, bool isActive) {

    }
}
