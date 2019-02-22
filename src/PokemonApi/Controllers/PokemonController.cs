using Microsoft.AspNetCore.Mvc;
using PokemonApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private static readonly IList<Pokemon> PokemonList = new List<Models.Pokemon>();

        public PokemonController()
        {

        }

        // POST api/pokemon
        [HttpPost]
        public ActionResult Post([FromBody] Pokemon pokemon)
        {
            PokemonList.Add(pokemon);

            using (var stream = new StreamWriter("pokemons.json"))
            {
                stream.Write(JsonConvert.SerializeObject(PokemonList));
            }

            return base.Created($"http://localhost:5000/api/pokemon/{pokemon.Name}", pokemon);
        }

        // POST api/pokemon/{name}
        [HttpGet("{name}")]
        public ActionResult GetFromRoute([FromRoute] string name)
        {
            var pokemon = PokemonList.FirstOrDefault(p => p.Name.ToLower().Equals(name.ToLower()));

            return base.Ok(pokemon);
        }



        [HttpGet]
        public ActionResult GetFromQuery([FromQuery] string name, Type? tipo)
        {

            
            if (name != null)
            {
               var pokemon = PokemonList.Where(p => p.Name.ToLower().Equals(name?.ToLower()));
                return base.Ok(pokemon);
            } else {
                var pokemon = PokemonList.Where(p => p.Type.Equals(tipo));
                return base.Ok(pokemon);
            }
            
        }

        [HttpGet("type/{type}")]
        public ActionResult Get([FromRoute]Type type, [FromQuery]int atk, [FromQuery]int def, [FromQuery]int hp)
        {

            var pokemonListResult = PokemonList.Where(p => p.Type == type);

            if (pokemonListResult != null)
            {
                if (atk != null)
                {
                    var resul = pokemonListResult.Where(p => p.Statistics.Attack == atk ||
                    p.Statistics.Deffense == def ||
                    p.Statistics.HealthPoint == hp);
                }
            }


            return Ok(pokemonListResult);
        }

        //[HttpGet]
        //public ActionResult GetByStatistic([FromQuery]int? atk, [FromQuery]int? def, [FromQuery]int? hp, [FromQuery]int? speed)
        //{

        //    var pokemon = PokemonList.Where(p => (p.Statistics.Attack == atk));
        //}

    }
}
