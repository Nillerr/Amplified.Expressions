using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Arithmetic_Double
    {
        [Fact]
        public void Add()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void CheckedAdd()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left + right);
            value.Should().Be(left + right);
        }

        [Fact]
        public void Subtract()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void CheckedSubtract()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left - right);
            value.Should().Be(left - right);
        }

        [Fact]
        public void Divide()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left / right);
            value.Should().Be(left / right);
        }

        [Fact]
        public void Modulo()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left % right);
            value.Should().Be(left % right);
        }

        [Fact]
        public void Multiply()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }

        [Fact]
        public void CheckedMultiply()
        {
            double left = 5;
            double right = 10;
            var value = ExpressionHelper.ResolveValue(() => left * right);
            value.Should().Be(left * right);
        }
    }
}