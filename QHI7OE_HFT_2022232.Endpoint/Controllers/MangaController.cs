using Microsoft.AspNetCore.Mvc;
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

        public MangaController(IMangaLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Manga value)
        {
            this.logic.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
