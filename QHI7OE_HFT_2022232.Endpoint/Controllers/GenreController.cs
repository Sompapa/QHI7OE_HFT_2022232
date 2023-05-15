using Microsoft.AspNetCore.Mvc;
using QHI7OE_HFT_2022232.Logic;
using QHI7OE_HFT_2022232.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QHI7OE_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        IGenreLogic logic;

        public GenreController(IGenreLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Genre value)
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
