using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QHI7OE_HFT_2022232.Endpoint.Services;
using QHI7OE_HFT_2022232.Logic;
using QHI7OE_HFT_2022232.Models;
using System.Collections.Generic;

namespace QHI7OE_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        IGenreLogic logic;
        IHubContext<SignalRHub> hub;

        public GenreController(IGenreLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Genre> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Genre Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Genre value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("GenreCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Genre value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("GenreUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var genreToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GenreDeleted", genreToDelete);
        }
    }
}
