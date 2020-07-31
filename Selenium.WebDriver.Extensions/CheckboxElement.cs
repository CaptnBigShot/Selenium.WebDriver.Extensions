using System;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions
{
    public class CheckboxElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckboxElement"/> class.
        /// </summary>
        /// <param name="element">The element to be wrapped</param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
        /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a table element.</exception>
        public CheckboxElement(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Element cannot be null");
            }

            if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "input", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new UnexpectedTagNameException("input", element.TagName);
            }

            WrappedElement = element;
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Sets the checkbox state (regardless of it's current state).
        /// </summary>
        /// <param name="shouldBeSelected"></param>
        public void SetCheckbox(bool shouldBeSelected)
        {
            if ((shouldBeSelected && !WrappedElement.Selected) || (!shouldBeSelected && WrappedElement.Selected))
                WrappedElement.Click();
        }

        /// <summary>
        /// Gets the checkbox state.
        /// </summary>
        public bool IsChecked => WrappedElement.Selected;
    }
}
