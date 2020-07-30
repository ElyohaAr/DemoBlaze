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
        private IWebElement _inputMail => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("recipient-email")));
        private IWebElement _inputName => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("recipient-name")));
        private IWebElement _inputMessage => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("message-text")));
        private IWebElement _sendMessageButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Send message')]")));
        private IWebElement _pageName=> wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#exampleModalLabel")));
        private IAlert _popOut => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());


        private readonly IWebDriver Driver;
        WebDriverWait wait;
        public ContactUs(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
        }

        
        public void ContactEmail(String Text)
        {
           _inputMail.SendKeys(Text);
        }

        public void ContactName(String Text)
        {
           _inputName.SendKeys(Text);
        }

        public void Message(String Text)
        {
            
          _inputMessage.SendKeys(Text);
        }
        public void SendMessageButton()
        {
           _sendMessageButton.Click();

        }
        public string Name()
        {

            return _pageName.Text;
        }

        public void FillDataRequired(string email, string user, string message)
        {
            ContactEmail(email);
            ContactName(user);
            Message(message);
        }

        public string AlertText()
        {
            var text = _popOut.Text;
            _popOut.Accept();//click on submit button
            return text ;
        }


    }
}
