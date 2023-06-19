using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface ICollaboratorDL
    {
        public CollabResponseModel AddCollaborate(long notesId, long uesrid, CollabModel model);
    }
}
