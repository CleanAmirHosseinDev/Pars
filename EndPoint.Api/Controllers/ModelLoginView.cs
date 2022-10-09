namespace EndPoint.Api.Controllers
{
    public class ModelLoginView
    {
        public string Userneme { get; set; }
        public string Password { get; set; }
    }
    public class JsonResultViewModel
    {
        public int ResultCode
        {
            get;
            set;
        }
        public object Data
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
      
    }
}