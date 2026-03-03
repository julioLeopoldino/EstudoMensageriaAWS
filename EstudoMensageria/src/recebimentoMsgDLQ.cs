using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

class Program
{
    private const string QUEUE_URL = "https://sqs.us-east-1.amazonaws.com/364350264218/msg-tst-dlq";

    static async Task Main(string[] args)
    {
        var region = RegionEndpoint.USEast1;

        using (var sqsClient = new AmazonSQSClient(region))
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = QUEUE_URL,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 20
            };

            var response = await sqsClient.ReceiveMessageAsync(request);

            if (response.Messages != null && response.Messages.Count > 0)
            {
                foreach (var message in response.Messages)
                {
                    Console.WriteLine("processando mensagem da DLQ...");
                    Console.WriteLine($"conteúdo da mensagem: {message.Body}");

                    await sqsClient.DeleteMessageAsync(new DeleteMessageRequest
                    {
                        QueueUrl = QUEUE_URL,
                        ReceiptHandle = message.ReceiptHandle
                    });

                    Console.WriteLine("mensagem da DLQ processada (e excluída) com sucesso");
                }
            }
        }
    }
}