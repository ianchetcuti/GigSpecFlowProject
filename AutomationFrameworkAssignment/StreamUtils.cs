using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GigSpecFlowProject
{
    public class StreamUtils
    {
        /// <summary>
        /// Async method which publishes to kafka.
        /// </summary>
        /// <param name="kafkaURI">URI where Kafka is hosted</param>
        /// <param name="topic">Topic name</param>
        /// <param name="message">String message to publish</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<String> publishKafkaMessageAsync(String kafkaURI, String topic, string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaURI
            };

            // Create a producer that can be used to send messages to kafka
            using var p = new ProducerBuilder<Null, string>(config).Build();


            // Construct the message with same type (string)
            var msg = new Message<Null, string>
            {
                Value = message
            };

            // Send the message to topic in Kafka                
            var dr = await p.ProduceAsync(topic, msg);

            return $"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}";
        }

        /// <summary>
        /// Returns a list of strings made up of Kafka messages from a particular topic. Cancels the read loop after timeoutInMillis.
        /// </summary>
        /// <param name="kafkaURI">where Kafka is hosted</param>
        /// <param name="topic">Topic name</param>
        /// <param name="timeoutInMillis">Loop time in millis (5000 minimum recommended)</param>
        /// <returns></returns>
        public List<string> consumeKafkaMessages(String kafkaURI, String topic, int timeoutInMillis)
        {
            //Initialize a list of strings that will hold the streamed messages
            List<string> streamList = new List<string>();

            //Basic consumer configuration
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = kafkaURI,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            //Initiate the consumer and subscribe to a topic based on the group id
            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe(topic);

            //Setup a cancellation token to break the while loop after a certain period of time (millis)
            var cts = new CancellationTokenSource();
            cts.CancelAfter(timeoutInMillis);

            try
            {
                while (true)
                {
                    // Consume a message from the topic. Pass in a cancellation token to break out of the loop after a certain amount of millis
                    var cr = c.Consume(cts.Token);
                    // Commit messages so that they aren't consumed again
                    c.Commit();
                    //Add message to list
                    streamList.Add($"Consumed message '{cr.Value}' from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Loop broken; returning streamlist");
            }
            finally
            {
                c.Close();
            }
            return streamList;
        }

    }
}
