using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_ArrayIndex
    {
        [Fact]
        public void ArrayIndex()
        {
            var other = new[] {new object()};
            var value = ExpressionHelper.ResolveValue(() => other[0]);
            value.Should().BeSameAs(other[0]);
        }
    }
}