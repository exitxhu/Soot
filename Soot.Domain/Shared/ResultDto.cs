namespace Soot.Domain.Shared
{
    public class ResultDto
    {
        public ResultDto()
        {

        }
        public ResultDto(bool isSuccessful, string message, object data)
        {
            IsSuccessful = isSuccessful;
            Message = message;
            Data = data;
        }

        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
