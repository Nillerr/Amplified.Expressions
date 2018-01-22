using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Amplified.Expressions.Tests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ExpressionHelper_MemberInit
    {
        [Fact]
        public void WithMemberInit()
        {
            var value = ExpressionHelper.ResolveValue(() => new WithMembers("Hello, World!") {Str = "Foo, Bar!", Field = "Hello"});
            value.Should().NotBeNull();
            value.Str.Should().Be("Foo, Bar!");
        }

        private sealed class WithMembers
        {
            public WithMembers(string str)
            {
                Str = str;
            }
            
            public string Str { get; set; }
            public string Field;
        }
    }
}