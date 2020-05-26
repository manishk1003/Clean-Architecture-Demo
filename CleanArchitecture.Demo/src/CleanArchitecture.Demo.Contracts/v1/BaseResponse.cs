using System.Collections.Generic;

namespace CleanArchitecture.Demo.Contracts.v1
{
    public class BaseResponse
    {
        public List<string> Errors { get; set; }


        public BaseResponse()
        {
            Errors = new List<string>();
        }
    }
}
