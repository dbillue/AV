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

namespace FamilyDemoAPIv2.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/birthstate")]
    [ApiController]
    public class BirthStateController : ControllerBase
    {
        private readonly IFamilyDemoAPIv2Repository _familyDemoAPIv2Repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly Logger _log;

        //ctor.
        public BirthStateController(IFamilyDemoAPIv2Repository familyDemoAPIv2Repository,
                                    IMapper mapper,
                                    ILogger<BirthStateController> logger)
        {
            _familyDemoAPIv2Repository = familyDemoAPIv2Repository;
            _mapper = mapper;
            _logger = logger;
            _log = new Logger(_logger);
        }

        /// <summary>
        /// Use this method to return all birth states.
        /// </summary>
        /// <returns>A collection of states.</returns>
        /// <remarks>HttpGet verb.</remarks>
        [HttpGet(Name = "GetStates")]
        public ActionResult<IEnumerable<GetBirthStateDTO>> GetStates()
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:BirthState,API:GetStates,DateTime:" + DateTime.Now.ToString());

            // Obtain list of states.
            var states = _familyDemoAPIv2Repository.GetBirthStates();

            // Map list of states to DTO.
            var birthstates = _mapper.Map<IEnumerable<GetBirthStateDTO>>(states);

            // Return states collection.
            return Ok(birthstates);
        }
    }
}
