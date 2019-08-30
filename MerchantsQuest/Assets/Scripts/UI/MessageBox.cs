using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public float defaultWaitTime;
    public Canvas messageCanvas;
    public TextMeshProUGUI messageText;
    private bool messageBoxOpen;

    public static MessageBox instance;

    private void Awake() {
        instance = this;
    }

    public void NewMessage(string message) {
        NewMessage(message, defaultWaitTime);
    }

    public void NewMessage(string message, float duration) {
        StartCoroutine(Message(message, duration));
    }

    IEnumerator Message(string message, float duration) {
        while (messageBoxOpen) {
            yield return null;
        }

        messageBoxOpen = true;
        messageCanvas.enabled = true;
        messageText.text = message;
        yield return new WaitForSeconds(duration);
        messageCanvas.enabled = false;
        messageBoxOpen = false;
    }
}
