using Newtonsoft.Json;
using Nflix.Features.Support.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.IO;

namespace Nflix.Features.Pages.Views
{
    public class NewMovieView : BasePage
    {
        public NewMovieView(RemoteWebDriver _driver) : base(_driver) { }

        public void Register_Movie(dynamic movie)
        {
            IWebElement title = this.GetElement("title", EnumElementTypes.Name);
            title.SendKeys(movie.title.ToString());

            if (movie.status != null)
            {
                IWebElement status = this.GetElement("el-input__inner", EnumElementTypes.Class);
                status.Click();

                IWebElement status_type = this.GetElement("el-select-dropdown__item", EnumElementTypes.Class);
                status_type.Text.Equals(movie.status.ToString());
                status_type.Click();
            }

            IWebElement year = this.GetElement("year", EnumElementTypes.Name);
            year.SendKeys(movie.year.ToString());

            IWebElement release_date = this.GetElement("release_date", EnumElementTypes.Name);
            release_date.SendKeys(movie.release_date.ToString());

            Add_Cast(movie.cast);

            IWebElement overview = this.GetElement("overview", EnumElementTypes.Name);
            overview.SendKeys(movie.overview.ToString());

            if (movie.image != null)
                Upload(movie.image.ToString());

            this.GetElement("create-movie", EnumElementTypes.Id).Click();
        }

        public void Add_Cast(dynamic actorList)
        {
           IWebElement add_actor = this.GetElement("cast", EnumElementTypes.Class);
            foreach (var actor in actorList)
            {
                add_actor.SendKeys(actor.ToString());
                add_actor.SendKeys(Keys.Tab);
            }
        }

        public void Upload(string file_name)
        {
            string file = @"..\..\..\Features\Support\Images\" + file_name;
            IWebElement upload = this.GetElement("upcover", EnumElementTypes.Id);
            upload.SendKeys(Path.GetFullPath(file));
        }
    }
}
