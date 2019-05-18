using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Moq;
using Xunit;

namespace SpecificationPattern.Tests
{
    public class SpecificationExtensionsTests
    {
        public enum ErrorLevel
        {
            [Description("failureReason")]
            Low,
            [Description("failureReason2")]
            High
        };

        [Fact]
        public void Check_ShouldThrowException_WhenSourceIsNull()
        {
            ISpecification<string> specification = null;

            Assert.Throws<ArgumentNullException>(() => specification.Check(true, "failureReason"));
        }

        [Fact]
        public void Check_ShouldThrowException_WhenFailureReasonIsNull()
        {
            ISpecification<string> specification = Mock.Of<ISpecification<string>>();

            Assert.Throws<ArgumentNullException>(() => specification.Check(true, failureReason: null));
        }

        [Fact]
        public void Check_ShouldBeTrue_WhenItIsSatisfied()
        {
            ISpecification<string> specification = Mock.Of<ISpecification<string>>();

            Assert.True(specification.Check(isSatisfied: true, failureReason: "failureReason"));
        }

        [Fact]
        public void Check_ShouldBeFalse_WhenItIsNotSatisfied()
        {
            ISpecification<string> specification = Mock.Of<ISpecification<string>>();

            Assert.False(specification.Check(isSatisfied: false, failureReason: "failureReason"));
        }


        [Fact]
        public void Check_ShouldNotRecordErrors_WhenItIsSatisfied()
        {
            var specification = Mock.Of<ISpecification<string>>(s => s.FailureReasons == new HashSet<string>());

            specification.Check(isSatisfied: true, failureReason: "failureReason");

            Assert.Empty(specification.FailureReasons);
        }

        [Fact]
        public void Check_ShouldNotRecordErrors_WhenFailureReasonsCollectionIsNull()
        {
            var specification = Mock.Of<ISpecification<string>>();

            specification.Check(isSatisfied: false, failureReason: "failureReason");

            Assert.Null(specification.FailureReasons);
        }

        [Fact]
        public void Check_ShouldRecordErrors_WhenItIsNotSatisfied()
        {
            string failureReason = ErrorLevel.Low.GetDescription();

            var specification = Mock.Of<ISpecification<string>>(s => s.FailureReasons == new HashSet<string>());

            specification.Check(isSatisfied: false, failureReason: failureReason);

            Assert.Equal(failureReason, specification.FailureReasons.Single());
        }

        [Fact]
        public void Check_ShouldRecordMultipleErrors_WhenItIsNotSatisfied()
        {
            string failureReason = ErrorLevel.Low.GetDescription();
            string failureReason2 = ErrorLevel.High.GetDescription();

            var specification = Mock.Of<ISpecification<string>>(s => s.FailureReasons == new List<string>());

            specification.Check(isSatisfied: false, failureReason: failureReason);
            specification.Check(isSatisfied: false, failureReason: failureReason2);

            Assert.Equal(2, specification.FailureReasons.Count);
            Assert.Equal(failureReason, specification.FailureReasons.First());
            Assert.Equal(failureReason2, specification.FailureReasons.Last());
        }

        [Fact]
        public void Check_WithErrorType_ShouldThrowException_WhenSourceIsNull()
        {
            ISpecification<string, ErrorLevel> specification = null;

            Assert.Throws<ArgumentNullException>(() => specification.Check(true, ErrorLevel.Low));
        }

        [Fact]
        public void Check_WithErrorType_ShouldThrowException_WhenFailureReasonIsNull()
        {
            ISpecification<string, ErrorLevel> specification = null;

            Assert.Throws<ArgumentNullException>(() => specification.Check(true, ErrorLevel.Low));
        }

        [Fact]
        public void Check_WithErrorType_ShouldBeTrue_WhenItIsSatisfied()
        {
            var specification = Mock.Of<ISpecification<string, ErrorLevel>>();

            Assert.True(specification.Check(isSatisfied: true, ErrorLevel.Low));
        }

        [Fact]
        public void Check_WithErrorType_ShouldBeFalse_WhenItIsNotSatisfied()
        {
            var specification = Mock.Of<ISpecification<string, ErrorLevel>>();

            Assert.False(specification.Check(isSatisfied: false, ErrorLevel.Low));
        }

        [Fact]
        public void Check_WithErrorType_ShouldNotRecordErrors_WhenItIsSatisfied()
        {
            var specification =
                Mock.Of<ISpecification<string, ErrorLevel>>(s => s.FailureReasons == new Dictionary<ErrorLevel, string>());

            specification.Check(isSatisfied: true, ErrorLevel.Low);

            Assert.Empty(specification.FailureReasons);
        }

        [Fact]
        public void Check_WithErrorType_ShouldNotRecordErrors_WhenFailureReasonsCollectionIsNull()
        {
            var specification = Mock.Of<ISpecification<string, ErrorLevel>>();

            specification.Check(isSatisfied: false, ErrorLevel.Low);

            Assert.Null(specification.FailureReasons);
        }

        [Fact]
        public void Check_WithErrorType_ShouldRecordErrors_WhenItIsNotSatisfied()
        {
            string failureReason = ErrorLevel.Low.GetDescription();

            var specification = Mock.Of<ISpecification<string, ErrorLevel>>(s =>
                s.FailureReasons == new Dictionary<ErrorLevel, string>());

            specification.Check(isSatisfied: false, failureType: ErrorLevel.Low);

            Assert.Equal(failureReason, specification.FailureReasons[ErrorLevel.Low]);
            Assert.False(specification.FailureReasons.ContainsKey(ErrorLevel.High));
        }

        [Fact]
        public void Check_WithErrorType_ShouldRecordMultipleErrors_WhenItIsNotSatisfied()
        {
            string failureReason = ErrorLevel.Low.GetDescription();
            string failureReason2 = ErrorLevel.High.GetDescription();

            var specification = Mock.Of<ISpecification<string, ErrorLevel>>(s =>
                s.FailureReasons == new Dictionary<ErrorLevel, string>());

            specification.Check(isSatisfied: false, failureType: ErrorLevel.Low);
            specification.Check(isSatisfied: false, failureType: ErrorLevel.High);

            Assert.Equal(2, specification.FailureReasons.Count);
            Assert.Equal(failureReason, specification.FailureReasons[ErrorLevel.Low]);
            Assert.Equal(failureReason2, specification.FailureReasons[ErrorLevel.High]);
        }

        [Fact]
        public void Check_WithErrorType_ShouldReplaceMessage_WhenMultipleErrorsHappenForSameType()
        {
            string failureReason = ErrorLevel.Low.GetDescription();

            var specification =
                Mock.Of<ISpecification<string, ErrorLevel>>(s => s.FailureReasons == 
                    new Dictionary<ErrorLevel, string>()
                    {
                        { ErrorLevel.Low, "old failure reason" }
                    });

            specification.Check(isSatisfied: false, failureType: ErrorLevel.Low);
            specification.Check(isSatisfied: false, failureType: ErrorLevel.Low);

            Assert.Equal(1, specification.FailureReasons.Count);
            Assert.Equal(failureReason, specification.FailureReasons[ErrorLevel.Low]);
            Assert.False(specification.FailureReasons.ContainsKey(ErrorLevel.High));
        }
    }
}
