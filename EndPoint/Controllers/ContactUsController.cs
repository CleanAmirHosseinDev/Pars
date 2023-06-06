using EndPoint.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using ParsKyanCrm.Application.Dtos.BasicInfo;
using ParsKyanCrm.Application.Services.Email;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static ParsKyanCrm.Application.Services.WebService.CaptchaService;

namespace EndPoint.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ILogger<ContactUsController> _logger;

        public ContactUsController(ILogger<ContactUsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [CaptchaCheck]
        [HttpPost]
        public JsonResult ContactUsForm([FromForm] ContactUsFormDto form)
        {
            var error = "";
            Boolean isOk = false;
            if (ModelState.IsValid)
            {
                try
                {
                    if (form.email == null || form.email.Length == 0) form.email = "nomail@z.z";
                    if (form.subject == null || form.subject.Length == 0) form.subject = "بدون موضوع";
                    string prependstr =
                    "<br>موضوع: " + form.subject
                    + "<br>نام: " + form.name
                    + "<br>ایمیل: " + form.email
                    + "<br>تلفن: " + form.phone
                    + "<br>-----------------------------------<br>";

                    form.message = prependstr + form.message;

                    EmailSend.SendMail(form.email, form.name, form.subject, form.message, "info@parscrc.ir");
                    isOk = true;
                }
                catch (Exception ex)
                {
                    error = "خطا در اجرای درخواست";
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                foreach (var item in errors)
                {
                    error += item.First().ErrorMessage + " ";
                }

            }
            return Json(new
            {
                IsSuccess = isOk,
                Message = isOk ? "پیام شما با موفقیت ارسال شد." : error
            });

        }
    }
}
