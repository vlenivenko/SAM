namespace SAM.Search.SearchEngines.Requests
{
    public class SearchEnginesRequest2
    {
        public SearchPatientEngineRequest Patient { get; set; }

        public class SearchPatientEngineRequest
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string DateOfBirth { get; set; }

            public string DiseaseType { get; set; }

        }
    }
}
