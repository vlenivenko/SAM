namespace SAM.Repository.Models
{
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
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Type of disease
        /// </summary>
        public string DiseaseType { get; set; }
    }
}
