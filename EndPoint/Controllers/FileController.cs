using Microsoft.AspNetCore.Mvc;
using ParsKyanCrm.Common;
using ParsKyanCrm.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class FileController : Controller
    {

        [HttpGet]
        public IActionResult Download(string path)
        {
            if (FileOperation.ExistsFile(path)) return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
            return NotFound();
        }

    }
}
