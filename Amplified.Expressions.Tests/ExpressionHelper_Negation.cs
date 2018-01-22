using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Negation
    {
        [Fact]
        public void NegatedValue()
        {
            long? captured = 5L;
            var value = ExpressionHelper.ResolveValue(() => -captured);
            value.Should().Be(-captured);
        }
        
        [Fact]
        public void CheckedNegatedValue()
        {
            var captured = long.MaxValue;
            var value = ExpressionHelper.ResolveValue(() => checked(-captured));
            value.Should().Be(-captured);
        }
        
        [Fact]
        public void UncheckedNegatedValue()
        {
            var captured = long.MaxValue;
            var value = ExpressionHelper.ResolveValue(() => unchecked(-captured));
            value.Should().Be(-captured);
        }
        
        [Fact]
        public void NullableValue()
        {
            int? captured = null;
            var value = ExpressionHelper.ResolveValue(() => -captured);
            value.Should().Be(-captured);
        }
        
        [Fact]
        public void DecimalNullValue()
        {
            decimal? captured = null;
            var value = ExpressionHelper.ResolveValue(() => -captured);
            value.Should().Be(-captured);
        }
        
        [Fact]
        public void DecimalValue()
        {
            decimal? captured = 5;
            var value = ExpressionHelper.ResolveValue(() => -captured);
            value.Should().Be(-captured);
        }
    }
}