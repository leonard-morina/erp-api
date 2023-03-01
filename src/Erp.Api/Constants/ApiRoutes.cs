namespace Erp.Api.Constants;

public static class ApiRoutes
{
    public static class Company
    {
        public const string MY_LIST = "list/my";
        public const string CREATE = "create";
        public const string REQUEST_JOIN = "request/join";
        public const string JOIN_CODE_BY_COMPANY_ID = "join/code";
        public const string GET_JOIN_REQUESTS_AS_OWNER = "join/requests";
        public const string GET_JOIN_MY_REQUESTS = "join/requests/my";
    }

    public static class Account
    {
        public const string LOGIN = "login";
        public const string REGISTER = "register";
        public const string USERNAME_EMAIL_VALID = "username-or-email/valid";
    }

    public static class Settings
    {
    }
}