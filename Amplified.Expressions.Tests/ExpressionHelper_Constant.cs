using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Constant
    {
        [Fact]
        public void WithConstant()
        {
            var value = ExpressionHelper.ResolveValue(() => 5);
            value.Should().Be(5);
        }

        [Fact]
        public void WithConstantReferenceExpression()
        {
            const int constantValue = 5;
            var value = ExpressionHelper.ResolveValue(() => constantValue);
            value.Should().Be(constantValue);
        }
        
        [Fact]
        public void Null()
        {
            var value = ExpressionHelper.ResolveValue<Attribute>(() => null);
            value.Should().BeNull();
        }
    }
}