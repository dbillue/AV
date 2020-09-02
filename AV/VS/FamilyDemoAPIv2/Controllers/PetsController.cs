using AutoMapper;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.Models;
using FamilyDemoAPIv2.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<PetDTO>> GetPets(string GetPets = "GetPets")
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:Pets,API:GetPets,DateTime:" + DateTime.Now.ToString());

            // Obtain list of pets.
            var pets = _familyDemoAPIv2Repository.GetPets().Result;

            // Map list of pets to DTO.
            var petsList = _mapper.Map<IEnumerable<PetDTO>>(pets);

            // Add name to JSON array.
            var petsListCollection = new
            {
                value = petsList
            };

            // Return pets collection.
            return Ok(petsListCollection);
        }

        /// <summary>
        /// Use this method to query for a pet
        /// </summary>
        /// <param name="petId"></param>
        /// <param name="GetPet"></param>
        /// <returns>A pet</returns>
        /// <remarks>HttpGet verb.</remarks>
        [HttpGet("{GetPet}", Name = "GetPet")]
        [Route("GetPet")]
        public async Task<ActionResult> GetPet([FromQuery] Guid petId, string GetPet = "GetPet")
        {
            // Guid petId = new Guid();

            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:Pets,API:GetPet,DateTime:" + DateTime.Now.ToString());


            // Ensure person exists.
            if (!_familyDemoAPIv2Repository.PetExists(petId).Result)
            {
                return NotFound();
            }

            var petFromRepo = await _familyDemoAPIv2Repository.GetPet(petId); // Obtain record via DbContext query and store in entity.
            var petToReturn = _mapper.Map<PetDTO>(petFromRepo); // Map entity to return DTO.

            // Return person.
            return Ok(petToReturn);
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
            var petTypeList = _familyDemoAPIv2Repository.GetPetTypes().Result;

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
        [HttpPost(Name = "AddPetAsync")]
        public async Task<ActionResult<PetDTO>> AddPetAsync(PetDTO pet)
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:PetsController,API:AddPet,DateTime:" + DateTime.Now.ToString());

            try
            {
                var petEntity = _mapper.Map<Entities.Pet>(pet); // Map to entity.
                await _familyDemoAPIv2Repository.AddPet(petEntity); // Add.

                var petToReturn = _mapper.Map<PetDTO>(petEntity);

                // Log addition of new pet.
                _log.WriteInformation("New pet added", null, petToReturn);

                // Return person added.
                //return Ok(petToReturn);

                // Return link in header.
                return CreatedAtRoute("AddPetAsync",
                    new { petToReturn.PetId },
                    petToReturn);
            }
            catch (Exception ex)
            {
                _log.WriteError(ex.Message, ex.InnerException);
                return NoContent();
            }
        }

        /// <summary>
        /// Use this method to update a pet.
        /// </summary>
        /// <param name="petId">The pet's Guid Id</param>
        /// <param name="patchDocument">The pets information in JSON patch format.</param>
        /// <returns>The updated pet's information via 200 response.</returns>
        /// <remarks>HttpPatch verb. \
        /// pet/petId \
        /// [ \
        ///     { \
        ///         "op": "replace", \
        ///         "path": "/Name", \
        ///         "value": "Pet's name." \
        ///     } \
        /// ]
        /// </remarks>
        [HttpPatch("{petId}", Name = "UpdatePetAsync")]
        public async Task<ActionResult> UpdatePetAsync(Guid petId, JsonPatchDocument<PetDTO> patchDocument)
        {
            // Ensure pet exists.
            var exists = await _familyDemoAPIv2Repository.PetExists(petId);
            if (!exists)
            {
                return NotFound();
            }

            var petFromRepo = _familyDemoAPIv2Repository.GetPet(petId).Result; // Obtain record via DbContext query and store in entity.
            var petToPatch = _mapper.Map<PetDTO>(petFromRepo); // Map populated entity to DTO.
            patchDocument.ApplyTo(petToPatch, ModelState); // Apply (patch) new values to populated DTO.

            // Validate model using ControllerBase.
            if (!TryValidateModel(petToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(petToPatch, petFromRepo); // Map new values from patched DTO to populated entity.
            await _familyDemoAPIv2Repository.UpdatePet(petFromRepo); // Call repo and update context with with populated entity.

            // Return link in header.
            return CreatedAtRoute("UpdatePetAsync",
                new { petFromRepo.PetId },
                petFromRepo);
        }

        /// <summary>
        /// Use this method to delete a pet.
        /// </summary>
        /// <param name="petId"></param>
        /// <returns>Whether the pet was deleted or not.</returns>
        /// <remarks>HttpDelete verb.</remarks>
        [HttpDelete("{petId}", Name = "DeletePet")]
        [Route("DeletePet")]
        public ActionResult DeletePet(Guid petId)
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:PetsController,API:AddDeletePetPet,DateTime:" + DateTime.Now.ToString());

            try
            {
                // Ensure pet exists.
                if (!_familyDemoAPIv2Repository.PetExists(petId).Result)
                {
                    return NotFound();
                }

                var petToDelete = _familyDemoAPIv2Repository.GetPet(petId).Result; // Obtain record via DbContext query and store in entity.
                _familyDemoAPIv2Repository.DeletePet(petToDelete); // Call to repository delete method.
            }
            catch (Exception ex)
            {
                _log.WriteError(ex.Message, ex.InnerException);
                return NoContent();
            }

            return Ok();
        }
    }
}
