using ConsoleApp1;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text.RegularExpressions;


//wrong namespace.
namespace DemoBlaze
{
    public class HomePage
    {
        private readonly IWebDriver Driver;
        private IWebElement _phoneCategory => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'phone')]")));
        private IWebElement _monitorCategory => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'monitor')]")));
        private IWebElement _notebookCategory => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'notebook')]")));
        private IWebElement _productsOnPage => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")));
        private IWebElement _cart => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#cartur")));
        private IWebElement _loginButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login2")));
        private IWebElement _contactButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("Contact")));
        private IWebElement _logoutButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#logout2")));
        private IWebElement _aboutUsButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("About us")));
        private IWebElement _aboutUsName => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("videoModalLabel")));
        private IWebElement _loginUsername => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("loginusername")));
        private IWebElement _loginPassword => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("loginpassword")));
        private IWebElement _loginForm => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(text(),'Log in')]")));
        private IWebElement _signinButton => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("signin2")));
        private IWebElement _welcomeUserName => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("nameofuser")));
        private IWebElement _sliderNext => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"carouselExampleIndicators\"]/a[2]/span[1]")));
        private IWebElement _sliderPrevious => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"carouselExampleIndicators\"]/a[1]/span[1]")));
        private IWebElement _currentActiveSlide => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.XPath("//*[contains(@class,'carousel-item active')]/img"))));

        public IWebElement DellLaptop => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("2017 Dell 15.6 Inch"))); //*[@id="tbodyid"]/div[5]/div/div/h4/a

        private const string PageUrl = "https://www.demoblaze.com/";
        ProductPage prodPage;
        WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
            prodPage = new ProductPage(Driver);
        }



        public void NavigateTo()
        {
            MaximizeWindow();
            Driver.Navigate().GoToUrl(PageUrl);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(2);

        }

        public void FilterByPhone()
        {

            Driver.Navigate().GoToUrl(PageUrl);
            _phoneCategory.Click();

        }
        public void FilterbyMonitors()
        {

            Driver.Navigate().GoToUrl(PageUrl);
            _monitorCategory.Click();

        }
        public void FilterbyLaptops()
        {

            Driver.Navigate().GoToUrl(PageUrl);
            _notebookCategory.Click();

        }

        public int CountElements()
        {
            return _productsOnPage.FindElements(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")).Count;

        }

        public void MaximizeWindow()
        {
            Driver.Manage().Window.Maximize();
        }

        public void GoToCartPage()
        {
            _cart.Click();
        }

        public void GoToLogIn()
        {

            _loginButton.Click();

        }
        public void GoToContact()
        {
            _contactButton.Click();

        }


        public void GoToLogOut()
        {
            _logoutButton.Click();

        }
        public void GoToAboutUs()
        {
            _aboutUsButton.Click();
        }

        public IWebElement AboutUsName()
        {
            return _aboutUsName;
        }

        public void LoginUsername(string username)
        {
            _loginUsername.SendKeys(username);
        }
        public void LoginPassword(string password)
        {
            _loginPassword.SendKeys(password);
        }

        public void LoginButtonForm()
        {
            _loginForm.Click();
        }

        public void EnterLoginInfo(string username, string password)
        {
            LoginUsername(username);
            LoginPassword(password);

        }

        public void GoToSignIn()
        {
            _signinButton.Click();
        }


        public IWebElement WelcomeElement()
        {
            return _welcomeUserName;
        }

        public void SliderNextImage()
        {

            _sliderNext.Click();

        }

        public void SliderPreviousImage()
        {
            _sliderPrevious.Click();

        }
        public string CurrentImage()
        {
            return _currentActiveSlide.GetAttribute("alt").ToString();

        }

        public void RandomElementChosen()
        {

            Random rnd = new Random();
            int countElements;
            string product;

            countElements = CountElements();
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
            return _loginButton.Text;
        }

        public void LoadProducts() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("list-group-item")));



        public void AddDellLaptopToCart()
        {
            NavigateTo();
            FilterbyLaptops();
            Assert.AreEqual("2017 Dell 15.6 Inch", DellLaptop.Text);
            DellLaptop.Click();
            prodPage.AddToCart();
        }

        public void Login()
        {
            NavigateTo();
            GoToLogIn();
            EnterLoginInfo("User02", "password02");
            LoginButtonForm();
            Assert.True(WelcomeElement().Text.Contains("Welcome"));
        }

        public float MeanValue()
        {
            int currentProducts, counter;
            var Value = 0;

            currentProducts = CountElements();

            string product;

            for (counter = 1; counter <= currentProducts; counter++)
            {
                //try to get rid of thread.sleep

                product = "//*[@id=\"tbodyid\"]/div[" + counter + "]/div/div/h5";
                Value += ProductPrice(product);

            }

            return Value / currentProducts;


        }

        public void FilterByProduct(string Product)
        {
            LoadProducts();

            switch (Product)
            {
                case "Phones":
                    FilterByPhone();
                    break;
                case "Monitors":
                    FilterbyMonitors();
                    break;
                case "Laptops":
                    FilterbyLaptops();
                    break;
            }
        }
    }
}


