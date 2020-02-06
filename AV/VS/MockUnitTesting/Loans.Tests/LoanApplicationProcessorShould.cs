using System;
using System.Collections.Generic;
using System.Text;
using Demo.Domain.Applications;
using Moq;
using NUnit.Framework;

namespace MockUnitDemo.Tests
{
    [Category("Loan Application Processor Comparison")]
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void DeclineLowSalary()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                                                  product,
                                                  amount,
                                                  "Sarah",
                                                  25,
                                                  "123 Main St Love, Anywhere USA",
                                                  64_999);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                    mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted, Is.False);
        }

        delegate void ValidateCallback(string applicantName,
                                       int applicantAge,
                                       string applicantAddress,
                                       ref IdentityVerificationStatus status);

        [Test]
        public void Accept()
        {
            LoanProduct product = new LoanProduct(99, "Loan", 5.25m);
            LoanAmount amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                                                  product,
                                                  amount,
                                                  "Sarah",
                                                  25,
                                                  "123 Main St Love, Anywhere USA",
                                                  65_000);

            // Instantiate Mock objects.
            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();
            var mockScoreValue = new Mock<ScoreValue>();
            var mockScoreResult = new Mock<ScoreResult>();

            // Pass in values to method Validate(p1, p2, p3).
            mockIdentityVerifier
                .Setup(x => x.Validate
                    ("Sarah",
                     25,
                     "123 Main St Love, Anywhere USA"))
                .Returns(true);

            mockCreditScorer.SetupAllProperties();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            //mockCreditScorer.SetupProperty(x => x.Count);

            // Pass in IsAny<> for parameter replacement.
            //mockIdentityVerifier
            //    .Setup(x => x.Validate
            //        (It.IsAny<string>(),
            //         It.IsAny<int>(),
            //         It.IsAny<string>()))
            //    .Returns(true);

            // Pass in values to method Validate(p1, p2, p3, out p4) using out parameter.
            //bool isValidOutValue = true;
            //mockIdentityVerifier
            //    .Setup(x => x.Validate
            //        ("Sarah",
            //         25,
            //         "123 Main St Love, Anywhere USA",
            //         out isValidOutValue));

            // Pass in values to method Validate(p1, p2, p3, ref p4) using reference parameter
            // and CallBack delegate.  Ugh.
            //mockIdentityVerifier
            //    .Setup(x => x.Validate
            //        ("Sarah",
            //         25,
            //         "123 Main St Love, Anywhere USA",
            //         ref It.Ref<IdentityVerificationStatus>.IsAny))
            //    .Callback(new ValidateCallback(
            //            (string applicantName,
            //             int applicantAge,
            //             string applicantAddress,
            //             ref IdentityVerificationStatus status) =>
            //                status = new IdentityVerificationStatus(true)));


            // Pass in Mock parameters.
            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object,
                                                    mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted, Is.True);
            Assert.That(mockCreditScorer.Object.Count, Is.EqualTo(1));
        }

        [Test]
        public void NullReturnExample()
        {
            var mock = new Mock<INullExample>();

            mock.Setup(x => x.SomeMethod())
                .Returns<string>(null);

            string mockReturnValue = mock.Object.SomeMethod();

            Assert.That(mockReturnValue, Is.Null);
        }
    }

    public interface INullExample
    {
        string SomeMethod();
    }
}
