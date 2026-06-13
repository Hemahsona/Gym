using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic
{
    public sealed record Result(bool success, string? error = null, ResultKind resultKind = ResultKind.Ok)
    {
        public static Result Success() => new(true);
        public static Result Failure(string error, ResultKind resultKind = ResultKind.Conflict) => new(false, error, resultKind);
        //public static Result NotFound(string error = "Not found") => new(false, error, ResultKind.NotFound);
        //public static Result Validation(string error) => new(false, error, ResultKind.ValidationError);

    }

    public sealed record Result<T>(bool success, T? value = default, string? error = null, ResultKind resultKind = ResultKind.Ok)
    {
        public static Result<T> IsSuccess(T value) => new(true, value);
        public static Result<T> Failure(string error, ResultKind resultKind = ResultKind.Conflict) => new(false, default, error, resultKind);
        //public static Result<T> NotFound(string error = "Not found") => new(false, default, error, ResultKind.NotFound);
        //public static Result<T> Validation(string error) => new(false, default, error, ResultKind.ValidationError);
    }
}
