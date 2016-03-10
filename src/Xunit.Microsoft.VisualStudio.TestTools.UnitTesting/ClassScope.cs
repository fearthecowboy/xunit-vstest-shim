// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Sdk;

    public class ClassScope
    {
        internal Type Type;
        internal Type BaseType { get { return Type.GetTypeInfo().BaseType;}}
        internal string TypeName { get { return this.Type.Name; } }
        

        internal List<MethodInfo> testInitialize = new List<MethodInfo>();
        internal List<MethodInfo> testCleanup = new List<MethodInfo>();

        internal List<MethodInfo> classInitialize = new List<MethodInfo>();
        internal List<MethodInfo> classCleanup = new List<MethodInfo>();

        internal AssemblyScope AssemblyScope;

        public void TestInitialize(VsUnitTestInvoker invoker,object instance)
        {
            if (BaseType != typeof (object))
            {
                BaseType.GetClassScope().TestInitialize(invoker,instance);
            }

            if (testInitialize.Any())
            {
                foreach (var method in testInitialize.Where(each => each != null))
                {
#if VERBOSE
                    invoker.DebugMessage("Calling ClassScope.TestInitialize: {0}/{1}", method.DeclaringType.Name, method.Name);
#endif 
                    method.Invoke(instance, null);
                }
            }
        }

        public void TestCleanup(VsUnitTestInvoker invoker,object instance)
        {
            if (BaseType != typeof (object))
            {
                BaseType.GetClassScope().TestCleanup(invoker,instance);
            }

            if (testCleanup.Any())
            {
                foreach (var method in testCleanup.Where(each => each != null))
                {
#if VERBOSE
                    invoker.DebugMessage("Calling ClassScope.TestCleanup: {0}/{1}", method.DeclaringType.Name, method.Name);
#endif
                    method.Invoke(instance, null);
                }
            }
        }

        public void ClassInitialize(VsUnitTestInvoker invoker)
        {
            lock (this)
            {
                var methods = classInitialize.ToArray();
                classInitialize.Clear();

                if (BaseType != typeof (object))
                {
                    BaseType.GetClassScope().ClassInitialize(invoker);
                }

                if (methods.Any())
                {
                    foreach (var method in methods.Where(each => each != null))
                    {
#if VERBOSE
                        invoker.DebugMessage("Calling ClassScope.ClassInitialize: {0}/{1}", method.DeclaringType.Name, method.Name);
#endif
                        method.Invoke(null, null);
                    }
                }
            }
        }

        public void ClassCleanup(VsUnitTestInvoker invoker)
        {
            lock (this)
            {
                var methods = classCleanup.ToArray();
                classCleanup.Clear();

                if (BaseType != typeof (object))
                {
                    BaseType.GetClassScope().ClassCleanup(invoker);
                }

                if (methods.Any())
                {
                    // method.Invoke(null, null);
                    invoker.DebugMessage("WARNING: Test Class '{0}' has [ClassCleanup] attribute, will not be called because you shouldn't need that.", TypeName);
                }
            }
        }
    }
}