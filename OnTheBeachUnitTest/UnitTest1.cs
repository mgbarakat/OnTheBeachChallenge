using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnTheBeachChallenge;

namespace OnTheBeachChallengeUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test Empty Job list
        /// </summary>
        [TestMethod]
        public void TestEmptyJobList()
        {
            var testresult = new OrderesSequence("");
            Assert.AreEqual(expected: "", actual: testresult.JobsList, "Empty Orders Worked as expected");
        }

        /// <summary>
        /// Test Job list with only one job
        /// </summary>
        [TestMethod]
        public void TestOneJob()
        {
            var testresult = new OrderesSequence("a=>");
            Assert.AreEqual(expected: "a", actual: testresult.JobsList, "List with only one job Worked as expected");
        }

        /// <summary>
        /// Test Job list Without Dependencies
        /// </summary>
        [TestMethod]
        public void TestJoblistWithoutDependencies()
        {
            var testresult = new OrderesSequence("a=> \r\n b=> \r\n c=> \r\n");
            Assert.AreEqual(expected: "abc", actual: testresult.JobsList, "List with only one job Worked as expected");
        }

        /// <summary>
        /// Test Job list With Basic Dependencies
        /// </summary>
        [TestMethod]
        public void TestJoblistWithBasicDependencies()
        {
            var testresult = new OrderesSequence("a=> \r\n b=>c \r\n c=> \r\n");
            Assert.AreEqual(expected: "acb", actual: testresult.JobsList, "List with only one job Worked as expected");
        }

        /// <summary>
        /// Test Job list Complex Dependencies
        /// </summary>
        [TestMethod]
        public void TestJoblistComplexDependencies()
        {
            var testresult = new OrderesSequence("a=> \r\n b=>c \r\n c=>f \r\n d=>a \r\n e=>b \r\n f=> \r\n");
            Assert.AreEqual(expected: "afcbde", actual: testresult.JobsList, "List with only one job Worked as expected");
        }

        /// <summary>
        /// Test Job list Complex With Multi Dependency
        /// </summary>
        [TestMethod]
        public void TestJoblistComplexWithMultiDependency()
        {
            var testresult = new OrderesSequence("a=> \r\n b=>c \r\n c=>a \r\n d=>a \r\n e=>b \r\n f=>c \r\n");
            Assert.AreEqual(expected: "acbdef", actual: testresult.JobsList, "List with only one job Worked as expected");
        }

        /// <summary>
        /// Test Job list With Self Reference
        /// should fail with exception type [SelfReferencingException]
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SelfReferencingException), "Self Referencing is not allowed.")]
        public void TestJoblistWithSelfRefrence()
        {
            var testresult = new OrderesSequence("a=> \r\n b=> \r\n c=>c \r\n");
        }

        /// <summary>
        /// Test Job list With Circular dependency
        /// should fail with exception type [SelfReferencingException]
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularDependencyException), "Circular Dependency is not allowed.")]
        public void TestJoblistWithCircularDependency()
        {


            var testresult = new OrderesSequence("a=> \r\n b=>c \r\n c=>f \r\n d=>a \r\n e=> \r\n f=>b \r\n");
        }
    }
}
