using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ClientBlazorApp
{
	public class ClientRabbit
	{
		public string message;
		public async Task Client()
		{
			//var factory = new ConnectionFactory() { HostName = "localhost" };
			var factory = new ConnectionFactory()
			{
				HostName = "localhost"
			};
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "RabbitMq.01",
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				var consumer = new EventingBasicConsumer(channel);

				consumer.Received += (model, ea) =>
				{
					message = Encoding.UTF8.GetString(ea.Body.ToArray());
				};

				channel.BasicConsume(queue: "RabbitMq.01", autoAck: true, consumer: consumer);
			}
		}
	}
}
