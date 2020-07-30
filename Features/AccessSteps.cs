using ConsoleApp1;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Xml.Serialization;
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
        SignUp signup;
        readonly Constants constantVariable;
        WebDriverWait wait;
        ITakesScreenshot screenShootError;
        string productName;
        int productPrice;

        Boolean failFlag = false;

        public RegisterSteps()
        {
            homePage = new HomePage(_driver);
            cartPage = new CartPage(_driver);
            prodPage = new ProductPage(_driver);
            contactUs = new ContactUs(_driver);
            signup = new SignUp(_driver);
            usefulFunction = new UsefulFunctions(_driver);
            constantVariable = new Constants();
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
            homePage.EnterLoginInfo(constantVariable.UserName, constantVariable.Password);
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

        [When(@"I fill in required data for (.*)")]
        public void WhenIFillInRequiredData(string userName)
        {
            SignUp LogINPage = new SignUp(_driver);

            if (userName == "RandomUser")
            {
                userName = usefulFunction.Random(6);
                LogINPage.EnterSignInInfo(userName, constantVariable.Password);
                LogINPage.SignInButton();
            }
            else
            {

                LogINPage.EnterSignInInfo(userName, constantVariable.Password);
                LogINPage.SignInButton();
            }
        }

        [Then(@"I get registered")]
        public void ThenIGetRegistered()
        {
            Assert.AreEqual("Sign up successful.", signup.AlertText());

        }

        [Then(@"I get informed that the username is taken")]
        public void ThenIGetInformedThatTheUsernameIsTaken()
        {
            Assert.AreEqual("This user already exist.", signup.AlertText());
        }


        [When(@"I click on the “Next” buttons from Image Slider")]
        public void WhenIClickOnTheNextButtonsFromImageSlider()
        {
            mainImage = homePage.CurrentImage();

            homePage.SliderNextImage();

        }

        [When(@"I click on the “Previous” buttons from Image Slider")]
        public void WhenIClickOnThePreviousButtonsFromImageSlider()
        {

            mainImage = homePage.CurrentImage();
            homePage.SliderPreviousImage();

        }

        [Then(@"I see a different product")]
        public void ThenISeeADifferentProduct()
        {

            homePage.SliderLoading(mainImage);

            currentImage = homePage.CurrentImage();
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
           homePage.FilterByProduct(Product);

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

            }


        }

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
            Console.WriteLine("MeanValue is {0}", homePage.MeanValue());

        }

        [Given(@"I click on (.*) page")]
        public void GivenIClickOnPage(string pageType)
        {
            usefulFunction.AccessPage(pageType);

        }


        [Then(@"the correct page is displayed")]
        public void ThenTheCorrectPageIsDisplayed()
        {
            TestContext.Out.WriteLine("title: ", _driver.Title);
            Assert.AreEqual(_driver.Title, constantVariable.MainName);

        }


        [Given(@"I click on the first product available")]
        public void GivenIClickOnTheFirstProductAvailable()
        {

            homePage.FirstAvailableProduct().Click();

        }

        [Then(@"I can see it's price")]
        public void ThenICanSeeItSPrice()
        {
            Console.Out.WriteLine("The first available product costs " + prodPage.Price());
        }

        [Given(@"I have items in Cart")]
        public void GivenIHaveItemsInCart()
        {

            Assert.AreNotEqual(0, cartPage.CountElements());

        }

        [Given(@"I remove them")]
        public void ThenIRemoveThem()
        {
            cartPage.RemoveAllElementsFromCart();
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

            Assert.AreEqual(productName, cartPage.NameText());
            Assert.AreEqual(productPrice, cartPage.Price());

        }

        [Then(@"the (.*) page is displayed")]
        public void ThenThePageIsDisplayed(string wantedPage)
        {
            usefulFunction.WantedPage(wantedPage);

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
            cartPage.FillRequiredData(constantVariable.UserName, constantVariable.Country, constantVariable.City, constantVariable.Card, constantVariable.Month, constantVariable.Year);
        }
        
        [When(@"I dont fill the required data to purchase")]
        public void WhenIDontFillTheRequiredDataToPurchase()
        {
            cartPage.FillRequiredData("", constantVariable.Country, "", constantVariable.Card, constantVariable.Month, constantVariable.Year);
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
            contactUs.FillDataRequired(constantVariable.email, constantVariable.UserName, constantVariable.MessageText);
        }

        [Then(@"I can send the message")]
        public void ThenICanSendTheMessage()
        {
            contactUs.SendMessageButton();

            Assert.AreEqual("Sign up successful.", contactUs.AlertText());

        }

        [Then(@"I can't buy what is in cart")]
        public void ThenICanTBuyWhatIsInCart()
        {
            cartPage.CheckThatOrderNotFinalized();
          
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
            Assert.AreNotEqual("Thanks for the message!!", contactUs.AlertText());

        }

        [Then(@"My message is sent")]
        public void ThenMyMessageIsSent()
        {
            contactUs.SendMessageButton();
            Assert.AreEqual("Thanks for the message!!", contactUs.AlertText());

        }


        [AfterScenario()]
        public void DisposeWebDriver()
        {
            Thread.Sleep(5000);
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