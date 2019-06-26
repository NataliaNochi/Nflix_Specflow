using Nflix.Features.Pages;
using Nflix.Features.Pages.Views;
using Nflix.Features.Support;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;
using Xunit;

namespace Nflix.features.steps_definition
{
    [Binding]
    public class LoginSteps
    {
        private RemoteWebDriver _driver;
        public LoginSteps(RemoteWebDriver driver) => _driver = driver;

        private LoginPage login_page;
        private Helpers helper;
        private SidebarView sidebar_view;

        [Given(@"the access to the login page")]
        public void GivenTheAccessToTheLoginPage()
        {
            login_page = new LoginPage(_driver);
            helper = new Helpers(_driver);
            sidebar_view = new SidebarView(_driver);

            login_page.Access_URL("http://192.168.99.100:8080/login");

        }

        [When(@"a user do the login with ""(.*)"" and ""(.*)""")]
        public void WhenAUserDoTheLoginWithAnd(string email, string password)
        {
            login_page.Login(email, password);
        }

        [Then(@"the user must be authenticated")]
        public void ThenTheUserMustBeAuthenticated()
        {
            Assert.Equal(147, helper.Get_Token().Length);
        }

        [Then(@"the name ""(.*)"" must be seen in the logging area")]
        public void ThenTheNameMustBeSeenInTheLoggingArea(string user_name)
        {
            Assert.Equal(user_name, sidebar_view.Logged_User());
        }

        //Login Failed

        [Then(@"the user must not be authenticated")]
        public void ThenTheUserMustNotBeAuthenticated()
        {
            Assert.Null(helper.Get_Token());
        }

        [Then(@"the alert message ""(.*)"" must be seen")]
        public void ThenTheAlertMessageMustBeSeen(string message)
        {
            Assert.Equal(message, login_page.Alert());
        }
    }
}
