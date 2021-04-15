using FluentAssertions;
using Xunit;

namespace Toto.Utilities.Extensions.Tests
{
    public class StringExtensionsTest
    {
        [Fact]
        public void Can_Add_Unclosed_Html_Tag()
        {
            const string input = "Das ist ein toller <b> Text.";
            const string expected = "Das ist ein toller <b> Text.</b>";

            var result = input.SanitizeHtml();

            result.Should().Be(expected);
        }
        
        [Fact]
        public void Can_Remove_Unclosed_Html_Tag()
        {
            const string input = "Das ist ein toller <script> var b = 1+1; //Text.";
            const string expected = "Das ist ein toller ";

            var result = input.SanitizeHtml();

            result.Should().Be(expected);
        }
    }
}