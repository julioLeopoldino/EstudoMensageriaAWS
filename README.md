📦 SQS Message Processor (.NET)

Este projeto demonstra a implementação de um fluxo completo de mensageria utilizando Amazon SQS com .NET, abordando envio, consumo, tratamento de falhas e uso de Dead Letter Queue (DLQ).

Mais do que apenas código, o foco aqui é mostrar conceitos fundamentais de sistemas que utilizam filas de para processamento de dados

📬Envio de mensagens

O Producer é responsável por publicar eventos na fila.

Neste projeto, ele simula uma transferência bancária, enviando um payload JSON com:

- Conta de origem

- Conta de destino

- Valor

- Moeda

Consumo das Mensagens:

- Leitura da fila (long polling)

- Interpretação do JSON

- Execução da regra de negócio (simulada)

- Remoção da mensagem da fila

⏳Conceitos aplicados:

Utilizie o WaitTimeSeconds = 20 para reduzir as chamadas desnecessárias na AWS

- Diminui custo

- Melhora eficiência

Dead Letter Queue (DLQ)

Os benefícios são evita bloqueio do processamento, permite análise posterior e tmabém ajuda a isolar falhas
  
- Mensagens que falham repetidamente não ficam travando o sistema.
  
- Elas são redirecionadas para uma fila separada (DLQ).
