using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace HomeWorks
{
    [TestFixture]
    public class NewAccaunt
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
        public void CreateNewAccaunt()
        {
            Random randomInt = new Random();

            string country = "Afghanistan";
            bool subscribe = false;
            string password = "Password";
            string email = "AT" + randomInt.Next(0, 99999) + "@gmail.com";
            List<IWebElement> input = new List<IWebElement>();
            Helpers helper = new Helpers();

            driver.Url = ("http://localhost/litecart/en/");
            driver.FindElement(By.CssSelector("form[name=login_form]")).FindElement(By.LinkText("New customers click here")).Click();

            IWebElement parentForm = (driver.FindElement(By.CssSelector("form table")));
            input.AddRange(parentForm.FindElements(By.CssSelector("input")));

            IWebElement countryDD = parentForm.FindElement(By.CssSelector("select.select2-hidden-accessible"));
            SelectElement dd = new SelectElement(countryDD);
            dd.SelectByText(country);

            foreach (IWebElement element in input) {
                switch (element.GetAttribute("Type")) {
                    case "text":
                        string name = element.GetAttribute("name");
                        element.SendKeys("AT_" + name);
                        break;
                    case "email":
                        element.SendKeys(email);
                        break;
                    case "tel":
                        string pleaseHolder = element.GetAttribute("placeholder");
                        element.SendKeys(pleaseHolder + randomInt.Next(1000000, 9999999));
                        break;
                    case "checkbox":
                        helper.CheckCheckbox(element, subscribe);
                        break;
                    case "password":
                        element.SendKeys(password);
                        break;
                }
            }
 
            driver.FindElement(By.CssSelector("button[type=submit]")).Click();


            helper.Logout(driver);

            helper.Login(driver, email, password);

            helper.Logout(driver);
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
