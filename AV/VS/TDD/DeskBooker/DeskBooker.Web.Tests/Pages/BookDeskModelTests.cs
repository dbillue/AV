using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DeskBooker.Web.Pages
{
    public class BookDeskModelTests
    {
        private readonly Mock<IDeskBookingRequestProcessor> _processorMock;
        private readonly BookDeskModel _bookDeskModel;
        private readonly DeskBookingResult _deskBookingResult;

        public BookDeskModelTests()
        {
            // Create Mock of interface IDeskBookingRequestProcessor.
            _processorMock = new Mock<IDeskBookingRequestProcessor>();

            _bookDeskModel = new BookDeskModel(_processorMock.Object)
            {
                DeskBookingRequest = new DeskBookingRequest()
            };

            _deskBookingResult = new DeskBookingResult
            {
                Code = DeskBookingResultCode.Success
            };

            // Setup mock.
            _processorMock.Setup(x => x.BookDesk(_bookDeskModel.DeskBookingRequest))
                .Returns(_deskBookingResult);
        }

        [Fact]
        public void ShouldCallBookDeskMethofOfProcessor()
        {
            _bookDeskModel.OnPost();

            // Verify.
            _processorMock.Verify(x => x.BookDesk(_bookDeskModel.DeskBookingRequest),
                Times.Once);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void ShouldCallBookDeskMethodfOfProcessorIfModelIsValid(int expectedBookCalls, bool isModelValid)
        {
            if (!isModelValid)
            {
                _bookDeskModel.ModelState.AddModelError("JustAKey", "AnErrorMessage");
            }

            _bookDeskModel.OnPost();

            // Verify.
            _processorMock.Verify(x => x.BookDesk(_bookDeskModel.DeskBookingRequest),
                Times.Exactly(expectedBookCalls));
        }

        [Fact]
        public void ShouldAddModelErrorIfNoDeskIsAvailable()
        {
            _deskBookingResult.Code = DeskBookingResultCode.NoDeskAvailable;

            _bookDeskModel.OnPost();

            var modelStateEntry =
                Assert.Contains("DeskBookingRequest.Date", _bookDeskModel.ModelState);
            var modelError =
                Assert.Single(modelStateEntry.Errors);
            Assert.Equal("No desk availble for selected date", modelError.ErrorMessage);
        }

        [Fact]
        public void ShouldNotAddModelErrorIfNoDeskIsAvailable()
        {
            _deskBookingResult.Code = DeskBookingResultCode.Success;

            _bookDeskModel.OnPost();

            Assert.DoesNotContain("DeskBookingRequest.Date", _bookDeskModel.ModelState);
        }

        [Theory]
        [InlineData(typeof(PageResult), false, null)]
        [InlineData(typeof(PageResult), true, DeskBookingResultCode.NoDeskAvailable)]
        [InlineData(typeof(RedirectToPageResult), true, DeskBookingResultCode.Success)]
        public void ShouldReturnExpectedActionResult(Type expectedActionResulTtype,
                                                     bool isModelValid,
                                                     DeskBookingResultCode? deskBookingResultCode)
        {
            if(!isModelValid)
            {
                _bookDeskModel.ModelState.AddModelError("JustAKey", "AnErrorMessage");
            }

            if(deskBookingResultCode.HasValue)
            {
                _deskBookingResult.Code = deskBookingResultCode.Value;
            }

            IActionResult actionResult = _bookDeskModel.OnPost();

            Assert.IsType(expectedActionResulTtype, actionResult);
        }

        [Fact]
        public void ShouldRedirectToBookDeskConfirmationPage()
        {
            _deskBookingResult.Code = DeskBookingResultCode.Success;
            _deskBookingResult.DeskBookingId = 15;
            _deskBookingResult.FirstName = "Allen Smarts";
            _deskBookingResult.Date = new DateTime(2020,02,14);

            IActionResult actionResult = _bookDeskModel.OnPost();

            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(actionResult);
            Assert.Equal("BookDeskConfirmation", redirectToPageResult.PageName);

            IDictionary<string, object> routeValues = redirectToPageResult.RouteValues;
            Assert.Equal(3, routeValues.Count);

            var deskBookingId = Assert.Contains("DeskBookingId", routeValues);
            Assert.Equal(_deskBookingResult.DeskBookingId, deskBookingId);

            var firstName = Assert.Contains("FirstName", routeValues);
            Assert.Equal(_deskBookingResult.FirstName, firstName);

            var date = Assert.Contains("Date", routeValues);
            Assert.Equal(_deskBookingResult.Date, date);
        }
    }
}
