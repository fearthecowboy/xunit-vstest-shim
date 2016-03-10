// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using Abstractions;
    using Sdk;

    public class VsUnitTestInvoker : XunitTestInvoker
    {
        private Type ExpectedException;
        private ITestOutputHelper testOutputHelper;

        public VsUnitTestInvoker(ITest test, IMessageBus messageBus, Type testClass, object[] constructorArguments, MethodInfo testMethod, object[] testMethodArguments, IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes, ExceptionAggregator aggregator, CancellationTokenSource cancellationTokenSource, Type expectedException,ITestOutputHelper outputHelper)
            : base(test, messageBus, testClass, constructorArguments, testMethod, testMethodArguments, beforeAfterAttributes, aggregator, cancellationTokenSource)
        {
            ExpectedException = expectedException;
            testOutputHelper = outputHelper;
        }

        private void SetTestContext(object testClassInstance)
        {
            try
            {
                var tc = testClassInstance.GetType().GetTypeInfo().GetProperty("TestContext");
                if (tc != null)
                {
                    var testContext = tc.GetValue(testClassInstance) as TestContext;
                    if (testContext == null)
                    {
                        testContext = new TestContext(testOutputHelper);
                        tc.SetValue(testClassInstance, testContext);
                    }

                    testContext.TestName = this.Test.DisplayName;
                    testContext.TestDir = Directory.GetCurrentDirectory();
                }
            }
            catch
            {
                
            }
        }

        internal void DebugMessage(string format, params object[] args)
        {
            var message = string.Format("{0}/{1} :", this.TestClass.Name, this.TestMethod.Name);
#if DEBUG
            testOutputHelper.WriteLine(format,args);
#endif
            this.MessageBus.QueueMessage(new DiagnosticMessage(message + format, args));
        }

        protected override object CallTestMethod(object testClassInstance)
        {
            object result = null;

            SetTestContext(testClassInstance);

            var init = testClassInstance.GetType().GetClassScope();

            // call assembly init (will only actually run once)
            Aggregator.Run(() => { init.AssemblyScope.AssemblyInitialize(this); });

            if (Aggregator.HasExceptions)
            {
                DebugMessage("Assembly Init Failed. Aborting");
                return result;
            }

            // call class init (will only actually run once)
            Aggregator.Run(() => { init.ClassInitialize(this); });

            if (Aggregator.HasExceptions)
            {
                DebugMessage("Class Init Failed. Aborting");
                return result;
            }

            // call testinitialize
            Aggregator.Run(() => { init.TestInitialize(this,testClassInstance); });

            if (Aggregator.HasExceptions)
            {
                DebugMessage("Test Init Failed. Aborting");
                return result;
            }
#if VERBOSE
            DebugMessage("Calling Test Code ");
#endif
            
            // is ok, call test method
            
            
                try
                {
#if VERBOSE
                    if (ExpectedException != null)
                    {
                        DebugMessage("Is expecting exception type: {0}", ExpectedException.Name);
                    }
#endif
                    result = base.CallTestMethod(testClassInstance);
                }
                catch (TargetInvocationException e)
                {
                    if (ExpectedException == null)
                    {
                        throw;
                    }

                    if (e.InnerException.GetType() != ExpectedException)
                    {
                        DebugMessage("Expected to throw type '{0}', got a '{1}'", ExpectedException.Name, e.InnerException.GetType().Name);
                        throw;
                    }
                }
                finally
                {
#if VERBOSE
                    DebugMessage("Done Calling Test Code ");
#endif
                    // call test cleanup 
                    Aggregator.Run(() => { init.TestCleanup(this, testClassInstance); });

                    // call class cleanup
                    Aggregator.Run(() => { init.ClassCleanup(this); });

                    // call assembly cleanup
                    Aggregator.Run(() => { init.AssemblyScope.AssemblyCleanup(this); });

                }
           
            
            return result;
        }
    }
}