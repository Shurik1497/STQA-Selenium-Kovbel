using HomeWorks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SeleniumExample
{
    [TestFixture]
    public class BrowserLogs
    {
        private ChromeDriver driver;
        private WebDriverWait wait;
        [SetUp]

        public void Start()
        {
            
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private void Driver_FindingElement(object sender, FindElementEventArgs e)
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetBrowserLogs()
        {
            Helpers helper = new Helpers();
            List<IWebElement> products = new List<IWebElement>();
            List<string> productsLinks = new List<string>();

            helper.LoginAT(driver);

            driver.FindElement(By.XPath("//div[@id='box-apps-menu-wrapper']//a[contains(., 'Catalog')]")).Click();
            driver.FindElement(By.XPath("(//input[contains(@name,'categories')]/../..//a)[1]")).Click();

            products.AddRange(driver.FindElements(By.XPath("(//input[contains(@name,'product')]/../..)//a[@title='Edit']")));

            productsLinks = helper.GetAttribut(products, "href");


            foreach (string product in productsLinks) {
                driver.FindElement(By.XPath("//a[@title='Edit' and @href='" + product + "']")).Click();
                driver.Navigate().Back();

            }

            //Other way:

            //foreach (string product in productsLinks)
            //{
            //    driver.Url = (product);
            //}

            ReadOnlyCollection<LogEntry> logs;
            logs = driver.Manage().Logs.GetLog("browser");

            foreach (LogEntry l in logs)
            {
                Console.WriteLine(l);
            }

            if (logs.Count > 0)
                throw new System.Exception("Logs of browser contain at least one message. See 'Output' for getting details");

        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
