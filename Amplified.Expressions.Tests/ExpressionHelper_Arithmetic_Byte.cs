using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Arithmetic_Byte
    {
        [Fact]
        public void Add()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void CheckedAdd()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left + right));
            value.Should().Be(left + right);
        }

        [Fact]
        public void Subtract()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void CheckedSubtract()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left - right));
            value.Should().Be(left - right);
        }

        [Fact]
        public void Divide()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => left / right);
            value.Should().Be(left / right);
        }

        [Fact]
        public void Modulo()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => left % right);
            value.Should().Be(left % right);
        }

        [Fact]
        public void Multiply()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }

        [Fact]
        public void CheckedMultiply()
        {
            byte left = 5;
            byte right = 10;
            var value = ExpressionHelper.ResolveValue(() => checked(left * right));
            value.Should().Be(left * right);
        }
    }
}