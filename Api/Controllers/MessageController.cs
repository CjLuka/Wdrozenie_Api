using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public List<Message> messagesList = new List<Message>();
        [HttpPost]
        public async Task<IActionResult> AddMessage(int id, string context, int userId)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.ExchangeDeclare(exchange: "Test",
                //    ExchangeType.Fanout,
                //    true, false, null);
                channel.QueueDeclare(queue: "RabbitMq.01",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                User user = new User(1, "TestUser");
                Message message = new Message(id, context, userId);
                messagesList.Add(message);

                var body = Encoding.UTF8.GetBytes(message.Context + message.UserId);

                channel.BasicPublish("",
                    "RabbitMq.01",
                    null, body);

            }
            return Ok(messagesList);
        }

        //[HttpGet]
        //public async Task<string> GetMessage()
        //{
        //    string message;
        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    using (var connection = factory.CreateConnection())
        //    using (var channel = connection.CreateModel())
        //    {
        //        //channel.ExchangeDeclare(exchange: "test",
        //        //        ExchangeType.Fanout,
        //        //        true, false, null);
        //        channel.QueueDeclare(queue: "RabbitMq.01",
        //            durable: false,
        //            exclusive: false,
        //            autoDelete: false,
        //            arguments: null);

        //        var consumer = new EventingBasicConsumer(channel);

        //        consumer.Received += (model, ea) =>
        //        {
        //            message = Encoding.UTF8.GetString(ea.Body.ToArray());
        //            //InvokeAsync(StateHasChanged);
        //        };

        //        channel.BasicConsume(queue: "RabbitMq.01", autoAck: true, consumer: consumer);
        //    }

        //    return string.Empty;
        //    //[HttpGet]
        //    //public async Task<ActionResult<List<Message>>> AllMessages()
        //    //{
        //    //    return Ok(messagesList);
        //    //} 
        //}
    }
}
