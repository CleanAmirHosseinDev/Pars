namespace ParsKyanCrm.Application.Services.Reports.Queries.IndexBoxAdmin
{
    public class ResultIndexBoxAdminDto
    {

        //تعداد کل مشتریان که قرارداد را تایید کردند
        public string TotalNumberCustomersApprovedContract { get; set; } = "0";

        //تعداد کل مشتریان بدون ثبت درخواست
        public string TotalNumberCustomersWithoutRegistration { get; set; } = "0";

        //تعداد کل درخواست ارزیابی وزارت صمت
        public string TotalNumberApplicationsAssessmentMinistryPrivacy { get; set; } = "0";

        //تعداد پرونده های کدال شده
        public string NumberCodedFiles { get; set; } = "0";

        //تعداد پرونده های حاکمیت شرکتی
        public string NumberCorporateCustomer { get; set; } = "0";


    }
}
