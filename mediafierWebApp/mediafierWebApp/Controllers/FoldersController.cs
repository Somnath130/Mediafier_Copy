using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mediafierWebApp.Models;
using mediafierWebApp.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mediafierWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        // GET: api/Folders
        private readonly MediafierContext _mediafiercontext;
        public FoldersController(MediafierContext x)
        {
            _mediafiercontext = x;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var res = _mediafiercontext.Folders.ToList();
            return Ok(res);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _mediafiercontext.Folders.Where(o => o.FoldersCreatedBy == id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        // POST: api/Folders
        [HttpPost]
            public void Post([FromBody] FoldersRequest value)
            {
                Folders obj = new Folders();
                obj.FoldersName = value.FoldersName;
                obj.FoldersCreatedBy = value.FoldersCreatedBy;
                obj.FoldersCreatedAt = value.FoldersCreatedAt;
                obj.FoldersIsdeleted = value.FoldersIsdeleted;
            _mediafiercontext.Folders.Add(obj);
            _mediafiercontext.SaveChanges();
            }
        [HttpGet("Folders/{id}/{value}")]
        public IActionResult Get(int id,string value)
        {
            var result = _mediafiercontext.Folders.Where(obj => obj.FoldersName.Contains(value));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //var folds = _mediafiercontext.Folders.Where(res => res.FoldersId == id).ToList();
            //folds.ForEach(res => _mediafiercontext.Folders.Remove(res));
           // _mediafiercontext.SaveChanges();

           var folds = _mediafiercontext.Document.Where(result => result.DocFolderId == id).ToList();
            folds.ForEach(result => _mediafiercontext.Document.Remove(result));
            _mediafiercontext.SaveChanges();
            var res = _mediafiercontext.Folders.Where(result => result.FoldersId == id).ToList();
            res.ForEach(result => _mediafiercontext.Folders.Remove(result));
            _mediafiercontext.SaveChanges();
        }
    }

    }

