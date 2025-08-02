namespace BudgetApp.Api.Core.Constants;

public static class ApplicationConstants
{
    public static class Authentication
    {
        public const int TokenExpirationDays = 7;
        public const int MinPasswordLength = 6;
        public const int MaxPasswordLength = 100;
        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 50;
    }

    public static class Validation
    {
        public const int MaxDescriptionLength = 500;
        public const int MaxNameLength = 100;
        public const int MaxEmailLength = 100;
        public const decimal MinAmount = 0.01m;
        public const int MinStatementDay = 1;
        public const int MaxStatementDay = 31;
        public const int MinPaymentOffset = 0;
        public const int MaxPaymentOffset = 30;
    }

    public static class Claims
    {
        public const string UserId = "uid";
        public const string Username = "username";
        public const string Email = "email";
    }

    public static class Patterns
    {
        public const string Last4Digits = @"^\d{4}$";
        public const string ExpirationDate = @"^(0[1-9]|1[0-2])\/\d{2}$";
        public const string Email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    }
}