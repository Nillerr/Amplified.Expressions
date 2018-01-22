using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Bitwise
    {
        [Fact]
        public void And()
        {
            var left = 1;
            var right = 2;
            var value = ExpressionHelper.ResolveValue(() => left & right);
            value.Should().Be(left & right);
        }

        [Fact]
        public void Or()
        {
            var left = 1;
            var right = 2;
            var value = ExpressionHelper.ResolveValue(() => left | right);
            value.Should().Be(left | right);
        }

        [Fact]
        public void ExclusiveOr()
        {
            var left = 1;
            var right = 2;
            var value = ExpressionHelper.ResolveValue(() => left ^ right);
            value.Should().Be(left ^ right);
        }

        [Fact]
        public void LeftShift()
        {
            var left = 1;
            var right = 2;
            var value = ExpressionHelper.ResolveValue(() => left << right);
            value.Should().Be(left << right);
        }

        [Fact]
        public void RightShift()
        {
            var left = 1;
            var right = 2;
            var value = ExpressionHelper.ResolveValue(() => left >> right);
            value.Should().Be(left >> right);
        }
    }
}