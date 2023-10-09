namespace ParsKyanCrm.Application.Services.Reports.Queries.TotalNumberCustomersWithoutRegistration
{
    public class ResultTotalNumberCustomersWithoutRegistrationDto
    {
        public int CustomerID { get; set; }

        public string SaveDateStr { get; set; }

        public string CompanyName { get; set; }

        public string AgentName { get; set; }

        public string NationalCode { get; set; }

        public string AgentMobile { get; set; }
    }
}
