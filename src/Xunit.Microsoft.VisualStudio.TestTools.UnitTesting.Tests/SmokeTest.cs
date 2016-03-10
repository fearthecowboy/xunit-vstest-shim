// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Xunit;

[assembly: TestCaseOrderer("Xunit.Microsoft.VisualStudio.TestTools.UnitTesting.PriorityOrderer", "Xunit.Microsoft.VisualStudio.TestTools.UnitTesting")]
namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class SmokeTestFailing
    {
        [TestMethod, Trait("green", "blue"), Priority(3)]
        public void ThisShouldPri3()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }

        [TestMethod, Timeout(1000), Priority(1), ExpectedException(typeof(TimeoutException))]
        public void ThisShouldRunPri1()
        {
            Task.Delay(10000).Wait();
            Assert.IsTrue(true, "This test can't fail.");
        }

        [TestMethod, Priority(2)]
        public void ThisShouldRunPri2()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }


        [TestMethod, Timeout(1000), Priority(0), ExpectedException(typeof( TimeoutException))]
        public void ThisShouldRunPriZero()
        {
            Task.Delay(10000).Wait();
            Assert.IsTrue(true, "This test can't fail.");
        }
    }

    [TestClass]
    public class ExpectTheException
    {

        [TestMethod]
        public void IsOk()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ShouldFail()
        {
            throw new NullReferenceException("wheeee");
        }
    }

    public class SmokeTest
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

        [Fact]
        public void SeeIfItWorksWithFact()
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

        [TestMethod, Trait("Category", "Red")]
        public void SeeIfItWorksWithTrait()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }

        [Fact, Trait("Category", "Red")]
        public void SeeIfItWorksWithFactAndTrait()
        {
            Assert.IsTrue(true, "This test can't fail.");
        }
    }
}