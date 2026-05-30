using VentasLimpieza.Core.CustomEntities;

namespace SocialMedia.Api.Responses
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public Pagination Pagination { get; set; }
        public Message[] Messages { get; set; }
        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}



//namespace VentasLimpieza.Api.Responses
//{
//    public class ApiResponse<T>
//    {
//        public T Data { get; set; }
//        public bool Success { get; set; }
//        public string Message { get; set; }

//        public ApiResponse(T data, bool success = true, string message = null)
//        {
//            Data = data;
//            Success = success;
//            Message = message;
//        }
//    }
//}
