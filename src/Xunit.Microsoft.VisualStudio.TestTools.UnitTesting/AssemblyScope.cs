// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class AssemblyScope
    {
        internal string assemblyName;
        internal List<MethodInfo> assemblyInitialize = new List<MethodInfo>();
        internal List<MethodInfo> assemblyCleanup = new List<MethodInfo>();

        public void AssemblyInitialize(VsUnitTestInvoker invoker)
        {
            lock (this)
            {
                var methods = assemblyInitialize.ToArray();

                assemblyInitialize.Clear();
                if (methods.Any())
                {
                    foreach (var method in methods.Where(each => each != null))
                    {
#if VERBOSE
                        invoker.DebugMessage("Calling AssemblyScope.AssemblyInitialize: {0}/{1}", method.DeclaringType.Name, method.Name);
#endif
                        method.Invoke(null, null);
                    }
                }
            }
        }

        public void AssemblyCleanup(VsUnitTestInvoker invoker)
        {
            lock (this)
            {
                var methods = assemblyCleanup.ToArray();
                assemblyCleanup.Clear();
                if (methods.Any())
                {
                    // method.Invoke(null, null);
                    invoker.DebugMessage("WARNING: Test Assembly '{0}' has [AssemblyCleanup] attribute, will not be called because you shouldn't need that.", assemblyName);
                }
            }
        }
    }
}