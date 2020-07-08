using FamilyApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FamilyApp.Utils
{
    public class JsonUtils
    {
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions();

        public JsonUtils()
        {
            jsonOptions.PropertyNameCaseInsensitive = true;
        }

        public async Task<List<Person>> DeserializePeople(string jsonPeople)
        {
            List<Person> person = new List<Person>();

            JsonTextReader reader = new JsonTextReader(new StringReader(jsonPeople));

            JObject jsonString = await JObject.LoadAsync(reader);
            IList<JToken> results = jsonString["value"].Children().ToList();

            foreach(JToken result in results)
            {
                Person apiPersonObjToAdd = result.ToObject<Person>();
                person.Add(apiPersonObjToAdd);
            }

            return person;
        }
    }
}
