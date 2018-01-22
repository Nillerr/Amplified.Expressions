using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Nullable
    {
        [Fact]
        public void NullableLiftedConversion()
        {
            var capturedValue = 5;
            var value = ExpressionHelper.ResolveValue(() => new TestClass().FromNullable(capturedValue));
            value.Should().Be(capturedValue);
        }

        private sealed class TestClass
        {
            public int FromNullable(int? value) => value ?? 0;
        }
    }
}