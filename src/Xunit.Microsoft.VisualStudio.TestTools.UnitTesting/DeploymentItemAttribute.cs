// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class DeploymentItemAttribute : Attribute
    {
        public DeploymentItemAttribute(string s, string ss)
        {
        }

        public DeploymentItemAttribute(string s)
        {
        }
    }
}