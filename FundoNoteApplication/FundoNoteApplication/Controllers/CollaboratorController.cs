using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using DataLayer.Service;
using DataLayer.Interface;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        
        public readonly ICollabBL collabBL;

        public CollaboratorController( ICollabBL collabBL)
        {
         
            this.collabBL = collabBL;
        }

        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(long noteid,CollabModel model)
        {

            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var addresult = collabBL.AddCollaborate(noteid, userID, model);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "New collaborator add sucessfully.", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Not added new collaborator." });
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpDelete]
        [Route("deletecollaborator")]

        public IActionResult deletecollaborator(long collaboratorID)
        {
            var deleteResult = collabBL.DeleteCollaborator(collaboratorID);
            if (deleteResult != null)
            {
                return this.Ok(new { sucess = true, msg = "Collaborator Deleted sucessfull", data = deleteResult }); //SSMD form
            }
            else
            {
                return this.BadRequest(new { sucess = false, msg = "Collaborator not Deleted" });
            }
        }
    }
}
