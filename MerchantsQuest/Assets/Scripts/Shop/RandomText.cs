using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomText : MonoBehaviour
{
    public TMP_Text tmpTextObject;
    public TextAsset textFile;

    string text;
    string textToShow = "";
    public float fSize;
    public float textSpeed = 2.0f;
    public float fadeSpeed = 40.0f;

    public Color32 startCol, endCol;

    public string[] greetings;
    private void Awake()
    {
        greetings = Helpers.ReadFile(textFile);
    }

    public void OnEnable()
    {
        text = "";
        textToShow = greetings[Random.Range(0, greetings.Length)];

        tmpTextObject.text = textToShow;
        tmpTextObject.color = new Color(tmpTextObject.color.r, tmpTextObject.color.g, tmpTextObject.color.b, 0.0f);
        tmpTextObject.ForceMeshUpdate();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        tmpTextObject.ForceMeshUpdate();

        //Taken from a text mesh pro example

        TMP_TextInfo textInfo = tmpTextObject.textInfo;
        Color32[] newVertexColors;

        int currentCharacter = 0;
        int startingCharacterRange = currentCharacter;
        bool isRangeMax = false;

        while (!isRangeMax)
        {
            int characterCount = textInfo.characterCount;

            // Spread should not exceed the number of characters.
            byte fadeSteps = (byte)Mathf.Max(1, 255 / textSpeed);


            for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
            {
                // Skip characters that are not visible
                if (!textInfo.characterInfo[i].isVisible) continue;

                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                // Get the vertex colors of the mesh used by this text element (character or sprite).
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // Get the current character's alpha value.
                byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex + 0].a + fadeSteps, 0, 255);

                // Set new alpha values.
                newVertexColors[vertexIndex + 0].a = alpha;
                newVertexColors[vertexIndex + 1].a = alpha;
                newVertexColors[vertexIndex + 2].a = alpha;
                newVertexColors[vertexIndex + 3].a = alpha;


                if (alpha == 0)
                {
                    startingCharacterRange += 1;

                    if (startingCharacterRange == characterCount)
                    {
                        // Update mesh vertex data one last time.
                        tmpTextObject.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                        yield return new WaitForSeconds(1.0f);

                        // Reset the text object back to original state.
                        tmpTextObject.ForceMeshUpdate();

                        yield return new WaitForSeconds(1.0f);

                        // Reset our counters.
                        currentCharacter = 0;
                        startingCharacterRange = 0;
                        //isRangeMax = true; // Would end the coroutine.
                    }
                }
            }

            // Upload the changed vertex colors to the Mesh.
            tmpTextObject.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            if (currentCharacter + 1 < characterCount) currentCharacter += 1;

            yield return new WaitForSeconds(0.25f - fadeSpeed * 0.01f);
        }

    }

    IEnumerator ShowNewText()
    {

        TMP_TextInfo textInfo = tmpTextObject.textInfo;
        int currentCharacter = 0;

        Color32[] newVertexColors;
        tmpTextObject.color = startCol;

        // Get the index of the material used by the current character.
        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

        // Get the vertex colors of the mesh used by this text element (character or sprite).
        newVertexColors = textInfo.meshInfo[materialIndex].colors32;

        // Get the index of the first vertex used by this text element.
        int vertexIndex = textInfo.characterInfo[0].vertexIndex;

        newVertexColors[vertexIndex + 0] = endCol;
        newVertexColors[vertexIndex + 1] = endCol;
        newVertexColors[vertexIndex + 2] = endCol;
        newVertexColors[vertexIndex + 3] = endCol;
        tmpTextObject.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);


        while (currentCharacter < textInfo.characterCount)
        {


            materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;

            newVertexColors = textInfo.meshInfo[materialIndex].colors32;
            vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;

            // Only change the vertex color if the text element is visible.
            if (textInfo.characterInfo[currentCharacter].isVisible)
            {
                newVertexColors[vertexIndex + 0] = endCol;
                newVertexColors[vertexIndex + 1] = endCol;
                newVertexColors[vertexIndex + 2] = endCol;
                newVertexColors[vertexIndex + 3] = endCol;

                // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
                tmpTextObject.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                // This last process could be done to only update the vertex data that has changed as opposed to all of the vertex data but it would require extra steps and knowing what type of renderer is used.
                // These extra steps would be a performance optimization but it is unlikely that such optimization will be necessary.
            }

            currentCharacter = (currentCharacter + 1) % textInfo.characterCount;

            yield return null;
        }
    }
}
