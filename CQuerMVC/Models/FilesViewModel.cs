using Common.DTOs;
using System.Collections.Generic;

namespace CQuerMVC.Models
{
    public class FilesViewModel
    {
        public IEnumerable<FileReferenceDto> Files { get; set; }
    }
}
