using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Project.Core.Extensions
{
    public static class JsonSerializeExtensions
    {

        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };


        //JSONCONVERT SINIFI STATİK BİR SINIF OLDUĞU İÇİN, EXTENSION YAZAMIYORUZ.
        public static string CamelCaseSerialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize, Formatting.Indented, Settings);
        }
    }
}