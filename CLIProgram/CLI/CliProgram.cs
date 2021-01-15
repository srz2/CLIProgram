using System;
using System.Collections.Generic;
using System.Linq;
using CLIProgram.CLI.Support;

namespace CLIProgram.CLI
{
    /// <summary>
    /// The status of the <see cref="CliProgram.startupCheck"/> method
    /// </summary>
    public enum StartupCode
    {
        /// <summary>
        /// Unknow startup status
        /// </summary>
        Unknown,
        /// <summary>
        /// Success. Startup parameters were met
        /// </summary>
        Success,
        /// <summary>
        /// Incorrect number of arguments are given
        /// </summary>
        InvalidArgumentCount,
        /// <summary>
        /// <para>Requesting version via the version tag</para>
        /// <para>See <see cref="CliProgram.checkForVersionFlag(string)"/></para>
        /// </summary>
        RequestingVersion,
        /// <summary>
        /// <para>Requesting help via a help tag</para>
        /// <para>See <see cref="CliProgram.checkForHelpFlag(string)"/></para>
        /// </summary>
        RequestingHelp,
        /// <summary>
        /// General startup check failed
        /// </summary>
        StartupCheckFailed,
        /// <summary>
        /// Custom error made outside of the library
        /// </summary>
        CustomError,
    }
    /// <summary>
    /// The result of the startup
    /// </summary>
    public class StartupResult
    {
        /// <summary>
        /// The code for how the program started
        /// </summary>
        public StartupCode code;
        /// <summary>
        /// Optional message
        /// </summary>
        public string message;

        /// <summary>
        /// Creates a new <see cref="StartupResult"/>
        /// </summary>
        /// <param name="code">The code for the status of the startup</param>
        /// <param name="msg">The optional message of the startup</param>
        /// <returns></returns>
        public static StartupResult createStartupResult(StartupCode code, string msg)
        {
            StartupResult result = new StartupResult();

            result.code = code;
            result.message = msg;

            return result;
        }
    }

    /// <summary>
    /// A class which must be interherited to signify a CLI program
    /// </summary>
    public abstract class CliProgram : CLIProgram.CLI.Base.CliProgramBase
    {
        /// <summary>
        /// Settings for the CLI application
        /// </summary>
        public CliProgramSettings Settings;
        /// <summary>
        /// General usage for the CLI application
        /// </summary>
        protected CliProgramUsage Usage;
        /// <summary>
        /// Error handling for the CLI application
        /// </summary>
        protected CliProgramError Error;

        #region "Getters and Setters"
        /// <summary>
        /// The CLI Program class has been initialized
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Get the list of strings which make up the help prompt
        /// </summary>
        public List<string> HelpDoc
        {
            get
            {
                return new List<string>(Usage.ToArray());
            }
        }
        #endregion

        /// <summary>
        /// Constructor for the CLIProgram Helper
        /// </summary>
        /// <param name="args">The program's given argument list</param>
        /// <param name="minArgs">The minimum allowed arguments</param>
        protected CliProgram(string[] args, int minArgs) : base()
        {
            init(args, minArgs);
        }

        private void init(string[] args, int n_MinArgs)
        {
            Settings = new CliProgramSettings(args, n_MinArgs);
            Usage = new CliProgramUsage();
            Error = new CliProgramError();

            setParents();
            setDefaultSettings();

            Initialized = true;
        }

        private void setDefaultSettings()
        {
            Settings.QUIT_WITH_ERROR_MESSAGE = true;
        }

        private void setParents()
        {
            Settings.SetParent(this);
            Usage.SetParent(this);
            Error.SetParent(this);
        }

#if DEBUG
        /// <summary>
        /// TESTING ONLY - Public method to test the return value of the startup check
        /// </summary>
        /// <returns></returns>
        public StartupResult test_startupCheck()
        {
            return startupCheck();
        }
#endif

        private bool checkForVersionFlag(string val)
        {
            string[] a_RecongizedVersionFlags = {
                "-v",
                "--v",
                "-version",
                "--version"
            };

            string tmp = val.ToLower();
            if (a_RecongizedVersionFlags.Contains(tmp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkForHelpFlag(string val)
        {
            string[] a_RecongizedHelpFlags = {
                "-h",
                "--h",
                "-help",
                "--help",
                "-?",
                "--?",
                "/?",
            };

            string tmp = val.ToLower();
            if (a_RecongizedHelpFlags.Contains(tmp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <para>Overridable statup check</para>
        /// <para>This method evaluates CLI arguments and any other information on startup (via override)</para>
        /// <para></para>
        /// <para>Note: Supermethod should be called</para>
        /// </summary>
        /// <returns>The result of the startup</returns>
        protected virtual StartupResult startupCheck()
        {
            int n_MinArgs = Settings.MinArgs;
            List<string> lst_ProgramArgs = new List<string>(Settings.ProgramArgs);

            StartupResult result = StartupResult.createStartupResult(StartupCode.Success, "Default Startup Check Passed");

            // Exit if nulled arguments
            if (lst_ProgramArgs == null)
            {
                string message = "Argument array is nulled";
                result = StartupResult.createStartupResult(StartupCode.StartupCheckFailed, message);
                return result;
            }

            // Check for version/help flags
            foreach (string arg in lst_ProgramArgs)
            {
                // Check for version flag
                if (checkForVersionFlag(arg))
                {
                    result = StartupResult.createStartupResult(StartupCode.RequestingVersion, "Program version was requested");
                    break;
                }

                // Check for help flag
                if (checkForHelpFlag(arg))
                {
                    result = StartupResult.createStartupResult  (StartupCode.RequestingHelp, "Program help was requested");
                    break;
                }
            }

            // Make sure we have arguments and they are within our min aruments
            if (result.code == StartupCode.Success && lst_ProgramArgs.Count < n_MinArgs)
            {
                string message = $"Invalid argument count, expecting {n_MinArgs} but received {lst_ProgramArgs.Count}";
                result = StartupResult.createStartupResult(StartupCode.InvalidArgumentCount, message);
                return result;
            }

            return result;
        }

        /// <summary>
        /// <para>Overrideable start method</para>
        /// <para></para>
        /// <para>NOTE: Supermethod should be called</para>
        /// </summary>
        /// <returns></returns>
        public virtual void Start()
        {
            StartupResult result = startupCheck();
            switch (result.code)
            {
                case StartupCode.RequestingVersion:
                    Console.WriteLine(Settings.getVersionInfo()["app"]);
                    this.Error.quit(1);
                    break;
                case StartupCode.RequestingHelp:
                    Usage.showHelp();
                    this.Error.quit(2);
                    break;
                case StartupCode.InvalidArgumentCount:
                    this.Error.quit(3, $"Expecting {Settings.MinArgs} arguments but received {Settings.ProgramArgs.Count}");
                    break;
                case StartupCode.Success:
                    // Do nothing
                    break;
                default:
                    Console.WriteLine("Unexpected startup code: " + result.code);
                    break;
            }
        }

        /// <summary>
        /// <para>Overrideable Stop method which will terminate the application successfully</para>
        /// </summary>
        public virtual void Stop()
        {
            Error.quit(0);
        }
    }
}
