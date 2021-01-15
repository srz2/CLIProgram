using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLIProgram.CLI.Base
{
    /// <summary>
    /// Base class for an CLI Program
    /// </summary>
    public class CliProgramBase
    {
        /// <summary>
        /// The root reference
        /// </summary>
        protected CliProgram ip_mRoot;

        /// <summary>
        /// Set the root reference
        /// </summary>
        /// <param name="obj">Should be reference for the root cli program</param>
        public void SetParent(CliProgram obj)
        {
            ip_mRoot = obj;
        }
    }
}
