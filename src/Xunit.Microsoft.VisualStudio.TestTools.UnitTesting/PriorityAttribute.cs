// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using Sdk;

    [TraitDiscoverer("Xunit.Microsoft.VisualStudio.TestTools.UnitTesting.PriorityDiscoverer", "Xunit.Microsoft.VisualStudio.TestTools.UnitTesting")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PriorityAttribute : Attribute, ITraitAttribute
    {
        public int Priority { get; private set; }

        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}