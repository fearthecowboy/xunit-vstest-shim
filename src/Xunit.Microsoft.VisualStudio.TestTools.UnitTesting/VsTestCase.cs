// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions;
    using Sdk;

    public class VsTestCase : XunitTestCase
    {
        public int Timeout;
        private Type ExpectedException;

        [Obsolete("Called by the de-serializer; should only be called by deriving classes for de-serialization purposes")]
        public VsTestCase()
        {
        }

        public VsTestCase(IMessageSink diagnosticMessageSink, TestMethodDisplay defaultMethodDisplay, ITestMethod testMethod, object[] testMethodArguments, int timeout, Type expectedException)
            : base(diagnosticMessageSink, defaultMethodDisplay, testMethod, testMethodArguments)
        {
            Timeout = timeout;
            ExpectedException = expectedException;
        }

        public override async Task<RunSummary> RunAsync(IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            object[] constructorArguments,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token);

            cts.CancelAfter(Timeout);

            // beacuse this isn't always an async call, and we really do want it always be that way.
            var run = Task.Factory.StartNew(() => new VsTestCaseRunner((IXunitTestCase) this, this.DisplayName, this.SkipReason, constructorArguments, this.TestMethodArguments, messageBus, aggregator, cts,ExpectedException).RunAsync().Result, cts.Token);

            try
            {
                if (run.Wait(Timeout, cts.Token))
                {
                    return run.Result;
                }
            }
            catch
            {
               
            }

            if (ExpectedException == typeof(TimeoutException)) {
                return new RunSummary {
                    Failed = 0
                };
            }

            // ReSharper disable once UseStringInterpolation
            messageBus.QueueMessage(new ErrorMessage(new[] {this}, new TimeoutException(string.Format("Test '{0}' with timeout failed to complete in expected time of {1} msec", this.BaseDisplayName, Timeout))));
            return new RunSummary
            {
                Failed = 1
            };
        }
    }
}