using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_Captured
    {
        [Fact]
        public void WithCapturedValue()
        {
            var captured = false;
            var value = ExpressionHelper.ResolveValue(() => captured);
            value.Should().BeFalse();
        }

        [Fact]
        public void WithNegatedCaptured()
        {
            var captured = false;
            var value = ExpressionHelper.ResolveValue(() => !captured);
            value.Should().BeTrue();
        }

        [Fact]
        public void WithInstanceReference()
        {
            var instance = new { Value = new { Inner = 5 } };
            var value = ExpressionHelper.ResolveValue(() => instance);
            value.Should().BeSameAs(instance);
        }

        [Fact]
        public void WithInstancePropertyReference()
        {
            var instance = new { Value = new { Inner = 5 } };
            var value = ExpressionHelper.ResolveValue(() => instance.Value);
            value.Should().BeSameAs(instance.Value);
        }

        [Fact]
        public void WithNestedInstancePropertyReference()
        {
            var instance = new { Value = new { Inner = 5 } };
            var value = ExpressionHelper.ResolveValue(() => instance.Value.Inner);
            value.Should().Be(instance.Value.Inner);
        }

        private sealed class FieldContainer
        {
            public Nested nestedField;

            public sealed class Nested
            {
                public string value;
            }
        }

        [Fact]
        public void WithInstanceFieldReference()
        {
            var instance = new FieldContainer();
            instance.nestedField = new FieldContainer.Nested();

            var value = ExpressionHelper.ResolveValue(() => instance.nestedField);
            value.Should().BeSameAs(instance.nestedField);
        }

        [Fact]
        public void WithNestedInstanceFieldReference()
        {
            var instance = new FieldContainer();
            instance.nestedField = new FieldContainer.Nested();
            instance.nestedField.value = "Foo";

            var value = ExpressionHelper.ResolveValue(() => instance.nestedField.value);
            value.Should().BeSameAs(instance.nestedField.value);
        }

        [Fact]
        public void WithConstructor()
        {
            var value = ExpressionHelper.ResolveValue(() => new ValueContainer(10));
            value.Should().NotBeNull();
            value.ValueProperty.Should().Be(10);
        }

        [Fact]
        public void WithPropertyAccessorOfConstructedInstance()
        {
            var container = new ValueContainer(5);
            var value = ExpressionHelper.ResolveValue(() => new ValueContainer(5).ValueProperty);
            value.Should().Be(container.ValueProperty);
        }

        [Fact]
        public void WithFieldAccessorOfConstructedInstance()
        {
            var container = new ValueContainer(5);
            var value = ExpressionHelper.ResolveValue(() => new ValueContainer(5).ValueField);
            value.Should().Be(container.ValueField);
        }

        [Fact]
        public void WithMethodAccessorOfConstructedInstance()
        {
            var container = new ValueContainer(5);
            var value = ExpressionHelper.ResolveValue(() => new ValueContainer(5).ValueMethod());
            value.Should().Be(container.ValueMethod());
        }

        private sealed class ValueContainer
        {
            public readonly int ValueField;

            public ValueContainer(int value)
            {
                ValueField = value;
            }

            public int ValueProperty => ValueField;

            public int ValueMethod() => ValueProperty;
        }

        [Fact]
        public void WithMethodReferenceMemberAccess()
        {
            var container = new ValueContainer(5);
            var value = ExpressionHelper.ResolveValue<Func<int>>(() => new ValueContainer(5).ValueMethod);
            value.Should().NotBeNull();
            value().Should().Be(container.ValueMethod());
        }
    }
}