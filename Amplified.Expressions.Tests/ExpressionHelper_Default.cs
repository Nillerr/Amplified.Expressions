using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Default
    {
        [Fact]
        public void DefaultValueType()
        {
            var value = Expression.Default(typeof(TimeSpan)).ResolveValue();
            value.Should().Be(default(TimeSpan));
        }

        [Fact]
        public void DefaultReferenceType()
        {
            var value = Expression.Default(typeof(object)).ResolveValue();
            value.Should().Be(default(object));
        }
    }
}