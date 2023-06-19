using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DataLayer.Service
{
    public class CollaboratorDL : ICollaboratorDL
    {
        public readonly FundoContext context;
        public CollaboratorDL(FundoContext context)
        {
                this.context = context;
        }

        public CollabResponseModel AddCollaborate(long notesId, long uesrid, CollabModel model)
        {
            try
            {
              //  var validNotesAndUser = this.context.UserTable.Where(e => e.UserId == jwtUserId);
                CollaboratorEntity collaborate = new CollaboratorEntity();

                collaborate.NoteID = notesId;
                collaborate.UserId = uesrid;
                collaborate.CollaboratedEmail = model.CollaboratedEmail;

                CollabResponseModel responseModel = new CollabResponseModel();

                responseModel.CollaboratorID = collaborate.CollaboratorID;
                responseModel.noteID = collaborate.NoteID;
                responseModel.UserId = collaborate.UserId;
                responseModel.CollaboratedEmail = collaborate.CollaboratedEmail;

                context.Collaborator.Add(collaborate);
                context.SaveChanges();

               

                return responseModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
