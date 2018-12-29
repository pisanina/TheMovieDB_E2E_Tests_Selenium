using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System;
using Xunit;

namespace TheMovieDB
{
    public class DiscoverTests
    {
        private IWebDriver driver;

        [Fact]
        public void Discover()
        {
            driver.Url = "https://www.themoviedb.org/discover/movie";

            IWebElement Year = driver.FindElement(By.XPath("//*[@id=\"discover\"]/span[1]/span"));
            Year.Click();
            Year.SendKeys(Keys.ArrowDown);
            Year.Click();

            IWebElement Sort = driver.FindElement(By.Id("sort_by_label"));

            Sort.Click();
            Actions action = new Actions(driver);
            action.SendKeys(Keys.ArrowDown).Build().Perform();

            Sort.Click();

            IWebElement Genres = driver.FindElement(By.XPath("//*[@id=\"discover\"]/span[3]/div/div/input"));
            Genres.Click();
            Genres.SendKeys("comedy");
            Genres.SendKeys(Keys.Enter);

            Assert.NotNull(driver.FindElement(By.Id("movie_571177")));
        }

        public DiscoverTests()
        {
            driver = new EdgeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}