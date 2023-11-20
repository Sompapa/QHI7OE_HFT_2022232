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
    public class MangaController : ControllerBase
    {

        IMangaLogic logic;
        IHubContext<SignalRHub> hub;

        public MangaController(IMangaLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Manga> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Manga Read(int id)
        {
            return this.logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Manga value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("MangaCreated", value);
        }

        [HttpPut]
        public void Update([FromBody] Manga value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("MangaUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var mangaToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("MangaDeleted", mangaToDelete);
        }
    }
}
