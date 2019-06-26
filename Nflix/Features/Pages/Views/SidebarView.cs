using Nflix.Features.Support.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Nflix.Features.Pages.Views
{
    public class SidebarView : BasePage
    {
        public SidebarView(RemoteWebDriver _driver) : base(_driver)
        { }

        public string Logged_User()
        {
            IWebElement user_name = this.GetElement(".sidebar-wrapper .user .info span", EnumElementTypes.Css);
            return user_name.Text;
        }
    }
}
