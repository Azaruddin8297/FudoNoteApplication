using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
		public readonly ICollaboratorDL collaboratorDL;
        public CollabBL(ICollaboratorDL collaboratorDL)
        {
              this.collaboratorDL = collaboratorDL;
        }
        public CollabResponseModel AddCollaborate(long notesId,long uesrid, CollabModel model)
        {
			try
			{
                return this.collaboratorDL.AddCollaborate(notesId, uesrid, model);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
