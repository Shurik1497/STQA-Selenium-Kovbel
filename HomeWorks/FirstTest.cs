using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumExample
{
    [TestFixture]
    public class FirstTest
    {
        //ChromeDriver chrome;
        private IWebDriver driver;
        private WebDriverWait wait;
        [SetUp]

        public void Start()
        {
            driver = new ChromeDriver();
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

        // разрушение объекта драйвера после окончание теста.
        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
