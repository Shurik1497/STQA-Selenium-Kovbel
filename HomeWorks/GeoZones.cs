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


            driver.Url = ("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            headerCells.AddRange(driver.FindElements(By.CssSelector("table.dataTable tr.header th")));
            cells.AddRange(driver.FindElements(By.CssSelector("tr.row td")));

            helper.GridsParser(cells, headerCells, countries, "Name");



            foreach (string country in countries) {
                driver.FindElement(By.LinkText(country)).Click();

                cells.Clear();
                headerCells.Clear();

                headerCells.AddRange(driver.FindElements(By.CssSelector("table.dataTable tr.header th")));
                cells.AddRange(driver.FindElements(By.CssSelector("table.dataTable tr td")));

                //helper.GridsParser(cells, headerCells, timeZones, "Zone"); - не осилил я хелпер который будет универсальным для всех тестов, потому - костыль (...

                string index = "";

                foreach (IWebElement element in headerCells)
                {
                    if (element.GetAttribute("innerText") == "Zone")
                    {
                        index = element.GetAttribute("cellIndex");
                    }
                }


                foreach (IWebElement cell in cells)
                {
                    if (cell.GetAttribute("cellIndex") == index)
                    {
                        timeZones.Add(cell.FindElement(By.CssSelector("[selected=selected]")).GetAttribute("innerText"));
                        string zones = timeZones[0];
                    }
                }

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
