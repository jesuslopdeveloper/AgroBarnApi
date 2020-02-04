namespace AgroBarn.Domain.ApiModels.V1.Request
{
    public class MessageRequest
    {
        public string Module { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}
