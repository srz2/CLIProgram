using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLIProgram.CLI.Support;

namespace CLIProgram.Tests
{
    [TestClass]
    public class TestCliProgramUsage
    {
        [TestMethod]
        public void TestInititalization()
        {
            CliProgramUsage usage = new CliProgramUsage();
            Assert.IsNotNull(usage);
        }

        public void AssertForEmpty(CliProgramUsage usage)
        {
            string[] a_Help = usage.ToArray();
            Assert.IsNotNull(a_Help);

            string sz_Help = usage.ToString();
            Assert.IsNotNull(sz_Help);
            Assert.IsTrue(string.IsNullOrEmpty(sz_Help));
        }

        public void AssertForFilled(CliProgramUsage usage, int size)
        {
            string[] a_Help = usage.ToArray();
            Assert.IsNotNull(a_Help);
            Assert.IsTrue(a_Help.Length == 3, "The number of lines is different than expected");

            string sz_Help = usage.ToString();
            Assert.IsNotNull(sz_Help);
            Assert.IsFalse(string.IsNullOrEmpty(sz_Help));
        }

        [TestMethod]
        public void TestGettingHelpDocument_Empty()
        {
            CliProgramUsage usage = new CliProgramUsage();

            AssertForEmpty(usage);
        }

        [TestMethod]
        public void TestGettingHelpDocument_Filled()
        {
            const string LINE1 = "This is my first line";
            const string LINE2 = "This is my second line";
            const string LINE3 = "This is my third line";

            CliProgramUsage usage = new CliProgramUsage();
            usage.addToHelp(LINE1);
            usage.addToHelp(LINE2);
            usage.addToHelp(LINE3);

            AssertForFilled(usage, 3);

            string[] a_Help = usage.ToArray();
            Assert.AreEqual(LINE1, a_Help[0]);
            Assert.AreEqual(LINE2, a_Help[1]);
            Assert.AreEqual(LINE3, a_Help[2]);
        }

        [TestMethod]
        public void TestGettingHelpDocument_Cleared()
        {
            const string LINE1 = "This is my first line";
            const string LINE2 = "This is my second line";
            const string LINE3 = "This is my third line";

            CliProgramUsage usage = new CliProgramUsage();
            usage.addToHelp(LINE1);
            usage.addToHelp(LINE2);
            usage.addToHelp(LINE3);

            // Clear Help
            usage.clearHelp();

            AssertForEmpty(usage);
        }

        [TestMethod]
        public void TestMultiLineAddSingleCall()
        {
            int expectedLines = 3;
            string CONTENT_3_LINES = "";
            CONTENT_3_LINES += "This is my first line\n";
            CONTENT_3_LINES += "This is my second line\n";
            CONTENT_3_LINES += "This is my last line";

            CliProgramUsage usage = new CliProgramUsage();
            usage.addToHelp(CONTENT_3_LINES);

            string[] helpDoc = usage.ToArray();
            Assert.AreEqual(expectedLines, helpDoc.Length);
        }
    }
}
