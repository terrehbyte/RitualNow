using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Item
{
    // TODO: tag is a bad name for this. should be enumerated in an enum anyway
    public string tag;
    public Sprite image;
}


[System.Serializable]
[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    public Item[] data;
}