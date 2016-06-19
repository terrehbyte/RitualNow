using UnityEngine;
using System.Collections;

namespace RitualWarehouse
{
    [System.Serializable]
    public struct Item
    {
        // TODO: tag is a bad name for this. should be enumerated in an enum anyway
        public string tag;
        public Sprite image;

        public bool Equals(Item otherItem)
        {
            return tag == otherItem.tag && image == otherItem.image;
        }
    }


    [System.Serializable]
    [CreateAssetMenu]
    public class ItemDatabase : ScriptableObject
    {
        public Item[] data;
    }
}