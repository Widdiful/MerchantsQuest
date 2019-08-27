using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomGreeting : MonoBehaviour
{
    public TMP_Text greetingsText;
    public TextAsset greetingsFile;

    string text;
    string textToShow = "";
    public float fSize;
    public float textSpeed = 0.1f;

    public string[] greetings;
    private void Awake()
    {
        greetings = Helpers.ReadFile(greetingsFile);
    }

    public void OnEnable()
    {
        text = "";
        textToShow = greetings[Random.Range(0, greetings.Length)];

        greetingsText.enableAutoSizing = true;
        greetingsText.text = textToShow;
        greetingsText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        greetingsText.ForceMeshUpdate();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        yield return new WaitForFixedUpdate();
        fSize = greetingsText.fontSize;
        greetingsText.text = "";
        greetingsText.enableAutoSizing = false;
        greetingsText.fontSize = fSize;
        greetingsText.color = Color.black;
        for (int i = 0; i < textToShow.Length; i++)
        {
            char s = textToShow[i];
            if (s == '/')
            {
                s = '\n';
            }
            text += s;
            greetingsText.text = text;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
