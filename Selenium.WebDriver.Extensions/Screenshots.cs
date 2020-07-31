using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace Selenium.WebDriver.Extensions
{
    public static class Screenshots
    {
        public static List<Screenshot> GetEntirePageScreenshots(this IWebDriver webDriver)
        {
            var screenshotList = new List<Screenshot>();

            ((IJavaScriptExecutor)webDriver).ExecuteScript("window.scrollTo(0, 0)");

            // Get the total size of the page
            var totalWidth = (int)(long)((IJavaScriptExecutor)webDriver).ExecuteScript("return document.body.scrollWidth");
            var totalHeight = (int)(long)((IJavaScriptExecutor)webDriver).ExecuteScript("return document.body.scrollHeight");

            // Get the size of the viewport
            var viewportWidth = (int)(long)((IJavaScriptExecutor)webDriver).ExecuteScript("return document.documentElement.clientWidth");
            var viewportHeight = (int)(long)((IJavaScriptExecutor)webDriver).ExecuteScript("return document.documentElement.clientHeight");

            // We only care about taking multiple images together if it doesn't already fit
            if (totalWidth <= viewportWidth && totalHeight <= viewportHeight)
            {
                screenshotList.Add(webDriver.TakeScreenshot());
                return screenshotList;
            }

            // Split the screen in multiple Rectangles
            var rectangles = new List<Rectangle>();

            // Loop until the totalHeight is reached
            for (var y = 0; y < totalHeight; y += viewportHeight)
            {
                var newHeight = viewportHeight;

                // Fix if the height of the element is too big
                if (y + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - y;
                }

                // Loop until the totalWidth is reached
                for (var x = 0; x < totalWidth; x += viewportWidth)
                {
                    var newWidth = viewportWidth;

                    // Fix if the Width of the Element is too big
                    if (x + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - x;
                    }

                    // Create and add the Rectangle
                    var currRect = new Rectangle(x, y, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }

            // Build the Image
            // var stitchedImage = new Bitmap(totalWidth, totalHeight);

            // Get all Screenshots and stitch them together
            const int maxNumOfScreenshots = 11;
            var numOfScreenshots = 0;
            var previous = Rectangle.Empty;
            foreach (var rectangle in rectangles)
            {
                // Make sure it doesn't take an unreasonable number of screenshots
                numOfScreenshots++;
                if (numOfScreenshots >= maxNumOfScreenshots)
                    break;

                // Calculate the scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    var xDiff = rectangle.Right - previous.Right;
                    var yDiff = rectangle.Bottom - previous.Bottom;

                    // Scroll
                    ((IJavaScriptExecutor)webDriver).ExecuteScript($"window.scrollBy({xDiff}, {yDiff})");
                }

                // Take Screenshot
                screenshotList.Add(webDriver.TakeScreenshot());

                // Set the Previous Rectangle
                previous = rectangle;
            }

            return screenshotList;
        }
    }
}
