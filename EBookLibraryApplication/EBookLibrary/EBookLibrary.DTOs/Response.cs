using Newtonsoft.Json;


namespace EBookLibrary.DTOs
{
    public class TResponse<T>
    {
        public TResponse(int statuCode, string message, T details = default)
        {
            StatusCode = statuCode;
            Message = message;
            Data = details;
        }
        public TResponse()
        {

        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }



        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
