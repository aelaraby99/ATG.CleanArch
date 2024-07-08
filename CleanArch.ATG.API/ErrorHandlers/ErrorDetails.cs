using System.Text.Json;

namespace CleanArch.ATG.API.ErrorHandlers
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public List<string> Message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
