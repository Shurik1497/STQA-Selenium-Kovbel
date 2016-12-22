using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace PageObjects
{
    internal class MainPage : Page
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal MainPage Open()
        {
            driver.Url = "http://localhost/litecart/en/";
            return this;
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='box-latest-products']//li")]
        internal IList<IWebElement> products;

        public void SelectProduct()
        {
            Random random = new Random();
            products[random.Next(1, 5)].Click();
        }
    }
}
