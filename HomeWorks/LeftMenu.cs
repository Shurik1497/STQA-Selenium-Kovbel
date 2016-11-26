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
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            //driver = new InternetExplorerDriver();
            //driver = new FirefoxDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            
        }

        [Test]
        public void ClickLeftMenuItems()
        {
            driver.Url = ("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));            
            
            List<IWebElement> ParentItemsObject = new List<IWebElement>();
            List<IWebElement> ChildItemsObject = new List<IWebElement>();
            List<String> ParentItemsString = new List<String>();
            List<String> ChildItemsString = new List<String>();

            ParentItemsObject.AddRange(driver.FindElements(By.CssSelector("#box-apps-menu-wrapper li#app-")));

            foreach (IWebElement objectIteml in ParentItemsObject)
            {
                ParentItemsString.Add(objectIteml.Text);
            }

            foreach (String stringItem in ParentItemsString)
            {
                driver.FindElement(By.LinkText(stringItem)).Click();
                wait.Until(ExpectedConditions.ElementExists(By.CssSelector("Head Title")));

                ChildItemsObject.AddRange(driver.FindElements(By.CssSelector("#box-apps-menu-wrapper li#app- li")));
                
                foreach (IWebElement objectIteml in ChildItemsObject)
                {
                    ChildItemsString.Add(objectIteml.Text);
                }
                
                int count = ChildItemsObject.Count;
                if (count != 0)
                {
                    for (int i = 1; i < count; i++)
                    {
                        driver.FindElement(By.LinkText(ChildItemsString[i])).Click();
                        wait.Until(ExpectedConditions.ElementExists(By.CssSelector("Head Title")));
                    }

                }

                ChildItemsObject.Clear();
                ChildItemsString.Clear();
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
