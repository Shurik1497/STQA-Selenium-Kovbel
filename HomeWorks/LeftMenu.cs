using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;



namespace HomeWorks
{
    [TestFixture]
    public class LeftMenu
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
        public void CheckLeftMenu()
        {
            driver.Manage().Window.Maximize();
            driver.Url = ("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));            
            
            List<IWebElement> lmParentItemsObject = new List<IWebElement>();
            List<IWebElement> lmChildItemsObject = new List<IWebElement>();
            List<String> lmParentItemsString = new List<String>();
            List<String> lmChildItemsString = new List<String>();

            lmParentItemsObject.AddRange(driver.FindElements(By.CssSelector("#box-apps-menu-wrapper li#app-")));

            foreach (IWebElement objectIteml in lmParentItemsObject)
            {
                lmParentItemsString.Add(objectIteml.Text);
            }

            foreach (String stringItem in lmParentItemsString)
            {

                driver.FindElement(By.LinkText(stringItem)).Click();

                driver.FindElement(By.CssSelector("Head Title"));

                lmChildItemsObject.AddRange(driver.FindElements(By.CssSelector("#box-apps-menu-wrapper li#app- li")));
                
                foreach (IWebElement objectIteml in lmChildItemsObject)
                {
                    lmChildItemsString.Add(objectIteml.Text);
                }
                
                int count = lmChildItemsObject.Count;
                if (count != 0)
                {
                    for (int i = 1; i < count; i++)
                    {
                        driver.FindElement(By.LinkText(lmChildItemsString[i])).Click();
                        driver.FindElement(By.CssSelector("Head Title"));
                    }

                }

                lmChildItemsObject.RemoveRange(0, lmChildItemsObject.Count);
                lmChildItemsString.RemoveRange(0, lmChildItemsString.Count);

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
