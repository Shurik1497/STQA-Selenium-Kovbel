using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorks
{
    class Helpers
    {
        public void LoginAT(IWebDriver driver) {
            driver.Manage().Window.Maximize();
            driver.Url = ("http://localhost/litecart/admin");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
        }

        public void Login(IWebDriver driver, string email, string password)
        {
            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[type=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[type=submit]")).Click();
        }

        public void Logout(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        public void GetInnerText (List<IWebElement> gridCells, List<string> outputCollection, Int32 columnIndex) {
            foreach (IWebElement cell in gridCells)
            {
                outputCollection.Add(cell.GetAttribute("innerText"));         
            }
        }


        public Int32 GetColumnIndex(List<IWebElement> headerCells, string columnName) {
            Int32 i = 0;
            foreach (IWebElement element in headerCells)
            {
                if (element.GetAttribute("innerText") == columnName)
                {
                    i = i + Int32.Parse(element.GetAttribute("cellIndex"));
                }
            }
            return i;
        }

        public void CheckOrder(List<string> list) {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (String.Compare(list[i], list[i + 1]) > 0)
                {
                    throw new System.Exception("Order isn't correct");
                }
            }
        }

        public void CheckCheckbox(IWebElement checkBox, bool value) {
            if (value == false && checkBox.GetAttribute("checked") == "true")
                checkBox.Click();
            if (value == true && checkBox.GetAttribute("checked") != "true")
                checkBox.Click();
        }

        public void CheckCheckbox(IWebDriver driver, string xpath, bool value)
        {
            IWebElement checkBox = driver.FindElement(By.XPath(xpath));
            CheckCheckbox(checkBox, value);
        }



        public void Select(IWebDriver driver, string xpath, string option) {
            IWebElement select = driver.FindElement(By.XPath(xpath));
            SelectElement dd = new SelectElement(select);
            dd.SelectByText(option);
        }

        public void ClearInputs(IWebDriver driver, string xPath) {
            List<IWebElement> list = new List<IWebElement>();
            list.AddRange(driver.FindElements(By.XPath(xPath)));
            foreach (IWebElement element in list) {
                if (element.GetAttribute("type") == "checkbox")
                {
                    CheckCheckbox(element,false);
                }
            }
        }

        public void AddToCart(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//button[@name='add_cart_product']")).Click();
        }

    }
}
