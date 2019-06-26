using Nflix.Features.Support.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace Nflix.Features.Pages
{
    public class MoviePage : BasePage
    {
        public MoviePage(RemoteWebDriver _driver) : base(_driver)
        {
        }

        public void New_Movie()
        {
            this.GetElement("movie-add", EnumElementTypes.Class).Click();
        }

        public string Contains_Movie(string title)
        {
            string new_movie_added = null;

            this.GetElement("table", EnumElementTypes.Class);

            IList<IWebElement> registered_movies = _driver.FindElements(By.CssSelector("table tbody tr td:nth-child(2)"));

            foreach (var movie in registered_movies)
            {
                if (movie.Text.Equals(title))
                {
                    new_movie_added = movie.ToString();
                    break;
                }
            }
            return new_movie_added;
        }
    }
}