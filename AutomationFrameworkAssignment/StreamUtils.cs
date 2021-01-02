using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GigSpecFlowProject
{
    public class StreamUtils
    {
        public async System.Threading.Tasks.Task<String> publishKafkaMessageAsync(String kafkaURI, String topic, string message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaURI
            };

            // Create a producer that can be used to send messages to kafka that have no key and a value of type string 
            using var p = new ProducerBuilder<Null, string>(config).Build();


            // Construct the message to send (generic type must match what was used above when creating the producer)
            var msg = new Message<Null, string>
            {
                Value = message
            };

            // Send the message to our test topic in Kafka                
            var dr = await p.ProduceAsync(topic, msg);
            //Console.WriteLine($"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}");

            return $"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}";
        }

        public List<string> consumeKafkaMessages(String kafkaURI, String topic, int timeoutInMillis)
        {
            List<string> streamList = new List<string>();

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = kafkaURI,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe(topic);

            var cts = new CancellationTokenSource();
            cts.CancelAfter(timeoutInMillis);

            try
            {
                while (true)
                {
                    // Consume a message from the test topic. Pass in a cancellation token so we can break out of our loop when Ctrl+C is pressed
                    var cr = c.Consume(cts.Token);
                    c.Commit();
                    streamList.Add($"Consumed message '{cr.Value}' from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
                    // Do something interesting with the message you consumed
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                c.Close();
            }
            return streamList;
        }

    }
}
