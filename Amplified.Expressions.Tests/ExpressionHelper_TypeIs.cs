using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_TypeIs
    {
        [Fact]
        public void DefaultValueType()
        {
            var operand = TimeSpan.FromSeconds(5);
            var value = ExpressionHelper.ResolveValue(() => operand is TimeSpan);
            value.Should().BeTrue();
        }
        
        [Fact]
        public void NullTypeComparison()
        {
            var value = ExpressionHelper.ResolveValue(() => null is TimeSpan);
            value.Should().BeFalse();
        }
    }
}