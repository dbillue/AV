using FamilyApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FamilyApp.Utils
{
    public class JsonUtils
    {
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions();

        public JsonUtils()
        {
            jsonOptions.PropertyNameCaseInsensitive = true;
        }

        public List<T> Deserialize<T>(ref T obj, string data)
        {
            List<T> objModel = new List<T>();

            JsonTextReader reader = new JsonTextReader(new StringReader(data));

            JObject jsonString = JObject.Load(reader);
            IList<JToken> results = jsonString["value"].Children().ToList();

            foreach (var dataValue in results)
            {
                var valueAdded = dataValue.ToObject<T>();
                objModel.Add(valueAdded);
            }

            return objModel;
        }

        public string SerializeObj<T>(ref T obj)
        {
            string jsonPet = string.Empty;
            return jsonPet = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
