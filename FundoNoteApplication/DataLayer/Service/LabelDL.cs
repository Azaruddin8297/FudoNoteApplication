﻿using CommonLayer.Models;
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
        /// <summary>
        /// This Method is Used for Creating the Labels
        /// </summary>
        /// <param name="notesId"></param>
        /// <param name="jwtUserId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This Method is Used to get all Lables
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// This Method is Used to Update the Tables
        /// </summary>
        /// <param name="lableid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseLable UpdateLable( long lableid,UpdateLabelModel model)
        {
            try
            {
                var response = this.context.Label.FirstOrDefault(e => e.LabelID == lableid);
                if (response != null)
                {
                    response.LabelName = model.LabelName;
                    response.noteID = model.NoteID;
                    response.UserId = model.userId;
                    this.context.SaveChanges();
                    ResponseLable models = new ResponseLable();
                    models.LabelID = response.LabelID;
                    models.NoteID = response.noteID;
                    models.UserID = response.UserId;
                    models.LabelName = response.LabelName;
                    return models;
                }

                return null;
            }
            catch (Exception )
            {
                throw;
            }
        }
        /// <summary>
        /// This Method is Used for Deleting the Lables
        /// </summary>
        /// <param name="labelID"></param>
        /// <returns></returns>
        public LabelEntity DeleteLable(long labelID)
        {
            try
            {
                var validlabel = this.context.Label.FirstOrDefault(e => e.LabelID == labelID);
                if (validlabel != null)
                {
                    this.context.Label.Remove(validlabel);
                    this.context.SaveChanges();
                    return validlabel;
                }
                else return null;
            }
            catch (Exception )
            {
                throw;
            }
        }

    }
}
