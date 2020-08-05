using AutoMapper;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.Models;
using FamilyDemoAPIv2.ResourceParameters;
using FamilyDemoAPIv2.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace FamilyDemoAPIv2.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api/pets")]
    public class PetsController : ControllerBase
    {
        private readonly IFamilyDemoAPIv2Repository _familyDemoAPIv2Repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly Logger _log;

        // ctor.
        public PetsController(IFamilyDemoAPIv2Repository familyDemoAPIv2Repository,
                            IMapper mapper,
                            ILogger<PetsController> logger)
        {
            _familyDemoAPIv2Repository = familyDemoAPIv2Repository;
            _mapper = mapper;
            _logger = logger;
            _log = new Logger(_logger);
        }

        [HttpGet(Name = "Index")]
        public string Index()
        {
            return "Hello from the Pets controller of the FamilyAPIService.";
        }

        /// <summary>
        /// Use this method to query for a list of pets.
        /// </summary>
        /// <param name="GetPets"></param>
        /// <returns>List of pets</returns>
        /// <remarks>HttpGet verb.</remarks>
        [HttpGet("{GetPets}", Name = "GetPets")]
        [Route("GetPets")]
        public ActionResult<IEnumerable<GetPetsDTO>> GetPets(string GetPets = "GetPets")
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:Pets,API:GetPets,DateTime:" + DateTime.Now.ToString());

            // Obtain list of pets.
            var pets = _familyDemoAPIv2Repository.GetPets();

            // Map list of pets to DTO.
            var petsList = _mapper.Map<IEnumerable<GetPetsDTO>>(pets);

            // Add name to JSON array.
            var petsListCollection = new
            {
                value = petsList
            };

            // Return pets collection.
            return Ok(petsListCollection);
        }

        /// <summary>
        /// Use this method to query for a list of pet types.
        /// </summary>
        /// <param name="GetPetTypes"></param>
        /// <returns>List of pet types</returns>
        /// <remarks>HttpGet verb.</remarks>
        [HttpGet("{GetPetTypes}", Name = "GetPetTypes")]
        [Route("GetPetTypes")]
        public ActionResult<IEnumerable<GetPetTypesDTO>> GetPetTypes(string GetPetTypes = "GetPetTypes")
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:Pets,API:GetPetTypes,DateTime:" + DateTime.Now.ToString());

            // Obtain list of pet types.
            var petTypeList = _familyDemoAPIv2Repository.GetPetTypes();

            // Map list of pet types to DTO.
            var petTypeListMapping = _mapper.Map<IEnumerable<GetPetTypesDTO>>(petTypeList);

            // Add name to JSON array.
            var petTypeListCollection = new
            {
                value = petTypeListMapping,
            };

            // Return pet type collection.
            return Ok(petTypeListCollection);
        }

        /// <summary>
        /// Use this method to add a new pet.
        /// </summary>
        /// <param name="pet"></param>
        /// <returns>The newly created pet via 200 response.</returns>
        /// <remarks>HttpPost verb.</remarks>
        [HttpPost(Name = "AddPet")]
        public async Task<ActionResult<AddPetDTO>> AddPet(AddPetDTO pet)
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:PetsController,API:AddPet,DateTime:" + DateTime.Now.ToString());
            
            try
            {
                var petEntity = _mapper.Map<Entities.Pet>(pet); // Map to entity.
                await _familyDemoAPIv2Repository.AddPet(petEntity); // Add.
                await _familyDemoAPIv2Repository.Save(); // Save.

                var petToReturn = _mapper.Map<AddPetDTO>(petEntity);

                // Log addition of new pet.
                _log.WriteInformation("New pet added", null, petToReturn);

                // Return person added.
                //return Ok(petToReturn);

                // Return link in header.
                return CreatedAtRoute("AddPet",
                    new { petToReturn.PetId },
                    petToReturn);
            } catch (Exception ex) {
                _log.WriteError(ex.Message, ex.InnerException);
                return NoContent();
            }
        }
    }
}
