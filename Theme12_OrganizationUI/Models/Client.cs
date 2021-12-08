using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme12_OrganizationUI.Models
{
    class Client : Person
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// VIP?
        /// </summary>
        public bool IsVIP { get; set; }
    }
}
