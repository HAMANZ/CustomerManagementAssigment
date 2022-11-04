using System.Net;

namespace CustomerManagment.DomainLayer.CommonObjects
{
    public class DynamicResponse<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string ServerMessage { get; set; }

    }
}
