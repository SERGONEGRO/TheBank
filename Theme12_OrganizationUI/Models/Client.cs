using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBank.Models
{
    public class Client : Person
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// VIP?
        /// </summary>
        public bool IsVIP { get; set; }

        //public Client(string firstname,string lastname, string dateOfBirth, int id, bool isVip)
        //{
        //    FirstName = firstname;
        //    LastName = lastname;
        //    DateOfBirth = (DateTime)dateOfBirth;
        //}
    }
}
