using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace DataLayer.Service
{
    public class NotesDL : INotesDL
    {
        private readonly FundoContext context;
        private readonly IConfiguration Config;
        public NotesDL(FundoContext context, IConfiguration Config)
        {
            this.context = context;
            this.Config = Config;

        }
        /// <summary>
        /// Checking the Enterd Id is correct or not
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool CheckUserId(long userID)
        {
            try
            {

                var check = context.UserTable.FirstOrDefault(x => x.UserId == userID);
                if (check != null)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This Method is Used for Adding Notes
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        public NotesEntity AddNote(NoteModel notes)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = notes.Title;
                notesEntity.Note = notes.Note;
                notesEntity.Color = notes.Color;
                notesEntity.Image = notes.Image;
                notesEntity.IsArchive = notes.IsArchive;
                notesEntity.IsPin = notes.IsPin;
                notesEntity.UserId = notes.UserId;
                this.context.Notes.Add(notesEntity);  
                this.context.SaveChanges(); 

                if (notesEntity != null)
                {
                    return notesEntity;
                }
                else return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// This Method is Used for Deleting the Notes Using NoteID
        /// </summary>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public NotesEntity DeleteNote(long NoteId)
        {
            try
            {

                var deleteNote = context.Notes.FirstOrDefault(x => x.NoteID == NoteId);
                if (deleteNote != null)
                {
                    context.Notes.Remove(deleteNote);
                    context.SaveChanges();
                    return deleteNote;
                }

                return null;


            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Updating the Notes
        /// </summary>
        /// <param name="noteModel"></param>
        /// <param name="NoteId"></param>
        /// <returns></returns>
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId)
        {
            try
            {
                var update = context.Notes.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.Title = noteModel.Title;
                    update.Note = noteModel.Note;
                    update.IsArchive = noteModel.IsArchive;
                    update.Color = noteModel.Color;
                    update.Image = noteModel.Image;
                    update.IsPin = noteModel.IsPin;
                    update.IsTrash = noteModel.IsTrash;
                    context.Notes.Update(update);
                    context.SaveChanges();
                    return update;

                }


                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This Method is Used for Changing pin
        /// </summary>
        /// <param name="NoteID"></param>
        /// <returns></returns>
        public bool Pinned(long NoteID)
        {
            try
            {
                var result = context.Notes.Where(r => r.NoteID == NoteID).FirstOrDefault();

                result.IsPin = !result.IsPin;
                context.SaveChanges();
                return result.IsPin;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This Method is Used for Changing Trash
        /// </summary>
        /// <param name="NoteID"></param>
        /// <returns></returns>
        public bool Trashed(long NoteID)
        {
            try
            {
                var result = context.Notes.Where(r => r.NoteID == NoteID).FirstOrDefault();

                result.IsTrash = !result.IsTrash;
                context.SaveChanges();
                return result.IsTrash;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This Method is Used for Changing Archiev
        /// </summary>
        /// <param name="NoteID"></param>
        /// <returns></returns>
        public bool Archieved(long NoteID)
        {
            try
            {
                var result = context.Notes.Where(r => r.NoteID == NoteID).FirstOrDefault();
                result.IsArchive = !result.IsArchive;
                context.SaveChanges();
                return result.IsArchive;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// This Method is Used for Changing Color of the Notes
        /// </summary>
        /// <param name="NoteId"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public NotesEntity ColorNote(long NoteId, string color)
        {
            var result = context.Notes.Where(r => r.NoteID == NoteId).FirstOrDefault();
            if (result != null)
            {

                result.Color = color;
                context.Notes.Update(result);
                context.SaveChanges();
                return result;

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// This Method is Used for Inserting the Image
        /// </summary>
        /// <param name="NoteID"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Imaged(long NoteID, IFormFile image)
        {
            try
            {
                var result = context.Notes.Where(x => x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(
                        "dppgqwsxl",        // CLOUD_NAME,API_KEY,API_SECRET
                         "363283181543147",
                         "N5BUeGSnvAKY2zJ8iqJKmc7I638"
                        );

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = image.FileName;
                    context.SaveChanges();
                    return "Image Upload Successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// This Method is Used for Searching Notes by Query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<NotesEntity> Search(string query)
        { 
            var result = this.context.Notes.Where(e => e.Title.Contains(query));
            if(result != null)
            {
                return result;
            }
            else return null;
            
        }
        /// <summary>
        /// This Method is Used for Getting All Notes
        /// </summary>
        /// <returns></returns>
        public List<NotesEntity> GetAllNote()
        {
            try
            {
                var Note = context.Notes.FirstOrDefault();

                if (Note != null)
                {
                    return context.Notes.ToList();
                }

                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
