namespace Erp.Api.ViewModel;

public class ValidatedUsernameOrEmail
{
    public string Error { get; set; }
    public bool IsValid => UsernameOrEmailExists && UsernameOrEmailIsActive;
    public bool UsernameOrEmailExists { get; set; }
    public bool UsernameOrEmailIsActive { get; set; }
}