using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DemoBlaze
{
    public class SignIn
    {
        private readonly IWebDriver Driver;
        WebDriverWait wait;
        public SignIn(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
        }

        public void SignInUsername(String name)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sign-username")));

            Driver.FindElement(By.Id("sign-username")).SendKeys(name);
        }


        public void SignInPassword(String password)
        {
            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("sign-password")));

            Driver.FindElement(By.Id("sign-password")).SendKeys(password);
        }

        public void SignInButton()
        {
            string cssPathLoginButton = "#signInModal > div > div > div.modal-footer > button.btn.btn-primary";
            Driver.FindElement(By.CssSelector(cssPathLoginButton)).Click();
        }

        public void EnterSignInInfo(string username, string password)
        {
            SignInUsername(username);
            SignInPassword(password);

        }

        public void SuccesfullReqistrationAlert()
        {

            var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            IAlert popOut = Driver.SwitchTo().Alert();

            string popOutText = popOut.Text;

            // if (popOutText.Equals("Sign up successful."))
            Assert.AreEqual("Sign up successful.", popOutText);
            popOut.Accept();//click on submit button
        }

    }
}
