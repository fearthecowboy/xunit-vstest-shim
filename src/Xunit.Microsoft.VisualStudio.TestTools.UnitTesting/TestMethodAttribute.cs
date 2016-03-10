// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using Xunit;
    using Xunit.Sdk;

    [XunitTestCaseDiscoverer("Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodDiscoverer", "Xunit.Microsoft.VisualStudio.TestTools.UnitTesting")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestMethodAttribute : FactAttribute
    {
    }
}