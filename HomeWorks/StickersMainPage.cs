using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace SeleniumExample
{
    [TestFixture]
    public class StickersMainPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        [SetUp]

        public void Start()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void StickersOnMainPage()
        {
            List<IWebElement> products = new List<IWebElement>();
            List<IWebElement> stickers = new List<IWebElement>();
            List<int> itemsWithExceptions = new List<int>();  //can be used for detail description in log (not impemented)
            int i = 0;

            driver.Url = ("http://localhost/litecart/en/");

            products.AddRange(driver.FindElements(By.CssSelector("li.product.hover-light")));

            foreach (IWebElement product in products) {
                stickers.AddRange(product.FindElements(By.CssSelector(".sticker")));
                
                if (stickers.Count != 1) {
                    itemsWithExceptions.Add(i);
                }

                i++;
                stickers.Clear();
            }

            if (itemsWithExceptions.Count != 0) {
                throw new System.Exception("There is at least one item which has count of stickers != 1 ");
            }            
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
