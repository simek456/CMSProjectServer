namespace CMSProjectServer.Domain;

public class Result<T>
{
    private Result()
    { }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>()
        {
            Value = value,
            IsSuccess = true,
        };
    }

    public T? Value { get; set; }
    public bool IsSuccess { get; set; }
    public string? Error { get; set; }

    public static Result<T> Failure(string? error = null)
    {
        return new Result<T>() { IsSuccess = false, Error = error };
    }
}