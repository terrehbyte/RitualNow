using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Item
{
    public string tag;
    public Sprite image;
}


[System.Serializable]
[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    public Item[] data;
}