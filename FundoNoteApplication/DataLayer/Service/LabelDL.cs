using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Service
{
    public class LabelDL : ILableDL
    {
        public readonly FundoContext context;
        public LabelDL(FundoContext context)
        {
            this.context = context;
        }
        public ResponseLable CreateLable(long notesId, long jwtUserId, LableModel model)
        {
            try
            {
                var validNotesAndUser = this.context.UserTable.Where(e => e.UserId == jwtUserId);

                if (validNotesAndUser != null)
                {
                    LabelEntity label = new LabelEntity();

                    label.noteID = notesId;
                    label.UserId = jwtUserId;
                    label.LabelName = model.LabelName;

                    this.context.Add(label);
                    this.context.SaveChanges();

                    ResponseLable responseModel = new ResponseLable();

                    responseModel.LabelID = label.LabelID;
                    responseModel.NoteID = label.noteID;
                    responseModel.UserID = label.UserId;
                    responseModel.LabelName = label.LabelName;

                    return responseModel;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<LabelEntity> GetAllLable()
        {
            try
            {

                var result = this.context.Label.ToList();
                return result;

            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
