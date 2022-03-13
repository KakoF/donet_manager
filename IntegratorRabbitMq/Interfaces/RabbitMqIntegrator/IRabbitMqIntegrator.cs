using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratorRabbitMq.Interfaces.RabbitMqIntegrator
{
    public interface IRabbitMqIntegrator
    {
        public void CreateQueueDeclare(string queueName);
        public void ConfigureQueue(string queueName, bool durable = false, bool exclusive = false, bool autoDelete = false, IDictionary<string, object> arguments = null);
        public void PublishQueue<T>(T message, string queueName);
    }
}
