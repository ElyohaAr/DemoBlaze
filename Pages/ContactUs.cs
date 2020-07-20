using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace DemoBlaze
{
    public class ContactUs
    {
        private readonly IWebDriver Driver;
        WebDriverWait wait;
        public ContactUs(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
        }

        ////*[@id="exampleModalLabel"]

        public void ContactEmail(String Text)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("recipient-email")));

            Driver.FindElement(By.Id("recipient-email")).SendKeys(Text);
        }

        public void ContactName(String Text)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("recipient-name")));

            Driver.FindElement(By.Id("recipient-name")).SendKeys(Text);
        }

        public void Message(String Text)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("message-text")));

            Driver.FindElement(By.Id("message-text")).SendKeys(Text);
        }
        public void SendMessageButton()
        {
            string cssPathSendMsgButton = "#exampleModal > div > div > div.modal-footer > button.btn.btn-primary";
            Driver.FindElement(By.CssSelector(cssPathSendMsgButton)).Click();

        }
        public IWebElement Name()
        {

            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='exampleModalLabel']")));

            return element;
        }

        public void FillDataRequired(string email, string user, string message)
        {
            ContactEmail(email);
            ContactName(user);
            Message(message);
        }

     

    }
}
