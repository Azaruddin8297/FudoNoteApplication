using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {

        private readonly INotesDL NotesDL;

        public NotesBL(INotesDL NotesDL)
        {
            this.NotesDL = NotesDL;


        }
        public bool CheckUserId(long userID)
        {
            try
            {
               return this.NotesDL.CheckUserId(userID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity AddNote(NoteModel note)
        {
            try
            {
                return this.NotesDL.AddNote(note);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity DeleteNote(long NoteId)
        {
            try
            {
                return this.NotesDL.DeleteNote(NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId)
        {
            try
            {
                return this.NotesDL.UpdateNote(noteModel, NoteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
       
    }


