using System;
using System.Text;
using System.Threading.Tasks;
using EventBus;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Autofac;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQ: IEventBus
    {
        private readonly string broker_name = "eshop_events";
        private ILifetimeScope _autofac;
		private string _rabbitService;
        public EventBusRabbitMQ(ILifetimeScope autofac, string rabbitService)
        {
            _autofac = autofac;
			_rabbitService = rabbitService;
        }

        public void Publish(IntegrationEvent @event)
        {
            var factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
			factory.HostName = _rabbitService;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: broker_name, type: "direct");

                var eventName = @event.GetType().Name;
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                channel.BasicPublish(exchange: broker_name,
                                     routingKey: eventName,
                                     basicProperties: properties,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.HostName = "rabbit";

			var connection = factory.CreateConnection();
			var channel = connection.CreateModel();
            
                string routingKey = typeof(T).Name;
                channel.ExchangeDeclare(exchange: broker_name, type: "direct");

			//var queueName = channel.QueueDeclare().QueueName;
			var queueName = string.Format("{0}{1}", routingKey, "queue");
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(queue: queueName, exchange: broker_name, routingKey: routingKey);

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    //add code to handle message event
                    ProcessEvent(typeof(T), message, typeof(TH));

                    Console.WriteLine("on received event");
                    channel.BasicAck(ea.DeliveryTag, multiple: false);

                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: false,
                                     consumer: consumer);
                
                Console.WriteLine("123");
        }

        public void ProcessEvent(Type eventType, string message, Type handlerType)
        {
            using (var scope = _autofac.BeginLifetimeScope("event_bus"))
            {
                Console.WriteLine("process");
                var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                var handler = scope.ResolveOptional(handlerType);

                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
