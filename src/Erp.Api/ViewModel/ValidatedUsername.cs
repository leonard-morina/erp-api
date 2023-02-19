namespace Erp.Api.ViewModel;

public class ValidatedUsername
{
    public string Error { get; set; }
    public bool IsValid => UsernameExists && UsernameIsActive;
    public bool UsernameExists { get; set; }
    public bool UsernameIsActive { get; set; }
}