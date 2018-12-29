using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using Xunit;

namespace TheMovieDB
{
   public  class SearchPageObject
    {
        private IWebDriver driver;

        public SearchPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Search(string Query)
        {
            IWebElement Search = driver.FindElement(By.Id("search_v4"));
            Search.Clear();
            Search.SendKeys(Query);
            Search.SendKeys(Keys.Enter);

        }

    }
}
