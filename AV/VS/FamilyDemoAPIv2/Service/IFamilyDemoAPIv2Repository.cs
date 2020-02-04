using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyDemoAPIv2.Entities;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.ResourceParameters;


namespace FamilyDemoAPIv2.Service
{
    public interface IFamilyDemoAPIv2Repository
    {
        string Response();

        bool PersonExists(Guid personId);

        void AddPerson(Person person);

        Person GetPerson(Guid personId);

        PagedList<Person> GetPersons(PersonResourceParameters authorsResourceParameters);

        Person UpdatePerson(Person person);

        void DeletePerson(Person person);

        bool Save();
    }
}
