﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestFullAspNet.Business;
using RestFullAspNet.Model;

namespace RestFullAspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    
    public class PersonController : ControllerBase
    {
        
        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet]
       public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindByID(id);
            if (person == null) return NotFound();
            return Ok(person);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
           
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);            
            return NoContent();
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            bool IsNumeric = double.TryParse(
                strNumber, System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return IsNumeric;
        }
    }
}
