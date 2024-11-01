using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace STACodeChallenge.Tests.StepDefinitions
{
    [Binding]
    public class TfLJourneyPlannerSteps
    {
        private readonly IPage _page;
        private readonly IBrowser _browser;

        public TfLJourneyPlannerSteps()
        {
            var playwright = Playwright.CreateAsync().Result;
            _browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }).Result;
            _page = _browser.NewPageAsync().Result;
        }

        [Given(@"I navigate to the TfL homepage")]
        public async Task GivenINavigateToTheTfLHomepage()
        {
            await _page.GotoAsync("https://tfl.gov.uk/");
        }
        
        [Given(@"I accept cookies")]
         public async Task GivenIAcceptCookies()
         {
             await _page.WaitForSelectorAsync("#cb-cookieoverlay", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
             var cookieButton = _page.Locator("#cb-cookieoverlay #CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
             if (await cookieButton.IsVisibleAsync())
             {
                 await cookieButton.ClickAsync();
             }
             await _page.EvaluateAsync("document.querySelector('#cb-cookieoverlay').remove()");

         }

        [Given(@"I open the ""(.*)"" widget")]
        public async Task GivenIOpenTheWidget(string widgetName)
        {
            await _page.WaitForSelectorAsync("text='Plan a journey'", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
        }

        [When(@"I enter ""(.*)"" as the starting point")]
        public async Task WhenIEnterAsTheStartingPoint(string startLocation)
        {
            await _page.FillAsync("#InputFrom", startLocation);
            await _page.Locator(".tt-suggestions .tt-suggestion").First.ClickAsync();
        }

        [When(@"I enter ""(.*)"" as the destination")]
        public async Task WhenIEnterAsTheDestination(string destination)
        {
            await _page.FillAsync("#InputTo", destination);
            await _page.Locator(".tt-suggestions .tt-suggestion").First.ClickAsync();
        }

        [When(@"I enter invalid value ""(.*)"" as the starting point")]
        public async Task WhenIEnterInvalidValueAsTheStartingPoint(string startLocation)
        {
            await _page.FillAsync("#InputFrom", startLocation);
            await _page.PressAsync("#InputFrom", "Enter");

        }

        [When(@"I enter invalid value ""(.*)"" as the destination")]
        public async Task WhenIEnterInvalidValueAsTheDestination(string destination)
        {
            // await _page.WaitForTimeoutAsync(2000);  // Waits for 2 seconds
            await _page.FillAsync("#InputTo", destination);
            await _page.PressAsync("#InputTo", "Enter");
        }

        [When(@"I click on plan a journey button")]
        public async void WhenIClickOnPlanAJourneyButton()
        {
            await _page.WaitForSelectorAsync("#plan-journey-button");
            await _page.ClickAsync("#plan-journey-button");

        }

        [Then(@"I should see journey options for walking and cycling")]
        public async Task ThenIShouldSeeJourneyOptionsForWalkingAndCycling()
        {
            await _page.WaitForSelectorAsync("h4:has-text('Cycling')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });

            Assert.IsTrue(await _page.Locator("h4:has-text('Cycling')").IsVisibleAsync());
            Assert.IsTrue(await _page.Locator("h4:has-text('Walking')").IsVisibleAsync());
        }
        
        [When(@"I select a travel mode of ""(.*)""")]
        public async Task WhenISelectATravelModeOf(string mode)
        {
            await _page.WaitForSelectorAsync("h4:has-text('"+mode+"')", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
            await _page.ClickAsync("h4:has-text('"+mode+"')");
        }

         [Then(@"I should see journey options with a valid journey time for both walking and cycling")]
         public async Task ThenIShouldSeeJourneyOptionsWithAValidJourneyTime()
         {
             await _page.WaitForSelectorAsync(".summary-results", new PageWaitForSelectorOptions { State = WaitForSelectorState.Visible });
             Assert.IsTrue(await _page.Locator(".summary-results .journey-summary").First.IsVisibleAsync());
         }
        
        [GivenAttribute(@"I have planned a journey from ""(.*)"" to ""(.*)""")]
        public async void GivenIHavePlannedAJourneyFromTo(string startLocation, string destination)
        {
            await _page.FillAsync("#InputFrom", startLocation);
            await _page.Locator(".tt-suggestions .tt-suggestion").First.ClickAsync();
            await _page.FillAsync("#InputTo", destination);
            await _page.Locator(".tt-suggestions .tt-suggestion").First.ClickAsync();
            await _page.Locator("#plan-journey-button").ClickAsync();
        }

        [When(@"I click on edit preferences")]
        public async Task WhenIClickOnEditPreferences()
        {
            await _page.ClickAsync("button:has-text('Edit preferences')");
        }
        
        [When(@"I edit journey preferences to select routes with ""(.*)""")]
        public async Task WhenIEditJourneyPreferences(string preference)
        {
             await _page.Locator($"text={preference}").ClickAsync();
        }
        
        [When(@"I update the journey")]
        public async Task WhenIUpdateTheJourney()
        {
           await _page.Locator("text='Update journey'").Last.ClickAsync();
           await _page.WaitForSelectorAsync("button:has-text('View details')");
           await _page.WaitForTimeoutAsync(5000);
        }


        [Then(@"I should see the journey time updated according to the preference")]
        public async Task ThenIShouldSeeTheJourneyTimeUpdatedAccordingToThePreference()
        {
            var updatedTime = await _page.TextContentAsync(".time");
            Assert.IsNotNull(updatedTime);
        }

        [Then(@"I should see complete access information at ""(.*)""")]
        public async Task ThenIShouldSeeCompleteAccessInformationAt(string station)
        {
            var accessInfo = await _page.QuerySelectorAsync($"text='{station}'");
            Assert.IsNotNull(accessInfo, "Access information not found.");
        }

        [Then(@"I should see an error message indicating that no results were found")]
        public async Task ThenIShouldSeeAnErrorMessageIndicatingThatNoResultsWereFound()
        {
            var errorMsg = await _page.TextContentAsync(".field-validation-error");
            Assert.NotNull(errorMsg);
        }

        [Then(@"I should see an error message indicating that locations are required")]
        public async Task ThenIShouldSeeAnErrorMessageIndicatingThatLocationsAreRequired()
        {
            var errorMsgTo = await _page.TextContentAsync("#InputTo-error");
            Assert.IsNotNull(errorMsgTo, "The To field is required.");
            var errorMsgFrom = await _page.TextContentAsync("#InputFrom-error");
            Assert.IsNotNull(errorMsgFrom, "The From field is required.");
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _page.CloseAsync();
            await _browser.CloseAsync();
        }

    }
}
