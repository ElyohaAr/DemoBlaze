using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoBlaze
{
    public class SignUp
    {
        private IAlert _popOut => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        private IWebElement _signUpUsername => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sign-username")));
        private IWebElement _signUpPassword => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sign-password")));
        private IWebElement _signUpButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(text(),'Sign up')]")));


        private readonly IWebDriver Driver;
        WebDriverWait wait;
        public SignUp(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
        }

        public void SignInUsername(String name)
        {
           _signUpUsername.SendKeys(name);
        }


        public void SignInPassword(String password)
        {
           _signUpPassword.SendKeys(password);
        }

        public void SignInButton()
        {
            _signUpButton.Click();
        }

        public void EnterSignInInfo(string username, string password)
        {
            SignInUsername(username);
            SignInPassword(password);

        }

        public void SuccesfullReqistrationAlert()
        {
           // if (popOutText.Equals("Sign up successful."))
            Assert.AreEqual("Sign up successful.", _popOut.Text);
            _popOut.Accept();//click on submit button
        }

        public string AlertText()
        {
            var text = _popOut.Text;
          
            _popOut.Accept();//click on submit button
            return text;
        }
    }
}
