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
    public class hWindows
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
        public void WorkWithWindows()
        {
            Helpers helper = new Helpers();
            List<IWebElement> blank = new List<IWebElement>();
            List<string> blankString = new List<string>();

            helper.LoginAT(driver);
            driver.Url = ("http://localhost/litecart/admin/?app=countries&doc=countries");

            driver.FindElement(By.XPath("//tr[@class='row'][1]//a[@title='Edit']")).Click();

            blank.AddRange(driver.FindElements(By.XPath("//form[@method='post']//a[@target='_blank']")));
            foreach (IWebElement element in blank) {
                blankString.Add(element.GetAttribute("href"));
            }

            string mainWindow = driver.CurrentWindowHandle;
            string newWindow = "";
            List<string> windows = new List<string>();

            foreach (string element in blankString) {
                driver.FindElement(By.XPath("//a[@href='" + element + "']")).Click();

                newWindow = wait.Until(helper.AnyWindowOtherThan(mainWindow));

                driver.SwitchTo().Window(newWindow);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
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