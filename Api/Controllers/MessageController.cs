using Api.Models;
using Api.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MyHub> _hubContext;
        public MessageController(IHubContext<MyHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public List<Message> messagesList = new List<Message>();
        public List<Message> messagesList2 = new List<Message>();
        [HttpPost]
        public async Task<IActionResult> AddMessage1(string key, string userId, string email)
        {

            //User user = new User(1, "TestUser");
            Message message = new Message(key, userId, email);
            messagesList.Add(message);

            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost",
                MaxMessageSize = 1024,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                string exchangeName = "my_exchange";

                channel.ExchangeDeclare(exchange: exchangeName, type: "direct");


				var jsonBody = JsonConvert.SerializeObject(message);
				var body = Encoding.UTF8.GetBytes(jsonBody);
				channel.BasicPublish(exchange: exchangeName,
                    routingKey: key,
                    basicProperties: null,
                    body: body);

            }
            return Ok(messagesList);
        }

        [HttpPost]
        [Route("Test")]
        public async Task<IActionResult> Test(string key, string userId, string email)
        {

            User user = new User(1, "TestUser");
            Message message = new Message(key, userId, email);
            messagesList2.Add(message);
            //await _hubContext.Clients.All.SendAsync("Message", message);
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                MaxMessageSize = 1024,
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Test2",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);


                //var body = Encoding.UTF8.GetBytes(message.Context + message.UserId);
                var jsonBody = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonBody);
                channel.BasicPublish("",
                    "Test2",
                    null, body);

            }
            return Ok(messagesList);
        }

      
    }
}
