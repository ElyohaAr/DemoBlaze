
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace DemoBlaze
{
    class CartPage
    {

        private readonly IWebDriver Driver;
        WebDriverWait wait;


        //elements
        private IWebElement _cartName => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#page-wrapper > div > div.col-lg-8 > h2")));
        private IWebElement _cartButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Cart")));
        private IWebElement _placeOrder => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@class,'btn btn-success')]")));
        private IWebElement _inputName => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"name\"]")));
        private IWebElement _inputCountry => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"country\"]")));
        private IWebElement _inputCity => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"city\"]")));
        private IWebElement _inputCard => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"card\"]")));
        private IWebElement _inputMonth => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"month\"]")));
        private IWebElement _inputYear => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"year\"]")));
        private IWebElement _purchaseButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"orderModal\"]/div/div/div[3]/button[2]")));
        private IWebElement _acceptSuccesfullOrderPopOut => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@class,'sa-confirm-button-container')]")));
        private IWebElement _toDeleteElement => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[contains(@onclick,'delete')]")));
        private IWebElement _waitForDelete => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@onclick,'delete')]")));
        private IAlert _popOut => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

        //method
        private IWebElement _tableElement() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("success")));
        private IWebElement WaitUntilOrderProcessingIsCompleted() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("body > div.sweet-alert.showSweetAlert.visible > div.sa-icon.sa-success.animate > div.sa-fix")));
        private IWebElement TotalButton() => Driver.FindElement(By.XPath("//*[@id='page-wrapper']/div/div[2]/h2"));
        private IWebElement Name() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'tbodyid')]/tr/td[2]")));
        private IWebElement ElementsLoaded() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//tr[@class='success']//td//img")));
        public int Price() => int.Parse(Regex.Match(Driver.FindElement(By.XPath("//*[contains(@id,'tbodyid')]/tr/td[3]")).Text, @"\d+").Value);

        Constants constants;

        public CartPage(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            constants = new Constants();
        }

        public void ShowCart()
        {
            _cartButton.Click();
        }

        public void PlaceOrder()
        {
            //wait until elements are loaded 
            ElementsLoaded();
            //search for place order button
            _placeOrder.Click();

        }
        public void BuyAll()
        {
            ShowCart();
            PlaceOrder();
            Name(constants.UserName);
            Country(constants.Country);
            City(constants.City);
            Card(constants.Card);
            Month(constants.Month);
            Year(constants.Year);
            ConfirmOrder();
            AcceptOrderPopOut();
        }


        public void Name(String name)
        {
            _inputName.SendKeys(name);
        }
        public void Country(String country)
        {
            _inputCountry.SendKeys(country);
        }

        public void City(String city)
        {
            _inputCity.SendKeys(city);
        }
        public void Card(String card)
        {

            _inputCard.SendKeys(card);
        }
        public void Month(String month)
        {

            _inputMonth.SendKeys(month);
        }
        public void Year(String year)
        {
            _inputYear.SendKeys(year);
        }

        public void ConfirmOrder()
        {
            _purchaseButton.Click();
          
        }
        public void AcceptOrderPopOut()
        {
            //class="sa-line sa-tip animateSuccessTip";
            WaitUntilOrderProcessingIsCompleted();
            _acceptSuccesfullOrderPopOut.Click();

        }


        public int CountElements()
        {

            _tableElement();
            return _toDeleteElement.FindElements(By.XPath("//*[contains(@class,'col-lg-8')]")).Count;

        }

        public void DeleteElement()
        {
            _tableElement();

            _waitForDelete.Click();
            
        }

        public string NameText()
        {
            return Name().Text;
        }

        public void FillRequiredData(string name, string country, string city, string card, string month, string year)
        {
            Name(name);
            Country(country);
            City(city);
            Card(card);
            Month(month);
            Year(year);

        }


        public void FinalizeOrder()
        {
            ConfirmOrder();
            AcceptOrderPopOut();
        }

        public string CartNameText()
        {
            return _cartName.Text;
        }
        public void RemoveAllElementsFromCart()
        {
            var elementInTable = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[contains(@class,'col-lg-8')]")));
            var countItems = elementInTable.Count();
            var count = 0;
            //this can be optimized. Read all elements in cart, then do a loop over them and delete them.
            try
            {
                do
                {
                    DeleteElement();
                    while (!countItems.Equals(elementInTable))
                    {
                        Thread.Sleep(100);
                        elementInTable = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("success")));
                        if (count == 200) break;
                        count++;
                    }

                    countItems--;

                } while (CountElements() != 0);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("No more elements to delete, please excuse error : " + ex.Message);
                ITakesScreenshot screenShootError = (ITakesScreenshot)Driver;
                Screenshot screenshot = screenShootError.GetScreenshot();
                screenshot.SaveAsFile(" timeout error message.png", ScreenshotImageFormat.Png);
            }
        }

        public string AlertText()
        {                 
            Driver.SwitchTo().Alert();
            _popOut.Accept();//click on submit button
            return _popOut.Text;
        }

        public void CheckThatOrderNotFinalized()
        {
            ConfirmOrder();
            Driver.SwitchTo().Alert();
            Assert.AreEqual("Please fill out Name and Creditcard.", _popOut.Text);
            _popOut.Accept();
            
           
        }
    }
}
