using AutoMapper;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.Service;
using FamilyDemoAPIv2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace FamilyDemoAPIv2.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/pets")]
    [ApiController]
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
    }
}
