using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestFullAspNet.Business;
using RestFullAspNet.Model;

namespace RestFullAspNet.Controllers
{
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private IBooksBusiness _booksBusiness;
        private BooksController(ILogger<BooksController> logger, IBooksBusiness booksBusiness)
        {
            _logger = logger;
            _booksBusiness = booksBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_booksBusiness.FindAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get (long id)
        {
            var book = _booksBusiness.FindByID(id);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Books books)
        {
            if (books == null) return BadRequest();
            return Ok(_booksBusiness.Create(books));
        }
        [HttpPut]
        public IActionResult Put([FromBody] Books books)
        {
            if (books == null) return BadRequest();
            return Ok(_booksBusiness.Update(books));
        }
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            _booksBusiness.Delete(id);
            return NoContent();
        }

    }
}
