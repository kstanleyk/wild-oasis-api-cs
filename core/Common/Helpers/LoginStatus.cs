namespace Core.Common.Helpers;

public enum LoginStatus
{
    AccountLocked,
    InvalidCredentials,
    ActiveConnection,
    LoginSuccess,
    LoginProgress,
    PasswordUnMatch,
    ChangePasswordProgress,
    ChangePasswordSuccess,
    RequireTwoFactor,
    Error
}