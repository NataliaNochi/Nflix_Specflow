using Nflix.Features.Support.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
namespace Nflix.Features.Pages
{
    public class BasePage
    {
        protected RemoteWebDriver _driver;
        public BasePage(RemoteWebDriver driver) => _driver = driver;

        protected WebDriverWait wait;

        protected IWebElement GetElement(string value, EnumElementTypes type)
        {
            IWebElement element = null;
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            switch (type)
            {
                case EnumElementTypes.Id:
                    element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id(value)));
                    break;
                case EnumElementTypes.Class:
                    element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName(value)));
                    break;
                case EnumElementTypes.Name:
                    element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name(value)));
                    break;
                case EnumElementTypes.Css:
                    element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(value)));
                    break;
            }
            return element;
        }
    }
}
