// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;

    public class TimeoutAttribute : Attribute
    {
        public int Timeout { get; private set; }

        public TimeoutAttribute(int timeout)
        {
            Timeout = timeout;
        }
    }
}