﻿using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using Xunit;

namespace TheMovieDB
{
    public class MovieTest : IDisposable
    {
        private IWebDriver driver;
        private SearchPageObject SearchObject;


        [Theory]
        [InlineData("am"                 , "movie_6479")]
        [InlineData("AM"                 , "movie_6479")]   //case sensitive
        [InlineData("AM "                , "movie_6479")]
        [InlineData(" AM"                , "movie_6479")]
        [InlineData("a"                  , "movie_484247")]  // minimal number of leters to run search
        [InlineData("miś"                , "movie_31407")]   // national characters - polish
        [InlineData("影武者"              , "movie_11953")]    // japanese Unicode
        [InlineData("' or 1=1 --"        , "movie_113082")]   // sql injection
        [InlineData("Kit Harington"      , "person_239019")]
        [InlineData("star is born"       , "movie_19610")]
        [InlineData("star is born y:2018", "movie_332562")]
        [InlineData("star is born Y:2018", "movie_332562")]
        [InlineData("star is borny:2018" , "movie_332562")]
        [InlineData("star is born y:"    , "movie_3111")]
        [InlineData("Game of Thrones"    , "tv_1399")]
        public void TestsSearch(string Query, string Result)
        {


            SearchObject.Search(Query);

            IWebElement IAm = driver.FindElement(By.Id(Result));
            Assert.NotNull(IAm);
        }

        [Theory]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do " +
            "eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad " +
            "minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip " +
            "ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate " +
            "velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat " +
            "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est " +
            "laborum.")]  // test lenght of query, it seems there is no limit
        [InlineData("star is borny :2018")]
        [InlineData("star is born y:18")]
        [InlineData("star is born y: 2018")]
        [InlineData("")]
        [InlineData(" ")]
        public void TestsSearchToNoResults(string Query)

        {

            SearchObject.Search(Query);


            IWebElement IAm = driver.FindElement(By.XPath("//*[@id=\"main\"]/div/section/section/div[1]/div/p"));
            Assert.NotNull(IAm);
        }

        [Fact]
        public void JavaScriptInjectionTest()
        {


            SearchObject.Search("<script>alert('Hello World!');</script>");

            Assert.Throws<NoAlertPresentException>(() => driver.SwitchTo().Alert());
        }


        [Fact]
        public void SecondSearchTest()
        {


            SearchObject.Search("Star Wars");

            IWebElement Result = driver.FindElement(By.Id("movie_11"));

            SearchObject.Search("Star Trek");

            Result = driver.FindElement(By.Id("tv_253"));

            Assert.NotNull(Result);

        }

        [Theory]
        [InlineData("tv"        , "//*[@id=\"tv_41759\"]")]
        [InlineData("movie"     , "//*[@id=\"movie_567098\"]")]
        [InlineData("person"    , "//*[@id=\"person_1208627\"]")]
        [InlineData("company"   , "//*[@id=\"main\"]/div/section/section/div/div/ul/li[1]/a/div")]
        [InlineData("keyword"   , "//*[@id=\"main\"]/div/section/section/div/div/ul/li[1]/a")]
        [InlineData("collection", "//*[@id=\"main\"]/div/section/section/div/div/div[1]/div[1]/a/div")]
        [InlineData("network"   , "//*[@id=\"main\"]/div/section/section/div/div/ul/li/a/div/img")]
        public void TestsOfCategories(string Category, string Result)
        {


            SearchObject.Search("ama");

            IWebElement SearchCategory = driver.FindElement(By.Id(Category));

            SearchCategory.Click();

            IWebElement IAm = driver.FindElement(By.XPath(Result));
            Assert.NotNull(IAm);
        }

        public MovieTest()
        {
            driver = new EdgeDriver();
            SearchObject = new SearchPageObject(driver);
            driver.Manage().Timeouts().ImplicitWait = Config.TimeOut;

            driver.Url = "https://www.themoviedb.org/";

        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}