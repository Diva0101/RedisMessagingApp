using StackExchange.Redis;
using System;

namespace RedisMessagingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Send a message");
                Console.WriteLine("2. Receive a message");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SendMessage();
                        break;
                    case "2":
                        ReceiveMessage();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }

        }
        static void SendMessage()
        {
            // Prompt the user to enter a message
            Console.Write("Enter a message: ");
            string message = Console.ReadLine();

            // Connect to Redis server
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");

            // Get the Redis database instance
            IDatabase db = redis.GetDatabase();

            // Create a new Redis key and set its value to empty string
            db.KeyDelete("queue2");

            // Push the message to a Redis list
            db.ListRightPush("queue2", message);

            // Disconnect from Redis server
            redis.Dispose();

            Console.WriteLine($"Sent message: {message}");
        }
        static void ReceiveMessage()
        {
            // Connect to Redis server
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");

            // Get the Redis database instance
            IDatabase db = redis.GetDatabase();

            // Pop a message from the Redis list

            RedisValue message = db.ListLeftPop("queue2");

            // Disconnect from Redis server
            redis.Dispose();

            if (message.HasValue)
            {
                Console.WriteLine($"Received message: {message}");
            }
            else
            {
                Console.WriteLine("No messages found.");
            }
        }
    }
}



//using StackExchange.Redis;
//using System;
//using System.Threading.Tasks;

//namespace RedisMessagingApp
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Choose an option:");
//            Console.WriteLine("1. Send a message");
//            Console.WriteLine("2. Receive messages");

//            string choice = Console.ReadLine();

//            switch (choice)
//            {
//                case "1":
//                    SendMessage();
//                    break;
//                case "2":
//                    Console.Write("Enter the number of receivers: ");
//                    int numReceivers = int.Parse(Console.ReadLine());

//                    for (int i = 0; i < numReceivers; i++)
//                    {
//                        Task.Run(() => ReceiveMessage($"queue{i + 1}"));
//                    }

//                    Console.ReadLine();
//                    break;
//                default:
//                    Console.WriteLine("Invalid choice.");
//                    break;


//            }
//            Console.ReadLine();

//        }

//        static void SendMessage()
//        {
//            // Prompt the user to enter a message
//            Console.Write("Enter a message: ");
//            string message = Console.ReadLine();

//            // Connect to Redis server
//            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");

//            // Get the Redis database instance
//            IDatabase db = redis.GetDatabase();

//            // Push the message to a Redis list
//            db.ListRightPush("queue1", message);

//            // Disconnect from Redis server
//            redis.Dispose();

//            Console.WriteLine($"Sent message: {message}");
//        }

//        static void ReceiveMessage(string queueName)
//        {
//            // Connect to Redis server
//            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");

//            // Get the Redis database instance
//            IDatabase db = redis.GetDatabase();

//            while (true)
//            {
//                // Pop a message from the Redis list
//                RedisValue message = db.ListLeftPop(queueName);

//                if (message.HasValue)
//                {
//                    Console.WriteLine($"Received message from {queueName}: {message}");
//                }
//            }
//        }
//    }
//}
