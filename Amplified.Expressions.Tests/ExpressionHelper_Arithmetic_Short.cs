using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Arithmetic_Short
    {
        [Fact]
        public void Add()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void CheckedAdd()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left + right));
            value.Should().Be(left + right);
        }

        [Fact]
        public void Subtract()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void CheckedSubtract()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left - right));
            value.Should().Be(left - right);
        }

        [Fact]
        public void Divide()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => left / right);
            value.Should().Be(left / right);
        }

        [Fact]
        public void Modulo()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => left % right);
            value.Should().Be(left % right);
        }

        [Fact]
        public void Multiply()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }

        [Fact]
        public void CheckedMultiply()
        {
            short left = 5;
            short right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left * right));
            value.Should().Be(left * right);
        }
    }
}