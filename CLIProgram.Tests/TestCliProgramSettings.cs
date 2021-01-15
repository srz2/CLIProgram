using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLIProgram.CLI.Support;

namespace CLIProgram.Tests
{
    [TestClass]
    public class TestCliProgramSettings
    {
        [TestMethod]
        public void TestInititalization1()
        {
            CliProgramSettings settings = new CliProgramSettings(null, 0);
            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.ProgramArgs);
            Assert.AreEqual(0, settings.MinArgs);
        }

        public void TestInitialization2()
        {
            CliProgramSettings settings = new CliProgramSettings(new string[] { }, -1);
            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.ProgramArgs);
            Assert.AreEqual(0, settings.MinArgs);
            Assert.AreEqual(0, settings.ProgramArgs.Count);
        }

        public void TestInitialization3()
        {
            const int EXPECTED_MIN_ARGS = 5;
            const int EXPECTED_NUM_ARGS = 3;
            string[] test_Args = {
                "Arg1",
                "Arg2",
                "Arg3"
            };

            CliProgramSettings settings = new CliProgramSettings(test_Args, EXPECTED_MIN_ARGS);
            Assert.IsNotNull(settings);
            Assert.IsNotNull(settings.ProgramArgs);
            Assert.AreEqual(EXPECTED_MIN_ARGS, settings.MinArgs);
            Assert.AreEqual(EXPECTED_NUM_ARGS, settings.ProgramArgs.Count);
        }

        public void TestInitializationDefaultSettingValues()
        {
            const bool DEFAULT_SETTING_DEBUG = false;
            const bool DEFAULT_SETTING_QUIT_WITH_ERROR_MESSAGE = true;
            const bool DEFAULT_SETTING_PAUSE_ON_QUIT = false;

            CliProgramSettings settings = new CliProgramSettings(null, 0);
            Assert.AreEqual(DEFAULT_SETTING_DEBUG, settings.DEBUG);
            Assert.AreEqual(DEFAULT_SETTING_QUIT_WITH_ERROR_MESSAGE, settings.QUIT_WITH_ERROR_MESSAGE);
            Assert.AreEqual(DEFAULT_SETTING_PAUSE_ON_QUIT, settings.PAUSE_ON_QUIT);
        }

        public void TestInitializationChangingSettingValues()
        {
            const bool DEFAULT_SETTING_DEBUG = true;
            const bool DEFAULT_SETTING_QUIT_WITH_ERROR_MESSAGE = false;
            const bool DEFAULT_SETTING_PAUSE_ON_QUIT = true;

            CliProgramSettings settings = new CliProgramSettings(null, 0);

            // Change default setting
            settings.DEBUG = !settings.DEBUG;
            settings.QUIT_WITH_ERROR_MESSAGE = !settings.QUIT_WITH_ERROR_MESSAGE;
            settings.PAUSE_ON_QUIT = !settings.PAUSE_ON_QUIT;

            Assert.AreEqual(DEFAULT_SETTING_DEBUG, settings.DEBUG);
            Assert.AreEqual(DEFAULT_SETTING_QUIT_WITH_ERROR_MESSAGE, settings.QUIT_WITH_ERROR_MESSAGE);
            Assert.AreEqual(DEFAULT_SETTING_PAUSE_ON_QUIT, settings.PAUSE_ON_QUIT);
        }

        public string getVersionOfTestSuite()
        {
            string version = System.Reflection.Assembly.GetCallingAssembly().GetName().Version.ToString();
            return version;
        }

        [TestMethod]
        public void TestVersionInformation()
        {
            CliProgramSettings settings = new CliProgramSettings(null, 0);
            Assert.AreEqual(getVersionOfTestSuite(), settings.getVersionInfo()["app"]);
        }
    }
}
