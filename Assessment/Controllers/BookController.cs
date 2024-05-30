using Assessment.Logging.Interface;
using Assessment.ViewModel;
using AutoMapper;
using BusinessAccessLayer.Interface;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

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
            catch (Exception ex)
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

        [HttpGet("Test")]
        public string Test()
        {
            var factory = new ConnectionFactory
            {
                //Uri = new Uri("amqp://guest:guest@localhost:5672"),
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                Port = 5672,
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            //QueueProducer.Publish(channel);

            Console.WriteLine("Message send");

            //using var connection1 = factory.CreateConnection();
            //using var channel1 = connection.CreateModel();
            //QueueConsumer.Consume(channel1);

            channel.QueueDeclare("demo-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) => {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                };

                channel.BasicConsume("demo-queue", true, consumer);
            //Console.WriteLine("Consumer started");
            //Console.ReadLine();
            return "connected";

        }
    }
}
