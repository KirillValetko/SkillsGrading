using System.Text.Json.Serialization;

namespace SkillsGrading.BusinessLogic.Responses
{
    public class ApiResponse<T>
    {
        public T Payload { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        [JsonConstructor]
        public ApiResponse()
        {
        }

        public ApiResponse(T payload)
        {
            Payload = payload;
            ErrorMessage = string.Empty;
            ErrorCode = 0;
        }

        public ApiResponse(string errorMessage) : this(errorMessage, 601)
        {
        }

        public ApiResponse(string errorMessage, int errorCode)
        {
            Payload = default;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}
