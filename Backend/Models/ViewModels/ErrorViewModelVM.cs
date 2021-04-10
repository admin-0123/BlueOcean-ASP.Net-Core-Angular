namespace Virta.MVC.ViewModels
{
    public class ErrorViewModelVM
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
