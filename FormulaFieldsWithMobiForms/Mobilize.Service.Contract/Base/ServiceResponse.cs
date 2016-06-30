namespace Mobilize.Contract.Base
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ResponseCode { get; set; }
    }
}
