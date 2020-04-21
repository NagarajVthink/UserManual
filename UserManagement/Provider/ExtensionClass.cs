using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace UserManagement.Provider
{
    public static class ExtensionClass
    {
        //Convert HttpContent to JObject
        public static JObject ConvertHttpResponse(this HttpContent content)
        {
            var responseContent = content.ReadAsStringAsync();
            return JObject.Parse(responseContent.Result);
        }
    }
}
