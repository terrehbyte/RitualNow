using UnityEngine;
using System.Text;

namespace TBYTEConsole
{
    public static class StockCommandDefinition
    {
        // TODO: Find a better way to handle this!

        [InitializeMethodOnStartup]
        public static void Register()
        {
            CCommand cmd = new CCommand("clear", clearCmd);
            CCommand echo = new CCommand("echo", echoCmd);
        }

        private static string clearCmd(int argc, string[] argv)
        {
            Console.ClearHistory();
            return string.Empty;
        }

        private static string echoCmd(int argc, string[] argv)
        {
            StringBuilder bldr = new StringBuilder();

            foreach(var arg in argv)
            {
                bldr.Append(arg);
                bldr.Append(' ');
            }

            return bldr.ToString();
        }

        private static string condumpCmd(int argc, string[] argv)
        {
            //Application.persistentDataPath;
            return string.Empty;
        }
    }
}
