using Newtonsoft.Json;
using Nflix.Features.Pages;
using Nflix.Features.Pages.Views;
using Nflix.Features.Support;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using TechTalk.SpecFlow;
using Xunit;

namespace Nflix.Features.StepsDefinition
{
    [Binding]
    public class MovieRegisterSteps
    {
        private RemoteWebDriver _driver;
        public MovieRegisterSteps(RemoteWebDriver driver) => _driver = driver;

        MoviePage movie_page;
        NewMovieView movie_view;
        Database database;

        string move_code = null;
        dynamic movies;

        [Given(@"a code ""(.*)"" of a new movie")]
        public void GivenACodeOfANewMovie(string code)
        {
            movie_page = new MoviePage(_driver);
            movie_view = new NewMovieView(_driver);

            move_code = code;
            string file = @"..\..\..\Features\Support\TestsDataJson\MoviesData.json";
            StreamReader json = new StreamReader(file);
            var data = json.ReadToEnd();
            movies = JsonConvert.DeserializeObject<dynamic>(data);

            using (database = new Database())
            {
                database.Execute_Sql("delete from movies where title = '" + movies[move_code].title + "'");
            }
        }

        [When(@"the manager register this movie")]
        public void WhenTheManagerRegisterThisMovie()
        {
            movie_page.New_Movie();
            movie_view.Register_Movie(movies[move_code]);
        }


        [Then(@"the movie should be seen on the catalog")]
        public void ThenTheMovieShouldBeSeenOnTheCatalog()
        {
            Assert.NotNull(movie_page.Contains_Movie(movies[move_code].title.ToString()));
        }
    }
}
