namespace Todo.Domain.ViewModels
{
    public class ResponseViewModel
    {
        public ResponseViewModel(bool s, string m, object d)
        {
            Success = s;
            Message = m;
            Data = d;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class ResponseViewModel<T>
    {
        public ResponseViewModel(bool s, string m, T d)
        {
            Success = s;
            Message = m;
            Data = d;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
