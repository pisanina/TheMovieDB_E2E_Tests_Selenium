using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using Xunit;

namespace TheMovieDB
{
    public class DiscoverTests : IDisposable
    {
        private IWebDriver driver;
        private DiscoverPageObject DiscoverSearch;


        [Fact]
        public void DiscoverManyGenresTest()
        {
            DiscoverSearch.ChooseYear("2017");
            DiscoverSearch.ChooseSortBy("Release Date Ascending");
            DiscoverSearch.ChooseGenre("Action");
            DiscoverSearch.ChooseGenre("Comedy");
            DiscoverSearch.ChooseGenre("Family");
            DiscoverSearch.ChooseGenre("Music");

            Assert.NotNull(driver.FindElement(By.Id("movie_417320")));
        }

        [Fact]
        public void DiscoverGenreJavaScriptInjectionTest()
        {
            DiscoverSearch.ChooseYear("2017");
            DiscoverSearch.ChooseSortBy("Release Date Ascending");

            IWebElement Genres = driver.FindElement(By.XPath("//*[@id=\"discover\"]/span[3]/div/div/input"));
            Genres.Click();

            Genres.SendKeys("<script>alert('Hello World!');</script>");

            Assert.Throws<NoAlertPresentException>(() => driver.SwitchTo().Alert());
        }

        [Theory]
        [InlineData("1900", "Release Date Ascending", "", "movie_195608")]
        [InlineData("1950", "Release Date Ascending", "", "movie_525111")]
        [InlineData("None", "Release Date Ascending", "", "movie_315946")]
        [InlineData("2000", "Popularity Descending", "", "movie_8871")]
        [InlineData("2000", "Popularity Ascending", "", "movie_571402")]
        [InlineData("2000", "Rating Descending", "", "movie_544919")]
        [InlineData("2000", "Rating Ascending", "", "movie_571402")]
        [InlineData("2000", "Release Date Descending", "", "movie_79563")]
        [InlineData("2000", "Release Date Ascending", "", "movie_505505")]
        [InlineData("2000", "Title (A-Z)", "", "movie_277289")]
        [InlineData("2000", "Title (Z-A)", "", "movie_426967")]
        [InlineData("2010", "Release Date Ascending", "Comedy", "movie_269640")]
        [InlineData("2010", "Release Date Ascending", "War", "movie_147503")]
        public void TestsDiscover(string Year, string Sort, string Genre, string Result)
        {
            DiscoverSearch.ChooseYear(Year);
            DiscoverSearch.ChooseSortBy(Sort);
            DiscoverSearch.ChooseGenre(Genre);

            Assert.NotNull(driver.FindElement(By.Id(Result)));
        }

        public DiscoverTests()
        {
            driver = new EdgeDriver();
            DiscoverSearch = new DiscoverPageObject(driver);
            driver.Manage().Timeouts().ImplicitWait = Config.TimeOut;
            driver.Url = "https://www.themoviedb.org/discover/movie";
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}