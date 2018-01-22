using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Arithmetic_Long
    {
        [Fact]
        public void Add()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void CheckedAdd()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left + right));
            value.Should().Be(left + right);
        }

        [Fact]
        public void Subtract()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void CheckedSubtract()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left - right));
            value.Should().Be(left - right);
        }

        [Fact]
        public void Divide()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => left / right);
            value.Should().Be(left / right);
        }

        [Fact]
        public void Modulo()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => left % right);
            value.Should().Be(left % right);
        }

        [Fact]
        public void Multiply()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }

        [Fact]
        public void CheckedMultiply()
        {
            long left = 5;
            long right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left * right));
            value.Should().Be(left * right);
        }
    }
}