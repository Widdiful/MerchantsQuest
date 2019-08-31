using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalStats : MonoBehaviour
{
    public TMP_Text highestLevel;
    public TMP_Text countText;
    public TMP_Text goldSpent;

    public ShowText showText;

    private void Awake()
    {
        countText.text = (GameManager.instance.dungeon.highestFloorAchieved + 1).ToString();
        StartCoroutine(Showing(highestLevel, 2.0f, 40.0f));
        StartCoroutine(Showing(countText, 2.0f, 40.0f));
        StartCoroutine(Showing(goldSpent, 2.0f, 40.0f));
        showText.Show();
    }

    IEnumerator Showing(TMP_Text tmpTextObject, float textSpeed, float fadeSpeed)
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
}
