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

    public static class Console
    {
        private static List<CCommand> commands = new List<CCommand>();
        private static string consoleHistory;   // HACK: not sure who should have this

        public static void Register(CCommand newCommand)
        {
            commands.Add(newCommand);
        }

        private static string ProcessCommand(string command, int argc, string[] argv)
        {
            foreach(var cmd in CCommand.commands)
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

            // HACK: Now there's always a space at the beginning of the console...
            consoleHistory += "\n" + ProcessCommand(input[0], args.Length, args);

            return consoleHistory;
        }
        public static void ClearHistory()
        {
            consoleHistory = string.Empty;
        }
    }
}