using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Remote;

namespace Nflix.Features.Support
{
    public class Helpers
    {
        private readonly RemoteWebDriver _driver;
        public Helpers(RemoteWebDriver driver) => _driver = driver;

        public string Get_Token()
        {
            string script = "return window.localStorage.getItem('default_auth_token');";
            IJavaScriptExecutor js = _driver as IJavaScriptExecutor;
            return js.ExecuteScript(script)?.ToString();

        }
    }
}
