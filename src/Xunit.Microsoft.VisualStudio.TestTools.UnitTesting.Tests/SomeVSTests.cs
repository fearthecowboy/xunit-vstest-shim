// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------



[assembly: Xunit.TestCaseOrderer("Microsoft.VisualStudio.TestTools.UnitTesting.PriorityOrderer", "Microsoft.VisualStudio.TestTools.UnitTesting")]

namespace Microsoft.VisualStudio.TestTools.UnitTesting.Tests
{
    using System;
    using System.Threading.Tasks;
    
    using Assert = UnitTesting.Assert;

    [TestClass]
    public class SomeVSTests
    {

        public TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public static void VeryFirstThing()
        {
          
        }

        [TestInitialize]
        public void Init()
        {
            TestContext.WriteLine("Initializing...");
        }


        [ClassInitialize]
        public static void Initclass()
        {
           //  Console.WriteLine("Initializing class...");
        }


        [ClassCleanup]
        public static void Cleanupclass()
        {
           // Console.WriteLine("Cleanup class...");
        }


        [TestCleanup]
        public void Shutdown()
        {
            TestContext.WriteLine("Cleaning up...");
        }


        [TestMethod]
        public void SeeIfItWorksWithTestMethod()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }

       [TestMethod]
        [Ignore]
        public void IgnoreThisTest()
        {
            Assert.Fail("This test should have been ignored.");
        }

        [TestMethod, TestCategory("Blue")]
        public void SeeIfItWorksWithTestMethodAndCategory()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }

        [TestMethod, Priority(1)]
        public void SeeIfItWorksWithTestMethodAndPriority()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }

        [TestMethod, Timeout(100), ExpectedException(typeof(TimeoutException))]
        public void ThisShouldRunFour()
        {
            Task.Delay(10000).Wait();

            // should never get here.
            Assert.IsTrue(true, "This test can't fail.");
        }

        [TestMethod, Timeout(500)]
        public void SeeIfItWorksWithTestMethodAndShortTimeout()
        {
            Task.Delay(10).Wait();

            Assert.IsTrue(true, "This test can't fail.");
        }
    }
}