using System;
using System.Globalization;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions
{
    /// <summary>
    /// The exception thrown when using an extension class on a tag that
    /// does not support the HTML select element's selection semantics.
    /// </summary>
    [Serializable]
    public class UnexpectedTagNameException : WebDriverException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedTagNameException"/> class.
        /// </summary>
        public UnexpectedTagNameException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnexpectedTagNameException"/> class with
        /// the expected tag name and the actual tag name.
        /// </summary>
        /// <param name="expected">The tag name that was expected.</param>
        /// <param name="actual">The actual tag name of the element.</param>
        public UnexpectedTagNameException(string expected, string actual)
            : base(string.Format(CultureInfo.InvariantCulture, "Element tag name should have been '{0}' but was '{1}'", expected, actual))
        {
        }
    }
}
