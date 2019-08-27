using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Helpers
{
    public static string[] ReadFile(TextAsset file)
    {
        if (file != null)
        {
            string text = file.text;
            string[] textList = Regex.Split(text, "\n");
            return textList;
        }
        else
        {
            Debug.Log("Failed to read file, " + file.name);
            return null;
        }
    }
}
