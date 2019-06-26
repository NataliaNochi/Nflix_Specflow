using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using Nflix.Features.Pages;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Reflection;
using TechTalk.SpecFlow;

namespace Nflix.Features.Support
{
    [Binding]
    public class Hooks : Steps
    {
        private readonly IObjectContainer _objectContainer;

        private RemoteWebDriver _driver;

        ExtentTest featureTitle;
        ExtentTest scenarioDescription;
        ExtentReports report;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            var htmlReport = new ExtentHtmlReporter(@"C:\Users\natal\Desktop\Teste\Specflow\Nflix\Nflix\Features\Support\Log\ExtentReport.html");
            htmlReport.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            report = new ExtentReports();
            report.AttachReporter(htmlReport);

            _driver = new FirefoxDriver();
            _objectContainer.RegisterInstanceAs<RemoteWebDriver>(_driver);

            featureTitle = report.CreateTest<Feature>(this.FeatureContext.FeatureInfo.Title);
            scenarioDescription = featureTitle.CreateNode<Scenario>(this.ScenarioContext.ScenarioInfo.Title);
        }

        [BeforeScenario(Order = 1)]
        [Scope(Tag = "login")]
        public void BeforeTagLogin()
        {
            LoginPage login_page = new LoginPage(_driver);
            login_page.Access_URL("http://192.168.99.100:8080/login");
            login_page.Login("tony@stark.com", "123456");
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepType = this.StepContext.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(this.ScenarioContext, null);

            if (this.ScenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenarioDescription.CreateNode<Given>(this.StepContext.StepInfo.Text);

                else if (stepType == "When")
                    scenarioDescription.CreateNode<When>(this.StepContext.StepInfo.Text);

                else if (stepType == "Then")
                    scenarioDescription.CreateNode<Then>(this.StepContext.StepInfo.Text);

                else if (stepType == "And")
                    scenarioDescription.CreateNode<And>(this.StepContext.StepInfo.Text);
            }
            else if (this.ScenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenarioDescription.CreateNode<Given>(this.StepContext.StepInfo.Text).Fail(this.ScenarioContext.TestError.InnerException);

                else if (stepType == "When")
                    scenarioDescription.CreateNode<When>(this.StepContext.StepInfo.Text).Fail(this.ScenarioContext.TestError.Message);

                else if (stepType == "Then")
                    scenarioDescription.CreateNode<Then>(this.StepContext.StepInfo.Text).Fail(this.ScenarioContext.TestError.Message);

                else if (stepType == "And")
                    scenarioDescription.CreateNode<And>(this.StepContext.StepInfo.Text).Fail(this.ScenarioContext.TestError.Message);
            }

            else if (this.ScenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenarioDescription.CreateNode<Given>(this.StepContext.StepInfo.Text).Skip("Step Definition Pending");

                else if (stepType == "When")
                    scenarioDescription.CreateNode<When>(this.StepContext.StepInfo.Text).Skip("Step Definition Pending");

                else if (stepType == "Then")
                    scenarioDescription.CreateNode<Then>(this.StepContext.StepInfo.Text).Skip("Step Definition Pending");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
            report.Flush();
        }

    }
}
