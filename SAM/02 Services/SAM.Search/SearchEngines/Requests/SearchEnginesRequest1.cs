namespace SAM.Search.SearchEngines.Requests
{
    public class SearchEnginesRequest1
    {
        public SearchPatientEngineRequest Patient { get; set; }

        public class SearchPatientEngineRequest
        {
            public string Forename { get; set; }

            public string Surname { get; set; }

            public string DateOfBirth { get; set; }

            public string DiseaseType { get; set; }

        }
    }
}
