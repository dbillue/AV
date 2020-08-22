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
            patchDocument = @"[{""op"": ""replace"",";
            patchDocument += @"""path"": ""/FirstName"",";
            patchDocument += @"""value"": """ + person.FirstName + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/MIddleName"",";
            patchDocument += @"""value"": """ + person.MIddleName + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/LastName"",";
            patchDocument += @"""value"": """ + person.LastName + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/Gender"",";
            patchDocument += @"""value"": """ + person.Gender + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/Age"",";
            patchDocument += @"""value"": """ + person.Age.ToString() + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/Country"",";
            patchDocument += @"""value"": """ + person.Country + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/City"",";
            patchDocument += @"""value"": """ + person.City + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/StateId"",";
            patchDocument += @"""value"": """ + person.StateId.ToString() + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/DateOfBirth"",";
            patchDocument += @"""value"": """ + person.DateOfBirth.ToString() + "";
            patchDocument += @"""}]";

            return patchDocument;
        }

        public string BuildPatchDocumentPet(Pet pet)
        {
            patchDocument = @"[{""op"": ""replace"",";
            patchDocument += @"""path"": ""/Name"",";
            patchDocument += @"""value"": """ + pet.Name + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/NickName"",";
            patchDocument += @"""value"": """ + pet.NickName + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/PetTypeId"",";
            patchDocument += @"""value"": """ + pet.PetTypeId.ToString() + "";
            patchDocument += @"""},";
            patchDocument += @"{""op"": ""replace"",";
            patchDocument += @"""path"": ""/PersonId"",";
            patchDocument += @"""value"": """ + pet.PersonId + "";
            patchDocument += @"""}]";

            return patchDocument;
        }
    }
}
