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
    public class GeoZones
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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void CheckGeoZones()
        {
            List<IWebElement> cells = new List<IWebElement>();
            List<IWebElement> headerCells = new List<IWebElement>();
            List<string> countries = new List<string>();
            List<string> timeZones = new List<string>();
            Helpers helper = new Helpers();
            Int32 index;
            string locator;

            helper.LoginAT(driver);

            driver.Url = ("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");

            headerCells.AddRange(driver.FindElements(By.CssSelector("table.dataTable tr.header th")));
            
            index = helper.GetColumnIndex(headerCells, "Name") + 1;
            locator = "tr.row td:nth-of-type(" + index + ")";
            cells.AddRange(driver.FindElements(By.CssSelector(locator)));

            helper.GetInnerText(cells, countries, index);

            foreach (string country in countries) {
                driver.FindElement(By.LinkText(country)).Click();

                cells.Clear();
                headerCells.Clear();

                headerCells.AddRange(driver.FindElements(By.CssSelector("table.dataTable tr.header th")));

                index = helper.GetColumnIndex(headerCells, "Zone") + 1;
                locator = "table.dataTable td:nth-of-type(" + index + ") option[selected=selected]";
                cells.AddRange(driver.FindElements(By.CssSelector(locator)));

                helper.GetInnerText(cells, timeZones, index);

                helper.CheckOrder(timeZones);

                timeZones.Clear();

                driver.Url = ("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
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
