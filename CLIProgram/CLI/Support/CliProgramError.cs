using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLIProgram.CLI.Base;

namespace CLIProgram.CLI.Support
{
    /// <summary>
    /// The default code values
    /// </summary>
    public struct DefaultCode
    {
        /// <summary>
        /// Default Code for success
        /// </summary>
        public int SUCCESS;
        /// <summary>
        /// Default code for a general error
        /// </summary>
        public int GENERAL;
        /// <summary>
        /// Default Code for version
        /// </summary>
        public int SHOWVERSION;
        /// <summary>
        /// Default Code for help
        /// </summary>
        public int SHOWHELP;
        /// <summary>
        /// Default code for invalid args
        /// </summary>
        public int INVALIDARGS;
    }

    /// <summary>
    /// A class representing the functionality of error handling
    /// </summary>
    public class CliProgramError : CliProgramBase
    {
        /// <summary>
        /// The default codes
        /// </summary>
        public DefaultCode DefaultCodes;

        private Dictionary<int, string> dic_mErrorCodes;
        private List<int> lst_mDefaultErrorCodes;

        /// <summary>
        /// The constructor for error handling class
        /// </summary>
        public CliProgramError()
        {
            dic_mErrorCodes = new Dictionary<int, string>();
            lst_mDefaultErrorCodes = new List<int>();

            addDefaultErrorCodes();
        }

        /// <summary>
        /// Get the list of error codes
        /// </summary>
        public Dictionary<int, string> ErrorCodes
        {
            get
            {
                Dictionary<int, string> temp = new Dictionary<int, string>(dic_mErrorCodes);
                return temp;
            }
        }

        private void addDefaultErrorCodes()
        {
            lst_mDefaultErrorCodes.Clear();

            // Add default codes
            DefaultCodes.SUCCESS = addErrorCode("Succeeded With No Errors", true);
            DefaultCodes.GENERAL = addErrorCode("General error occured", true);
            DefaultCodes.SHOWVERSION = addErrorCode("Exited to show version info", true);
            DefaultCodes.SHOWHELP = addErrorCode("Exited to show help info", true);
            DefaultCodes.INVALIDARGS = addErrorCode("Invalid argument count", true);
        }
        /// <summary>
        /// Add an error code
        /// </summary>
        /// <param name="message">The message for the error code</param>
        /// <returns></returns>
        public int addErrorCode(string message)
        {
            int code = dic_mErrorCodes.Count;

            if (dic_mErrorCodes.ContainsKey(code)) { throw new Exception($"Error code {code} already exists"); }
            if (string.IsNullOrEmpty(message)) { return -1; }

            dic_mErrorCodes.Add(code, message);
            return code;
        }
        private int addErrorCode(string message, bool isDefault)
        {
            int code = dic_mErrorCodes.Count;

            int n_NewCodeIndex = addErrorCode(message);
            if (n_NewCodeIndex >= 0 && isDefault)
            {
                lst_mDefaultErrorCodes.Add(code);
            }

            return n_NewCodeIndex;
        }
        /// <summary>
        /// Clear a specific error code
        /// </summary>
        /// <param name="code">Clear an error code</param>
        public void clearSpecificErrorCode(int code)
        {
            // Dont remove default error codes
            if (lst_mDefaultErrorCodes.Contains(code))
            {
                return;
            }

            if (dic_mErrorCodes.ContainsKey(code))
            {
                dic_mErrorCodes.Remove(code);
            }
        }
        /// <summary>
        /// Clear all stored error codes except for the default codes
        /// </summary>
        public void clearAllErrorCodes()
        {
            dic_mErrorCodes.Clear();
            addDefaultErrorCodes();
        }
        /// <summary>
        /// Quit with an error code and optional message
        /// </summary>
        /// <param name="code">The error code to exit with</param>
        /// <param name="msg">The message to display in addition to the error code/message</param>
        public void quit(int code, string msg = "")
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Console.WriteLine(msg);
            }

            if (dic_mErrorCodes.ContainsKey(code))
            {
                if (ip_mRoot.Settings.DEBUG)
                {
                    Console.WriteLine($"Error Code ({code}): {dic_mErrorCodes[code]}");
                }
                else if(ip_mRoot.Settings.QUIT_WITH_ERROR_MESSAGE)
                {
                    Console.WriteLine(dic_mErrorCodes[code]);
                }
            }

            if (ip_mRoot.Settings.PAUSE_ON_QUIT)
            {
                Console.ReadKey();
            }
            Environment.Exit(code);
        }
    }
}
