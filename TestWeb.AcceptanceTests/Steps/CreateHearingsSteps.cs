﻿using System;
using System.Threading;
using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Helpers;
using AcceptanceTests.Common.Test.Steps;
using FluentAssertions;
using TechTalk.SpecFlow;
using TestWeb.AcceptanceTests.Data;
using TestWeb.AcceptanceTests.Helpers;
using TestWeb.AcceptanceTests.Pages;

namespace TestWeb.AcceptanceTests.Steps
{
    [Binding]
    public class CreateHearingsSteps : ISteps
    {
        private readonly  UserBrowser _browser;
        private readonly TestContext _c;
        private readonly CommonSharedSteps _commonSharedSteps;
        private int _numberOfHearings = 1;

        public CreateHearingsSteps(UserBrowser browser, TestContext testContext, CommonSharedSteps commonSharedSteps)
        {
            _browser = browser;
            _c = testContext;
            _commonSharedSteps = commonSharedSteps;
        }

        public void ProgressToNextPage()
        {
            SetTheParticipants();
            ClickBook();
            GetTheHearingNames();
            ClickContinue();
            _c.Test.CaseNames.Should().NotBeNullOrEmpty();
            _browser.ClickLink(HeaderPage.DeleteHearingsLink);
        }

        [When(@"the user creates (.*) hearings")]
        public void WhenTheUserCreatesHearings(int numberOfHearings)
        {
            _numberOfHearings = numberOfHearings;
            _browser.Driver.WaitForListToBePopulated(CreateHearingPage.NumberOfHearingsDropdown);
            _commonSharedSteps.WhenTheUserSelectsTheOptionFromTheDropdown(_browser.Driver, CreateHearingPage.NumberOfHearingsDropdown, numberOfHearings.ToString());
            SetTheParticipants();
            ClickBook();
        }

        private void ClickBook()
        {
            _browser.Click(CreateHearingPage.BookAndConfirmButton);
        }

        private void ClickContinue()
        {
            _browser.Click(CreateHearingPage.ContinueButton);
        }

        private void SetTheParticipants()
        {
            _browser.Clear(CreateHearingPage.IndividualsTextfield);
            _browser.Driver.WaitUntilVisible(CreateHearingPage.IndividualsTextfield).SendKeys(DefaultData.Individuals.ToString());
            _browser.Clear(CreateHearingPage.RepresentativesTextfield);
            _browser.Driver.WaitUntilVisible(CreateHearingPage.RepresentativesTextfield).SendKeys(DefaultData.Representatives.ToString());
            _browser.Clear(CreateHearingPage.ObserversTextfield);
            _browser.Driver.WaitUntilVisible(CreateHearingPage.ObserversTextfield).SendKeys(DefaultData.Observers.ToString());
            _browser.Clear(CreateHearingPage.PanelMembersTextfield);
            _browser.Driver.WaitUntilVisible(CreateHearingPage.PanelMembersTextfield).SendKeys(DefaultData.PanelMembers.ToString());
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        [Then(@"the confirmation dialog shows hearings were created")]
        public void ThenTheConfirmationDialogShowsHearingsWereCreated()
        {
            _browser.Driver.WaitUntilVisible(CreateHearingPage.ProgressDialog).Displayed.Should().BeTrue();
            _browser.Driver.WaitUntilVisible(CreateHearingPage.InProgressTitle).Displayed.Should().BeTrue();
            _browser.Driver.WaitUntilElementNotVisible(CreateHearingPage.InProgressTitle, 30);
            _browser.Driver.WaitUntilVisible(CreateHearingPage.CompleteTitle).Displayed.Should().BeTrue();
            GetTheHearingNames();
            ClickContinue();
            _browser.Driver.WaitUntilElementNotVisible(CreateHearingPage.CompleteTitle);
            _browser.Driver.WaitUntilElementNotVisible(CreateHearingPage.ProgressDialog);
        }

        private void GetTheHearingNames()
        {
            _browser.Driver.WaitUntilVisible(CreateHearingPage.CompleteTitle).Displayed.Should().BeTrue();
            var numberOfCaseNames = _browser.Driver.WaitUntilElementsVisible(CreateHearingPage.NumberOfCaseNames).Count;
            numberOfCaseNames.Should().BeGreaterThan(0);

            for (var i = 0; i < numberOfCaseNames; i++)
            {
                var caseName = _browser.Driver.WaitUntilVisible(CreateHearingPage.CaseName(i)).Text;
                _c.Test.CaseNames.Add(caseName);
            }
            _c.Test.CaseNames.Count.Should().Be(_numberOfHearings, $"Case names were not correctly saved. Expected {_numberOfHearings} hearings but {_c.Test.CaseNames.Count} were created.");
        }

        [When(@"the date is set to a past date")]
        public void WhenTheDateIsSetToThePast()
        {
            _browser.Driver.WaitUntilVisible(CreateHearingPage.HearingStartTimeHour).Clear();
            _browser.Driver.WaitUntilVisible(CreateHearingPage.HearingStartTimeHour).SendKeys("00");
            _browser.Driver.WaitUntilVisible(CreateHearingPage.HearingStartTimeMinute).Clear();
            _browser.Driver.WaitUntilVisible(CreateHearingPage.HearingStartTimeMinute).SendKeys("00");
        }

        [Then(@"an error appears stating the hearing time must be in the future")]
        public void ThenAnErrorAppearsStatingTheHearingTimeMustBeInTheFuture()
        {
            _browser.Driver.WaitUntilVisible(CreateHearingPage.HearingTimeError).Displayed.Should().BeTrue();
        }
    }
}
