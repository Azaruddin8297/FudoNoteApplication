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
        /// <summary>
        /// This Method is Used for Adding Collaborators
        /// </summary>
        /// <param name="noteID"></param>
        /// <param name="userID"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public CollaboratorEntity AddCollaborate(long noteID, long userID, CollabModel model)
        {
            try
            {
                var result = context.Collaborator.FirstOrDefault(x => x.UserId == userID && x.NoteID == noteID);
                CollaboratorEntity collaborator = new CollaboratorEntity();

                collaborator.NoteID = noteID;
                collaborator.UserId = userID;
                collaborator.CollaboratedEmail = model.CollaboratedEmail;
                context.Collaborator.Add(collaborator);
                context.SaveChanges();

                if (collaborator != null)
                {

                    return collaborator;
                }
                else { return null; }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This Method is Used to Delete the Collab
        /// </summary>
        /// <param name="collaboratorID"></param>
        /// <returns></returns>
        public CollaboratorEntity DeleteCollaborator(long collaboratorID)
        {
            try
            {
                var idCheck = context.Collaborator.FirstOrDefault(x => x.CollaboratorID == collaboratorID);
                if (idCheck != null)
                {
                    context.Collaborator.Remove(idCheck);
                    context.SaveChanges();
                    return idCheck;
                }
                else return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This Method is Used to get all Collaboratorss
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CollaboratorEntity> GetCollab()
        {
            try
            {

                var result = this.context.Collaborator.ToList();
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
