// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using Abstractions;

    public class TestContext
    {
        private readonly ITestOutputHelper testOutputHelper;
        public TestContext(ITestOutputHelper outputHelper )
        {
            testOutputHelper = outputHelper;
        }
        public void WriteLine(string format, params object[] args)
        {
            testOutputHelper.WriteLine(format,args);
        }

        public string TestDir { get; set; }

        public string TestName { get; set; }
    }
}