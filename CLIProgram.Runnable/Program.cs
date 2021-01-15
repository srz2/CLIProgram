using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLIProgram.CLI;

namespace CLIProgram.Runnable
{
    class MyProgram : CliProgram
    {
        public MyProgram(string[] args) : base(args, 2)
        {

        }

        /// <summary>
        /// This is where to fire off the main cli application logic
        /// </summary>
        public override void Start()
        {
            base.Start();

            // *********************
            // Put your program here
            // **********************
        }

        public override void Stop()
        {
            base.Stop();

            // **********************
            // Clean up here
            // **********************
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // This is all that is needed here in the main() function
            MyProgram program = new MyProgram(args);
            program.Start();
            program.Stop();
        }
    }
}
