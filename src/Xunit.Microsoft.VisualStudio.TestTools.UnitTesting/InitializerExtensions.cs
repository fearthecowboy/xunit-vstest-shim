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
    using System.Reflection;

    internal static class InitializerExtensions
    {
        private static readonly Dictionary<Type, ClassScope> types = new Dictionary<Type, ClassScope>();
        private static readonly Dictionary<Assembly, AssemblyScope> assemblies = new Dictionary<Assembly, AssemblyScope>();

        internal static AssemblyScope GetAssemblyScope(this Type type)
        {
            var assembly = type.GetTypeInfo().Assembly;
            // have we handled this type before?
            if (!types.ContainsKey(type))
            {
                
                var staticMethods = type.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Static ).ToArray();

                // grab the assemblyscope 
                if (!assemblies.ContainsKey(assembly))
                {
                    assemblies.Add(assembly, new AssemblyScope{ assemblyName = type.AssemblyQualifiedName });
                }

                assemblies[assembly].assemblyInitialize.AddRange(staticMethods.Where(each => each.GetCustomAttributes(typeof (AssemblyInitializeAttribute)).Any()));
                assemblies[assembly].assemblyCleanup.AddRange(staticMethods.Where(each => each.GetCustomAttributes(typeof (AssemblyCleanupAttribute)).Any()));
            }
            return assemblies[assembly];
        }

        internal static ClassScope GetClassScope(this Type type)
        {
            if (!types.ContainsKey(type))
            {
                var instanceMethods = type.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToArray();
                var staticMethods = type.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Static ).ToArray();
                var scope = new ClassScope
                {
                    Type = type,
                    AssemblyScope = GetAssemblyScope(type)
                };

                scope.testInitialize.AddRange(instanceMethods.Where(each => each.GetCustomAttributes(typeof (TestInitializeAttribute)).Any()));
                scope.testCleanup.AddRange(instanceMethods.Where(each => each.GetCustomAttributes(typeof (TestCleanupAttribute)).Any()));
                scope.classInitialize.AddRange(staticMethods.Where(each => each.GetCustomAttributes(typeof (ClassInitializeAttribute)).Any()));
                scope.classCleanup.AddRange(staticMethods.Where(each => each.GetCustomAttributes(typeof (ClassCleanupAttribute)).Any()));

                types.Add(type, scope);
                if (type.GetTypeInfo().BaseType != typeof (object))
                {
                    GetClassScope(type.GetTypeInfo().BaseType);
                }
            }
            return types[type];
        }
    }
}