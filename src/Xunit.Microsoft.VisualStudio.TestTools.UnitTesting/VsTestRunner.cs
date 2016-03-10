// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;
    using Sdk;

    public class VsTestRunner : XunitTestRunner
    {
        private Type ExpectedException;
        private TestOutputHelper testOutputHelper = new TestOutputHelper();

        public VsTestRunner(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, string skipReason, IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, Type expectedException)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, skipReason, beforeAfterAttributes, aggregator, cancellationTokenSource)
        {
            ExpectedException = expectedException;
        }

        protected override Task<Tuple<decimal, string>> InvokeTestAsync(ExceptionAggregator aggregator) {
            
            testOutputHelper.Initialize(MessageBus, Test);

            return base.InvokeTestAsync(aggregator).ContinueWith(antecedent =>
            {
                string output = testOutputHelper.Output;
                testOutputHelper.Uninitialize();

                return Tuple.Create( antecedent.Result.Item1, output);
            });
        }

        protected override Task<Decimal> InvokeTestMethodAsync(ExceptionAggregator aggregator)
        {
            return new VsUnitTestInvoker(this.Test, this.MessageBus, this.TestClass, this.ConstructorArguments, this.TestMethod, this.TestMethodArguments, this.BeforeAfterAttributes, aggregator, this.CancellationTokenSource, ExpectedException,testOutputHelper).RunAsync();
        }
    }
}