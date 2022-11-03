using Assessment.Logging.Interface;
using Assessment.ViewModel;
using AutoMapper;
using BusinessAccessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public BookController(IBookService bookService, IMapper mapper, ILoggerManager logger)
        {
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetBooks")]
        public ActionResult<List<BookViewModel>> GetBooks()
        {
            try
            {
                var books = _bookService.GetBooks();
                _logger.LogInfo("Get Books successfully");
                return Ok(_mapper.Map<List<BookViewModel>>(books));
            }
            catch (Exception)
            {
                _logger.LogError("There is some while retriving your data");
                return StatusCode(StatusCodes.Status500InternalServerError,
               "There is some while retriving your data");
            }
            
        }

        [HttpGet("{id}")]
        public ActionResult<BookViewModel> GetBookById(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("Book Id can't be empty");
                }
                var book = _bookService.GetBookById(id);

                if (book == null)
                {
                    return StatusCode(StatusCodes.Status200OK,
                   "Data not found");
                }
                _logger.LogInfo("Get Book successfully by id");
                return Ok(_mapper.Map<BookViewModel>(book));
            }
            catch (Exception)
            {
                _logger.LogError("There is some while retriving your data by id");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "There is some while retriving your data by id");
            }

        }

        [HttpPost("SaveBook")]
        public ActionResult SaveBook([FromBody] BookViewModel bookViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var book = _mapper.Map<Book>(bookViewModel);
                _bookService.SaveBook(book);
                _logger.LogInfo("Sucessfully saved the data");
                return Ok();
            }
            catch (Exception)
            {
                _logger.LogError("There is some error while saving your data");
                return StatusCode(StatusCodes.Status500InternalServerError,
                "There is some error while saving your data");
            }

        }
    }
}
