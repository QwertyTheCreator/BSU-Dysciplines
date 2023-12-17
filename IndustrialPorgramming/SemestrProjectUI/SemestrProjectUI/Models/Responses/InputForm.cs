namespace SemesterProjectUI.Models.Responses
{
    public enum OutputFormat
    {
        XML, TXT, JSON
    }

    public enum AdditionalOutputFormat
    {
        OnlyEnc, OnlyZip, ZipThenEnc, EncThenZip, Nothing
    }

    public class InputForm
    {
        public string? StarterPath { get; set; }

        public string? DirectPath { get; set; }

        public string? AnswerName { get; set; } = "Answer";

        public OutputFormat? AnswerFormat { get; set; }

        public AdditionalOutputFormat? AdditionalOutputFormat { get; set; }

        public bool IsValid()
        {
            if(StarterPath is null || !File.Exists(StarterPath))
            {
                return false;
            }

            if(DirectPath is null || !Directory.Exists(DirectPath)) 
            {
                return false;
            }

            if (AnswerFormat is null)
            {
                return false;
            }

            if(AdditionalOutputFormat is null)
            {
                return false;
            }

            return true;
        }
    }
}
