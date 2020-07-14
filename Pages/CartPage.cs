﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DemoBlaze
{
    class CartPage
    {

        private readonly IWebDriver Driver;
        WebDriverWait wait;
       
        public IWebElement TotalButton() => Driver.FindElement(By.XPath("//*[@id='page-wrapper']/div/div[2]/h2"));
        public IWebElement _name() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'tbodyid')]/tr/td[2]")));
        public IWebElement CartName() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#page-wrapper > div > div.col-lg-8 > h2")));
        public int Price() => int.Parse(Regex.Match(Driver.FindElement(By.XPath("//*[contains(@id,'tbodyid')]/tr/td[3]")).Text, @"\d+").Value);
        public CartPage(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
        }

        public void ShowCart()
        {
            Thread.Sleep(1500);
            Driver.FindElement(By.CssSelector("#navbarExample>ul>li:nth-child(4)>a")).Click();
            Driver.FindElement(By.XPath("//*[contains(@onclick,'showcart')]")).Click();
            Thread.Sleep(1500);
        }

        public void PlaceOrder()
        {

            _ = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'btn btn-success')]")));

            Driver.FindElement(By.XPath("//*[contains(@class,'btn btn-success')]")).Click();
        }
        public void BuyAll()
        {

            ShowCart();
            PlaceOrder();
            Name("User02");
            Country("Romania");
            City("Timisoara");
            Card("1111-2222-3333-4444-5555");
            Month("Feb");
            Year("2025");
            ConfirmOrder();
            AcceptOrderPopOut();


        }


        public void Name(String name)
        {
            IWebElement nameElement = Driver.FindElement(By.XPath("//*[@id=\"name\"]"));
            Thread.Sleep(1000);
            nameElement.Click();
            nameElement.SendKeys(name);
        }
        public void Country(String country)
        {
            IWebElement countryElement = Driver.FindElement(By.XPath("//*[contains(@id,'country')]"));
            Thread.Sleep(1000);
            countryElement.Click();
            countryElement.SendKeys(country);
        }

        public void City(String city)
        {

            IWebElement City = Driver.FindElement(By.XPath("//*[contains(@id,'city')]"));
            Thread.Sleep(1000);
            City.Click();
            City.SendKeys(city);
        }
        public void Card(String card)
        {

            IWebElement CreditCard = Driver.FindElement(By.XPath("//*[contains(@id,'card')]"));
            Thread.Sleep(1000);
            CreditCard.Click();
            CreditCard.SendKeys(card);
        }
        public void Month(String month)
        {

            IWebElement Month = Driver.FindElement(By.XPath("//*[contains(@id,'month')]"));
            Thread.Sleep(1000);
            Month.Click();
            Month.SendKeys(month);
        }
        public void Year(String year)
        {

            IWebElement Year = Driver.FindElement(By.XPath("//*[contains(@id,'year')]"));
            Year.SendKeys(year);
        }

        public void ConfirmOrder()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"orderModal\"]/div/div/div[3]/button[2]"))).Click();
          //  Driver.FindElement(By.XPath("//*[@id=\"orderModal\"]/div/div/div[3]/button[2]")).Click();
            
        }
        public void AcceptOrderPopOut()
        {
            //class="sa-line sa-tip animateSuccessTip";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("body > div.sweet-alert.showSweetAlert.visible > div.sa-icon.sa-success.animate > div.sa-fix")));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@class,'sa-confirm-button-container')]")));
            // accept popout after placing order
            Driver.FindElement(By.XPath("//*[contains(@class,'sa-confirm-button-container')]")).Click();
        }

        
        public int CountElements()
        {
            //  Thread.Sleep(2000);

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("success")));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[contains(@onclick,'delete')]")));
            return Driver.FindElements(By.XPath("//*[contains(@class,'col-lg-8')]")).Count;

        }

        public void DeleteElement()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@onclick,'delete')]")));

            Driver.FindElement(By.XPath("//*[contains(@onclick,'delete')]")).Click();
        }

        public string Name()
        {
            return _name().Text;
        }

        public void FillRequiredData()
        {

            ShowCart();
            PlaceOrder();
            Name("User02");
            Country("Romania");
            City("Timisoara");
            Card("1111-2222-3333-4444-5555");
            Month("Feb");
            Year("2025");
         
        }


        public void FinalizeOrder()
        {
            ConfirmOrder();
            AcceptOrderPopOut();
        }

    }
}