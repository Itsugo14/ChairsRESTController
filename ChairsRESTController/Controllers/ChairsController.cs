using Microsoft.AspNetCore.Mvc;
using ChairsLib;

namespace ChairsLib.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChairsController : ControllerBase
    {
        private readonly ChairsRepository _repository;

        public ChairsController()
        {
            _repository = new ChairsRepository();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Chair>> GetAll()
        {
            var chairs = _repository.GetAll();
            return Ok(chairs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Chair> GetById(int id)
        {
            var chair = _repository.GetById(id);
            if (chair == null)
            {
                return NotFound();
            }
            return Ok(chair);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Chair> Add([FromBody] Chair chair)
        {
            if (chair == null)
            {
                return BadRequest();
            }
            var addedChair = _repository.Add(chair);
            return CreatedAtAction(nameof(GetById), new { id = addedChair.Id }, addedChair);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Chair> Delete(int id)
        {
            var deletedChair = _repository.Delete(id);
            if (deletedChair == null)
            {
                return NotFound();
            }
            return Ok(deletedChair);
        }
    }
}
