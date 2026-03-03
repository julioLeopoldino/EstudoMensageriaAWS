using System;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

class Program
{
    private const string QUEUE_URL = "https://sqs.us-east-1.amazonaws.com/364350264218/msg-tst";

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
                    try
                    {
                        using var doc = JsonDocument.Parse(message.Body);
                        var root = doc.RootElement;

                        var contaOrigem = root.GetProperty("conta_origem").GetProperty("numero_conta").GetString();
                        var contaDestino = root.GetProperty("conta_destino").GetProperty("numero_conta").GetString();
                        var valor = root.GetProperty("valor").GetDecimal();
                        var moeda = root.GetProperty("moeda").GetString();

                        Console.WriteLine("processando mensagem...");
                        Console.WriteLine($"realizando transferência da conta {contaOrigem} para a conta {contaDestino}");
                        Console.WriteLine($"transferência no valor de {valor} {moeda} efetivada!");

                        await sqsClient.DeleteMessageAsync(new DeleteMessageRequest
                        {
                            QueueUrl = QUEUE_URL,
                            ReceiptHandle = message.ReceiptHandle
                        });

                        Console.WriteLine("mensagem processada (e excluída) com sucesso!");
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Mensagem não está no formato JSON: {ex.Message}");
                    }
                }
            }
        }
    }
}