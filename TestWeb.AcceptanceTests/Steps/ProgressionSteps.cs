﻿using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Helpers;
using TechTalk.SpecFlow;
using TestWeb.AcceptanceTests.Helpers;
using TestWeb.AcceptanceTests.Pages;
using TestWeb.TestApi.Client;

namespace TestWeb.AcceptanceTests.Steps
{
    [Binding]
    public class ProgressionSteps
    {
        private readonly UserBrowser _browser;
        private readonly TestContext _c;
        private readonly BrowserSteps _browserSteps;
        private readonly CreateHearingsSteps _createHearingsSteps;
        private readonly LoginSteps _loginSteps;

        public ProgressionSteps(UserBrowser browser, TestContext testContext, LoginSteps loginSteps, BrowserSteps browserSteps, CreateHearingsSteps createHearingsSteps)
        {
            _browser = browser;
            _c = testContext;
            _loginSteps = loginSteps;
            _browserSteps = browserSteps;
            _createHearingsSteps = createHearingsSteps;
        }

        [Given(@"the user has progressed to the Create Hearings page")]
        public void GivenTheUserHasProgressedToTheCreateHearingsPage()
        {
            _browserSteps.GivenANewBrowserIsOpenForAUser(UserType.Tester.ToString());
            _loginSteps.ProgressToNextPage();
        }

        [Given(@"the user has progressed to the Delete Hearings page with a hearing")]
        public void GivenTheUserHasProgressedToTheDeleteHearingsPageWithHearing()
        {
            _browserSteps.GivenANewBrowserIsOpenForAUser(UserType.Tester.ToString());
            _loginSteps.ProgressToNextPage();
            _createHearingsSteps.ProgressToNextPage();
        }

        [Given(@"the user has progressed to the Delete Hearings page")]
        public void GivenTheUserHasProgressedToTheDeleteHearingsPageWithoutHearing()
        {
            _browserSteps.GivenANewBrowserIsOpenForAUser(UserType.Tester.ToString());
            _loginSteps.ProgressToNextPage();
            _browser.Driver.WaitUntilVisible(HeaderPage.DeleteHearingsLink);
            _browser.ClickLink(HeaderPage.DeleteHearingsLink);
        }
    }
}