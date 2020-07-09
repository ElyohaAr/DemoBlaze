using ConsoleApp1;
using ConsoleApp1.PageObjectModels;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;

namespace DemoBlaze
{
    [Binding]
    public class RegisterSteps
    {
        public IWebDriver _driver;
        IWebElement usernameRegister;
        IWebElement passwordRegister;
        IWebElement usernameLogin;
        IWebElement passwordLogin;
        IWebElement mainImage, currentImage;
        int budgetInput;
        string cssPath;
        string productPage, productName;
        int productPrice;
        string MainName = "STORE";
        Boolean failFlag = false;



        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {
            _driver = new ChromeDriver();
            var homePage = new HomePage(_driver);
            homePage.NavigateTo();
            homePage.MaximizeWindow();

        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {
            _driver.FindElement(By.Id("login2")).Click();
        }

        [When(@"I enter my credentials")]
        public void WhenIEnterMyCredentials()
        {

            //try not to use thread.sleep
            usernameLogin = _driver.FindElement(By.Id("loginusername"));
            string User02 = "User02";
            Thread.Sleep(1000);
            usernameLogin.Click();
            usernameLogin.SendKeys(User02);

            passwordLogin = _driver.FindElement(By.Id("loginpassword"));
            Thread.Sleep(1000);
            passwordLogin.Click();
            passwordLogin.SendKeys("password02");
            Thread.Sleep(1000);
            cssPath = "#logInModal > div > div > div.modal-footer > button.btn.btn-primary";
            _driver.FindElement(By.CssSelector(cssPath)).Click();
        }

        [Then(@"I get logged in")]
        public void ThenIGetLoggedIn()
        {

            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("nameofuser")));

            string userName = element.Text;

            Assert.AreEqual("Welcome User02", userName);

        }

        [Given(@"I click on Sign Up button")]
        public void GivenIClickOnSignUpButton()
        {

            //move elements to POM
            _driver.FindElement(By.Id("signin2")).Click();

        }

        //instead of hardcoding the username,
        //try using usernames that are generated on the fly, maybe using a GUID
        //that way if you run the test 2 times, it creates a new user every time
        [When(@"I fill in required data for (.*)")]
        public void WhenIFillInRequiredData(string userName)
        {
            //what do these 2 actions do?
            _driver.FindElement(By.ClassName("modal-title"));
            _driver.FindElement(By.ClassName("form-control-label"));


            usernameRegister = _driver.FindElement(By.Id("sign-username"));
            //remove thread.sleep. Try using explicit wait (wait until element is visible/clickable)
            Thread.Sleep(1000);
            //no need for extra click in the field
            usernameRegister.Click();
            usernameRegister.SendKeys(userName);

            passwordRegister = _driver.FindElement(By.Id("sign-password"));
            Thread.Sleep(1000);
            passwordRegister.Click();
            passwordRegister.SendKeys("password02");
            Thread.Sleep(1000);
            //give more meaningful names to the variables
            cssPath = "#signInModal > div > div > div.modal-footer > button.btn.btn-primary";
            _driver.FindElement(By.CssSelector(cssPath)).Click();
        }

        [Then(@"I get registered")]
        public void ThenIGetRegistered()
        {
            Thread.Sleep(1000);
            IAlert popOut = _driver.SwitchTo().Alert();

            string popOutText = popOut.Text;

            // if (popOutText.Equals("Sign up successful."))
            Assert.AreEqual("Sign up successful.", popOutText);
            popOut.Accept();//click on submit button

        }
        [Then(@"I get informed that the username is taken")]
        public void ThenIGetInformedThatTheUsernameIsTaken()
        {
            Thread.Sleep(1000);
            IAlert popOut = _driver.SwitchTo().Alert();

            string popOutText = popOut.Text;

            // if (popOutText.Equals("Sign up successful."))
            Assert.AreEqual("This user already exist.", popOutText);
            popOut.Accept();//click on submit button
        }


        [When(@"I click on the “Next” buttons from Image Slider")]
        public void WhenIClickOnTheNextButtonsFromImageSlider()
        {
            //this step will always pass, as the elements are always generated into the code
            //you need to take the element that has the "active" class added,
            //then try to compare it
            Thread.Sleep(2000);
            mainImage = _driver.FindElement(By.XPath("//*[@id=\"carouselExampleIndicators\"]/div/div[1]/img"));

            _driver.FindElement(By.XPath("//*[@id=\"carouselExampleIndicators\"]/a[2]/span[1]")).Click(); // click next

            currentImage = _driver.FindElement(By.XPath("//*[@id=\"carouselExampleIndicators\"]/div/div[2]/img"));

            Assert.False(mainImage == currentImage);

        }

        [When(@"I click on the “Previous” buttons from Image Slider")]
        public void WhenIClickOnThePreviousButtonsFromImageSlider()
        {
            //same as above
            Thread.Sleep(2000);
            mainImage = _driver.FindElement(By.XPath("//*[@id=\"carouselExampleIndicators\"]/div/div[1]/img"));
            _driver.FindElement(By.XPath("//*[@id=\"carouselExampleIndicators\"]/a[1]/span[1]")).Click(); // click previous

            currentImage = _driver.FindElement(By.XPath("//*[@id=\"carouselExampleIndicators\"]/div/div[1]/img"));

            Assert.False(mainImage == currentImage);
        }

        [Then(@"I see a different product")]
        public void ThenISeeADifferentProduct()
        {
            currentImage = _driver.FindElement(By.XPath("//*[@id=\"carouselExampleIndicators\"]/div/div[2]/img"));

            Assert.False(mainImage == currentImage);
        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            GivenIAmOnTheHomepage();
            WhenIClickOnTheLoginButton();
            WhenIEnterMyCredentials();
            ThenIGetLoggedIn();

        }

        [Given(@"I have a budget of (.*)\$")]
        public void GivenIHaveABudgetOf(int p0)
        {
            budgetInput = p0;

        }


        [When(@"I filter by (.*)")]
        public void WhenIFilterByProduct(string Product)
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("list-group-item")));
            switch (Product)
            {
                case "Phones":
                    _driver.FindElement(By.XPath("//*[contains(@onclick,'phone')]")).Click();
                    productPage = "//*[contains(@onclick,'phone')]";
                    break;
                case "Monitors":
                    _driver.FindElement(By.XPath("//*[contains(@onclick,'monitor')]")).Click();
                    productPage = "//*[contains(@onclick,'monitor')]";
                    break;
                case "Laptops":
                    _driver.FindElement(By.XPath("//*[contains(@onclick,'notebook')]")).Click();
                    productPage = "//*[contains(@onclick,'notebook')]";
                    break;
            }
            //try to remove thread.sleep
            Thread.Sleep(2000);
            TestContext.Out.WriteLine("Enterd laptop page");

        }

        [Then(@"I can add to cart (.*) random phones that don't exceed my budget")]
        public void ThenICanAddToCartRandomPhonesThatDonTExceedMyBudget(int p0)
        {
            Random rnd = new Random();
            int countPhones;
            int i;
            countPhones = _driver.FindElements(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")).Count;
            Debug.WriteLine(countPhones);
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            string product;
            i = 1;
            while (i <= p0)
            {

                product = "//*[@id=\"tbodyid\"]/div[" + (rnd.Next(countPhones - 1) + 1) + "]/div/div/h4/a";
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(product)));
                element.Click();
                Debug.WriteLine("before addtocart = " + budgetInput + " \ni=" + i);

                if (AddToCartBudgetBased() == 1)
                {
                    i++;
                }
                Debug.WriteLine("after addtocart = " + budgetInput + " \ni=" + i);
                //why is this next line needed?
                WhenIFilterByProduct("Phones");

            }


        }

        //try moving this to bool, instead of int. it's easier to follow
        //also, could this be a little simplified?
        int AddToCartBudgetBased()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            int prodPrice;
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("price-container")));
            prodPrice = int.Parse(Regex.Match(_driver.FindElement(By.ClassName("price-container")).Text, @"\d+").Value);
            Debug.WriteLine(prodPrice);
            Debug.Write("prod price: " + _driver.FindElement(By.ClassName("price-container")).Text);

            if (budgetInput - prodPrice >= 0)
            {
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'addToCart')]")));
                element.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                IAlert popOut = _driver.SwitchTo().Alert();
                string popOutText = popOut.Text;
                budgetInput -= prodPrice;
                Debug.WriteLine("inside if Budget = " + budgetInput);
                Assert.AreEqual("Product added.", popOutText);
                popOut.Accept();
                _driver.FindElement(By.Id("nava")).Click();
                return 1;

            }
            Debug.WriteLine("Outside if Budget = " + budgetInput);
            _driver.FindElement(By.Id("nava")).Click();

            return 0;
        }

        [Then(@"I can see in the test output the mean value of each product")]
        public void ThenICanSeeInTheTestOutputTheMeanValueOfEachProduct()
        {

            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            int currentProducts;
            int i, prodPrice, meanValue = 0;
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")));
            currentProducts = _driver.FindElements(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")).Count;
            Debug.WriteLine(currentProducts);
            string product;

            for (i = 1; i <= currentProducts; i++)
            {
                //try to get rid of thread.sleep
                Thread.Sleep(2000);
                product = "//*[@id=\"tbodyid\"]/div[" + i + "]/div/div/h4/a";
                var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(product)));
                element.Click();
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("price-container")));
                prodPrice = int.Parse(Regex.Match(_driver.FindElement(By.ClassName("price-container")).Text, @"\d+").Value);
                meanValue += prodPrice;

                _driver.FindElement(By.Id("nava")).Click();

                _driver.FindElement(By.XPath(productPage)).Click();


            }


            TestContext.Out.WriteLine("Sum {0}", meanValue);
            TestContext.Out.WriteLine("MeanValue is {0}", meanValue / currentProducts);

        }

        [Given(@"I click on (.*) page")]
        public void GivenIClickOnPage(string pageType)
        {

            switch (pageType)
            {
                case "Home":
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                    _driver.FindElement(By.Id("nava")).Click();
                    break;

                case "Cart":
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                    _driver.FindElement(By.CssSelector("#navbarExample>ul>li:nth-child(4)>a")).Click();
                    //in xpath, if you use "//" you don't need to use the * right after 
                    _driver.FindElement(By.XPath("//*[contains(@onclick,'showcart')]")).Click();
                    Thread.Sleep(3000);
                    break;

                case "Contact":
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                    //_driver.FindElement(By.CssSelector("#navbarExample>ul>li:nth-child(4)>a")).Click();
                    _driver.FindElement(By.XPath("//*[@id=\"navbarExample\"]/ul/li[2]/a")).Click();
                    Thread.Sleep(3000);
                    break;

                case "Log out":
                    //_driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                    Thread.Sleep(3000);
                    _driver.FindElement(By.XPath("//*[@id=\"logout2\"]")).Click();
                    Thread.Sleep(3000);
                    break;

                case "About us":
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                    //_driver.FindElement(By.CssSelector("#navbarExample>ul>li:nth-child(4)>a")).Click();
                    _driver.FindElement(By.XPath("//*[@id=\"navbarExample\"]/ul/li[3]/a")).Click();
                    Thread.Sleep(3000);
                    break;

                case "Log in":
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                    _driver.FindElement(By.Id("login2")).Click();
                    Thread.Sleep(3000);
                    break;

                case "Sign up":
                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(3);
                    _driver.FindElement(By.CssSelector("#signin2")).Click();
                    Thread.Sleep(3000);
                    break;
            }

        }


        [Then(@"the correct page is displayed")]
        public void ThenTheCorrectPageIsDisplayed()
        {
            TestContext.Out.WriteLine("title: ", _driver.Title);
            Assert.AreEqual(_driver.Title, MainName);

        }

        [Then(@"Cart page is displayed")]
        public void ThenCartPageIsDisplayed()
        {
            string title;
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            try
            {

                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#page-wrapper>div>div.col-lg-1>button")));
                title = _driver.FindElement(By.CssSelector("#page-wrapper>div>div.col-lg-1>button")).Text;
                Assert.AreEqual("Place Order", title);
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("ERROR: ", ex.Message);

            }
        }
        [Given(@"I click on the first product available")]
        public void GivenIClickOnTheFirstProductAvailable()
        {

            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'col-lg-4 col-md-6 mb-4')]")));
            string product;

            Thread.Sleep(2000);
            product = "//*[@id=\"tbodyid\"]/div[1]/div/div/h4/a";
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(product)));
            element.Click();



        }

        [Then(@"I can see it's price")]
        public void ThenICanSeeItSPrice()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            string prodPrice;

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("price-container")));
            prodPrice = _driver.FindElement(By.ClassName("price-container")).Text;
            TestContext.Out.WriteLine("The first available product costs " + prodPrice);
        }

        [Given(@"I have items in Cart")]
        public void GivenIHaveItemsInCart()
        {
            int countPhones;
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'delete')]")));
            countPhones = _driver.FindElements(By.XPath("//*[contains(@class,'col-lg-8')]")).Count;
            Assert.AreNotEqual(0, countPhones);

        }

        [Given(@"I remove them")]
        public void ThenIRemoveThem()
        {

            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 3));
            //this can be optimized. Read all elements in cart, then do a loop over them and delete them.
            try
            {
                while (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@onclick,'delete')]"))).Displayed)
                {

                    _driver.FindElement(By.XPath("//*[contains(@onclick,'delete')]")).Click();
                    Thread.Sleep(1500);

                }
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("No more elements to delete, please excuse error : " + ex.Message);
                ITakesScreenshot screenShootError = (ITakesScreenshot)_driver;
                Screenshot screenshot = screenShootError.GetScreenshot();
                screenshot.SaveAsFile(" timeout error message.png", ScreenshotImageFormat.Png);
            }
        }


        [Then(@"My cart is empty")]
        public void ThenMyCartIsEmpty()
        {
            int countPhones = 0;
            try
            {
                Thread.Sleep(1500);
                countPhones = _driver.FindElements(By.XPath("//*[contains(@onClick,'deleteItem')]")).Count;

            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("My cart is empty, so the following error presents : " + ex.Message);
                failFlag = true;
            }
            ITakesScreenshot screenShootError = (ITakesScreenshot)_driver;
            Screenshot screenshot = screenShootError.GetScreenshot();
            screenshot.SaveAsFile("error message count.png", ScreenshotImageFormat.Png);
            Assert.AreEqual(0, countPhones);
            Assert.False(failFlag);
        }


        [Given(@"I add the product to my cart")]
        public void GivenIAddTheProductToMyCart()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("price-container")));
            productPrice = int.Parse(Regex.Match(_driver.FindElement(By.ClassName("price-container")).Text, @"\d+").Value);
            productName = _driver.FindElement(By.ClassName("name")).Text;

            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@onclick,'addToCart')]")));
            element.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            IAlert popOut = _driver.SwitchTo().Alert();
            string popOutText = popOut.Text;

            Assert.AreEqual("Product added.", popOutText);
            popOut.Accept();
            //remove thread.sleep
            Thread.Sleep(2500);

        }

        [Then(@"The selected product should be in cart")]
        public void ThenTheSelectedProductShouldBeInCart()
        {

            ITakesScreenshot screenShootError = (ITakesScreenshot)_driver;
            //screenshot could be taken on an [AfterScenario] hook
            Screenshot screenshot = screenShootError.GetScreenshot();
            screenshot.SaveAsFile(" timeout error message.png", ScreenshotImageFormat.Png);

            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@class,'success')]")));
            Thread.Sleep(1500);
            string product = _driver.FindElement(By.XPath("//*[contains(@id,'tbodyid')]/tr/td[2]")).Text;

            // bool result =product.Equals(productName) && int.Parse(Regex.Match(_driver.FindElement(By.XPath("//*[contains(@id,'tbodyid')]/tr/td[3]")).Text, @"\d+").Value).Equals(productPrice);

            //if you put the assert into a try/catch block, this won't count as a fail, program would continue
            try
            {
                Assert.AreEqual(productName, product);
            }
            catch (Exception ex)
            {
                //here, the screenshot taken at line 528 will be saved, no new screenshot will be taken
                screenshot.SaveAsFile("Name not the same.png", ScreenshotImageFormat.Png);
                TestContext.Out.WriteLine("ERROR: " + ex.Message);
                failFlag = true;

            }

            try
            {
                Assert.AreEqual(productPrice, int.Parse(Regex.Match(_driver.FindElement(By.XPath("//*[contains(@id,'tbodyid')]/tr/td[3]")).Text, @"\d+").Value));
            }
            catch (Exception ex)
            {

                screenshot.SaveAsFile("Not the same price.png", ScreenshotImageFormat.Png);
                TestContext.Out.WriteLine("ERROR: " + ex.Message);
                failFlag = true;

            }
            Assert.False(failFlag);
        }

        [Then(@"the (.*) page is displayed")]
        public void ThenThePageIsDisplayed(string wantedPage)
        {
            ITakesScreenshot screenShootError = (ITakesScreenshot)_driver;
            Screenshot screenshot = screenShootError.GetScreenshot();
            screenshot.SaveAsFile(" timeout error message.png", ScreenshotImageFormat.Png);
            string title;
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));

            switch (wantedPage)
            {
                case "Home":
                    TestContext.Out.WriteLine("title: ", _driver.Title);
                    Assert.AreEqual(_driver.Title, MainName);
                    break;

                case "Cart":
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("#page-wrapper>div>div.col-lg-1>button")));
                        title = _driver.FindElement(By.CssSelector("#page-wrapper>div>div.col-lg-1>button")).Text;
                        Assert.AreEqual("Place Order", title);
                    }
                    catch (Exception ex)
                    {
                        //same here, new screenshot is not being taken, the one taken at the beginning of the class is saved again
                        screenshot.SaveAsFile("Not Cart page.png", ScreenshotImageFormat.Png);
                        TestContext.Out.WriteLine("ERROR: " + ex.Message);
                        failFlag = true;
                    }
                    break;

                case "Contact":
                    try
                    {
                        _driver.FindElement(By.ClassName("modal-title"));
                        _driver.FindElement(By.Id("exampleModalLabel"));
                    }
                    catch (Exception ex)
                    {
                        screenshot.SaveAsFile("No Contact pop-out.png", ScreenshotImageFormat.Png);
                        TestContext.Out.WriteLine("ERROR: " + ex.Message);
                        failFlag = true;
                    }
                    break;

                case "Log out":

                    try
                    {

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login2")));
                    }
                    catch (Exception ex)
                    {
                        screenshot.SaveAsFile("Not logged out.png", ScreenshotImageFormat.Png);
                        TestContext.Out.WriteLine("ERROR: " + ex.Message);
                        failFlag = true;
                    }
                    break;

                case "About us":
                    try
                    {
                        Thread.Sleep(3000);
                        _driver.FindElement(By.ClassName("modal-title"));
                        _driver.FindElement(By.Id("videoModalLabel"));
                    }
                    catch (Exception ex)
                    {
                        screenshot.SaveAsFile("No Aboutus pop-out.png", ScreenshotImageFormat.Png);
                        TestContext.Out.WriteLine("ERROR: " + ex.Message);
                        failFlag = true;
                    }
                    break;

                case "Log in":
                    try
                    {
                        _driver.FindElement(By.ClassName("modal-title"));
                        _driver.FindElement(By.Id("logInModalLabel"));
                    }
                    catch (Exception ex)
                    {
                        screenshot.SaveAsFile("No log in pop-out.png", ScreenshotImageFormat.Png);
                        TestContext.Out.WriteLine("ERROR: " + ex.Message);
                        failFlag = true;
                    }
                    break;

                case "Sign up":
                    try
                    {
                        _driver.FindElement(By.ClassName("modal-title"));
                        _driver.FindElement(By.Id("signInModalLabel"));
                    }
                    catch (Exception ex)
                    {
                        screenshot.SaveAsFile("No Sign in pop-out.png", ScreenshotImageFormat.Png);
                        TestContext.Out.WriteLine("ERROR: " + ex.Message);
                        failFlag = true;
                    }
                    break;

            }

            Assert.IsFalse(failFlag);
        }


        [Given(@"I add in cart a laptop dell from (.*)")]
        public void GivenIAddInCartALaptopDellFrom(int p0)
        {
            string title;
            //*[@id="tbodyid"]/div[5]/div/div/h4/a
            //#tbodyid > div:nth-child(5) > div > div > h4 > a
            title = _driver.FindElement(By.CssSelector("//#tbodyid>div:nth-child(5)>div>div>h4>a")).Text;
            Assert.AreEqual("2017 Dell 15.6 Inch", title);
            _driver.FindElement(By.CssSelector("//#tbodyid>div:nth-child(5)>div>div>h4>a")).Click();

            GivenIAddTheProductToMyCart();

        }

        [When(@"I fill the required data to purchase")]
        public void WhenIFillTheRequiredDataToPurchase()
        {
            _driver.FindElement(By.ClassName("modal-title"));
            _driver.FindElement(By.Id("orderModalLabel"));

            _driver.FindElement(By.XPath("//*[contains(@class,'form-control')]"));

            //IWebElement name = _driver.FindElement(By.XPath("//*[contains(@id,'name')]"));
            IWebElement name = _driver.FindElement(By.XPath("//*[@id=\"name\"]"));
            Thread.Sleep(1000);
            name.Click();
            name.SendKeys("User02");


            IWebElement Country = _driver.FindElement(By.XPath("//*[contains(@id,'country')]"));
            Thread.Sleep(1000);
            Country.Click();
            Country.SendKeys("Romania");

            IWebElement City = _driver.FindElement(By.XPath("//*[contains(@id,'city')]"));
            Thread.Sleep(1000);
            City.Click();
            City.SendKeys("Timisoara");

            IWebElement CreditCard = _driver.FindElement(By.XPath("//*[contains(@id,'card')]"));
            Thread.Sleep(1000);
            CreditCard.Click();
            CreditCard.SendKeys("1111-2222-3333-4444-5555");

            IWebElement Month = _driver.FindElement(By.XPath("//*[contains(@id,'month')]"));
            Thread.Sleep(1000);
            Month.Click();
            Month.SendKeys("Feb");

            IWebElement Year = _driver.FindElement(By.XPath("//*[contains(@id,'year')]"));
            Thread.Sleep(1000);
            Year.Click();
            Year.SendKeys("2025");



        }

        [Then(@"I can buy what is in cart")]
        public void ThenICanBuyWhatIsInCart()
        {
            //// place order
            _driver.FindElement(By.XPath("//*[@id=\"orderModal\"]/div/div/div[3]/button[2]")).Click();
            Thread.Sleep(3000);

            // accept popout after placing order
            _driver.FindElement(By.XPath("//*[contains(@class,'sa-confirm-button-container')]")).Click();

        }

        [Given(@"I added in my cart a laptop from 2017")]
        public void GivenIAddedInMyCartALaptopFrom()
        {
            string title;

            GivenIAmLoggedIn();
            WhenIFilterByProduct("Laptops");
            Thread.Sleep(2000);

            title = _driver.FindElement(By.CssSelector("#tbodyid > div:nth-child(5) > div > div > h4 > a")).Text;
            Assert.AreEqual("2017 Dell 15.6 Inch", title);
            Thread.Sleep(2000);

            _driver.FindElement(By.CssSelector("#tbodyid > div:nth-child(5) > div > div > h4 > a")).Click();
            GivenIAddTheProductToMyCart();

        }

        [Given(@"I click on Place order button")]
        public void GivenIClickOnPlaceOrderButton()
        {

            var cartPage = new CartPage(_driver);
            cartPage.PlaceOrder();
            //var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 50));
            //var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@class,'btn btn-success')]")));

            //_driver.FindElement(By.XPath("//*[contains(@class,'btn btn-success')]")).Click();
        }
        [Given(@"I purchased an item")]
        public void GivenIPurchasedAnItem()
        {
            GivenIAddedInMyCartALaptopFrom();

            GivenIClickOnPage("Cart");

            GivenIClickOnPlaceOrderButton();

            WhenIFillTheRequiredDataToPurchase();

            ThenICanBuyWhatIsInCart();
            Thread.Sleep(2000);

        }

        //try to simplify this method
        [When(@"I add in cart a laptop, monitor and phone that don't exceed my budget")]
        public void WhenIAddInCartALaptopMonitorAndPhoneThatDonTExceedMyBudget()
        {
            int countPhones, countMonitors, countLaptops, productPriceM, productPriceL, productPriceP;
            var homePage = new HomePage(_driver);
            bool passflag = false;
            var productPage = new ProductPage(_driver);
            int i, j, k, Sum;
            homePage.FilterByPhone();
            countPhones = homePage.CountElements();

            homePage.FilterbyMonitors();
            countMonitors = homePage.CountElements();

            homePage.FilterbyLaptops();
            countLaptops = homePage.CountElements();

            //wait not used
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            string product;

            for (i = 1; i < countPhones && (passflag != true); i++)
            {

                homePage.FilterByPhone();
                product = "//*[@id=\"tbodyid\"]/div[" + i + "]/div/div/h5";
                productPriceP = int.Parse(Regex.Match(_driver.FindElement(By.XPath(product)).Text, @"\d+").Value);


                for (j = 1; j <= countLaptops && (passflag != true); j++)
                {

                    homePage.FilterbyLaptops();
                    product = "//*[@id=\"tbodyid\"]/div[" + j + "]/div/div/h5";
                    productPriceL = int.Parse(Regex.Match(_driver.FindElement(By.XPath(product)).Text, @"\d+").Value);

                    for (k = 1; (k <= countMonitors); k++)
                    {

                        homePage.FilterbyMonitors();
                        product = "//*[@id=\"tbodyid\"]/div[" + k + "]/div/div/h5";
                        productPriceM = int.Parse(Regex.Match(_driver.FindElement(By.XPath(product)).Text, @"\d+").Value);

                        Sum = productPriceM + productPriceL + productPriceP;
                        Console.WriteLine(Sum);

                        if (Sum <= budgetInput)
                        {
                            homePage.NavigateTo();
                            homePage.FilterByPhone();
                            _driver.FindElement(By.XPath("//*[@id=\"tbodyid\"]/div[" + i + "]/div/div/h4/a")).Click();
                            productPage.AddToCart();

                            homePage.NavigateTo();
                            homePage.FilterbyMonitors();
                            _driver.FindElement(By.XPath("//*[@id=\"tbodyid\"]/div[" + k + "]/div/div/h4/a")).Click();
                            productPage.AddToCart();


                            homePage.NavigateTo();
                            Thread.Sleep(2000);
                            homePage.FilterbyLaptops();
                            Thread.Sleep(2000);

                            Console.WriteLine("Before clicking add to cart laptop");
                            _driver.FindElement(By.XPath("//*[@id=\"tbodyid\"]/div[" + j + "]/div/div/h4/a")).Click();
                            Console.WriteLine("After clicking add to cart laptop");
                            productPage.AddToCart();

                            Console.WriteLine("We can buy them");

                            passflag = true;
                        }
                        else
                            Console.WriteLine("Not enough money");



                    }

                }

            }

        }

        //all the steps should look like this, maybe some with an assert as well. Good job on this one
        [Then(@"I purchase all from cart")]
        public void ThenIPurchaseAllFromCart()
        {
            var cartPage = new CartPage(_driver);
            cartPage.BuyAll();
        }


        [AfterScenario()]
        public void DisposeWebDriver()
        {
            Thread.Sleep(1000);
            _driver.Dispose();
        }
    }
}

// Looks good overall, try to implement things in feedback. 
// Also try adding a folder structure, for page objects/features.
// Feature files can stay in the same folder as steps.cs files