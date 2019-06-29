using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {

        private IUsersService usersService;
        private IPackagesService packagesService;
        public PackagesController(IPackagesService packagesService, IUsersService usersService)
        {
            this.usersService = usersService;
            this.packagesService = packagesService;
        }

        /// <summary>
        /// Get all the packages by sender
        /// </summary>
        /// <param name="sender">date</param>
        /// <param name="page"></param>
        /// <returns>A list of package objects</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        // ? permite unui struct sa ia si valoare null
        public PaginatedList<Package> Get([FromQuery]string sender, [FromQuery]int page)
        {
            page = Math.Max(page, 1);
            return packagesService.GetAll(page,sender);
        }

        /// <summary>
        /// Gets a package by its ID
        /// </summary>
        /// <param name="id">The Id of the movie</param>
        /// <returns>The package associated with the id param</returns>
        // GET: api/Packages/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = packagesService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
        /// <summary>
        /// Add a package 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /packages
        ///  {
        ///  "OriginCountry ": "Test",
        ///   "Sender ": "Description of the movie",
        ///   "DestinationCountry ": "Romania",
        ///  "Receiver ": "Someone",
        ///  "Address ": "Address",
        ///  "Cost ": 10,
        ///  "TrackingCode ": "trackingCode",
        ///  }
        ///</remarks>
        /// <param name="package"></param>
        // POST: api/Packages
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Moderator")] 
        [HttpPost]
        public void Post([FromBody] Package package)
        {
            packagesService.Create(package);
        }

        /// <summary>
        /// Updates or creates a package with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        // PUT: api/packages/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Package package)
        {
            var result = packagesService.Upsert(id, package);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a package by it's id
        /// </summary>
        /// <param name="id">the id of the package to be deleted</param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = packagesService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

