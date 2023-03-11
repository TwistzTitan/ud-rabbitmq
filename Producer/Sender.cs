using RabbitMQ.Client;
using System.Text;

namespace Producer 
{ 

    public class Sender
    {   
        private static string host = "localhost";
        
        public static void Main(string [] args)
        {
            var factory = new ConnectionFactory(){ HostName = host };
            using(var conn = factory.CreateConnection())
            using(var channel = conn.CreateModel())
            {
                channel.QueueDeclare("BasicRabbitTest",false,false,false,null);

                string message = "Hello world with RabbitMQ .Net Core!";

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("","BasicRabbitTest",null,body);

                Console.WriteLine("Publish succeed.\n Message: {0}", message);
            }
            Console.WriteLine("Press [enter] to exit Sender");
            Console.ReadLine();
        }
    }

}