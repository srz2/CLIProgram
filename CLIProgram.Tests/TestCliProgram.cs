using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLIProgram.CLI;

namespace CLIProgram.Tests
{
    [TestClass]
    public class TestCliProgram
    {
        // This is required because the CLIProgram class must be inheriteted
        private class CLITestProgram : CliProgram
        {
            public CLITestProgram(string[] args) : base(args, 0)
            {

            }
        }

        [TestMethod]
        public void TestInitialization()
        {
            string[] emptyArgs = { };
            CliProgram obj = new CLITestProgram(emptyArgs);
            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(CliProgram), "Is not CliProgram Class");
            Assert.IsInstanceOfType(obj, typeof(CLI.Base.CliProgramBase), "Is not interited from CliProgramBase");
        }

#if DEBUG
        #region "TestVersionFlags"
        [TestMethod]
        public void TestVersionFlags_1()
        {
            string[] vArgs = { "-v" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingVersion, result.code);
        }

        [TestMethod]
        public void TestVersionFlags_2()
        {
            string[] vArgs = { "--v" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingVersion, result.code);
        }

        [TestMethod]
        public void TestVersionFlags_3()
        {
            string[] vArgs = { "-version" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingVersion, result.code);
        }

        [TestMethod]
        public void TestVersionFlags_4()
        {
            string[] vArgs = { "--version" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingVersion, result.code);
        }

        [TestMethod]
        public void TestVersionFlags_Fail()
        {
            string[] vArgs = { "ver" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreNotEqual(StartupCode.RequestingVersion, result.code);
        }
        #endregion
        #region "TestingHelpFlags"
        [TestMethod]
        public void TestHelpFlags_1()
        {
            string[] vArgs = { "-h" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingHelp, result.code);
        }

        [TestMethod]
        public void TestHelpFlags_2()
        {
            string[] vArgs = { "--h" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingHelp, result.code);
        }

        [TestMethod]
        public void TestHelpFlags_3()
        {
            string[] vArgs = { "-help" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingHelp, result.code);
        }

        [TestMethod]
        public void TestHelpFlags_4()
        {
            string[] vArgs = { "--help" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingHelp, result.code);
        }

        [TestMethod]
        public void TestHelpFlags_5()
        {
            string[] vArgs = { "-?" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingHelp, result.code);
        }

        [TestMethod]
        public void TestHelpFlags_6()
        {
            string[] vArgs = { "--?" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingHelp, result.code);
        }

        [TestMethod]
        public void TestHelpFlags_7()
        {
            string[] vArgs = { "/?" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreEqual(StartupCode.RequestingHelp, result.code);
        }

        [TestMethod]
        public void TestHelpFlags_8_Fail()
        {
            string[] vArgs = { "helpme" };
            CliProgram obj = new CLITestProgram(vArgs);
            StartupResult result = obj.test_startupCheck();
            Assert.AreNotEqual(StartupCode.RequestingHelp, result.code);
        }
        #endregion
#endif
    }
}
