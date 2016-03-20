using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace TBYTEConsole
{
    public delegate string cmd(int argc, string[] argv);

    public struct CCommand
    {
        public readonly static List<CCommand> commands = new List<CCommand>();

        public readonly string cmd;
        public cmd exec;

        public CCommand(string command, cmd callback)
        {
            if (callback == null)
            {
                throw new NullReferenceException("Callback can not be null!");
            }

            cmd = command; 
            exec = callback;

            commands.Add(this);
        }

        public string Execute(int argc, string[] argv)
        {
            return exec(argc, argv);
        }
    }

    public static class StockConsoleCommands
    {
        static StockConsoleCommands()
        {
            new CCommand("clear", clearCmd);
        }

        private static CCommand clear = new CCommand("clear", clearCmd);
        private static string clearCmd(int argc, string[] argv)
        {
            Console.ClearHistory();
            return string.Empty;
        }
    }

    public static class Console
    {
        static Console()
        {
            Debug.Log("Yes");
        }

        private static List<CCommand> commands = new List<CCommand>();
        private static string consoleHistory;

        public static void Register(CCommand newCommand)
        {
            commands.Add(newCommand);
        }

        private static string ProcessCommand(string command, int argc, string[] argv)
        {
            foreach(var cmd in commands)
            {
                if (cmd.cmd == command)
                {
                    return cmd.exec(argc, argv);
                }
            }

            return string.Format("{0} is not a valid command", command);
        }
        public static string ProcessConsoleInput(string command)
        {
            if (string.IsNullOrEmpty(command))
                return "";

            string[] input = command.Split(' ');
            string[] args = command.Substring(input[0].Length).Split(' ');
            // HACK: Can't think, too many annoying people in the background

            consoleHistory += ProcessCommand(input[0], args.Length, args) + "\n";

            return consoleHistory;
        }
        public static void ClearHistory()
        {
            consoleHistory = string.Empty;
        }
    }
}