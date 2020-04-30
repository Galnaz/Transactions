using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Application.Documents;

namespace TransactionManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentsService _documentsService;

        public DocumentsController(DocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            if (HttpContext.Request.Form.Files.Any())
            {
                foreach (var file in HttpContext.Request.Form.Files)
                {
                    var path = Path.Combine(@"C:\Users\nah\Desktop", "uploads", file.FileName);
                    using(var stream = file.OpenReadStream())
                    {
                        await _documentsService.Upload(stream, file.FileName, file.ContentType);
                    }
                }
            }

            return NoContent();
        }
    }
}
