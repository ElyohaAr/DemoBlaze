using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class ProductPage
    {
        private readonly IWebDriver Driver;
        public ProductPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void AddToCart()
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'addToCart')]")));
            element.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            IAlert popOut = Driver.SwitchTo().Alert();
            string popOutText = popOut.Text;
            Assert.AreEqual("Product added.", popOutText);
            popOut.Accept();

        }



    }
}
