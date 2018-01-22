using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Conditionals
    {
        [Fact]
        public void AndAlso()
        {
            var isTrue = true;
            var isFalse = false;
            var value = ExpressionHelper.ResolveValue(() => isTrue && isFalse);
            value.Should().Be(isTrue && isFalse);
        }

        [Fact]
        public void OrElse()
        {
            var isTrue = true;
            var isFalse = false;
            var value = ExpressionHelper.ResolveValue(() => isTrue || isFalse);
            value.Should().Be(isTrue || isFalse);
        }
    }
}