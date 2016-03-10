// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    public class VsTestCaseRunner : XunitTestCaseRunner
    {
        private Type ExpectedException;

        public VsTestCaseRunner(IXunitTestCase testCase, string displayName, string skipReason, object[] constructorArguments, object[] testMethodArguments, IMessageBus messageBus, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, Type expectedException)
            : base(testCase, displayName, skipReason, constructorArguments, testMethodArguments, messageBus, aggregator, cancellationTokenSource)
        {
            ExpectedException = expectedException;
        }

        protected override Task<RunSummary> RunTestAsync()
        {
            return new VsTestRunner((ITest) new XunitTest(this.TestCase, this.DisplayName), this.MessageBus, this.TestClass, this.ConstructorArguments, this.TestMethod, this.TestMethodArguments, this.SkipReason, (IReadOnlyList<BeforeAfterTestAttribute>) this.BeforeAfterAttributes, new ExceptionAggregator(this.Aggregator), this.CancellationTokenSource,ExpectedException).RunAsync();
        }
    }
}