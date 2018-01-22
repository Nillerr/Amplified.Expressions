using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Comparison_Comparable
    {
        [Fact]
        public void GreaterThanOrEqual()
        {
            var left = 5;
            var right = 10;
            var value = ExpressionHelper.ResolveValue(() => left >= right);
            value.Should().Be(left >= right);
        }

        [Fact]
        public void GreaterThan()
        {
            var left = 5;
            var right = 10;
            var value = ExpressionHelper.ResolveValue(() => left > right);
            value.Should().Be(left > right);
        }

        [Fact]
        public void LessThanOrEqual()
        {
            var left = 5;
            var right = 10;
            var value = ExpressionHelper.ResolveValue(() => left <= right);
            value.Should().Be(left <= right);
        }

        [Fact]
        public void LessThan()
        {
            var left = 5;
            var right = 10;
            var value = ExpressionHelper.ResolveValue(() => left < right);
            value.Should().Be(left < right);
        }

        [Fact]
        public void Equal()
        {
            var left = 5;
            var right = 10;
            var value = ExpressionHelper.ResolveValue(() => left == right);
            value.Should().Be(left == right);
        }

        [Fact]
        public void NotEqual()
        {
            var left = 5;
            var right = 10;
            var value = ExpressionHelper.ResolveValue(() => left != right);
            value.Should().Be(left != right);
        }
    }
}