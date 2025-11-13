using System;
using System.Collections.Generic;
using System.Linq;

namespace Morpho.Domain.Common
{
    /// <summary>
    /// Represents the result of an operation that can either succeed or fail.
    /// </summary>
    public class Result
    {
        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("Success result cannot have an error");

            if (!isSuccess && string.IsNullOrWhiteSpace(error))
                throw new InvalidOperationException("Failed result must have an error");

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }

        public static Result Success() => new Result(true, null);
        public static Result Failure(string error) => new Result(false, error);

        public static Result<T> Success<T>(T value) => new Result<T>(value, true, null);
        public static Result<T> Failure<T>(string error) => new Result<T>(default, false, error);

        public static implicit operator Result(string error) => Failure(error);
    }

    /// <summary>
    /// Represents the result of an operation that returns a value and can either succeed or fail.
    /// </summary>
    /// <typeparam name="T">The type of the value returned on success.</typeparam>
    public class Result<T> : Result
    {
        private readonly T _value;

        protected internal Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            _value = value;
        }

        public T Value
        {
            get
            {
                if (IsFailure)
                    throw new InvalidOperationException("Cannot access value of a failed result");
                return _value;
            }
        }

        public static implicit operator Result<T>(T value) => Success(value);
        public static implicit operator Result<T>(string error) => Failure<T>(error);
    }

    /// <summary>
    /// Extension methods for Result types to enable functional composition.
    /// </summary>
    public static class ResultExtensions
    {
        public static Result<TOutput> Map<TInput, TOutput>(this Result<TInput> result, Func<TInput, TOutput> func)
        {
            if (result.IsFailure)
                return Result.Failure<TOutput>(result.Error);

            try
            {
                return Result.Success(func(result.Value));
            }
            catch (Exception ex)
            {
                return Result.Failure<TOutput>(ex.Message);
            }
        }

        public static Result<TOutput> Bind<TInput, TOutput>(this Result<TInput> result, Func<TInput, Result<TOutput>> func)
        {
            if (result.IsFailure)
                return Result.Failure<TOutput>(result.Error);

            try
            {
                return func(result.Value);
            }
            catch (Exception ex)
            {
                return Result.Failure<TOutput>(ex.Message);
            }
        }

        public static Result Bind<T>(this Result<T> result, Func<T, Result> func)
        {
            if (result.IsFailure)
                return Result.Failure(result.Error);

            try
            {
                return func(result.Value);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public static T Match<T>(this Result result, Func<T> onSuccess, Func<string, T> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result.Error);
        }

        public static T Match<T, TValue>(this Result<TValue> result, Func<TValue, T> onSuccess, Func<string, T> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
        }
    }
}