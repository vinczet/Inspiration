using ErrorOr;

namespace Inspiration.Domain;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Duplicate email");

        public static Error EmptyName => Error.Validation(
            code: "User.EmptyName",
            description: "Name can not be empty");

        public static Error EmptyEmail => Error.Validation(
            code: "User.EmptyEmail",
            description: "Email can not be empty");

        public static Error EmptyPassword => Error.Validation(
            code: "User.EmptyPassword",
            description: "Passowrd can not be empty");

        public static Error UserNotFound => Error.Conflict(
            code: "User.UserNotFound",
            description: "User not found");

    }
}
