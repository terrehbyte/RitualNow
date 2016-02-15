using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// sourced:  http://forum.unity3d.com/threads/getcomponents-possible-to-use-with-c-interfaces.60596/

public static class InterfaceExtensions
{
    public static T GetInterface<T>(this GameObject inObj) where T : class
    {
        return inObj.GetComponents<Component>().OfType<T>().FirstOrDefault();
    }

    public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class
    {
        return inObj.GetComponents<Component>().OfType<T>();
    }
}