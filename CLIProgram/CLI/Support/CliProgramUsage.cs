﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLIProgram.CLI.Base;

namespace CLIProgram.CLI.Support
{
    /// <summary>
    /// A class for tracking app usage
    /// </summary>
    public class CliProgramUsage : CliProgramBase
    {
        private List<string> lst_mUsage;

        /// <summary>
        /// Constructor for application usage
        /// </summary>
        public CliProgramUsage()
        {
            lst_mUsage = new List<string>();
        }

        /// <summary>
        /// Get a string reprentation of the usage docs
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(Environment.NewLine, lst_mUsage);
        }

        /// <summary>
        /// Get an array representing the usage of an application
        /// </summary>
        /// <returns></returns>
        public string[] ToArray()
        {
            return lst_mUsage.ToArray();
        }

        /// <summary>
        /// Add a message to the help docs
        /// </summary>
        /// <param name="helpMessage"></param>
        public void addToHelp(string helpMessage)
        {
            if (string.IsNullOrEmpty(helpMessage))
            {
                return;
            }

            string[] messages = helpMessage.Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            lst_mUsage.AddRange(messages);
        }
        /// <summary>
        /// Clear the help docs
        /// </summary>
        public void clearHelp()
        {
            lst_mUsage.Clear();
        }
        /// <summary>
        /// Print the help in the console
        /// </summary>
        public void showHelp()
        {
            foreach (string s in lst_mUsage)
            {
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Get the list of strings which make up the help prompt
        /// </summary>
        public List<string> getHelp()
        {
            return new List<string>(this.ToArray());
        }
    }
}
