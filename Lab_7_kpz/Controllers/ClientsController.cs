using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace Lab_7_kpz
{
    
    
        [Route("api/[controller]")]
        public class ClientsController : Controller
        {
            Clients_repo clients_repo;

            public ClientsController(Clients_repo clients_repo)
            {
                this.clients_repo = clients_repo;
            }

            [HttpGet(Name = "GetAllClients")]
            [HttpGet("get.{format}"), FormatFilter]
            public IEnumerable<Clients> Get()
            {
                return clients_repo.Get();
            }

             [HttpGet(Name = "GetClientById")]
             [HttpGet("{id}/get.{format}"), FormatFilter]
            public IActionResult Get(int ID)
            {
                Clients client = clients_repo.Get(ID);

                if (client == null)
                {
                    return NotFound();
                }

                return new ObjectResult(client);
            }

            [HttpPost("post.{format}"), FormatFilter]
            public IActionResult Create([FromBody] Client_View_Model client_view_model)
            {
                if (client_view_model == null)
                {
                    return BadRequest();
                }

                Clients client =  Mapper_func(client_view_model);

                clients_repo.Create(client);
                return CreatedAtRoute("GetClientById", new { id = client.Id }, client);
            }


            [HttpPut("{id}/put.{format}"), FormatFilter]
            //[HttpPut("{id}")]
            public IActionResult Update(int ID, [FromBody] Client_View_Model client_view_model)
            {
                if (client_view_model == null)
                {
                    return BadRequest();
                }

                var up_client = clients_repo.Get(ID);
                if (up_client == null)
                {
                    return NotFound();
                }



                Clients client = Mapper_func(client_view_model);

                clients_repo.Update(client, ID);
                client.Id = ID;
                return new ObjectResult(client);
           
            }

            [HttpDelete("{id}/delete.{format}"), FormatFilter]
            //[HttpDelete("{id}")]
            public IActionResult Delete(int ID)
            {
                var del_client = clients_repo.Delete(ID);

                if (del_client == null)
                {
                    return BadRequest();
                }

                return new ObjectResult(del_client);
            }


             Clients Mapper_func(Client_View_Model client_view_model)
             {

                var config = new MapperConfiguration(cfg => cfg.CreateMap<Client_View_Model, Clients>()
                .ForMember("Email", opt => opt.MapFrom(src => src.Login)));
                var mapper = new Mapper(config);
                return mapper.Map<Client_View_Model, Clients>(client_view_model);
             }
                
        }
    
}
