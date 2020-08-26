using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongo_Vue.Models;
using MongoDB.Driver;

namespace Mongo_Vue.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        public IMongoCollection<Teams> Movies { get; }
        public TeamsController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("TeamsDb");
            Movies = db.GetCollection<Teams>("Teams");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var TeamList = await Movies.FindAsync(FilterDefinition<Teams>.Empty);
            return Ok(TeamList.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Teams Model)
        {
            await Movies.InsertOneAsync(Model);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Teams Model)
        {
            await Movies.DeleteOneAsync(q => q.Id == Model.Id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Teams Model)
        {
            await Movies.ReplaceOneAsync(q => q.Id == Model.Id, Model);
            return Ok();
        }
    }
}
