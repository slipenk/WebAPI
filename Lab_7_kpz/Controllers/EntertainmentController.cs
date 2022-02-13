using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace Lab_7_kpz.Controllers
{
    [Route("api/[controller]")]
    public class EntertainmentController : Controller
    {
        Entertainment_repo entertainment_repo;

        public EntertainmentController(Entertainment_repo entertainment_repo)
        {
            this.entertainment_repo = entertainment_repo;
        }

        [HttpGet(Name = "GetAllEntertainments")]
        [HttpGet("get.{format}"), FormatFilter]
        public IEnumerable<Entertainment> Get()
        {
            return entertainment_repo.Get();
        }

        [HttpGet(Name = "GetEntertainmentById")]
        [HttpGet("{id}/get.{format}"), FormatFilter]
        public IActionResult Get(int ID)
        {
            Entertainment entertainment = entertainment_repo.Get(ID);

            if (entertainment == null)
            {
                return NotFound();
            }

            return new ObjectResult(entertainment);
        }

        [HttpPost("post.{format}"), FormatFilter]
        public IActionResult Create([FromBody] Entertainment_View_Model entertainment_View_Model)
        {
            if (entertainment_View_Model == null)
            {
                return BadRequest();
            }

            Entertainment entertainment = Mapper_func(entertainment_View_Model);

            entertainment_repo.Create(entertainment);
            return CreatedAtRoute("GetEntertainmentById", new { id = entertainment.Id }, entertainment);
        }


        [HttpPut("{id}/put.{format}"), FormatFilter]
        //[HttpPut("{id}")]
        public IActionResult Update(int ID, [FromBody] Entertainment_View_Model entertainment_View_Model)
        {
            if (entertainment_View_Model == null)
            {
                return BadRequest();
            }

            var up_entertainment = entertainment_repo.Get(ID);
            if (up_entertainment == null)
            {
                return NotFound();
            }



            Entertainment entertainment = Mapper_func(entertainment_View_Model);

            entertainment_repo.Update(entertainment, ID);
            entertainment.Id = ID;
            return new ObjectResult(entertainment);

        }

        [HttpDelete("{id}/delete.{format}"), FormatFilter]
        //[HttpDelete("{id}")]
        public IActionResult Delete(int ID)
        {
            var del_entertainment = entertainment_repo.Delete(ID);

            if (del_entertainment == null)
            {
                return BadRequest();
            }

            return new ObjectResult(del_entertainment);
        }


        Entertainment Mapper_func(Entertainment_View_Model entertainment_View_Model)
        {

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entertainment_View_Model, Entertainment>()
            .ForMember("Description", opt => opt.MapFrom(c => c.Reviews + " " + c.Description)));
            var mapper = new Mapper(config);
            return mapper.Map<Entertainment_View_Model, Entertainment>(entertainment_View_Model);
        }

    }
}
