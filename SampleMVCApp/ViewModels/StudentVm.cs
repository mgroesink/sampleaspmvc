using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SampleMVCApp.ViewModels
{
    public class StudentVm
    {
        /* 
         * De fields en properties zijn hieronder op een niet consequente manier aangemaakt.
         * Dit is om te laten zien hoe je op verschillende manieren fields en properties
         * kunt gebruiken.
         * De meest logische manier zou zijngeweest om alles te doen met
         * auto implemented properties:
         * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
         * 
        */

        #region Auto implemented properties
        [Key]
        public string Id { get; set; }
        public string Crebo { get; set; }
        public string Group { get; set; }
        public string Email { get; set; }
        #endregion

        #region Fields
        [Required]
        private string studentId;
        private string slb;
        private string username;
        private string password;
        #endregion

        #region Properties
        // Read only
        public string StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        // Read-Write
        public string Slb
        {
            get { return slb; }
            set { slb = value.ToUpper(); }
        }

        // Read-Write
        public string Username
        {
            get { return username; }
            set { username = value.ToLower(); }
        }

        // Read-Write
        public string Password
        {
            get { return password; }
            set { password = value; }
        } 
        #endregion

    }
}