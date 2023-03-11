using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer {

    public class Receiver 
    {   
        private static string host = "localhost";
        public static void Main(string[] args){

            var factory = new ConnectionFactory(){HostName = host};
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel()){

                channel.QueueDeclare("BasicRabbitTest",false,false,false,null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) => {

                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Received: {0}", message);
                };

                channel.BasicConsume("BasicRabbitTest",true,consumer);

                Console.WriteLine("Press [enter] to exit the Consumer...");
                Console.ReadLine();
            }

        }
    }

}