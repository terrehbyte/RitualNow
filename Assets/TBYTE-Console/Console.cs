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
            // blank? send it back
            if (string.IsNullOrEmpty(command))
                return "";

            command.Trim();

            // split into command and args
            string[] input = command.Split(' ');
            string[] args = command.Substring(input[0].Length).Trim().Split(' ');

            if (string.IsNullOrEmpty(input[0]))
                return "";

            // echo command back to console
            consoleHistory += "\n>" + command;

            // HACK: why does this work
            // if I modify consoleHistory while its += is being evaluated, it gets written back
            string result = ProcessCommand(input[0], args.Length, args);
            if (!string.IsNullOrEmpty(result))
            {
                consoleHistory += "\n" + result;
            }

            return consoleHistory;
        }
        public static string ClearHistory()
        {
            consoleHistory = string.Empty;
            return string.Empty;
        }
    }
}