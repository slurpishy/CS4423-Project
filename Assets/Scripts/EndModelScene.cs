using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndModelScene : MonoBehaviour
{
    static public bool win = false;
    [SerializeField]
    private TMP_Text text;

    void Start() {
        if (win) {
            text.text = "Winner!";
        } else {
            text.text = "You Lose.";
        }
    }

}
