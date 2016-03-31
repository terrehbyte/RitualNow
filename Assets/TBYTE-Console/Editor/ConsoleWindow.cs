using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TBYTEConsole
{
    public class ConsoleWindow : EditorWindow
    {
        string consoleDisplay;
        Vector2 consoleScrollPos;

        string userEntry;

        [MenuItem("TBYTEConsole/Show Window")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ConsoleWindow));
        }

        void OnEnable()
        {
            titleContent = new GUIContent("Console");
        }

        // IMGUI
        void OnGUI()
        {
            // Input
            Event e = Event.current;
            if (Event.current.type == EventType.KeyDown &&
                e.keyCode == KeyCode.Return &&
                userEntry != string.Empty)
            {
                consoleScrollPos = new Vector2(0, Mathf.Infinity);
                consoleDisplay = Console.ProcessConsoleInput(userEntry);
                userEntry = string.Empty;
                Repaint();
                EditorGUI.FocusTextInControl("TBYTEConsole.ConsoleWindow.userEntry");
            }

            // Display
            consoleScrollPos = EditorGUILayout.BeginScrollView(consoleScrollPos);

            // TODO: prevent people from editting the display, but retain highlight
            EditorGUILayout.TextArea(consoleDisplay, GUILayout.ExpandHeight(true));
            

            EditorGUILayout.EndScrollView();

            GUI.SetNextControlName("TBYTEConsole.ConsoleWindow.userEntry");
            userEntry = EditorGUILayout.TextField(userEntry);
        }
    }
}
