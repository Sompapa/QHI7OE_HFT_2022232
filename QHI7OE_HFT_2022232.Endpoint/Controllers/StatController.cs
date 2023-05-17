using Microsoft.AspNetCore.Mvc;
using QHI7OE_HFT_2022232.Logic;
using System;
using System.Collections.Generic;

namespace QHI7OE_HFT_2022232.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        private IMangaLogic logic;

        public StatController(IMangaLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AVGPriceByAuthor()
        {
            return this.logic.AVGPriceByAuthor();
        }
  
        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AVGPriceByGenre()
        {
            return this.logic.AVGPriceByGenre();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<DateTime, double>> AllPriceByYears()
        {
            return this.logic.AllPriceByYears();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AllPriceByGenre()
        {
            return this.logic.AllPriceByGenre();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AVGRateByGenre()
        {
            return this.logic.AVGRateByGenre();
        }
    }
}
