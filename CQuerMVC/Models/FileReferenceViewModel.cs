namespace CQuerMVC.Models
{
    public class FileReferenceViewModel
    {
        public int TicketId { get; set; }
        public string DownloadUrl { get; set; }
        public string Sha256Checksum { get; set; }
    }
}
