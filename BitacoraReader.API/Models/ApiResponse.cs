namespace BitacoraReader.API.Models
{
    /// <summary>
    /// Modelo de respuesta estándar para la API
    /// </summary>
    /// <typeparam name="T">Tipo de datos de la respuesta</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indica si la operación fue exitosa
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mensaje descriptivo de la respuesta
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Datos de la respuesta
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Timestamp de la respuesta
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Lista de errores (si los hay)
        /// </summary>
        public List<string>? Errors { get; set; }

        public ApiResponse()
        {
        }

        public ApiResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }

    /// <summary>
    /// Modelo de respuesta estándar sin datos tipados
    /// </summary>
    public class ApiResponse : ApiResponse<object>
    {
        public ApiResponse() : base()
        {
        }

        public ApiResponse(bool success, string message) : base(success, message)
        {
        }

        /// <summary>
        /// Crea una respuesta exitosa
        /// </summary>
        /// <typeparam name="T">Tipo de datos</typeparam>
        /// <param name="data">Datos a retornar</param>
        /// <param name="message">Mensaje opcional</param>
        /// <returns>Respuesta exitosa</returns>
        public static ApiResponse<T> Success<T>(T data, string message = "Operación exitosa")
        {
            return new ApiResponse<T>(true, message, data);
        }

        /// <summary>
        /// Crea una respuesta exitosa sin datos
        /// </summary>
        /// <param name="message">Mensaje</param>
        /// <returns>Respuesta exitosa</returns>
        public static ApiResponse Success(string message = "Operación exitosa")
        {
            return new ApiResponse(true, message);
        }

        /// <summary>
        /// Crea una respuesta de error
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        /// <param name="errors">Lista de errores específicos</param>
        /// <returns>Respuesta de error</returns>
        public static ApiResponse Error(string message, List<string>? errors = null)
        {
            return new ApiResponse(false, message)
            {
                Errors = errors
            };
        }

        /// <summary>
        /// Crea una respuesta de error con datos tipados
        /// </summary>
        /// <typeparam name="T">Tipo de datos</typeparam>
        /// <param name="message">Mensaje de error</param>
        /// <param name="errors">Lista de errores específicos</param>
        /// <returns>Respuesta de error</returns>
        public static ApiResponse<T> Error<T>(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>(false, message)
            {
                Errors = errors
            };
        }
    }
}