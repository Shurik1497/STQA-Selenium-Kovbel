using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageObjects
{
    class ProductPage : Page
    {
        public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//select[@name='options[Size]']")]
        private IList<IWebElement> size;
        [FindsBy(How = How.XPath, Using = "//button[@name='add_cart_product']")]
        private IWebElement addButton;
        [FindsBy(How = How.XPath, Using = "//span[@class='quantity']")]
        private IWebElement quantity;

        public void AddToCart()
        {
            int currentQuantity = Int32.Parse(quantity.Text);
            addButton.Click();
            currentQuantity++;
            wait.Until(ExpectedConditions.TextToBePresentInElement(quantity, currentQuantity.ToString()));
        }

        public ProductPage SetSize(string sizeOption) {
            try
            {
                Application.Select(size[0], sizeOption);
            }
            catch (System.Reflection.TargetInvocationException) {
                Console.WriteLine("there are no size control on the page");
            }
            return this;
        }


    }
}
