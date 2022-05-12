using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Telerik.KendoUi
{
    public class KendoComboBoxElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KendoComboBoxElement"/> class.
        /// </summary>
        /// <param name="element"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="IWebElement"/> object is <see langword="null"/></exception>
        /// <exception cref="UnexpectedTagNameException">Thrown when the element wrapped is not a span element.</exception>
        public KendoComboBoxElement(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "Element cannot be null");
            }

            if (string.IsNullOrEmpty(element.TagName) || string.Compare(element.TagName, "span", StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new UnexpectedTagNameException("span", element.TagName);
            }

            WrappedElement = element;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KendoComboBoxElement"/> class using the Kendo element's ID
        /// (i.e. the input box aria-owns value, the select element aria-controls value, the list box id value).
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="kendoId"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="ISearchContext"/> object is <see langword="null"/></exception>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="string"/> object is <see langword="null"/></exception>
        public KendoComboBoxElement(ISearchContext searchContext, string kendoId)
        {
            if (searchContext == null)
            {
                throw new ArgumentNullException(nameof(searchContext), "Search context cannot be null");
            }

            if (kendoId == null)
            {
                throw new ArgumentNullException(nameof(kendoId), "kendoId cannot be null");
            }

            var inputElement = searchContext.FindElement(By.XPath($".//input[contains(@aria-owns, '{kendoId}')]"));
            var parentElement = inputElement.FindElement(By.XPath("./ancestor::span[contains(@class, 'k-combobox')]"));

            WrappedElement = parentElement;
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Gets the Kendo combo box's input element.
        /// </summary>
        public IWebElement InputElement => 
            WrappedElement.FindElement(By.XPath(".//input[@type='text']"));

        /// <summary>
        /// Gets the input text box's value.
        /// </summary>
        public string GetInputText =>
            InputElement.GetDomProperty("value");

        /// <summary>
        /// Sets the value of the Kendo combo box's input text box.
        /// </summary>
        /// <param name="text"></param>
        public void SetInputText(string text) =>
            InputElement.SendKeys(text, true, true);

        /// <summary>
        /// Gets the Kendo combo box's select element.
        /// </summary>
        public IWebElement SelectElement => 
            WrappedElement.FindElement(By.XPath(".//span[contains(@class, 'k-icon k-i-arrow')]"));

        /// <summary>
        /// Gets the Kendo List Box.
        /// </summary>
        public KendoListBoxElement KendoListBoxElement =>
            new KendoListBoxElement(WrappedElement, KendoId);

        /// <summary>
        /// Selects a list item from the Kendo combo box using the text of that list item.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skipNull"></param>
        /// <param name="shouldRetry"></param>
        /// <param name="skipIfAlreadySet"></param>
        public void SelectByText(string text, bool skipNull = true, bool shouldRetry = true, bool skipIfAlreadySet = true)
        {
            if (skipNull && text == null) return;
            if (skipIfAlreadySet && GetInputText == text) return;

            var retries = 0;
            const int maxRetries = 5;
            while (true)
            {
                try
                {
                    ExpandListBox();
                    KendoListBoxElement.ClickListBoxItemByText(text);
                    break;
                }
                catch when (shouldRetry && ++retries < maxRetries)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// Expands the list box, gets the list box items, then collapses the list box.
        /// </summary>
        /// <returns></returns>
        public IList<IWebElement> Options()
        {
            ExpandListBox();
            var listBoxItems = KendoListBoxElement.ListBoxItemElements;
            CollapseListBox();

            return listBoxItems;
        }

        /// <summary>
        /// Clicks the Kendo combo box's select element to view the list box.
        /// </summary>
        public void ExpandListBox()
        {
            SelectElement.Click();
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Clicks the Kendo combo box's select element to hide the list box.
        /// </summary>
        public void CollapseListBox()
        {
            SelectElement.Click();
            Thread.Sleep(1000);
        }

        private string KendoId => 
            InputElement.GetAttribute("aria-owns");
    }
}
