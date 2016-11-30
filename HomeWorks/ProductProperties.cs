using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorks
{
    class ProductProperties
    {
        public string productName { get; }
        public string regularPricee { get; }
        public string regularPriceeClass { get; }
        public string campaignPrice { get; }
        public string campaignPriceClass { get; }

        public ProductProperties (IWebElement element)
        {
            Dictionary<string, string> productProperties = new Dictionary<string, string>();

            try
            {
                productName = element.FindElement(By.CssSelector("h1.title")).GetAttribute("innerText");
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                productName = element.FindElement(By.CssSelector("div.name")).GetAttribute("innerText");
            }

            regularPricee = element.FindElement(By.CssSelector("div.price-wrapper s")).Text;
            regularPriceeClass = element.FindElement(By.CssSelector("div.price-wrapper s")).GetAttribute("class");

            campaignPrice = element.FindElement(By.CssSelector("div.price-wrapper strong")).Text;
            campaignPriceClass = element.FindElement(By.CssSelector("div.price-wrapper strong")).GetAttribute("class");
        }

    }
}
