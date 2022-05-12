using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;

namespace Selenium.WebDriver.Extensions.Telerik.KendoUi
{
    public class KendoMultiSelectElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KendoMultiSelectElement"/> class.
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="by"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="ISearchContext"/> object is <see langword="null"/></exception>
        public KendoMultiSelectElement(ISearchContext searchContext, By by)
        {
            if (searchContext == null)
            {
                throw new ArgumentNullException(nameof(searchContext), "Search context cannot be null");
            }

            if (by == null)
            {
                throw new ArgumentNullException(nameof(by), "By cannot be null");
            }

            SearchContext = searchContext;
            By = by;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KendoMultiSelectElement"/> class.
        /// </summary>
        /// <param name="searchContext"></param>
        /// <param name="kendoId"></param>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="ISearchContext"/> object is <see langword="null"/></exception>
        public KendoMultiSelectElement(ISearchContext searchContext, string kendoId)
        {
            if (searchContext == null)
            {
                throw new ArgumentNullException(nameof(searchContext), "Search context cannot be null");
            }

            if (kendoId == null)
            {
                throw new ArgumentNullException(nameof(kendoId), "kendoId cannot be null");
            }

            SearchContext = searchContext;
            By = By.XPath($".//*[contains(@id, '{kendoId}')]/ancestor::*[contains(@class, 'k-multiselect')]");
        }

        /// <summary>
        /// Gets the wrapped <see cref="IWebElement"/> instance.
        /// </summary>
        public IWebElement WrappedElement => 
            SearchContext.FindElement(By);

        /// <summary>
        /// Gets the input element.
        /// </summary>
        public IWebElement InputElement =>
            WrappedElement.FindElement(By.XPath(".//input"));

        /// <summary>
        /// Gets the Kendo Tag List.
        /// </summary>
        public KendoTagListElement KendoTagListElement => 
            new KendoTagListElement(TagListElement);

        /// <summary>
        /// Gets the Kendo List Box.
        /// </summary>
        public KendoListBoxElement KendoListBoxElement => 
            new KendoListBoxElement(WrappedElement, ListBoxId);

        /// <summary>
        /// Gets the selected Kendo Tag List Items.
        /// </summary>
        public IList<KendoTagListItemElement> SelectedOptions =>
            KendoTagListElement.KendoTagListItemElements;

        /// <summary>
        /// Gets the text of all selected tag list options.
        /// </summary>
        public IEnumerable<string> TextOfSelectedOptions =>
            KendoTagListElement.KendoTagListItemElements
                .Select(kendoTagListItemElement => kendoTagListItemElement.Text);

        /// <summary>
        /// Deselects a tag list item using the text of that list item.
        /// </summary>
        /// <param name="text"></param>
        public void DeselectByText(string text) => 
            KendoTagListElement.DeselectTagListItemByText(text);

        /// <summary>
        /// Deselects all tag list items.
        /// </summary>
        public void DeselectAllOptions() =>
            KendoTagListElement.DeselectAllTagListItems();

        /// <summary>
        /// Selects a list item using the text of that list item.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="skipNull"></param>
        /// <param name="shouldRetry"></param>
        public void SelectByText(string text, bool skipNull = true, bool shouldRetry = true)
        {
            if (skipNull && text == null) return;

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
        /// Clicks the Kendo input element to view the list box.
        /// </summary>
        public void ExpandListBox()
        {
            InputElement.Click();
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Clicks outside Kendo input element to hide the list box.
        /// </summary>
        public void CollapseListBox()
        {
            WrappedElement.FindElement(By.XPath("./parent::*")).Click();
            Thread.Sleep(1000);
        }

        private ISearchContext SearchContext { get; }

        private By By { get; }

        private string TagListId => 
            TagListElement.GetAttribute("id");

        private IWebElement TagListElement =>
            WrappedElement.FindElement(By.XPath(".//ul"));

        private string ListBoxId => 
            TagListId.Replace("taglist", "listbox");
    }
}