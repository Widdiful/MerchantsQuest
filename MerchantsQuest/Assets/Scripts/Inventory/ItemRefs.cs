using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ItemRef", menuName = "Item Ref", order = 51)]
public class ItemRefs : ScriptableObject
{
    public Sprite[] sprites;
    public TextAsset fakeDescriptions;
    public string[] descriptions;
}
