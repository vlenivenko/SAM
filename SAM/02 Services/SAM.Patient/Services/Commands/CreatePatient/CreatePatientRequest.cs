namespace SAM.Patient.Services.Commands.CreatePatient
{
    /// <summary>
    /// Create patient request
    /// </summary>
    public class CreatePatientRequest
    {
        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// DOB
        /// </summary>
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// Type of disease
        /// </summary>
        public string DiseaseType { get; set; }
    }
}