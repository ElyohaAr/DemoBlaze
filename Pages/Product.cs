using DemoBlaze;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class ProductPage
    {
        private readonly IWebDriver Driver;

        private IWebElement _priceContainer => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("price-container")));
        private IWebElement _addToCart => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'addToCart')]")));
        public IWebElement _name() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.ClassName("name")));
        private IAlert _popOut => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

        WebDriverWait wait;
        public ProductPage(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
     
        }


        public string AlertText()
        {
            Driver.SwitchTo().Alert();
            _popOut.Accept();//click on submit button
            return _popOut.Text;
        }
        public void AddToCart()
        {
            _addToCart.Click();
            
           _popOut.Text.Contains("Product added");

            _popOut.Accept();
          
        }

        public int Price()
        {

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("price-container")));
            return int.Parse(Regex.Match(_priceContainer.Text, @"\d+").Value);
        }

        public string Name()
        {
            return _name().Text;
        }

    }
}
