using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public ResponseLable CreateLable(long notesId, long jwtUserId, LableModel model);
    }
}
