using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        public readonly ILableDL label;

        public LabelBL(ILableDL label)
        {
            this.label = label;
        }
        public ResponseLable CreateLable(long notesId, long jwtUserId, LableModel model)
        {
			try
			{
				return this.label.CreateLable(notesId, jwtUserId, model);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
