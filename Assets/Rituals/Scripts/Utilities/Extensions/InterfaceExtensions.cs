using UnityEngine;
using System.Linq;
using System.Collections.Generic;

// sourced:  http://forum.unity3d.com/threads/getcomponents-possible-to-use-with-c-interfaces.60596/

namespace RitualWarehouse.Extensions
{
    public static class InterfaceExtensions
    {
        // TODO: potentially obsolete now
        public static T GetInterface<T>(this GameObject inObj) where T : class
        {
            return inObj.GetComponents<Component>().OfType<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class
        {
            return inObj.GetComponents<Component>().OfType<T>();
        }
    }
}