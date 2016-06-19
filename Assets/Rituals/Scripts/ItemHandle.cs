using UnityEngine;
using System.Collections;
using Zenject;

[System.Serializable]
public class ItemHandle
{
    public int ItemID;

    [Inject]
    private ItemDatabase ItemDB;

    public static implicit operator Item(ItemHandle handle)
    {
        return handle.ItemDB.data[handle.ItemID];
    }
}