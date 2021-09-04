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
    public class BookImagesController : ControllerBase
    {
        IBookImageService _bookImageService;

        public BookImagesController(IBookImageService bookImageService)
        {
            _bookImageService = bookImageService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _bookImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var result = _bookImageService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("bookid")]
        public IActionResult GetImagesByBookId([FromForm(Name = ("BookId"))] int bookId)
        {
            var result = _bookImageService.GetImagesByBookId(bookId);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("Image"))] IFormFile file, [FromForm] BookImage bookImage)
        {
            var result = _bookImageService.Add(file, bookImage);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] int id)
        {
            var bookImage = _bookImageService.GetById(id).Data;
            var result = _bookImageService.Delete(bookImage);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm(Name = ("Id"))] int Id)
        {
            var bookImage = _bookImageService.GetById(Id).Data;
            var result = _bookImageService.Update(file, bookImage);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
