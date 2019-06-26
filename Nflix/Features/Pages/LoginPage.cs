using Nflix.Features.Support.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Nflix.Features.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(RemoteWebDriver _driver) : base(_driver)
        {
        }

        public void Access_URL(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void Login(string email, string pass)
        {
            IWebElement user = this.GetElement("emailId", EnumElementTypes.Id);
            user.SendKeys(email);

            IWebElement password = this.GetElement("passId", EnumElementTypes.Id);
            password.SendKeys(pass);

            this.GetElement("btn-danger", EnumElementTypes.Class).Click();
        }

        public string Alert()
        {
            IWebElement alert = this.GetElement("alert", EnumElementTypes.Class);
            return alert.Text;
        }
    }
}
