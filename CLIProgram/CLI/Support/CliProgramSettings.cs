using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using CLIProgram.CLI.Base;

namespace CLIProgram.CLI.Support
{
    /// <summary>
    /// A class to represent the settings of the application
    /// </summary>
    public class CliProgramSettings : CliProgramBase
    {
        private int n_mMinArgs = -1;
        private List<string> lst_mProgramArgs;

        /// <summary>
        /// <para>Use debug mode</para>
        /// <para>Default: FALSE</para>
        /// </summary>
        public bool DEBUG;
        /// <summary>
        /// <para>If message exists with error code, display it</para>
        /// <para>Default: TRUE</para>
        /// </summary>
        public bool QUIT_WITH_ERROR_MESSAGE;
        /// <summary>
        /// <para>Use Console.ReadKey() on quitting the application on the Quit() method in order to pause the quitting process</para>
        /// <para>Default: FALSE</para>
        /// </summary>
        public bool PAUSE_ON_QUIT;

        /// <summary>
        /// The minimum number of arguments expected
        /// </summary>
        public int MinArgs
        {
            get
            {
                int val = n_mMinArgs;
                return val;
            }
        }

        /// <summary>
        /// The arguments given at program start
        /// </summary>
        public List<string> ProgramArgs
        {
            get
            {
                List<string> args = new List<string>(lst_mProgramArgs);
                return args;
            }
        }

        /// <summary>
        /// The constructor for creating the settings for the application
        /// </summary>
        /// <param name="args"></param>
        /// <param name="n_MinArgs"></param>
        public CliProgramSettings(string[] args, int n_MinArgs)
        {
            // Only Accept min args 0 or more
            n_mMinArgs = n_MinArgs > 0 ? n_MinArgs : 0;

            lst_mProgramArgs = new List<string>();
            if (args != null)
            {
                lst_mProgramArgs.AddRange(args);
            }
        }

        /// <summary>
        /// Get the name of the application
        /// </summary>
        /// <returns></returns>
        public string getProgramName()
        {
            string name = Assembly.GetCallingAssembly().GetName().Name;
            return name;
        }

        /// <summary>
        /// Get the version of the application
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> getVersionInfo()
        {
            Dictionary<string, string> dic_Version = new Dictionary<string, string>();
            dic_Version.Add("app", Assembly.GetEntryAssembly().GetName().Version.ToString());
            dic_Version.Add("lib", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            return dic_Version;
        }
    }
}
