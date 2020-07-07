using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace ConsoleApp1.PageObjectModels
{
    class CartPage
    {

        private readonly IWebDriver Driver;
        public CartPage(IWebDriver driver)
        {
            Driver = driver;
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
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 50));
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
            Thread.Sleep(1000);
            Year.Click();
            Year.SendKeys(year);
        }

        public void ConfirmOrder()
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"orderModal\"]/div/div/div[3]/button[2]")));
            Driver.FindElement(By.XPath("//*[@id=\"orderModal\"]/div/div/div[3]/button[2]")).Click();
            Thread.Sleep(1000);
        }
        public void AcceptOrderPopOut()
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'sa-confirm-button-container')]")));
            // accept popout after placing order
            Driver.FindElement(By.XPath("//*[contains(@class,'sa-confirm-button-container')]")).Click();
        }
    }
}
