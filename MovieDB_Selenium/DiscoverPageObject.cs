using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace TheMovieDB
{
    internal class DiscoverPageObject
    {
        private IWebDriver driver;

        public DiscoverPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ChooseGenre(string Genre)
        {
            if (String.IsNullOrEmpty(Genre)) { return; }

            IWebElement Genres = driver.FindElement(By.XPath("//*[@id=\"discover\"]/span[3]/div/div/input"));
            Genres.Click();

            By CategoryCrimexPath = By.XPath("//*[@id=\"with_genres_listbox\"]/li[contains(text(), '" + Genre + "')]");
            IWebElement CategorySelected = driver.FindElement(CategoryCrimexPath);

            WebDriverWait CategoryVisible = new WebDriverWait(driver, Config.TimeOut);
            CategoryVisible.Until(ExpectedConditions.ElementToBeClickable(CategoryCrimexPath));
            CategorySelected.Click();
        }

        public void ChooseYear(string Year)
        {
            IWebElement YearList = driver.FindElement(By.XPath("//*[@id=\"discover\"]/span[1]/span"));
            YearList.Click();

            By YearSelectionPath = By.XPath("//*[@id=\"year_listbox\"]/li[contains(text(), '" + Year + "')]");
            IWebElement YearSelected = driver.FindElement(YearSelectionPath);

            WebDriverWait YearVisible = new WebDriverWait(driver, Config.TimeOut);
            YearVisible.Until(ExpectedConditions.ElementToBeClickable(YearSelectionPath));

            YearSelected.Click();
        }

        public void ChooseSortBy(string SortMetod)
        {
            IWebElement SortList = driver.FindElement(By.XPath("//*[@id=\"discover\"]/span[2]/span/span/span[1]"));
            SortList.Click();

            By SortSelectionPath = By.XPath("//*[@id=\"sort_by_listbox\"]/li[contains(text(), '" + SortMetod + "')]");
            IWebElement SortSelected = driver.FindElement(SortSelectionPath);

            WebDriverWait SortVisible = new WebDriverWait(driver, Config.TimeOut);
            SortVisible.Until(ExpectedConditions.ElementToBeClickable(SortSelectionPath));
            
            SortSelected.Click();
        }

     
    }
}