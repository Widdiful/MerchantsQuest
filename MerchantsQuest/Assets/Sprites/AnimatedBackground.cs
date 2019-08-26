using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    public RectTransform rect;
    private float timer;

    void Update()
    {
        float newY = Mathf.Sin(timer) + 1.5f;
        timer += Time.deltaTime;
        rect.localScale = new Vector3(1, newY, 1);
        rect.Rotate(new Vector3(0, 0, Time.deltaTime * 10));
    }
}
