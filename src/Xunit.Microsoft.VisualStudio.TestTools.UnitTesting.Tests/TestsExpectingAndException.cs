namespace Microsoft.VisualStudio.TestTools.UnitTesting.Tests
{
    using System;

    [TestClass]
    public class TestsExpectingAndException
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
}