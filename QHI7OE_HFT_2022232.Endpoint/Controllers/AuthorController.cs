using Microsoft.AspNetCore.Mvc;
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

        public AuthorController(IAuthorLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Put([FromBody] Author value)
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
