using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TBYTEConsole
{
    public class ConsoleWindow : EditorWindow
    {
        string consoleDisplay;
        Vector2 consoleScrollPos;

        string userEntry;   // should this belong to the Console or ConsoleWindow?
                            // how do I process a command to clear the command window?

        [MenuItem("TBYTEConsole/Show Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ConsoleWindow));
        }

        void OnGUI()
        {
            // Input
            Event e = Event.current;
            if (Event.current.type == EventType.KeyDown &&
                e.keyCode == KeyCode.Return)
            {
                consoleScrollPos = new Vector2(0, Mathf.Infinity);
                consoleDisplay = Console.ProcessConsoleInput(userEntry);
                //EditorGUI.FocusTextInControl("TBYTEConsole.ConsoleWindow.userEntry");
            }

            // Display

            EditorGUILayout.LabelField("Console Window", EditorStyles.boldLabel);
            consoleScrollPos = EditorGUILayout.BeginScrollView(consoleScrollPos);

            // TODO: prevent people from editting the display, but retain highlight
            EditorGUILayout.TextArea(consoleDisplay, GUILayout.ExpandHeight(true));

            EditorGUILayout.EndScrollView();

            //GUI.SetNextControlName("TBYTEConsole.ConsoleWindow.userEntry");
            userEntry = EditorGUILayout.TextField(userEntry);   
        }
    }
}
