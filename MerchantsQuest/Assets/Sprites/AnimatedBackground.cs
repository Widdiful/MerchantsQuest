using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    public RectTransform rect;
    public Animator anim;
    private float timer;

    private void Awake() {
        anim.SetInteger("anim", Random.Range(0, 4));
    }

    void Update()
    {
        float newY = Mathf.Sin(timer) + 1.5f;
        float newX = Mathf.Sin(timer / 2) + 1.5f;
        timer += Time.deltaTime;
        rect.localScale = new Vector3(newX, newY, 1);
        rect.Rotate(new Vector3(0, 0, Time.deltaTime * 10));
    }
}
