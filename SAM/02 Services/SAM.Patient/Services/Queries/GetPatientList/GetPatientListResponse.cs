namespace SAM.Patient.Services.Queries.GetPatientList
{
    /// <summary>
    /// Get patient list response
    /// </summary>
    public class GetPatientListResponse
    {
        /// <summary>
        /// Nested patient object list
        /// </summary>
        public ICollection<Patient> PatientList { get; set; } = new List<Patient>();

        /// <summary>
        /// Nested patient object
        /// </summary>
        public class Patient
        {
            /// <summary>
            /// Patient Id
            /// </summary>
            public int Id { get; set; }

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
}