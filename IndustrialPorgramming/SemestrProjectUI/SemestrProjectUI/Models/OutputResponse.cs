namespace SemestrProjectUI.Models
{
    public enum Format
    {
        XML, TXT, JSON
    }

    public enum ZipEncFormat
    {
        None, OnlyEnc, OnlyZip, FirstZipThenEnc, FirstEncThenZip 
    }

    public class OutputResponse
    {
        public string? Path { get; set; }
        public string? outPath { get; set; }

        public Format? fileFormat { get; set; }

        public ZipEncFormat? format { get; set; }

        public string? AnswerName { get; set; }
    }
}
