using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Coalesce
    {
        [Fact]
        public void WithCapturedValue()
        {
            object left = null;
            object right = new object();
            var value = ExpressionHelper.ResolveValue(() => left ?? right);
            value.Should().BeSameAs(right);
        }
    }
}