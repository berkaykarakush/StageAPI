namespace StageAPI.Application
{
    /// <summary>
    /// A generic result class that holds information about the success or failure of an operation and its result.
    /// </summary>
    /// <typeparam name="T">Type of the result value.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Gets or sets the result value
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Gets or sets the error message in case of failure
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Creates a new instance of Result with a successful outcome and a specified value
        /// </summary>
        /// <param name="value">The value of the successful result</param>
        /// <returns>A new instance of Result with success state and the provided value</returns>
        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
        /// <summary>
        /// Creates a new instance of Result with a failure outcome and a specified error message
        /// </summary>
        /// <param name="error">The error message in case of failure</param>
        /// <returns>A new instance of Result with failure state and the provided error message</returns>
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Error = error };
    }
}