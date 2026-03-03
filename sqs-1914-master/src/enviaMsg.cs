using System;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

class Program
{
    static async Task Main(string[] args)
    {
        var region = RegionEndpoint.USEast1;

        using (var sqsClient = new AmazonSQSClient(region))
        {
            var payload = new
            {
                conta_origem = new
                {
                    agencia = 1,
                    numero_conta = "123456-7"
                },
                conta_destino = new
                {
                    agencia = 1,
                    numero_conta = "765432-1"
                },
                valor = 1000,
                moeda = "BRL"
            };

            var messageBody = JsonSerializer.Serialize(payload);

            var request = new SendMessageRequest
            {
                QueueUrl = "https://sqs.us-east-1.amazonaws.com/364350264218/msg-tst",
                MessageBody = messageBody
            };

            await sqsClient.SendMessageAsync(request);

            Console.WriteLine("mensagem enviada com sucesso!");
        }
    }
}