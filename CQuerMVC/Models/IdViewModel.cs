using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Models
{
    public class IdViewModel
    {
        public int Id { get; set; }
        public IdViewModel(int id)
        {
            Id = id;
        }
        public IdViewModel()
        {

        }
    }
}
