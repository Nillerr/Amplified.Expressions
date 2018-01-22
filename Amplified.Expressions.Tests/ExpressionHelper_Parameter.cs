using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Parameter
    {
        private static TResult ResolveValue<TSource, TResult>(Expression<Func<TSource, TResult>> expression)
        {
            return (TResult) expression.Body.ResolveValue();
        }
        
        [Fact]
        public void Parameter()
        {
            Assert.Throws<ArgumentException>(() => ResolveValue<object, int>(o => o.GetHashCode()));
        }

        [Fact]
        public void Member()
        {
            var other = new object();
            var value = ResolveValue<object, int>(o => other.GetHashCode());
            value.Should().Be(other.GetHashCode());
        }
    }
}