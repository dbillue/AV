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
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFamilyDemoAPIv2Repository _familyDemoAPIv2Repository;
        private readonly ILogger _logger;
        private readonly Logger _log;

        // ctor.
        public PersonsController(IFamilyDemoAPIv2Repository familyDemoAPIv2Repository, IMapper mapper, ILogger<PersonsController> logger)
        {
            _familyDemoAPIv2Repository = familyDemoAPIv2Repository;
            _mapper = mapper;
            _logger = logger;
            _log = new Logger(_logger);
        }

        /// <summary>
        /// Use this method to add a new person.
        /// </summary>
        /// <param name="person">The persons information.</param>
        /// <returns>The newly created person via 200 response.</returns>
        /// <remarks>HttpPost verb.</remarks>
        [HttpPost(Name = "AddPersonAsync")]
        public async Task<ActionResult<AddPersonDTO>> AddPersonAsync(AddPersonDTO person)
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:Persons,API:AddPerson,DateTime:" + DateTime.Now.ToString());

            try
            { 
                var personEntity = _mapper.Map<Entities.Person>(person); // Map to entity.
                await _familyDemoAPIv2Repository.AddPerson(personEntity); // Add.

                var personToReturn = _mapper.Map<AddPersonDTO>(personEntity); // Map to DTO.

                // Log addition of new person.
                _log.WriteInformation("New person added", personToReturn, null);

                // Return person added.
                //return Ok(personToReturn);

                // Return link in header.
                return CreatedAtRoute("GetPerson",
                    new { personToReturn.PersonId },
                    personToReturn);
            }
            catch (Exception ex) {
                _log.WriteError(ex.Message, ex.InnerException);
                return NoContent();
            }
        }

        /// <summary>
        /// Use this method to update a person.
        /// </summary>
        /// <param name="personId">The person's Guid Id</param>
        /// <param name="patchDocument">The persons information in JSON patch format.</param>
        /// <returns>The updated person's information via 200 response.</returns>
        /// <remarks>HttpPatch verb. \
        /// person/personId \
        /// [ \
        ///     { \
        ///         "op": "replace", \
        ///         "path": "/FirstName", \
        ///         "value": "Person's new first name." \
        ///     } \
        /// ]
        /// </remarks>
        [HttpPatch("{personId}", Name = "UpdatePersonAsync")]
        public async Task<ActionResult> UpdatePersonAsync(Guid personId, JsonPatchDocument<UpdatePersonDTO> patchDocument)
        {
            // Ensure person exists.
            if (!_familyDemoAPIv2Repository.PersonExists(personId).Result)
            {
                return NotFound();
            }

            var personFromRepo = _familyDemoAPIv2Repository.GetPerson(personId).Result; // Obtain record via DbContext query and store in entity.
            var personToPatch = _mapper.Map<UpdatePersonDTO>(personFromRepo); // Map populated entity to DTO.
            patchDocument.ApplyTo(personToPatch, ModelState); // Apply (patch) new values to populated DTO.

            // Validate model using ControllerBase.
            if (!TryValidateModel(personToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(personToPatch, personFromRepo); // Map new values from patched DTO to populated entity.
            await _familyDemoAPIv2Repository.UpdatePerson(personFromRepo); // Call repo and update context with with populated entity.

            // Return updated person.
            // return Ok(personFromRepo);

            // Return link in header.
            return CreatedAtRoute("GetPerson",
                new { personFromRepo.PersonId },
                personFromRepo);
        }

        /// <summary>
        /// Use this method to query for a person.
        /// </summary>
        /// <param name="personId">The person's Id.</param>
        /// <returns>The person queried for.</returns>
        /// <remarks>HttpGet verb.</remarks>
        [HttpGet("{personId}", Name = "GetPerson")]
        public async Task<ActionResult> GetPerson(Guid personId)
        {
            // Ensure person exists.
            if (!_familyDemoAPIv2Repository.PersonExists(personId).Result)
            {
                return NotFound();
            }

            var personFromRepo = await _familyDemoAPIv2Repository.GetPerson(personId); // Obtain record via DbContext query and store in entity.
            var personToReturn = _mapper.Map<GetPersonDTO>(personFromRepo); // Map entity to return DTO.

            // Return person.
            // return Ok(personToReturn);

            // Return link in header.
            return CreatedAtRoute("GetPerson",
                new { personToReturn.PersonId },
                personToReturn);
        }

        /// <summary>
        /// Use this method to return all persons.
        /// </summary>
        /// <returns>A collection of persons.</returns>
        /// <remarks>HttpGet verb.</remarks>
        [HttpGet(Name = "GetPersonsAsync")]
        public async Task<OkObjectResult> GetPersonsAsync([FromQuery] PersonResourceParameters authorsResourceParameters) // Pass in paged parameters via URI.
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:Persons,API:GetPersons,DateTime:" + DateTime.Now.ToString());

            // Obtain list of persons (PagedList<T>) using paged parameter values.
            var personsFromRepo = await _familyDemoAPIv2Repository.GetPersons(authorsResourceParameters);

            // Map list of persons to DTO.
            var persons = _mapper.Map<IEnumerable<GetPersonDTO>>(personsFromRepo);

            // Assign pagination values for returning via header. 
            #region // Paging
            var paginationMetadata = new
            {
                totalCount = personsFromRepo.TotalCount,
                pageSize = personsFromRepo.PageSize,
                currentPage = personsFromRepo.CurrentPage,
                totalPages = personsFromRepo.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            // HATEOAS method call.
            var links = CreateLinksForAuthor(authorsResourceParameters, personsFromRepo.HasNext, personsFromRepo.HasPrevious);
            #endregion

            // Add persons and links collections to return var.
            var linkedCollectionResource = new
            {
                value = persons,
                links
            };

            // Return persons and link collection.
            return Ok(linkedCollectionResource);
        }

        /// <summary>
        /// Use this method to delete a person.
        /// </summary>
        /// <param name="personId">The person's Id.</param>
        /// <returns>An ok status if deleted, else a not found status.</returns>
        /// <remarks>HttpDelete verb.</remarks>
        [HttpDelete("{personId}", Name = "DeletePersonAsync")]
        public async Task<ActionResult> DeletePersonAsync(Guid personId)
        {
            // Log Api call.  Could be moved to database for future anayltics.
            _log.WriteInformation("Controller:Persons,API:DeletePerson,DateTime:" + DateTime.Now.ToString());

            // Ensure person exists.
            if (!_familyDemoAPIv2Repository.PersonExists(personId).Result)
            {
                return NotFound();
            }

            var personToDelete = _familyDemoAPIv2Repository.GetPerson(personId).Result; // Obtain record via DbContext query and store in entity.
            await _familyDemoAPIv2Repository.DeletePerson(personToDelete); // Call to repository delete method.

            // Return ok status.
            return Ok();
        }

        /// <summary>
        /// Adds links for pagination.
        /// </summary>
        /// <param name="personResourceParameters"></param>
        /// <param name="hasNext"></param>
        /// <param name="hasPrevious"></param>
        /// <returns>List object containing pagination links.</returns>
        private object CreateLinksForAuthor(PersonResourceParameters personResourceParameters, bool hasNext, bool hasPrevious)
        {
            {
                var links = new List<LinkDTO>();

                links.Add(new LinkDTO(CreateAuthorResourceUri(personResourceParameters, ResourceUriType.Current),
                    "self", "GET"));

                if (hasNext)
                {
                    links.Add(new LinkDTO(CreateAuthorResourceUri(personResourceParameters, ResourceUriType.NextPage),
                        "nextpage", "GET"));
                }

                if (hasPrevious)
                {
                    links.Add(new LinkDTO(CreateAuthorResourceUri(personResourceParameters, ResourceUriType.PreviousPage),
                        "previouspage", "GET"));
                }

                return links;
            }
        }

        /// <summary>
        /// Constructs links for pagination.
        /// </summary>
        /// <param name="personResourceParameters"></param>
        /// <param name="type"></param>
        /// <returns>Link string.</returns>
        private string CreateAuthorResourceUri(PersonResourceParameters personResourceParameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetPersons",
                        new
                        {
                            pageNumber = personResourceParameters.PageNumber - 1,
                            pageSize = personResourceParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetPersons",
                        new
                        {
                            pageNumber = personResourceParameters.PageNumber + 1,
                            pageSize = personResourceParameters.PageSize
                        });
                case ResourceUriType.Current:
                default:
                    return Url.Link("GetPersons",
                        new
                        {
                            pageNumber = personResourceParameters.PageNumber,
                            pageSize = personResourceParameters.PageSize
                        });
            }
        }
    }
}