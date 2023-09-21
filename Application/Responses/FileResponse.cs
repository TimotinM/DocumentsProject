
namespace Application.Responses
{
    public class FileResponse
    {
        public bool Succes { get; set; }
        public FileStream? FileStream { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Message { get; set; }
    }
}
