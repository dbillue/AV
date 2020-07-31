﻿using FamilyApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        public async Task<List<BirthState>> DeserializeBirthStates(string jsonbirthStates)
        {
            List<BirthState> states = new List<BirthState>();

            JsonTextReader reader = new JsonTextReader(new StringReader(jsonbirthStates));

            JObject jsonString = await JObject.LoadAsync(reader);
            IList<JToken> results = jsonString["value"].Children().ToList();

            foreach(var state in results)
            {
                BirthState stateToAdd = state.ToObject<BirthState>();
                states.Add(stateToAdd);
            }

            return states;
        }
    }
}
