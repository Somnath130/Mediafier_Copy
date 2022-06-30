using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using mediafierWebApp.Models;
using mediafierWebApp.RequestModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace mediafierWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        // GET: api/Documents
        private readonly MediafierContext _mediafiercontext;
        private readonly IHostingEnvironment _env;
        private object _environment;

        public DocumentsController(MediafierContext y, IHostingEnvironment environment)
        {
            _mediafiercontext = y;
            _env = environment;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var res = _mediafiercontext.Document.ToList();
            return Ok(res);
        }



        // POST: api/Documents
        [HttpPost]
        public void Post([FromBody] DocumentRequest value)
        {
            Document obj = new Document();
            obj.DocName = value.DocName;
            obj.DocContentType = value.DocContentType;
            obj.DocSize = value.DocSize;
            obj.DocCreatedBy = value.DocCreatedBy;
            obj.DocCreatedAt = value.DocCreatedAt;
            obj.DocFolderId = value.DocFolderId;
            obj.DocIsDeleted = value.DocIsDeleted;
            _mediafiercontext.Document.Add(obj);
            _mediafiercontext.SaveChanges();
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _mediafiercontext.Document.Where(o => o.DocFolderId == id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var del = _mediafiercontext.Document.Where(r => r.DocId == id).ToList();
            del.ForEach(r => _mediafiercontext.Document.Remove(r));
            _mediafiercontext.SaveChanges();
        }

        [HttpPost]
        [Route("upload/{docCreatedBy}/{docCreatedAt}/{docFolderId}")]

        public IActionResult Post(int docCreatedBy, DateTime docCreatedAt, int docFolderId)
        {
            //long fsize = files.Sum(f => f.Length);
            if (Request.Form.Files.Count() > 0)
            {
                string abc = "aaa";

            }
            IFormFile file = Request.Form.Files[0];
            var RootPath = Path.Combine(_env.ContentRootPath, "Resources", "Documents");
            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);

            for (var i = 0; i < Request.Form.Files.Count(); i++)
            {
                var filePath = Path.Combine(RootPath, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Document obj = new Document();
                    {
                        obj.DocName = file.FileName;
                        obj.DocContentType = file.ContentType;
                        obj.DocSize = (int)file.Length;
                        obj.DocCreatedAt = docCreatedAt;
                        obj.DocCreatedBy = docCreatedBy;

                        obj.DocFolderId = docFolderId;
                        obj.DocIsDeleted = false;

                    };
                    file.CopyTo(stream);
                    _mediafiercontext.Document.Add(obj);
                    _mediafiercontext.SaveChanges();
                }
            }
            //return Ok(new { count = files.Count, fsize });
            return Ok();
        }
    }
}
