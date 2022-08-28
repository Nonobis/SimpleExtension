using Xunit;

namespace SimpleExtension.Core.Tests
{
    /// <summary>
    /// Unit Test on String Extensions
    /// </summary>
    public class StringExtensionsTest
    {
        ///
        /// <summary>When base 64 string is returned.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        ///
        [Theory]
        [InlineData("SimpleExtensions.Core", "U2ltcGxlRXh0ZW5zaW9ucy5Db3Jl")]
        [InlineData("simpleExtensions.core", "c2ltcGxlRXh0ZW5zaW9ucy5jb3Jl")]
        public void WhenBase64StringIsReturned(string value, string expectedValue) => Assert.Equal(value.ToBase64(), expectedValue);

        /// <summary>When camel case is returned.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Theory]
        [InlineData("CamelCase", "camelCase")]
        [InlineData("camelCase", "camelCase")]
        [InlineData("camelcase", "camelcase")]
        public void WhenCamelCaseIsReturned(string value, string expectedValue) => Assert.Equal(value.ToCamelCase(), expectedValue);

        /// <summary>
        /// Whens the first character to upper string is returned.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Theory]
        [InlineData("simple", "Simple")]
        public void WhenFirstCharToUpperStringIsReturned(string value, string expectedValue) => Assert.Equal(value.ToFirstCharToUpper(), expectedValue);

        ///
        /// <summary>When hexadecimal string is returned.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        ///
        [Theory]
        [InlineData("SimpleExtensions.Core", "53696d706c65457874656e73696f6e732e436f7265")]
        public void WhenHexStringIsReturned(string value, string expectedValue) => Assert.Equal(value.ToHex(), expectedValue);

        /// <summary>When pascal case is returned.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Theory]
        [InlineData("camelcase", "Camelcase")]
        [InlineData("camelCase", "CamelCase")]
        [InlineData("CAMELCASE", "Camelcase")]
        public void WhenPascalCaseIsReturned(string value, string expectedValue) => Assert.Equal(value.ToPascalCase(), expectedValue);

        /// <summary>
        /// Whens the base64 string is returned from from string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        [Theory]
        [InlineData("SimpleExtensions.Core", "U2ltcGxlRXh0ZW5zaW9ucy5Db3Jl")]
        public void WhenBase64StringIsReturnedFromFromString(string value, string expectedValue) => Assert.Equal(value.ToBase64(), expectedValue);
        ///
        /// <summary>When string is returned from base 64 string.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        ///
        [Theory]
        [InlineData("U2ltcGxlRXh0ZW5zaW9ucy5Db3Jl", "SimpleExtensions.Core")]
        public void WhenStringIsReturnedFromFromBase64String(string value, string expectedValue) => Assert.Equal(value.FromBase64(), expectedValue);

        ///
        /// <summary>When string is returned from hexadecimal string.</summary>
        /// <param name="value">        The value.</param>
        /// <param name="expectedValue">The expected value.</param>
        ///
        [Theory]
        [InlineData("53696d706c65457874656e73696f6e732e436f7265", "SimpleExtensions.Core")]
        public void WhenStringIsReturnedFromHexString(string value, string expectedValue) => Assert.Equal(value.ToAscii(), expectedValue);
    }
}