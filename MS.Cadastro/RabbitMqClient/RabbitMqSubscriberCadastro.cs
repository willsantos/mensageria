﻿using MS.Cadastro.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MS.Cadastro.RabbitMqClient
{
    public class RabbitMqSubscriberCadastro : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly string _nomeDaFila;
        private readonly IConnection _connection;
        private IModel _channel;
        private IProcessaEvento _processaEvento;

        public RabbitMqSubscriberCadastro(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new ConnectionFactory()
            {
                HostName = _configuration["MS_RABBITMQ_HOST"],
                Port = int.Parse(_configuration["MS_RABBITMQ_PORT"]),
                VirtualHost = _configuration["MS_RABBITMQ_VHOST"],
                UserName = _configuration["MS_RABBITMQ_USER"],
                Password = _configuration["MS_RABBITMQ_PASSWORD"]

            }.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _nomeDaFila = _channel.QueueDeclare("").QueueName;
            _channel.QueueBind(queue: _nomeDaFila, exchange: "trigger", routingKey: "usuarioAtivo");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumidor = new EventingBasicConsumer(_channel);
            consumidor.Received += (ch, ea) =>
            {
                var conteudo = Encoding.UTF8.GetString(ea.Body.ToArray());

                _processaEvento.Processa(conteudo);

            };

            _channel.BasicConsume(queue: _nomeDaFila, autoAck: true, consumer: consumidor);

            return Task.CompletedTask;

        }
    }
}
