using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Arithmetic_Int
    {
        [Fact]
        public void Add()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void CheckedAdd()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left + right));
            value.Should().Be(left + right);
        }

        [Fact]
        public void Subtract()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void CheckedSubtract()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left - right));
            value.Should().Be(left - right);
        }

        [Fact]
        public void Divide()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => left / right);
            value.Should().Be(left / right);
        }

        [Fact]
        public void Modulo()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => left % right);
            value.Should().Be(left % right);
        }

        [Fact]
        public void Multiply()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }

        [Fact]
        public void CheckedMultiply()
        {
            int left = 5;
            int right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left * right));
            value.Should().Be(left * right);
        }
    }
}