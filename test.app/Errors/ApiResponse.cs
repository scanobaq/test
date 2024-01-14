namespace test.app.Errors;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResponse(int statusCode, string message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessage(statusCode);
    }

    private string GetDefaultMessage(object statusCode)
    {
        return statusCode switch
        {
            400 => "Has realizado una peticion incorrecta.",
            401 => "Usuario no autorizado.",
            404 => "El recurzo solicitado no exisrte.",
            405 => "Este metodo HTTP no esta permitido en el servidor",
            500 => "Error en el servidor. No eres tu soy yo"
        };
    }

}
