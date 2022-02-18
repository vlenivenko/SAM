using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SAM.Patient.Services.Commands.CreatePatient;

namespace SAM.Patient.Tests.Services.CreatePatient.Commands
{
    [TestFixture]
    public class CreatePatientValidatorTests
    {
        private CreatePatientValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreatePatientValidator();
        }

        [Test]
        public async Task Validate_RequestIsValid_ValidationSuccess()
        {
            // arrange
            var request = new CreatePatientRequest
            {
                FirstName = "Vladislav",
                LastName = "Lenivenko",
                DateOfBirth = new DateOnly(1989, 08, 10),
                DiseaseType = "Allergy",
            };

            // act
            var result = await _validator.ValidateAsync(request);

            // assert
            result.PassedValidation.Should().BeTrue();
        }

        [Test]
        public async Task Validate_DateOfBirstIsInvalid_ValidationFailed()
        {
            // arrange
            var request = new CreatePatientRequest
            {
                FirstName = "Vladislav",
                LastName = "Lenivenko",
                DiseaseType = "Allergy",
            };

            // act
            var result = await _validator.ValidateAsync(request);

            // assert
            result.PassedValidation.Should().BeFalse();
            result.Response.ErrorResponse.Error.Message.Should().Be(CreatePatientValidator.InvalidDateOfBirthErrorMessage);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("Vlad")]
        [TestCase("Vladislav5")]
        public async Task Validate_FirstNameIsInvalid_ValidationFailed(string firstName)
        {
            // arrange
            var request = new CreatePatientRequest
            {
                FirstName = firstName,
                LastName = "Lenivenko",
                DateOfBirth = new DateOnly(1989, 08, 10),
                DiseaseType = "Allergy",
            };

            // act
            var result = await _validator.ValidateAsync(request);

            // assert
            result.PassedValidation.Should().BeFalse();
            result.Response.ErrorResponse.Error.Message.Should().Be(CreatePatientValidator.InvalidFirstNameErrorMessage);
        }
    }
}
