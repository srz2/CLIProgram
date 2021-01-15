using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CLIProgram.CLI.Support;

namespace CLIProgram.Tests
{
    [TestClass]
    public class TestCliProgramError
    {
        const int EXPECTED_DEFAULT_ERROR_CODES = 5;

        [TestMethod]
        public void TestInititalization()
        {
            CliProgramError error = new CliProgramError();
            Assert.IsNotNull(error);

            // Get the error codes
            Dictionary<int, string> errorCodes = error.ErrorCodes;
            Assert.IsNotNull(errorCodes);

            // Expected number of default codes
            Assert.AreEqual(EXPECTED_DEFAULT_ERROR_CODES, errorCodes.Count);

            // Error codes are in sequence
            List<int> keys = errorCodes.Keys.ToList<int>();
            for (int c = 0; c < EXPECTED_DEFAULT_ERROR_CODES; c++)
            {
                Assert.AreEqual(c, keys[c]);
            }
        }

        [TestMethod]
        public void TestAddingErrorCodes()
        {
            const int EXPECTED_ERROR_CODES = EXPECTED_DEFAULT_ERROR_CODES + 3;

            CliProgramError error = new CliProgramError();

            // Add custom error codes
            int customError1 = error.addErrorCode("CustomError1");
            int customError2 = error.addErrorCode("CustomError2");
            int customError3 = error.addErrorCode("CustomError3");

            // Get error codes
            Dictionary<int, string> errorCodes = error.ErrorCodes;

            // Check codes are there
            Assert.AreEqual(EXPECTED_ERROR_CODES, errorCodes.Count);
            Assert.AreEqual(errorCodes[customError1], "CustomError1");
            Assert.AreEqual(errorCodes[customError2], "CustomError2");
            Assert.AreEqual(errorCodes[customError3], "CustomError3");
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestAddingErrorCodes_ClearedCode()
        {
            CliProgramError error = new CliProgramError();

            // Add custom error codes
            int customError1 = error.addErrorCode("CustomError1");
            int customError2 = error.addErrorCode("CustomError2");
            int customError3 = error.addErrorCode("CustomError3");

            // Remove one of the custom errors
            error.clearSpecificErrorCode(customError2);

            Dictionary<int, string> errorCodes = error.ErrorCodes;
            string value = errorCodes[customError2];
        }

        [TestMethod]
        public void TestAddingErrorCodes_ClearedAllCodes()
        {
            CliProgramError error = new CliProgramError();

            // Add custom error codes
            int customError1 = error.addErrorCode("CustomError1");
            int customError2 = error.addErrorCode("CustomError2");
            int customError3 = error.addErrorCode("CustomError3");

            error.clearAllErrorCodes();

            Dictionary<int, string> errorCodes = error.ErrorCodes;
            Assert.AreEqual(EXPECTED_DEFAULT_ERROR_CODES, errorCodes.Count);
        }
    }
}
