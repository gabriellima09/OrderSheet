using System.Collections.Generic;

namespace OrderSheet.Application.Abstractions.Messaging
{
    public class ErrorMessageResponse
    {
        public int Status { get; set; }
        public string Detail { get; set; }
        public IReadOnlyDictionary<string, string[]> Errors { get; set; }
    }
}
