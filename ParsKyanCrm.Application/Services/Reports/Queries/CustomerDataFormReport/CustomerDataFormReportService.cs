using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using ParsKyanCrm.Application.Patterns.FacadPattern;
using ParsKyanCrm.Common.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParsKyanCrm.Application.Services.Reports.Queries.CustomerDataFormReport
{
    public class CustomerDataFormReportService : ICustomerDataFormReportService
    {
        private readonly IUserFacad _userFacad;
        public CustomerDataFormReportService(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }
        public async Task<ResultDto<ResultCustomerDataFormReportDto>> Execute(RequestCustomerDataFormReportDto request)
        {
            try
            {
                var _customerRequest = await _userFacad.GetRequestForRatingsService.Execute(new Dtos.Users.RequestRequestForRatingDto() { RequestId=request.RequestId, PageIndex=1, PageSize=1});
                var _customer = await _userFacad.GetCustomersService.Execute(new Dtos.Users.RequestCustomersDto() { CustomerId = _customerRequest.Data.FirstOrDefault().CustomerId });
                var _customerName = _customer.CompanyName;
                // همه دسته های سوالات
                var all_dataForms = await _userFacad.GetDataFormsService.Execute(new Dtos.Users.RequestDataFormsDto() { PageIndex=0, PageSize=0, IsActive=15, SortOrder="FormCode_A" });
                // همه سوالات
                var all_questions = await _userFacad.GetDataFormQuestionssService.Execute(new Dtos.Users.RequestDataFormQuestionsDto() { 
                    PageIndex=0, PageSize= 0, DataFormType= 2,
                    IsActive=15, Version=null, 
                });
                // همه پاسخ های مشتری بر اساس شماره درخواست
                var all_answers = await _userFacad.GetDataFromAnswerssService.Execute(new Dtos.Users.RequestDataFromAnswersDto() {
                    PageSize=0, PageIndex=0, IsActive=15, RequestId=request.RequestId
                });
                // همه ریپورت های کارشناس بر اساس شماره درخواست
                var all_reports = await _userFacad.GetDataFormReportsService.Execute(new Dtos.Users.RequestDataFormReportDto() { PageIndex=0, PageSize=0, RequestId=request.RequestId});
                var make_reports = new List<ReportQuestion>();

                // ساخت ریپورت برای هر سوال
                foreach(var answer in all_answers.Data)
                {
                    var dataform = all_dataForms.Data.FirstOrDefault(q => q.FormId == answer.FormId);
                    var question = all_questions.Data.FirstOrDefault(q => q.DataFormQuestionId == answer.DataFormQuestionId);
                    var report = all_reports.Data.FirstOrDefault(q => q.DataFormAnswerId == answer.AnswerId);
                    var rep = new ReportQuestion(question.DataFormQuestionId, question.DataFormId, answer.AnswerId, question.QuestionText,
                        question.QuestionType, question.Score, question.Version, answer.Answer, report.SystemScore, report.AnalizeScore,
                        answer.Description, dataform.FormTitle
                    );
                    make_reports.Add(rep);
                }

                var data = new ResultCustomerDataFormReportDto() { CustomerName=_customerName, RequestNo= _customerRequest.Data.FirstOrDefault().RequestNo, Reports= make_reports };

                return new ResultDto<ResultCustomerDataFormReportDto>()
                {
                    Data = data,
                    IsSuccess = true,
                    StatusCode=200,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> ToExcel(RequestCustomerDataFormReportDto request)
        {
            try
            {
                var q = await Execute(request);

                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[11] {
                new DataColumn("ردیف"),
                new DataColumn("عنوان دسته"),
                new DataColumn("متن سوال"),
                new DataColumn("نوع سوال"),
                new DataColumn("نمره سوال"),
                new DataColumn("پاسخ مشتری"),
                new DataColumn("نمره سیستم"),
                new DataColumn("نمره کارشناس"),
                new DataColumn("توضیحات مشتری"),
                new DataColumn("نام شرکت"),
                new DataColumn("شماره درخواست"),
            });
                int rowcount = 1;
                foreach (var item in q.Data.Reports)
                {
                    dt.Rows.Add(
                          rowcount,
                          item.DataFormTitle,
                          item.QuestionText,
                          item.QuestionType,
                          item.Score,
                          item.Answer,
                          item.SystemScore,
                          item.AnalizeScore,
                          item.Description,
                          q.Data.CustomerName,
                          q.Data.RequestNo
                        );
                    rowcount++;
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
