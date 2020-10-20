using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NoteApp
{
    /// <summary>
    /// DB Table
    /// </summary>
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int      ID         { get; set; }
        public string   Name       { get; set; }
        public string   Text       { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }

    /// <summary>
    /// DB Wrapper
    /// </summary>
    static class Notes
    {
        #region params

        private static SQLiteAsyncConnection connection;

        
        #endregion

        public static void Connect(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Note>().Wait();
        }

        #region queries

        public static Task<List<Note>> GetNotesAsync()
        {
            return connection.Table<Note>().ToListAsync();
        }

        public static Task<Note> GetNoteById(int id)
        {
            return connection.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public static Task<int> SaveNote(Note note)
        {
            if (note.ID != 0)
            {
                return connection.UpdateAsync(note);
            }
            else
            {
                return connection.InsertAsync(note);
            }
        }

        public static Task<int> DeleteNote(Note note)
        {
            return connection.DeleteAsync(note);
        }

        public static Task<int> DeleteNoteById(int id)
        {
            return connection.DeleteAsync(connection.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync());
        }

        #endregion
    }
}
