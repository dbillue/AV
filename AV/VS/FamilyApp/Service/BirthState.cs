using System;
using FamilyApp.Model;
using FamilyApp.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyApp.Service
{
    public class BirthState : IBirthState
    {
        SeriLog_Logger seriLogger = new SeriLog_Logger();

        public string GetBirthState(Person person)
        {
            string stateName = string.Empty;

            try
            {
                foreach (var state in person.birthState)
                {
                    if (person.StateId == state.StateId)
                    {
                        stateName = state.State;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }

            return stateName;
        }

        public int GetBirthStateId(Person person, List<Model.BirthState> birthStateList)
        {
            int stateId = 1;

            try
            {
                foreach (var state in birthStateList)
                {
                    if (person.state == state.State)
                    {
                        stateId = state.StateId;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                seriLogger.WriteError(ex.Message);
            }

            return stateId;
        }
    }
}
