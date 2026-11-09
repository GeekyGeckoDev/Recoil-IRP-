using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AuthApp.AuthMessages
{
    public class LoginMessages
    {
            public static string LoginSuccess()
                => "Login successful";

            public static string ConfirmationSent()
                => "Confirmation email successfully sent";

            public static string PasswordChanged()
                => "Password successfully changed";

            public static string IncorrectLogin()
                => "Incorrect email or password";

            public static string InvalidRefreshToken()
            => "Invalid refresh token";
    }
}
