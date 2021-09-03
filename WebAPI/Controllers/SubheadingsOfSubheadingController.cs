using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubheadingsOfSubheadingController : ControllerBase
    {
        private ISubheadingOfSubheadingService _subheadingOfSubheadingService;

        public SubheadingsOfSubheadingController(ISubheadingOfSubheadingService subheadingOfSubheadingService)
        {
            _subheadingOfSubheadingService = subheadingOfSubheadingService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _subheadingOfSubheadingService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("detail")]
        public IActionResult GetSubheadingOfSubheadingDetails()
        {
            var result = _subheadingOfSubheadingService.GetSubheadingOfSubheadingDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("categoryid")]
        public IActionResult GetListByCategoryId(int categoryId)
        {
            var result = _subheadingOfSubheadingService.GetListByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("subheadingid")]
        public IActionResult GetListBySubheadingId(int subheadingId)
        {
            var result = _subheadingOfSubheadingService.GetListBySubheadingId(subheadingId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("id")]
        public IActionResult GetById(int sosId)
        {
            var result = _subheadingOfSubheadingService.GetById(sosId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("name")]
        public IActionResult GetByName(string sosName
        )
        {
            var result = _subheadingOfSubheadingService.GetByName(sosName);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(SubheadingOfSubheading subheadingOfSubheading)
        {
            var result = _subheadingOfSubheadingService.Add(subheadingOfSubheading);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(SubheadingOfSubheading subheadingOfSubheading)
        {
            var result = _subheadingOfSubheadingService.Update(subheadingOfSubheading);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(SubheadingOfSubheading subheadingOfSubheading)
        {
            var result = _subheadingOfSubheadingService.Delete(subheadingOfSubheading);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
