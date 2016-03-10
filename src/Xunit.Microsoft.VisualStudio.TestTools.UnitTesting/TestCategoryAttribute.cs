// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using Xunit.Sdk;

    [TraitDiscoverer("Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryDiscoverer", "Xunit.Microsoft.VisualStudio.TestTools.UnitTesting")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestCategoryAttribute : Attribute, ITraitAttribute
    {
        public string Category { get; private set; }

        public TestCategoryAttribute(string category)
        {
            Category = category;
        }
    }
}