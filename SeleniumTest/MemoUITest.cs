using System;
using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace SeleniumTest
{
    public class MemoUITest : IDisposable
    {
        private readonly IWebDriver _driver;
        private const string _USER_EMAIL = "testacc@gmail.com";
        private const string _PASSWORD="Testacc1@";
        private const string _BASE_URL = "https://localhost:5001";

        public MemoUITest()
        {
            _driver = new ChromeDriver("./");
        }


        private void Login()
        {
            _driver.Navigate().GoToUrl(_BASE_URL+"/Identity/Account/Login");
            IWebElement email = _driver.FindElement(By.Name("Input.Email"));
            email.SendKeys(_USER_EMAIL);
            Delay();

            IWebElement password = _driver.FindElement(By.Name("Input.Password"));
            password.SendKeys(_PASSWORD);
            Delay();

            IWebElement loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();
            Delay();

        }

        [Fact]
        public void LoadHomePageTestTitle()
        {
            _driver.Navigate().GoToUrl(_BASE_URL);
            Assert.Equal("Home Page - memo", _driver.Title);
        }

        [Fact]
        public void LoginTest()
        {
            Login();    
            Assert.Equal("Index - memo", _driver.Title);

        }

        [Fact]
        public void CreateMemoTestFailedNoDate()
        {
            //You need to login frist to be able to  see the memo Index page

            Login();

            _driver.Navigate().GoToUrl(_BASE_URL+"/Memos");
            Delay();
            IWebElement create = _driver.FindElement(By.LinkText("Create New"));
            create.Click();

            Delay();
            IWebElement title = _driver.FindElement(By.Name("Title"));
            title.SendKeys("Demo Project");
            Delay();


            IWebElement details = _driver.FindElement(By.Name("Details"));
            details.SendKeys("Demo GIT Project to the tutor");
            Delay();

            IWebElement createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();
            Delay();

            IWebElement errorMessage = _driver.FindElement(By.CssSelector(".validation-summary-errors ul > li"));
            Assert.Equal("Please enter date and time", errorMessage.Text);

        }

        [Fact]
        public void LogoutTest()
        {
            Login();
            IWebElement acceptCookieButton = _driver.FindElement(By.CssSelector("button.btn.btn-default.navbar-btn"));
            acceptCookieButton.Click();
            Delay();
            IWebElement logoutButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            logoutButton.Click();
            Delay();
            Assert.Equal("Home Page - memo", _driver.Title);
        }


        private static void Delay()
        {
            Thread.Sleep(1000);
        }


        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

    }
}
