using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ClientBlazorApp
{
	public class ClientRabbit
	{
		public string message;
		private readonly string hostname = "localhost";
		//public void Client()
		//{
		//	//var factory = new ConnectionFactory() { HostName = "localhost" };
		//	var factory = new ConnectionFactory()
		//	{
		//		HostName = "localhost",
		//		MaxMessageSize = 1024,
		//		//Port = 5672,
		//		//VirtualHost = "localhost"
		//	};
		//	using (var connection = factory.CreateConnection())
		//	using (var channel = connection.CreateModel())
		//	{
		//		channel.QueueDeclare(queue: "RabbitMq.01",
		//			durable: false,
		//			exclusive: false,
		//			autoDelete: false,
		//			arguments: null);

		//		var consumer = new EventingBasicConsumer(channel);

		//		consumer.Received += (model, ea) =>
		//		{
		//			message = Encoding.UTF8.GetString(ea.Body.ToArray());
		//		};

		//		channel.BasicConsume(queue: "RabbitMq.01", autoAck: true, consumer: consumer);
		//	}
		//}
		public async Task Client()
		{

			var factory = new ConnectionFactory
			{
				HostName = hostname,
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

				await Task.Run(() => channel.BasicConsume(queue: "RabbitMq.01", autoAck: true, consumer: consumer));
			}
		}
	}
}
