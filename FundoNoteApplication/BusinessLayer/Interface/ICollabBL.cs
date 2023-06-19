using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabResponseModel AddCollaborate(long notesId,long uesrid, CollabModel model);
    }
}
