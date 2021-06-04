using Common.DTOs;
using System.Collections.Generic;

namespace CQuerMVC.Models
{
    public class TicketsViewModel
    {
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}


