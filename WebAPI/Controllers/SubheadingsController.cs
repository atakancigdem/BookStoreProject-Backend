using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubheadingsController : ControllerBase
    {
        private ISubheadingService _subheadingService;

        public SubheadingsController(ISubheadingService subheadingService)
        {
            _subheadingService = subheadingService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _subheadingService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("subheadingid")]
        public IActionResult GetBySubheadingId(int subheadingId)
        {
            var result = _subheadingService.GetBySubheadingId(subheadingId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("subheadingname")]
        public IActionResult GetBySubheadingName(string subheadingName)
        {
            var result = _subheadingService.GetBySubheadingName(subheadingName);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("categoryid")]
        public IActionResult GetListCategoryId(int categoryId)
        {
            var result = _subheadingService.GetListByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("detail")]
        public IActionResult GetSubheadingDetails()
        {
            var result = _subheadingService.GetSubheadingDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Subheading subheading)
        {
            var result = _subheadingService.Add(subheading);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Subheading subheading)
        {
            var result = _subheadingService.Update(subheading);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Subheading subheading)
        {
            var result = _subheadingService.Delete(subheading);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
