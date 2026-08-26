using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BusinessLogic
{
    public sealed record Result(bool IsSuccess, string? Error = null, ResultKind ResultKind = ResultKind.Ok)
    {
        public static Result Success() => new(true);
        public static Result Failure(string error, ResultKind resultKind = ResultKind.Conflict) => new(false, error, resultKind);
    }

    public sealed record Result<T>(bool IsSuccess, T? Value = default, string? Error = null, ResultKind ResultKind = ResultKind.Ok)
    {
        public static Result<T> Success(T value) => new(true, value);
        public static Result<T> Failure(string error, ResultKind resultKind = ResultKind.Conflict) => new(false, default, error, resultKind);
    }
}
