using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace PageObjects
{
    public class Application
    {
        private IWebDriver driver;

        internal MainPage mainPage;
        internal TestData data;
        internal ProductPage productPage;
        internal CartPage cart;

        public Application()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);

            mainPage = new MainPage(driver);
            productPage = new ProductPage(driver);
            cart = new CartPage(driver);
            data = new TestData();
        }

        public static void Select(IWebElement select, string option)
        {
            SelectElement dd = new SelectElement(select);
            dd.SelectByText(option);
        }

        public void Quit()
        {
            driver.Quit();
        }
    }
}