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
    public class CheckStickers
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        [SetUp]

        public void Start()
        {
            driver = new ChromeDriver();
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        [Test]
        public void CheckStickersOnMainPage()
        {
            List<IWebElement> products = new List<IWebElement>();
            List<IWebElement> stickers = new List<IWebElement>();

            driver.Manage().Window.Maximize();
            driver.Url = ("http://localhost/litecart/en/");

            products.AddRange(driver.FindElements(By.CssSelector("li.product.hover-light")));

            foreach (IWebElement product in products) {
                stickers.AddRange(product.FindElements(By.CssSelector(".sticker.sale")));
                stickers.AddRange(product.FindElements(By.CssSelector(".sticker.new")));

                int i = 0;
                
                if (stickers.Count != 1) {
                    // Exception;
                    throw new System.Exception(i + " item has count of stockers = " + stickers.Count + stickers[0].Text);
                }
                i++;
                stickers.Clear();
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
