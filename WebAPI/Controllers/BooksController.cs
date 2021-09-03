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
    public class BooksController : ControllerBase
    {
        IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("bookname")]
        public IActionResult GetByBookName(string bookName)
        {
            var result = _bookService.GetByBookName(bookName);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("id")]
        public IActionResult GetById(int bookId)
        {
            var result = _bookService.GetById(bookId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("detail")]
        public IActionResult GetBookDetail()
        {
            var result = _bookService.GetBookDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("categoryid")]
        public IActionResult GetListByCategoryId(int categoryId)
        {
            var result = _bookService.GetListByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("subheadingid")]
        public IActionResult GetListBySubheadingId(int subheadingId)
        {
            var result = _bookService.GetListBySubheadingId(subheadingId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("sosid")]
        public IActionResult GetListBySubheadingOfSubheadingId(int sosId)
        {
            var result = _bookService.GetListBySubheadingOfSubheadingId(sosId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("authorid")]
        public IActionResult GetListByAuthorId(int authorId)
        {
            var result = _bookService.GetListByAuthorId(authorId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("publisherid")]
        public IActionResult GetListByPublisherId(int publisherId)
        {
            var result = _bookService.GetListByPublisherId(publisherId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("languageid")]
        public IActionResult GetListByLanguageId(int languageId)
        {
            var result = _bookService.GetListByLanguageId(languageId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("unitprice")]
        public IActionResult GetListByUnitPrice(decimal minPrice, decimal maxPrice)
        {
            var result = _bookService.GetListByUnitPrice(minPrice, maxPrice);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Book book)
        {
            var result = _bookService.Add(book);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Book book)
        {
            var result = _bookService.Update(book);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Book book)
        {
            var result = _bookService.Delete(book);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
