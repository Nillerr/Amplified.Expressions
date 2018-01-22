using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Arithmetic_BigInteger
    {
        [Fact]
        public void Add()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void CheckedAdd()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void Subtract()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void CheckedSubtract()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void Divide()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left / right);
            value.Should().Be(left / right);
        }

        [Fact]
        public void Modulo()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left % right);
            value.Should().Be(left % right);
        }

        [Fact]
        public void Multiply()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }

        [Fact]
        public void CheckedMultiply()
        {
            var left = new BigInteger(5);
            var right = new BigInteger(10);
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }
    }
}