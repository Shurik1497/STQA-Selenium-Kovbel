using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;

namespace HomeWorks
{
    [TestFixture]
    public class ProductPage
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
        public void CheckProductPage()
        {
            string regularPriceExpectedClassMain = "regular-price";
            string campaignPriceExpectedClassMain = "campaign-price";
            string regularPriceExpectedClassProduct = "regular-price";
            string campaignPriceExpectedClassProduct = "campaign-price";

            driver.Url = ("http://localhost/litecart/en/");

            IWebElement webElement = driver.FindElement(By.CssSelector("div#box-campaigns li:nth-of-type(1) a.link"));

            ProductProperties propertiesMainPage = new ProductProperties(webElement);

            webElement.Click();

            wait.Until(ExpectedConditions.TitleContains(propertiesMainPage.productName));

            webElement = driver.FindElement(By.CssSelector("div#box-product"));

            ProductProperties propertiesProductPage = new ProductProperties(webElement);

            //Check styles
            if (propertiesMainPage.regularPriceeClass != regularPriceExpectedClassMain)
                throw new System.Exception("Class of regular price on Main page isn't correct");
            if (propertiesMainPage.campaignPriceClass != campaignPriceExpectedClassMain)
                throw new System.Exception("Class of campaign price on Main page isn't correct");
            if (propertiesProductPage.regularPriceeClass != regularPriceExpectedClassProduct)
                throw new System.Exception("Class of regular price on Product page isn't correct");
            if (propertiesProductPage.campaignPriceClass != campaignPriceExpectedClassProduct)
                throw new System.Exception("Class of campaign price on Product page isn't correct");

            //Check prices
            if (propertiesMainPage.regularPricee != propertiesProductPage.regularPricee)
                throw new System.Exception("Regular prise on Product page is different from regular prise on Main page");
            if (propertiesMainPage.campaignPrice != propertiesProductPage.campaignPrice)
                throw new System.Exception("Campaign prise on Product page is different from campaign prise on Main page");

            //Check Titles
            if (propertiesMainPage.productName != propertiesProductPage.productName)
                throw new System.Exception("Title on Product page is different from title on Main page");
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
