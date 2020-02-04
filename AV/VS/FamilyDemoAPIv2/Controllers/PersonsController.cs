using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using FamilyDemoAPIv2.Entities;
using FamilyDemoAPIv2.Helpers;
using FamilyDemoAPIv2.Models;
using FamilyDemoAPIv2.ResourceParameters;
using FamilyDemoAPIv2.Service;
using AutoMapper;

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

        // ctor.
        public PersonsController(IFamilyDemoAPIv2Repository familyDemoAPIv2Repository, IMapper mapper)
        {
            _familyDemoAPIv2Repository = familyDemoAPIv2Repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Use this method to add a new person.
        /// </summary>
        /// <param name="person">The persons information.</param>
        /// <returns>The newly created person via 200 response.</returns>
        /// <remarks>HttpPost verb.</remarks>
        [HttpPost(Name = "AddPerson")]
        public ActionResult<AddPersonDTO> AddPerson(AddPersonDTO person)
        {
            var personEntity = _mapper.Map<Entities.Person>(person); // Map to entity.
            _familyDemoAPIv2Repository.AddPerson(personEntity); // Add.
            _familyDemoAPIv2Repository.Save(); // Save.

            var personToReturn = _mapper.Map<AddPersonDTO>(personEntity); // Map to DTO.

            // Return person added.
            // return Ok(personToReturn);

            // Return link in header.
            return CreatedAtRoute("GetPerson",
                new { personToReturn.PersonId },
                personToReturn);
        }

        /// <summary>
        /// Use this method to update a person.
        /// </summary>
        /// <param name="personId">The person's Guid Id</param>
        /// <param name="patchDocument">The persons information in JSON patch format.</param>
        /// <returns>The updated person's information via 200 response.</returns>
        /// <remarks>HttpPatch verb. \
        /// persons/personId \
        /// [ \
        ///     { \
        ///         "op": "replace", \
        ///         "path": "/firstname", \
        ///         "value": "Person's new first name." \
        ///     } \
        /// ]
        /// </remarks>
        [HttpPatch("{personId}", Name = "UpdatePerson")]
        public ActionResult UpdatePerson(Guid personId, JsonPatchDocument<UpdatePersonDTO> patchDocument)
        {
            // Ensure person exists.
            if (!_familyDemoAPIv2Repository.PersonExists(personId))
            {
                return NotFound();
            }

            var personFromRepo = _familyDemoAPIv2Repository.GetPerson(personId); // Obtain record via DbContext query and store in entity.
            var personToPatch = _mapper.Map<UpdatePersonDTO>(personFromRepo); // Map populated entity to DTO.
            patchDocument.ApplyTo(personToPatch, ModelState); // Apply (patch) new values to populated DTO.

            // Validate model using ControllerBase.
            if (!TryValidateModel(personToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(personToPatch, personFromRepo); // Map new values from patched DTO to populated entity.
            _familyDemoAPIv2Repository.UpdatePerson(personFromRepo); // Call repo and update context with with populated entity.
            _familyDemoAPIv2Repository.Save();

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
        public ActionResult GetPerson(Guid personId)
        {
            // Ensure person exists.
            if (!_familyDemoAPIv2Repository.PersonExists(personId))
            {
                return NotFound();
            }

            var personFromRepo = _familyDemoAPIv2Repository.GetPerson(personId); // Obtain record via DbContext query and store in entity.
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
        [HttpGet(Name = "GetPersons")]
        public ActionResult<IEnumerable<GetPersonDTO>> GetPersons([FromQuery] PersonResourceParameters authorsResourceParameters) // Pass in paged parameters via URI.
        {
            // Obtain list of persons (PagedList<T>) using paged parameter values.
            var personsFromRepo = _familyDemoAPIv2Repository.GetPersons(authorsResourceParameters);

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
        [HttpDelete("{personId}", Name = "DeletePerson")]
        public ActionResult DeletePerson(Guid personId)
        {
            // Ensure person exists.
            if (!_familyDemoAPIv2Repository.PersonExists(personId))
            {
                return NotFound();
            }

            var personToDelete = _familyDemoAPIv2Repository.GetPerson(personId); // Obtain record via DbContext query and store in entity.
            _familyDemoAPIv2Repository.DeletePerson(personToDelete); // Call to repository delete method.
            _familyDemoAPIv2Repository.Save();

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