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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
        }

        [Test]
        public void SortCountries()
        {
            List<IWebElement> cells = new List<IWebElement>();
            List<string> countries = new List<string>();
            
            driver.Url = ("http://localhost/litecart/admin/?app=countries&doc=countries");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            
            cells.AddRange(driver.FindElements(By.CssSelector("tr.row td")));

            foreach (IWebElement cell in cells) {
                int index = Int32.Parse(cell.GetAttribute("cellIndex"));
                if (index == 4) {
                    countries.Add(cell.FindElement(By.CssSelector("a")).GetAttribute("text"));
                    string counrry = cell.FindElement(By.CssSelector("a")).GetAttribute("text");
                }
            }

            for (int i = 0; i < countries.Count - 1; i++) {
                if (String.Compare(countries[i], countries[i+1]) > 0) {
                    throw new System.Exception("Order of countries isn't correct");
                }
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
