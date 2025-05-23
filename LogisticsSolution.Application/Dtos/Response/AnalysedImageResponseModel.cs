namespace LogisticsSolution.Application.Dtos.Response
{
    public class AnalysedImageResponseModel
    {
        public string FileName { get; set; }
        public bool IsImage { get; set; }
        public bool IsAnalysed { get; set; }
        public int NumberOfDetectedItems { get; set; }
        public List<string>? items { get; set; } = null;
    }
}
