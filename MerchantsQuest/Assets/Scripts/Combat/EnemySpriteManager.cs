using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpriteManager : MonoBehaviour
{
    public List<Image> sprites = new List<Image>();
    public List<Animator> attackAnims = new List<Animator>();
    public RuntimeAnimatorController meleeAnim, magicAnim;

    public void SetSprites(List<EnemyStats> list) {
        for (int i = 0; i < sprites.Count; i++) {
            if (i < list.Count) {
                sprites[i].gameObject.SetActive(true);
                sprites[i].enabled = true;
                sprites[i].sprite = list[i].sprite;
                sprites[i].rectTransform.sizeDelta = new Vector2(list[i].spriteSize.x, list[i].spriteSize.y);
            }
            else {
                sprites[i].gameObject.SetActive(false);
            }

            attackAnims[i].runtimeAnimatorController = null;
        }
    }

    public void UpdateSprites(List<EnemyStats> list) {
        for (int i = 0; i < sprites.Count; i++) {
            if (sprites[i].gameObject.activeInHierarchy) {
                if (i < list.Count && !list[i].isDead) {
                    sprites[i].enabled = true;
                    sprites[i].sprite = list[i].sprite;
                    sprites[i].rectTransform.sizeDelta = new Vector2(list[i].spriteSize.x, list[i].spriteSize.y);
                }
                else {
                    sprites[i].enabled = false;
                }
            }
        }
    }

    public void PlayMeleeEffect(int id) {
        attackAnims[id].runtimeAnimatorController = null;
        attackAnims[id].runtimeAnimatorController = meleeAnim;
        attackAnims[id].enabled = false;
        attackAnims[id].enabled = true;
    }

    public void PlayMagicEffect(int id) {
        attackAnims[id].runtimeAnimatorController = null;
        attackAnims[id].runtimeAnimatorController = magicAnim;
        attackAnims[id].enabled = false;
        attackAnims[id].enabled = true;
    }
}
