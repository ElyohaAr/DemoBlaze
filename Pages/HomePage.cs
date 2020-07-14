using ConsoleApp1;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text.RegularExpressions;
using System.Threading;

//wrong namespace.
namespace DemoBlaze
{
    public class HomePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "https://www.demoblaze.com/";
        ProductPage prodPage;
        WebDriverWait wait;

        private IWebElement _loginButton() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login2")));
        public HomePage(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            prodPage = new ProductPage(Driver);
        }



        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(2);
        }

        public void FilterByPhone()
        {

            Driver.Navigate().GoToUrl(PageUrl);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'phone')]")));
            Driver.FindElement(By.XPath("//*[contains(@onclick,'phone')]")).Click();

        }
        public void FilterbyMonitors()
        {

            Driver.Navigate().GoToUrl(PageUrl);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'monitor')]")));
            Driver.FindElement(By.XPath("//*[contains(@onclick,'monitor')]")).Click();

        }
        public void FilterbyLaptops()
        {

            Driver.Navigate().GoToUrl(PageUrl);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'notebook')]")));
            Driver.FindElement(By.XPath("//*[contains(@onclick,'notebook')]")).Click();

        }

        public int CountElements()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")));
            int countElements = Driver.FindElements(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")).Count;
              return countElements;
         
        }

        public void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        public void GoToCartPage()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#cartur"))).Click();
        }

        public void GoToLogIn()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login2")));

            Driver.FindElement(By.Id("login2")).Click();

        }
        public void GoToContact()
        {
            Driver.FindElement(By.XPath("//*[@id=\"navbarExample\"]/ul/li[2]/a")).Click();

        }


        public void GoToLogOut()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"logout2\"]"))).Click();
            // Driver.FindElement(By.XPath("//*[@id=\"logout2\"]")).Click();


        }
        public void GoToAboutUs()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"navbarExample\"]/ul/li[3]/a"))).Click();
            Thread.Sleep(3000);

        }

        public IWebElement AboutUsName()
        {

            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='videoModalLabel']")));

            return element;
        }

        public void LoginUsername(string username)
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("loginusername")));

            Driver.FindElement(By.Id("loginusername")).SendKeys(username);

        }
        public void LoginPassword(string password)
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("loginpassword")));

            Driver.FindElement(By.Id("loginpassword")).SendKeys(password);

        }

        public void LoginButtonForm()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#logInModal > div > div > div.modal-footer > button.btn.btn-primary")));

            Driver.FindElement(By.CssSelector("#logInModal > div > div > div.modal-footer > button.btn.btn-primary")).Click();
        }

        public void EnterLoginInfo(string username, string password)
        {
            LoginUsername(username);
            LoginPassword(password);

        }

        public void GoToSignIn()
        {


            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("signin2")));

            Driver.FindElement(By.Id("signin2")).Click();
        }





        public IWebElement WelcomeElement()
        {

            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("nameofuser")));

            return element;
        }

        public void SliderNextImage()
        {


            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"carouselExampleIndicators\"]/a[2]/span[1]")));
            element.Click();

        }

        public void SliderPreviousImage()
        {


            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"carouselExampleIndicators\"]/a[1]/span[1]")));
            element.Click();

        }
        public IWebElement CurrentImage()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.XPath("//*[contains(@class,'carousel-item active')]"))));

            var element = Driver.FindElement(By.XPath("//*[contains(@class,'carousel-item active')]/img"));

            return element;

        }

        public int ElementsOnPage()
        {

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")));

            return Driver.FindElements(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")).Count;
        }

        public void RandomElementChosen()
        {

            Random rnd = new Random();
            int countElements;
            string product;

            countElements = ElementsOnPage();
            product = "//*[@id=\"tbodyid\"]/div[" + (rnd.Next(countElements - 1) + 1) + "]/div/div/h4/a";
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(product))).Click();

        }


        public int ProductPrice(string product)
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(product)));
            return int.Parse(Regex.Match(Driver.FindElement(By.XPath(product)).GetAttribute("innerText").ToString(), @"\d+").Value);

        }
        public IWebElement FirstAvailableProduct()
        {
            string product = "//*[@id=\"tbodyid\"]/div[1]/div/div/h4/a";
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(product)));

        }

        public void SliderLoading(string mainImage)
        {
            while (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.XPath("//*[contains(@class,'carousel-item active')]/img")))).GetAttribute("alt").ToString().Contains(mainImage)) ;

        }

        public string LoginMainPageName()
        {
            return _loginButton().Text;
        }

        public void LoadProducts()
        {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("list-group-item")));
        }

        public IWebElement DellLaptop => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#tbodyid > div:nth-child(5) > div > div > h4 > a")));

        public void AddDellLaptopToCart()
        {
            NavigateTo();
            Console.WriteLine("Filter by laptops");
            FilterbyLaptops();
            Thread.Sleep(2000);
            Console.WriteLine("Assert");
            Assert.AreEqual("2017 Dell 15.6 Inch", DellLaptop.Text);
            Console.WriteLine("Chose laptop");
            DellLaptop.Click();
            Console.WriteLine("Add to cart");
            prodPage.AddToCart();
        }

        public void Login()
        {
           NavigateTo();
           MaximizeWindow();
           GoToLogIn();
           EnterLoginInfo("User02", "password02");
           LoginButtonForm();
           Assert.True(WelcomeElement().Text.Contains("Welcome"));
        }
    }
}


