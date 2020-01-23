using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Protractor;
using WebDriverManager.DriverConfigs.Impl;

namespace Sample_Protractor
{
    [TestClass]
    public class TestProtractor
    {
        ChromeDriver driver;
        NgWebDriver ngdriver;
        private string URL="https://s3.ap-south-1.amazonaws.com/shiva1792.tk/DiscountCalculator/ShoppingDiscount.html";
        WebDriverWait Waittime;

        [TestInitialize]
        public void StartIntiliaze()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            Waittime = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ngdriver = new NgWebDriver(driver);

        }

        [TestMethod]
        public void TestMethod1()
        {

            ngdriver.Url = URL;
            ngdriver.WaitForAngular();

            //Find product price text box using the ng-model 
            NgWebElement ProductPrice = ngdriver.FindElement(NgBy.Model("productPrice"));
            ProductPrice.SendKeys("799");
            Thread.Sleep(2000);


            //Find discount text box using the ng-model 
            NgWebElement DiscountOnProduct = ngdriver.FindElement(NgBy.Model("discountPercent"));
            DiscountOnProduct.SendKeys("10");
            Thread.Sleep(2000);


            //Find button using selenium locator XPath
            NgWebElement BtnPriceAfterDiscount = ngdriver.FindElement(By.XPath("//*[@id='f1']/fieldset[2]/input[1]"));
            BtnPriceAfterDiscount.Click();
            Thread.Sleep(2000);


            //Find discounted product text box using the ng-model 
            NgWebElement afterDiscountValue = ngdriver.FindElement(NgBy.Model("afterDiscount"));
            string value = afterDiscountValue.GetAttribute("value");
            Thread.Sleep(2000);

            //Assert for corect value
            Assert.AreEqual<string>("719.1", value);

            //Use if condition for custom checks
            if (value == "719.1")
            {
                //Do Nothing.
            }
            else
            {
                Assert.Fail();
            }



        }


        [TestCleanup]
        public void Stop()
        {
            driver.Close();
            driver.Quit();

        }
    }
}
