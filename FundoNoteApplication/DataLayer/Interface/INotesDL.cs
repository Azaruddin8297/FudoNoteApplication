using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface INotesDL
    {
        public NotesEntity AddNote(NoteModel notes);
        public bool CheckUserId(long userID);
        public NotesEntity DeleteNote(long NoteId);
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId);

    }
}
