using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using QHI7OE_HFT_2022232.Endpoint.Services;
using QHI7OE_HFT_2022232.Logic;
using QHI7OE_HFT_2022232.Models;
using System.Collections.Generic;

namespace QHI7OE_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        IAuthorLogic logic;
        IHubContext<SignalRHub> hub;

        public AuthorController(IAuthorLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Author> Get()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Author Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Author value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("AuthorCreated", value);
        }

        [HttpPut]
        public void Put([FromBody] Author value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("AuthorUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
                var authorToDelet = this.logic.Read(id);
                this.logic.Delete(id);
                this.hub.Clients.All.SendAsync("AuthorDeleted", authorToDelet);
        }
    }
}
