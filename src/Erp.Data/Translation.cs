namespace Erp.Data;

public class Translation
{
    public string Json { get; set; }
    public string FileName { get; set; }
    public string FileNameWithExtension => $"{FileName}.json";
}