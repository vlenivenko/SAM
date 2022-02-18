using System.Net;
using System.Text.RegularExpressions;
using SAM.Core.CQRS.Validation;

namespace SAM.Patient.Services.Commands.CreatePatient
{
    /// <summary>
    /// Validates create patient request
    /// </summary>
    public class CreatePatientValidator : BaseValidator<CreatePatientRequest, CreatePatientResponse>
    {
        public const string InvalidFirstNameErrorMessage = "First name should not be empty, should not be less than 5 symbols and should contain letters only";
        public const string InvalidDateOfBirthErrorMessage = "Date of birth is required";

        private const int FirstNameMaxLength = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePatientValidator"/> class.
        /// </summary>
        public CreatePatientValidator()
        {
            SetupValidationSteps();
        }

        private void SetupValidationSteps()
        {
            Must(IsFirstNameValid)
                .WithErrorMessage(InvalidFirstNameErrorMessage)
                .WithStatusCode((int)HttpStatusCode.BadRequest);

            Must(IsDateOfBirthValid)
                .WithErrorMessage(InvalidDateOfBirthErrorMessage)
                .WithStatusCode((int)HttpStatusCode.BadRequest);
        }

        private bool IsFirstNameValid(CreatePatientRequest request)
        {
            var firstName = request.FirstName;

            if (string.IsNullOrWhiteSpace(firstName))
            {
                return false;
            }

            if (firstName.Length < FirstNameMaxLength)
            {
                return false;
            }


            return Regex.IsMatch(firstName, @"^[a-zA-Z]+$");
        }

        private bool IsDateOfBirthValid(CreatePatientRequest request)
        {
            var dateOfBirth = request.DateOfBirth;

            if (dateOfBirth == DateOnly.MinValue)
            {
                return false;
            }

            return true;
        }
    }
}