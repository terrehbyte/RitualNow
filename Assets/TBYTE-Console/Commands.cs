using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;



namespace TBYTEConsole
{
#if UNITY_EDITOR
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class InitializeMethodOnStartup : UnityEditor.InitializeOnLoadMethodAttribute
    {
        public InitializeMethodOnStartup() : base()
        {

        }
    }
#else
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class InitializeMethodOnStartup : UnityEngine.RuntimeInitializeOnLoadMethodAttribute
    {
        public InitializeMethodOnStartup() : base()
        {

        }
    }
#endif


    public static class StockCommandDefinition
    {
        // hack: this is a terrible method

        [InitializeMethodOnStartup]
        public static void Register()
        {
            CCommand cmd = new CCommand("clear", clearCmd);
            Debug.Log("Stock commands added.");
        }

        private static string clearCmd(int argc, string[] argv)
        {
            Console.ClearHistory();
            return string.Empty;
        }
    }
}
