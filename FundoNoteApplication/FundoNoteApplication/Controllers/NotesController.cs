using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;
using DataLayer.DB;
using DataLayer.Service;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL NoteBL;
        private readonly FundoContext context;

        public NotesController(INotesBL NoteBL,FundoContext context)
        {
            this.NoteBL = NoteBL;
            this.context = context;
        }
        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel addnote )
        {
            try
            {
                var UID = addnote.UserId;
                var check = NoteBL.CheckUserId(UID);
                if (check != true)
                {
                    return this.BadRequest(new { sucess = false, msg = "Not Created" });
                }
                var addresult = NoteBL.AddNote(addnote);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Note add sucessfull", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Note not Created" });
                }

            } 
            catch (System.Exception)
            {

                throw;
            }

        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long addnote)
        {
            try
            { 
                var addresult = NoteBL.DeleteNote(addnote);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Note Deleted sucessfull", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Note not Deleted" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost("Update")]
        public IActionResult UpdateNotes(NoteModel noteModel,long noteID)
        {
            try
            {
              //  long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "userID").Value);
                var addresult = NoteBL.UpdateNote(noteModel, noteID);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Note updated sucessfull", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Note not Deleted" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
