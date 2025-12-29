using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void ItemIdEventHandler(int itemId);
    public static event ItemIdEventHandler OnItemIdInit;

    public static void TriggerItemIdInit(int itemId)
    {
        if (OnItemIdInit != null)
        {
            OnItemIdInit(itemId);
        }
    }
}
