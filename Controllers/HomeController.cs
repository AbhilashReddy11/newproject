using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using newproject.Models;
using newproject.Models.Dto;
using newproject.Data;
using Microsoft.AspNetCore.JsonPatch;

 

namespace newproject.Controllers;

[Route("api/Home")]
[ApiController]

public class HomeController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDTO> GetVillas()
    {
        return VillaStore.villaList;
    }
    [HttpGet("{id:int}")]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }
    //public VillaDTO GetVilla(int id) 
    //{
    //   return VillaStore.villaList.FirstOrDefault(u => u.Id == id);
    //}
    [HttpPost]

    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
    {
        //if(villaDTO == null)
        //{
        //return BadRequest(villaDTO);
        //}
        //if(villaDTO.Id > 0)
        //{
        //return StatusCode(StatusCodes.Status500InternalServerError);

        //}
        villaDTO.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
        VillaStore.villaList.Add(villaDTO);
        return Ok(villaDTO);
    }
    [HttpDelete("{id:int}")]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        VillaStore.villaList.Remove(villa);
        return NoContent();
      }
      [HttpPut("{id:int}")]
    public IActionResult UpdateVillas(int id,[FromBody]VillaDTO villaDTO){
         if(villaDTO == null || id !=villaDTO.Id)
          {
            return BadRequest(villaDTO); 

        }
        var villa =VillaStore.villaList.FirstOrDefault(u => u.Id==id);
        villa.Name=villaDTO.Name;
        villa.Place=villaDTO.Place;
        return NoContent();

    }
    [HttpPatch("{id:int}")]
    public IActionResult UpdatePatchVilla (int id,JsonPatchDocument<VillaDTO> patchDTO){
         if(patchDTO == null || id ==0)
          {
            return BadRequest(); 

        }
        var villa =VillaStore.villaList.FirstOrDefault(u => u.Id==id);
        if(villa == null)
        {
          return BadRequest();

        }
        patchDTO.ApplyTo(villa,ModelState);
        if(!ModelState.IsValid)
        {
          return BadRequest();
        }
        return NoContent();
    }
    
}











