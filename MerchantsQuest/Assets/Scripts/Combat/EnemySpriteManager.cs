using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpriteManager : MonoBehaviour
{
    public List<Image> sprites = new List<Image>();

    public void SetSprites(List<EnemyStats> list) {
        for (int i = 0; i < sprites.Count; i++) {
            if (i < list.Count) {
                sprites[i].gameObject.SetActive(true);
                sprites[i].sprite = list[i].sprite;
            }
            else {
                sprites[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateSprites(List<EnemyStats> list) {
        for (int i = 0; i < sprites.Count; i++) {
            if (sprites[i].gameObject.activeInHierarchy) {
                if (i < list.Count && !list[i].isDead) {
                    sprites[i].enabled = true;
                    sprites[i].sprite = list[i].sprite;
                }
                else {
                    sprites[i].enabled = false;
                }
            }
        }
    }
}
