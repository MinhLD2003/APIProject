namespace Project.API.Helper
{
    public class APIResponse
    {
        public int ResponseCode { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public string ErrorMessage {  get; set; }
    }
}
