using OpenQA.Selenium;
using System;
using System.Threading;

//wrong namespace.
namespace ConsoleApp1.PageObjectModels
{
    public class HomePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "https://www.demoblaze.com/";
        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            Thread.Sleep(2000);
        }

        public void FilterByPhone()
        {
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("//*[contains(@onclick,'phone')]")).Click();
           
            Thread.Sleep(2000);
        }
        public void FilterbyMonitors()
        {
            Thread.Sleep(1000);
            Driver.FindElement(By.XPath("//*[contains(@onclick,'monitor')]")).Click();
            
            Thread.Sleep(2000);
        }
        public void FilterbyLaptops()
        {
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("//*[contains(@onclick,'notebook')]")).Click();
            Thread.Sleep(2000);
        }

        public int CountElements()
        {
            Thread.Sleep(2000);
            int countElements = Driver.FindElements(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")).Count;
            return countElements;
        }

        public void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        public void GoToCartPage()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
            Driver.FindElement(By.XPath("//*[contains(@onclick,'showcart')]")).Click();
            Thread.Sleep(3000);
        }
    }
}


