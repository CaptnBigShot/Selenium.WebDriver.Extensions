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
        /// <param name="webDriver"></param>
        /// <param name="elementId"></param>
        public KendoComboBoxElement(ISearchContext webDriver, string elementId)
        {
            WrappedElement = webDriver.FindElement(By.XPath($"//input[contains(@aria-owns, '{elementId}')]/parent::span/parent::span"));
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement { get; }

        /// <summary>
        /// Gets the Kendo combo box's input element.
        /// </summary>
        public IWebElement InputElement => WrappedElement.FindElement(By.XPath("./span/input[@type='text']"));

        /// <summary>
        /// Gets the Kendo combo box's select element.
        /// </summary>
        public IWebElement SelectElement => WrappedElement.FindElement(By.XPath("./span/span[@class='k-select']"));

        /// <summary>
        /// Gets the Kendo combo box's list box element. This only works if the list box is displayed/expanded.
        /// </summary>
        public IWebElement ListBoxElement => WrappedElement.FindElement(By.XPath($"//ul[@id='{AriaOwns}'][@aria-hidden='false']"));

        /// <summary>
        /// Gets the Kendo combo box's list box items.
        /// </summary>
        public IList<IWebElement> ListBoxItemElements => ListBoxElement.FindElements(By.XPath("./li"));

        /// <summary>
        /// Gets the input text box's value.
        /// </summary>
        public string GetInputText => InputElement.GetProperty("value");

        /// <summary>
        /// Sets the value of the Kendo combo box's input text box.
        /// </summary>
        /// <param name="text"></param>
        public void SetInputText(string text) => InputElement.SendKeys(text, true, true);

        /// <summary>
        /// Clicks the Kendo combo box's select element to view the list box.
        /// </summary>
        public void ExpandListBox()
        {
            // WrappedElement.ScrollToElement();
            SelectElement.Click();
            WaitForListBoxToBeDisplayed();
        }

        /// <summary>
        /// Clicks the Kendo combo box's select element to hide the list box.
        /// </summary>
        public void CollapseListBox()
        {
            SelectElement.Click();
            WaitForListBoxToNotBeDisplayed();
        }

        /// <summary>
        /// Finds a list box item by text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public IWebElement FindListBoxItemByText(string text)
        {
            var byText = text == "" ? "./li[not(text())]" : $"./li[text()='{text}']";

            return ListBoxElement.FindElement(By.XPath(byText));
        }

        /// <summary>
        /// Clicks a list box item by its text.
        /// </summary>
        /// <param name="text"></param>
        public void ClickListBoxItemByText(string text)
        {
            FindListBoxItemByText(text).Click();
            WaitForListBoxToNotBeDisplayed();
        }

        /// <summary>
        /// Selects a list item from the Kendo combo box using the text of that list item.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skipNull"></param>
        /// <param name="shouldRetry"></param>
        /// <param name="skipIfAlreadySet"></param>
        public void SelectListItemByText(string text, bool skipNull = true, bool shouldRetry = true, bool skipIfAlreadySet = true)
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
                    ClickListBoxItemByText(text);

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
        public IList<IWebElement> GetListBoxItems()
        {
            ExpandListBox();
            var listBoxItems = ListBoxItemElements;
            CollapseListBox();

            return listBoxItems;
        }

        private string AriaOwns => InputElement.GetAttribute("aria-owns");

        private void WaitForListBoxToBeDisplayed()
        {
            Thread.Sleep(1000);
        }

        private void WaitForListBoxToNotBeDisplayed()
        {
            Thread.Sleep(1000);
        }
    }
}
