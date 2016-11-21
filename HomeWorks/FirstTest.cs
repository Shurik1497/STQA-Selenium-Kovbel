using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumExample
{
    [TestFixture]
    public class FirstTest
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
        public void EasyTest()
        {
            driver.Manage().Window.Maximize();
            driver.Url = ("http://Google.com");
            driver.FindElement(By.Name("q")).SendKeys("WebDriver");
            driver.FindElement(By.Name("btnG")).Click();
            wait.Until(ExpectedConditions.TitleIs("WebDriver - Google Search"));
        }
                
        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
