using System;
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
            var messageBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
            <root>
              <conta_origem>
                <agencia>1</agencia>
                <numero_conta>123456-7</numero_conta>
              </conta_origem>
              <conta_destino>
                <agencia>1</agencia>
                <numero_conta>765432-1</numero_conta>
              </conta_destino>
              <valor>1000</valor>
              <moeda>BRL</moeda>
            </root>";

            var request = new SendMessageRequest
            {
                QueueUrl = "https://sqs.us-east-1.amazonaws.com/364350264218/msg-tst",
                MessageBody = messageBody
            };

            await sqsClient.SendMessageAsync(request);

            Console.WriteLine("mensagem envenenada enviada com sucesso!");
        }
    }
}