// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    /// <summary>
    /// The test method discoverer.
    /// </summary>
    public class TestMethodDiscoverer : IXunitTestCaseDiscoverer
    {
        /// <summary>
        /// The diagnostic message sink.
        /// </summary>
        private readonly IMessageSink diagnosticMessageSink;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodDiscoverer"/> class.
        /// </summary>
        /// <param name="diagnosticMessageSink">The message sink used to send diagnostic messages</param>
        public TestMethodDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this.diagnosticMessageSink = diagnosticMessageSink;
        }

        /// <summary>
        /// Creates a single <see cref="T:Xunit.Sdk.XunitTestCase"/> for the given test method.
        /// </summary>
        /// <param name="discoveryOptions">The discovery options to be used.</param><param name="testMethod">The test method.</param><param name="factAttribute">The attribute that decorates the test method.</param>
        /// <returns>A test case</returns>
        protected virtual IXunitTestCase CreateTestCase(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            return (IXunitTestCase) new XunitTestCase(this.diagnosticMessageSink, TestFrameworkOptionsReadExtensions.MethodDisplayOrDefault(discoveryOptions), testMethod, (object[]) null);
        }

        private static int GetTimeout(ITestMethod testMethod)
        {
            try
            {
                var timeoutAttribute = testMethod.Method.GetCustomAttributes(typeof (TimeoutAttribute)).FirstOrDefault();
                if (timeoutAttribute != null)
                {
                    return (int) (timeoutAttribute.GetConstructorArguments().FirstOrDefault() ?? -1);
                }
            }
            catch
            {
                
            }
            return -1;
        }

        private static Type GetExpectedExceptionType(ITestMethod testMethod) {
            try
            {
                var attribute = testMethod.Method.GetCustomAttributes(typeof (ExpectedExceptionAttribute)).FirstOrDefault();
                if (attribute != null)
                {
                    return (Type) (attribute.GetConstructorArguments().FirstOrDefault() ?? typeof (Exception));
                }
            }
            catch
            {
                
            }
            return null;
        }

        /// <summary>
        /// Discover test cases from a test method.
        /// </summary>
        /// <param name="discoveryOptions">
        /// The discovery options to be used.
        /// </param>
        /// <param name="testMethod">
        /// The test method the test cases belong to.
        /// </param>
        /// <param name="testMethodAttribute">
        /// The test Method Attribute.
        /// </param>
        /// <returns>
        /// Returns zero or more test cases represented by the test method.
        /// </returns>
        public virtual IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo testMethodAttribute)
        {
            // make sure we have this in the class scope collection.
            testMethod.TestClass.Class.ToRuntimeType().GetClassScope();

            // we can filter out ones based on Ignore 
            if (!testMethod.Method.GetCustomAttributes(typeof (IgnoreAttribute)).Any())
            {
                // return the test case.
                yield return (IXunitTestCase) new VsTestCase(this.diagnosticMessageSink, TestFrameworkOptionsReadExtensions.MethodDisplayOrDefault(discoveryOptions), testMethod, (object[]) null, GetTimeout(testMethod), GetExpectedExceptionType(testMethod));
            }
        }
    }
}