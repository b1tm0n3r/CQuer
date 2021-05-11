using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Models
{
    public class LoginViewModel
    {
        public LoginDto LoginDto { get; set; }
        public string RedirectUrl { get; set; }
    }
}
