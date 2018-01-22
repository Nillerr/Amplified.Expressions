using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Arithmetic_Float
    {
        [Fact]
        public void Add()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void CheckedAdd()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void Subtract()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void CheckedSubtract()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void Divide()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left / right);
            value.Should().Be(left / right);
        }

        [Fact]
        public void Modulo()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left % right);
            value.Should().Be(left % right);
        }

        [Fact]
        public void Multiply()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }

        [Fact]
        public void CheckedMultiply()
        {
            float left = 5;
            float right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }
    }
}