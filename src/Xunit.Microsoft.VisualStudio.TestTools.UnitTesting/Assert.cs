// --------------------------------------------------------------------------------------------------------------------
// <copyright>
//   Copyright (c) Microsoft. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Xunit.Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual)
        {
            Xunit.Assert.Equal(expected, actual);
        }

        public static void AreEqual<T>(T expected, T actual, string message)
        {
            Xunit.Assert.True(expected.Equals(actual), message);
        }

        public static void AreEqual<T>(T expected, T actual, string message, params object[] items)
        {
            Xunit.Assert.True(expected.Equals(actual), string.Format(message, items));
        }

        public static void IsNotNull(object result)
        {
            Xunit.Assert.NotNull(result);
        }

        public static void IsNotNull(object result, string message)
        {
            Xunit.Assert.True(result != null, message);
        }

        public static void IsFalse(bool expectedFalse)
        {
            Xunit.Assert.False(expectedFalse);
        }

        public static void IsFalse(bool expectedFalse, string message)
        {
            Xunit.Assert.False(expectedFalse, message);
        }

        public static void AreNotEqual(string notExpected, string actual, bool ignoreCase, string message)
        {
            Xunit.Assert.False(string.Equals(notExpected, actual, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal), message);
        }

        public static void AreNotEqual<T>(T notExpected, T actual, string message)
        {
            Xunit.Assert.False(notExpected.Equals(actual), message);
        }

        public static void AreNotEqual<T>(T notExpected, T actual)
        {
            Xunit.Assert.NotEqual(notExpected, actual);
        }

        public static void IsTrue(bool condition, string message)
        {
            Xunit.Assert.True(condition, message);
        }

        public static void IsTrue(bool condition)
        {
            Xunit.Assert.True(condition);
        }

        public static void Fail(string message)
        {
            Xunit.Assert.True(false, message);
        }
    }
}