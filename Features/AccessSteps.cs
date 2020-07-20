using ConsoleApp1;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;

namespace DemoBlaze
{
    [Binding]
    public class RegisterSteps
    {

        public IWebDriver _driver = new ChromeDriver();
        string mainImage, currentImage;
        int budgetInput;
        HomePage homePage;
        CartPage cartPage;
        ContactUs contactUs;
        ProductPage prodPage;
        UsefulFunctions usefulFunction;
        WebDriverWait wait;
        ITakesScreenshot screenShootError;
        string productName;
        int productPrice;
        readonly string MainName = "STORE";
        Boolean failFlag = false;

        public RegisterSteps()
        {
            homePage = new HomePage(_driver);
            cartPage = new CartPage(_driver);
            prodPage = new ProductPage(_driver);
            contactUs = new ContactUs(_driver);
            usefulFunction = new UsefulFunctions(_driver);
            wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            screenShootError = (ITakesScreenshot)_driver;
        }


        [Given(@"I am on the homepage")]
        public void GivenIAmOnTheHomepage()
        {

            homePage.NavigateTo();
        }

        [When(@"I click on the login button")]
        public void WhenIClickOnTheLoginButton()
        {

            homePage.GoToLogIn();

        }

        [When(@"I enter my credentials")]
        public void WhenIEnterMyCredentials()
        {

            homePage.EnterLoginInfo("User02", "password02");
            homePage.LoginButtonForm();

        }

        [Then(@"I get logged in")]
        public void ThenIGetLoggedIn()
        {

            Assert.True(homePage.WelcomeElement().Text.Contains("Welcome"));

        }

        [Given(@"I click on Sign Up button")]
        public void GivenIClickOnSignUpButton()
        {
            homePage.GoToSignIn();

        }

        //instead of hardcoding the username,
        //try using usernames that are generated on the fly, maybe using a GUID
        //that way if you run the test 2 times, it creates a new user every time
        [When(@"I fill in required data for (.*)")]
        public void WhenIFillInRequiredData(string userName)
        {
            SignIn LogINPage = new SignIn(_driver);

            if (userName == "RandomUser")
            {
                userName = usefulFunction.Random(6);
                LogINPage.EnterSignInInfo(userName, "password02");
                LogINPage.SignInButton();
            }
            else
            {

                LogINPage.EnterSignInInfo(userName, "password02");
                LogINPage.SignInButton();
            }
        }

        [Then(@"I get registered")]
        public void ThenIGetRegistered()
        {

            Assert.AreEqual("Sign up successful.", AlertPresent().Text);
            AlertPresent().Accept();//click on submit button

        }

        [Then(@"I get informed that the username is taken")]
        public void ThenIGetInformedThatTheUsernameIsTaken()
        {

            Assert.AreEqual("This user already exist.", AlertPresent().Text);
            AlertPresent().Accept();//click on submit button

        }


        [When(@"I click on the “Next” buttons from Image Slider")]
        public void WhenIClickOnTheNextButtonsFromImageSlider()
        {
            //this step will always pass, as the elements are always generated into the code
            //you need to take the element that has the "active" class added,
            //then try to compare it
            mainImage = homePage.CurrentImage().GetAttribute("alt").ToString();

            homePage.SliderNextImage();

        }

        [When(@"I click on the “Previous” buttons from Image Slider")]
        public void WhenIClickOnThePreviousButtonsFromImageSlider()
        {

            mainImage = homePage.CurrentImage().GetAttribute("alt").ToString();

            homePage.SliderPreviousImage();

        }

        [Then(@"I see a different product")]
        public void ThenISeeADifferentProduct()
        {

            homePage.SliderLoading(mainImage);

            currentImage = homePage.CurrentImage().GetAttribute("alt").ToString();
            Console.WriteLine(mainImage + "  " + currentImage);

            Assert.False(mainImage == currentImage);
        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            homePage.Login();

        }

        [Given(@"I have a budget of (.*)\$")]
        public void GivenIHaveABudgetOf(int p0)
        {
            budgetInput = p0;
        }


        [When(@"I filter by (.*)")]
        public void WhenIFilterByProduct(string Product)
        {
            homePage.LoadProducts();

            switch (Product)
            {
                case "Phones":
                    homePage.FilterByPhone();
                    break;
                case "Monitors":
                    homePage.FilterbyMonitors();
                    break;
                case "Laptops":
                    homePage.FilterbyLaptops();
                    break;
            }

        }

        [Then(@"I can add to cart (.*) random phones that don't exceed my budget")]
        public void ThenICanAddToCartRandomPhonesThatDonTExceedMyBudget(int budgetReceived)
        {

            int productsAdded = 1;

            while (productsAdded <= budgetReceived)
            {
                homePage.RandomElementChosen();

                if (AddToCartBudgetBased() == true)
                {
                    productsAdded++;
                }

                //why is this next line needed?
                //ElAr: after clicking on a product to go back to filter by phones page 

            }


        }

        //try moving this to bool, instead of int. it's easier to follow
        //also, could this be a little simplified?
        bool AddToCartBudgetBased()
        {

            int prodPrice;
            prodPrice = prodPage.Price();

            if (budgetInput - prodPrice >= 0)
            {

                budgetInput -= prodPrice;
                prodPage.AddToCart();
                homePage.FilterByPhone();

                return true;

            }

            homePage.FilterByPhone();
            return false;
        }

        [Then(@"I can see in the test output the mean value of each product")]
        public void ThenICanSeeInTheTestOutputTheMeanValueOfEachProduct()
        {

            int currentProducts;
            int i, Value = 0;

            currentProducts = homePage.CountElements();

            string product;

            for (i = 1; i <= currentProducts; i++)
            {
                //try to get rid of thread.sleep

                product = "//*[@id=\"tbodyid\"]/div[" + i + "]/div/div/h5";
                Value += homePage.ProductPrice(product);

            }

            Console.WriteLine("Sum {0}", Value);
            Console.WriteLine("MeanValue is {0}", Value / currentProducts);

        }

        [Given(@"I click on (.*) page")]
        public void GivenIClickOnPage(string pageType)
        {

            switch (pageType)
            {
                case "Home":
                    homePage.NavigateTo();
                    break;

                case "Cart":
                    homePage.GoToCartPage();
                    break;

                case "Contact":
                    homePage.GoToContact();
                    break;

                case "Log out":

                    homePage.GoToLogOut();
                    break;

                case "About us":
                    homePage.GoToAboutUs();
                    break;

                case "Log in":
                    homePage.GoToLogIn();
                    break;

                case "Sign up":
                    homePage.GoToSignIn();
                    break;
            }

        }


        [Then(@"the correct page is displayed")]
        public void ThenTheCorrectPageIsDisplayed()
        {
            TestContext.Out.WriteLine("title: ", _driver.Title);
            Assert.AreEqual(_driver.Title, MainName);

        }


        [Given(@"I click on the first product available")]
        public void GivenIClickOnTheFirstProductAvailable()
        {

            homePage.FirstAvailableProduct().Click();

        }

        [Then(@"I can see it's price")]
        public void ThenICanSeeItSPrice()
        {
            int prodPrice;
            prodPrice = prodPage.Price();

            Console.Out.WriteLine("The first available product costs " + prodPrice);
        }

        [Given(@"I have items in Cart")]
        public void GivenIHaveItemsInCart()
        {

            Assert.AreNotEqual(0, cartPage.CountElements());

        }

        [Given(@"I remove them")]
        public void ThenIRemoveThem()
        {
            var elementInTable = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[contains(@class,'col-lg-8')]")));
            var countItems = elementInTable.Count();
            var count = 0;
            //this can be optimized. Read all elements in cart, then do a loop over them and delete them.
            try
            {
                do
                {
                    cartPage.DeleteElement();
                    while (!countItems.Equals(elementInTable))
                    {
                        Thread.Sleep(100);
                        elementInTable = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("success")));
                        if (count == 40) break;
                        count++;
                    }

                    countItems--;

                } while (cartPage.CountElements() != 0);
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
                countPhones = cartPage.CountElements();

            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("My cart is empty, so the following error presents : " + ex.Message);

            }

            Assert.AreEqual(0, countPhones);

        }


        [Given(@"I add the product to my cart")]
        public void GivenIAddTheProductToMyCart()
        {
            productPrice = prodPage.Price();
            productName = prodPage.Name();

            prodPage.AddToCart();

        }

        [Then(@"The selected product should be in cart")]
        public void ThenTheSelectedProductShouldBeInCart()
        {

            Assert.AreEqual(productName, cartPage.Name());
            Assert.AreEqual(productPrice, cartPage.Price());

        }

        [Then(@"the (.*) page is displayed")]
        public void ThenThePageIsDisplayed(string wantedPage)
        {

            switch (wantedPage)
            {
                case "Home":
                    Console.WriteLine("title: ", _driver.Title);
                    Assert.AreEqual(_driver.Title, MainName);
                    break;

                case "Cart":
                    Assert.AreEqual("Products", cartPage.CartName().Text);
                    break;

                case "Contact":
                    Assert.AreEqual("New message", contactUs.Name().Text);
                    break;

                case "Log out":
                    Assert.AreEqual("Log in", homePage.LoginMainPageName());
                    break;

                case "About us":
                    Assert.AreEqual("About us", homePage.AboutUsName().Text);
                    break;

                case "Log in":
                    _driver.FindElement(By.Id("logInModalLabel"));
                    break;

                case "Sign up":
                    _driver.FindElement(By.Id("signInModalLabel"));
                    break;

            }

            Assert.IsFalse(failFlag);
        }


        [Given(@"I add in cart a laptop dell from 2017")]
        public void GivenIAddInCartALaptopDellFrom()
        {
            homePage.AddDellLaptopToCart();
        }

        [When(@"I fill the required data to purchase")]
        public void WhenIFillTheRequiredDataToPurchase()
        {
            cartPage.FillRequiredData("User02", "RO", "TM", "111-222-333-444", "FEB", "2025");
        }

        [Then(@"I can buy what is in cart")]
        public void ThenICanBuyWhatIsInCart()
        {
            cartPage.FinalizeOrder();

        }

        [Given(@"I added in my cart a laptop from 2017")]
        public void GivenIAddedInMyCartALaptopFrom()
        {
            homePage.AddDellLaptopToCart();

        }

        [Given(@"I click on Place order button")]
        public void GivenIClickOnPlaceOrderButton()
        {

            cartPage.PlaceOrder();

        }


        [Given(@"I purchased an item")]
        public void GivenIPurchasedAnItem()
        {
            homePage.AddDellLaptopToCart();
            cartPage.BuyAll();

        }

        //try to simplify this method
        [When(@"I add in cart a laptop, monitor and phone that don't exceed my budget")]
        public void WhenIAddInCartALaptopMonitorAndPhoneThatDonTExceedMyBudget()
        {
            usefulFunction.AddLaptopMonitoPhoneToCartBasedonBudget(budgetInput);
        }

        //all the steps should look like this, maybe some with an assert as well. Good job on this one
        [Then(@"I purchase all from cart")]
        public void ThenIPurchaseAllFromCart()
        {

            cartPage.BuyAll();
        }


        [When(@"I fill the required data for a new message")]
        public void WhenIFillTheRequiredDataForANewMessage()
        {
            contactUs.FillDataRequired("bla@gmail.com", "User", " My message");
        }

        [Then(@"I can send the message")]
        public void ThenICanSendTheMessage()
        {
            contactUs.SendMessageButton();

            Assert.AreEqual("Sign up successful.", AlertPresent().Text);
            AlertPresent().Accept();//click on submit button
        }


        [When(@"I dont fill the required data to purchase")]
        public void WhenIDontFillTheRequiredDataToPurchase()
        {
            cartPage.FillRequiredData("", "RO", "", "111-222-333-444", "FEB", "2025");
        }

        [Then(@"I can't buy what is in cart")]
        public void ThenICanTBuyWhatIsInCart()
        {
            cartPage.ConfirmOrder();
            Assert.AreEqual("Please fill out Name and Creditcard.", AlertPresent().Text);
            AlertPresent().Accept();//click on submit button
        }

        [When(@"I don't fill the required data for a new message")]
        public void WhenIDonTFillTheRequiredDataForANewMessage()
        {
            contactUs.FillDataRequired("", "", "");
        }

        [Then(@"My message is not sent")]
        public void ThenMyMessageIsNotSent()
        {
            contactUs.SendMessageButton();
            Assert.AreNotEqual("Thanks for the message!!", AlertPresent().Text);
            AlertPresent().Accept();//click on submit button
        }

        [Then(@"My message is sent")]
        public void ThenMyMessageIsSent()
        {
            contactUs.SendMessageButton();
            Assert.AreEqual("Thanks for the message!!", AlertPresent().Text);
            AlertPresent().Accept();//click on submit button

        }


        public IAlert AlertPresent()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            IAlert popOut = _driver.SwitchTo().Alert();

            return popOut;
        }

        [AfterScenario()]
        public void DisposeWebDriver()
        {

            _driver.Dispose();
        }

        [BeforeScenario("EmptyCart")]
        public void Precond()
        {
            homePage.Login();
            homePage.GoToCartPage();
            ThenIRemoveThem();
            homePage.GoToLogOut();
        }
    }
}

// Looks good overall, try to implement things in feedback. 
// Also try adding a folder structure, for page objects/features.
// Feature files can stay in the same folder as steps.cs files