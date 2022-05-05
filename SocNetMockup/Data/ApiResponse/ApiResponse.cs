using System.Collections.Generic;
using System.Linq;

namespace SocNetMockup.Data.ApiResponse
{
    public class ApiResponse : Dictionary<string, object>
    {
        public ApiResponse(object response)
        {
            this["success"] = true;
            this["response"] = response;
        }

        public ApiResponse(IEnumerable<object> response)
        {
            var resp = response.ToArray();

            this["success"] = true;
            this["count"] = resp.Length;
            this["response"] = resp;
        }
    }
}
