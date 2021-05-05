using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Models
{
    public class TicketIdViewModel
    {
        public int Id { get; set; }
        public TicketIdViewModel(int id)
        {
            Id = id;
        }
        public TicketIdViewModel()
        {

        }
    }
}
