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
    internal class CartPage : Page
    {
        public CartPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@id='cart']")]
        private IWebElement cart;

        internal CartPage Open()
        {
            cart.Click();
            return this;
        }

        [FindsBy(How = How.XPath, Using = "//li[@class='shortcut']")]
        private IList<IWebElement> shortcuts;
        [FindsBy(How = How.XPath, Using = "//li[@class='item'][1]//button[@name='remove_cart_item']")]
        private IWebElement deleteButton;

        public int CountOfItms()
        {
            int countOfItms;
            return countOfItms = shortcuts.Count;
        }

        public void RemoveItem()
        {
            try
            {
                shortcuts[0].Click();
                deleteButton.Click();
            }
            catch (System.Reflection.TargetInvocationException)
            {
                driver.FindElement(By.XPath("//button[@name='remove_cart_item']")).Click();
            }
            IWebElement element = driver.FindElement(By.XPath("//table[contains(@class, 'dataTable')]"));
            wait.Until(ExpectedConditions.StalenessOf(element));

        }
    }
}