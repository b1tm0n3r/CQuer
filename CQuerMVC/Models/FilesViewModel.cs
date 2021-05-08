﻿using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQuerMVC.Models
{
    public class FilesViewModel
    {
        public IEnumerable<FileReferenceDto> Files { get; set; }
    }
}