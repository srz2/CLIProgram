using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLIProgram.CLI.Base;


namespace CLIProgram.Tests
{
    [TestClass]
    public class TestCliProgramBase
    {
        [TestMethod]
        public void TestInitialization()
        {
            CliProgramBase obj = new CliProgramBase();
            Assert.IsNotNull(obj);
        }
    }
}
