using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Models;
using DataLayer.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        public readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {

            this.labelBL = labelBL;
        }

        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(long noteid , long userid, LableModel model)
        {

            try
            { 
                var addresult = labelBL.CreateLable(noteid, userid, model);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Label Added", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Label Not Added" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("ReadAll")]
        public IActionResult ReadAll()
        {
            try
            {

                var res = this.labelBL.GetAllLable();
                if (res != null)
                {
                    return this.Ok(new { sucess = true, msg = "Label", data = res }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "No Lables" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        } 
    }
}
