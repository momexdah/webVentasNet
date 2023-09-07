namespace WebSistemaVentas.Core;

public class Response
{
    public bool statusprocess { get; set; }
    public string messagetype { get; set; }
    public string responsemessage { get; set; }
    public object responsevalue { get; set; }

    public Response()
    {
        statusprocess = false;
        messagetype = "ERROR";
        responsemessage = "Error App: No Procesado";
        responsevalue = null;
    }
}