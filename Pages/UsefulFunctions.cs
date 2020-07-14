using ConsoleApp1;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace DemoBlaze
{
    public class UsefulFunctions
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "https://www.demoblaze.com/";
        HomePage homePage;
        CartPage cartPage;
        ContactUs contactUs;
        ProductPage prodPage;
        WebDriverWait wait;
   

        private IWebElement _loginButton() => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("login2")));
        public UsefulFunctions(IWebDriver driver)
        {
            Driver = driver;
            homePage = new HomePage(Driver);
            cartPage = new CartPage(Driver);
            prodPage = new ProductPage(Driver);
            contactUs = new ContactUs(Driver);
            wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 5));
        }

        public void AddLaptopMonitoPhoneToCartBasedonBudget(int budget)
        {
            int countPhones, countMonitors, countLaptops, productPriceM, productPriceL, productPriceP;

            bool passflag = false;

            int i, j, k, Sum;
            homePage.FilterByPhone();
            countPhones = homePage.CountElements();

            homePage.FilterbyMonitors();
            countMonitors = homePage.CountElements();
           
            homePage.FilterbyLaptops();
            countLaptops = homePage.CountElements();
          

            string product;

            for (i = 1; i < countPhones && (passflag != true); i++)
            {
                Console.WriteLine("Enteres for");
                homePage.FilterByPhone();
                product = "//*[@id=\"tbodyid\"]/div[" + i + "]/div/div/h5";
                productPriceP = homePage.ProductPrice(product);


                for (j = 1; j <= countLaptops && (passflag != true); j++)
                {

                    homePage.FilterbyLaptops();
                    product = "//*[@id=\"tbodyid\"]/div[" + j + "]/div/div/h5";
                    productPriceL = homePage.ProductPrice(product);

                    for (k = 1; (k <= countMonitors); k++)
                    {

                        homePage.FilterbyMonitors();
                        product = "//*[@id=\"tbodyid\"]/div[" + k + "]/div/div/h5";
                        productPriceM = homePage.ProductPrice(product);

                        Sum = productPriceM + productPriceL + productPriceP;
                        Console.WriteLine(Sum);

                        if (Sum <= budget)
                        {
                            homePage.NavigateTo();
                            homePage.FilterByPhone();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"tbodyid\"]/div[" + i + "]/div/div/h4/a")));
                            Driver.FindElement(By.XPath("//*[@id=\"tbodyid\"]/div[" + i + "]/div/div/h4/a")).Click();
                            prodPage.AddToCart();

                           
                            homePage.FilterbyMonitors();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"tbodyid\"]/div[" + k + "]/div/div/h4/a")));
                            Driver.FindElement(By.XPath("//*[@id=\"tbodyid\"]/div[" + k + "]/div/div/h4/a")).Click();
                            prodPage.AddToCart();


                            homePage.FilterbyLaptops();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"tbodyid\"]/div[" + j + "]/div/div/h4/a")));
                            Driver.FindElement(By.XPath("//*[@id=\"tbodyid\"]/div[" + j + "]/div/div/h4/a")).Click();
                            prodPage.AddToCart();

                            Console.WriteLine("We can buy them");

                            passflag = true;
                        }
                        else
                            Console.WriteLine("Not enough money");



                    }

                }

            }
        }

         public string Random(int length)
            {
                Random ran = new Random();

                String b = "abcdefghijklmnopqrstuvwxyz";

                String random = "";

                for (int i = 0; i < length; i++)
                {
                    int a = ran.Next(26);
                    random = random + b.ElementAt(a);
                }

                return random;
            }
        
    }


}
