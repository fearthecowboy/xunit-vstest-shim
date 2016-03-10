namespace Microsoft.VisualStudio.TestTools.UnitTesting.Tests
{
    using System;
    using System.Threading.Tasks;
    using Assert = UnitTesting.Assert;

    [TestClass]
    public class MoreTests
    {
        [TestMethod, Priority(3)]
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
}