using FamilyApp.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System;

namespace FamilyApp.Utils
{
    public class JsonUtils
    {
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        private string patchDocument = string.Empty;
        private string personPatchDocument = string.Empty;

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
            string jsonObject = string.Empty;
            return jsonObject = JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        // TODO: Make generic
        public string CreatePatchDocument(string dataType, Person person = null, Pet pet = null)
        {
            switch (dataType)
            {
                case "person":
                    patchDocument = BuildPatchDocumentPerson(person).ToString();
                    break;
                case "pet":
                    patchDocument = BuildPatchDocumentPet(pet).ToString();
                    break;
                default:
                    break;
            }

            return patchDocument;
        }

        // TODO: Add system reflection
        public string BuildPatchDocumentPerson(Person person)
        {
            personPatchDocument = @"[{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/FirstName"",";
            personPatchDocument += @"""value"": """ + person.FirstName + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/MIddleName"",";
            personPatchDocument += @"""value"": """ + person.MIddleName + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/LastName"",";
            personPatchDocument += @"""value"": """ + person.LastName + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/Gender"",";
            personPatchDocument += @"""value"": """ + person.Gender + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/Age"",";
            personPatchDocument += @"""value"": """ + person.Age.ToString() + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/Country"",";
            personPatchDocument += @"""value"": """ + person.Country + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/City"",";
            personPatchDocument += @"""value"": """ + person.City + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/StateId"",";
            personPatchDocument += @"""value"": """ + person.StateId.ToString() + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/DateOfBirth"",";
            personPatchDocument += @"""value"": """ + person.DateOfBirth.ToString() + "";
            personPatchDocument += @"""}]";

            return personPatchDocument;
        }

        public string BuildPatchDocumentPet(Pet pet)
        {
            personPatchDocument = @"[{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/Name"",";
            personPatchDocument += @"""value"": """ + pet.Name + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/NickName"",";
            personPatchDocument += @"""value"": """ + pet.NickName + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/PetTypeId"",";
            personPatchDocument += @"""value"": """ + pet.PetTypeId.ToString() + "";
            personPatchDocument += @"""},";
            personPatchDocument += @"{""op"": ""replace"",";
            personPatchDocument += @"""path"": ""/PersonId"",";
            personPatchDocument += @"""value"": """ + pet.PersonId + "";
            personPatchDocument += @"""}]";

            return personPatchDocument;
        }
    }
}
