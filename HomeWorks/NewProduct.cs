using HomeWorks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace SeleniumExample
{
    [TestFixture]
    public class NewProduct
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
        public void AddNewProduct()
        {
            Helpers helper = new Helpers();
            
            //General Section
            string name = "Dag";
            string code = "221b";
            List<string> category = new List<string> {  "Subcategory" }; 
            string defaultCategory = "Subcategory";
            string gender = "Male";
            string quantity = "10";
            string quantityUnit = "pcs";
            string deliveryStatusd = "3-5 days";
            string soldOutStatus = "Temporary sold out";
            string localPath = "\\img\\hongkongduck01.jpg";
            string fullPath = helper.GetAssemblyPath(localPath);

            string validFrom = "12/17/2016";
            string validTo = "12/17/2017";

            //Information Section
            string manufacturer = "ACME Corp.";   
            //string supplier = ""; //skipped because there are no values in current control
            string shortDescription = "shortDescription";
            string keywords = "keywords";
            string description = "description";
            string metaDescription = "metaDescription";
            string headTitle = "headTitle";
            
            //Price Section
            string purchasePricep = "25";
            string currencyCode = "Euros";
            //string taxClass; //skipped because there are no values in current control
            string priceUSD = "9.99";
            string priceEuro = "8.49";


            helper.LoginAT(driver);
            driver.FindElement(By.XPath("//div[@id='box-apps-menu-wrapper']//a[contains(., 'Catalog')]")).Click();
            driver.FindElement(By.XPath("//a[@class='button' and contains(.,'Add New Product')]")).Click();

            //Fill Section General
            string xPath = "//div[@id='tab-general']//input[@*]";
            helper.ClearInputs(driver, xPath);

            driver.FindElement(By.XPath("//label[contains(text(),'Enabled')]")).Click();
            driver.FindElement(By.XPath("//input[@name='name[en]']")).SendKeys(name);
            driver.FindElement(By.XPath("//input[@name='code']")).SendKeys(code);
            foreach (string element in category)
            {
                helper.CheckCheckbox(driver, "//input[@type='checkbox' and @data-name='" + element + "']", true);
            }
            helper.Select(driver, "//select[@name='default_category_id']", defaultCategory);
            helper.CheckCheckbox(driver, "//td[contains(text(),'" + gender + "')]/..//input[@name='product_groups[]']", true);
            IWebElement quantityField = driver.FindElement(By.XPath("//input[@name='quantity']"));
                quantityField.Clear();
                quantityField.SendKeys(quantity);
            helper.Select(driver, "//select[@name='quantity_unit_id']", quantityUnit);
            helper.Select(driver, "//select[@name='delivery_status_id']", deliveryStatusd);
            helper.Select(driver, "//select[@name='sold_out_status_id']", soldOutStatus);
            driver.FindElement(By.XPath("//input[@type='file' and @name='new_images[]']")).SendKeys(fullPath);
            driver.FindElement(By.XPath("//input[@type='date' and @name='date_valid_from']")).SendKeys(validFrom);
            driver.FindElement(By.XPath("//input[@type='date' and @name='date_valid_to']")).SendKeys(validTo);

            //Change Tab to Imformation one
            driver.FindElement(By.XPath("//div[@class='tabs']//a[contains(text(),'Information')]")).Click();

            //Fill Section Imformation
            helper.Select(driver, "//select[@name='manufacturer_id']", manufacturer);
            //helper.Select(driver, "//select[@name='supplier_id']", supplier);
            driver.FindElement(By.XPath("//input[@name='keywords']")).SendKeys(keywords);
            driver.FindElement(By.XPath("//input[@name='short_description[en]']")).SendKeys(shortDescription);
            driver.FindElement(By.XPath("//div[@class='trumbowyg-editor']")).SendKeys(description);
            driver.FindElement(By.XPath("//input[@name='head_title[en]']")).SendKeys(headTitle);
            driver.FindElement(By.XPath("//input[@name='meta_description[en]']")).SendKeys(metaDescription);

            //Change Tab to Prices one
            driver.FindElement(By.XPath("//div[@class='tabs']//a[contains(text(),'Prices')]")).Click();

            //Fill Section Prices
            IWebElement priceField = driver.FindElement(By.XPath("//input[@name='purchase_price']"));
                priceField.Clear();
                priceField.SendKeys(purchasePricep);
            helper.Select(driver, "//select[@name='purchase_price_currency_code']", currencyCode);
            //helper.Select(driver, "//select[@name='tax_class_id']", taxClass);
            driver.FindElement(By.XPath("//input[@name='prices[USD]']")).SendKeys(priceUSD);
            driver.FindElement(By.XPath("//input[@name='prices[EUR]']")).SendKeys(priceEuro);

            //Save
            driver.FindElement(By.XPath("//button[@name='save']")).Click();

            //Check Catalog - I didn't check category of product on Catalog page.
            List<IWebElement> poducts = new List<IWebElement>();
            poducts.AddRange(driver.FindElements(By.XPath("//table[@class='dataTable']//a[contains(., '" + name + "')]")));
            if (poducts.Count == 0) {
                throw new System.Exception("New product is missed in the catalog");
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