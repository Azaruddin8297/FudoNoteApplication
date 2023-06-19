using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        public readonly FundoContext Context;
        public readonly ICollabBL collabBL;

        public CollaboratorController(FundoContext Context, ICollabBL collabBL)
        {
            this.Context = Context;
            this.collabBL = collabBL;
        }

        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(long noteid,long uesrid,CollabModel model)
        {
            
            try
            {
                var id = this.Context.Notes.FirstOrDefault(e => e.NoteID == noteid && e.UserId == uesrid);
                if (id != null)
                {
                    var collaborate = this.collabBL.AddCollaborate(noteid, uesrid, model);
                    return Ok(new { Success = true, message = "Collaboration Successfull ", data = collaborate });

                   
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Email Missing For Collaboration" });
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
