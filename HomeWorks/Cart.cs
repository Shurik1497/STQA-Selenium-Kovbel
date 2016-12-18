using HomeWorks;
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
    public class Cart
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
        public void CheckCart()
        {
            driver.Url = ("http://localhost/litecart/en/");
            Helpers helper = new Helpers();
            string size = "Small";
            List<IWebElement> shortcuts = new List<IWebElement>();

            driver.Url = ("http://localhost/litecart/en/");


            for (int i = 1; i < 4; i++)
            {

                IWebElement quantity = driver.FindElement(By.XPath("//span[@class='quantity']"));

                driver.FindElement(By.XPath("//div[@id='box-latest-products']//li[" + i + "]")).Click();
                try
                {
                    string a = i.ToString();
                    helper.Select(driver, "//select[@name='options[Size]']", size);
                    helper.AddToCart(driver);
                    quantity = driver.FindElement(By.XPath("//span[@class='quantity']"));
                    wait.Until(ExpectedConditions.TextToBePresentInElement(quantity, i.ToString()));
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    helper.AddToCart(driver);
                    quantity = driver.FindElement(By.XPath("//span[@class='quantity']"));
                    wait.Until(ExpectedConditions.TextToBePresentInElement(quantity, i.ToString()));
                }

                driver.FindElement(By.XPath("//div[@id='logotype-wrapper']")).Click();
            }

            driver.FindElement(By.XPath("//div[@id='cart']")).Click();

            shortcuts.AddRange(driver.FindElements(By.XPath("//li[@class='shortcut']")));
            int countOfItms = shortcuts.Count;

            for (int i = 1; i <= countOfItms; i++)
            {
                if (i != countOfItms)
                {
                    driver.FindElement(By.XPath("//li[@class='shortcut'][1]")).Click();
                    driver.FindElement(By.XPath("//li[@class='item'][1]//button[@name='remove_cart_item']")).Click();
                }
                else
                {
                    driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();
                }
                IWebElement element = driver.FindElement(By.XPath("//table[contains(@class, 'dataTable')]"));
                wait.Until(ExpectedConditions.StalenessOf(element));
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