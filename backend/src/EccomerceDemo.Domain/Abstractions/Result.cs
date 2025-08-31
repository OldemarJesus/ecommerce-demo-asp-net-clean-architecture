using System.Diagnostics.CodeAnalysis;

namespace EccomerceDemo.Domain.Abstractions;

public class Result
{
    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
            throw new InvalidOperationException("Invalid result state");

        IsSuccess = isSuccess;
        Errors = new List<Error> { error };
    }

    public Result(bool isSuccess, IEnumerable<Error> errors)
    {
        if (isSuccess && errors.Any() ||
            !isSuccess && !errors.Any())
            throw new InvalidOperationException("Invalid result state");

        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public IEnumerable<Error> Errors { get; }
    public bool IsFailure => !IsSuccess;
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result Failure(IEnumerable<Error> errors) => new(false, errors);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result<TValue> Failure<TValue>(Error error) => new(default!, false, error);
    public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) => new(default!, false, errors);
}

public class Result<TValue> : Result
{
    private readonly TValue _value;

    public Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public Result(TValue value, bool isSuccess, IEnumerable<Error> errors)
        : base(isSuccess, errors)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failed result.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null
            ? Success(value)
            : Failure<TValue>(Error.None);

    public static Result<TValue> ValidationFailure(Error error) =>
        new(default!, false, error);
}