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
    public class SortCounries
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
        public void SortCountries()
        {
            List<IWebElement> cells = new List<IWebElement>();
            List<string> countries = new List<string>();
            List<IWebElement> headerCells = new List<IWebElement>();
            Helpers helper = new Helpers();
            

            driver.Url = ("http://localhost/litecart/admin/?app=countries&doc=countries");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            headerCells.AddRange(driver.FindElements(By.CssSelector("table.dataTable tr.header th")));

            cells.AddRange(driver.FindElements(By.CssSelector("tr.row td")));

            helper.GridsParser(cells, headerCells, countries, "Name");
            helper.CheckOrder(countries);
            
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
