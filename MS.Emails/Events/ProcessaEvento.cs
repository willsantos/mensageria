﻿using System.Text.Json;
using AutoMapper;
using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Events
{
    public class ProcessaEvento: IProcessaEvento
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessaEvento(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void Processa(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<ICodigoEmailService>();

            var email = JsonSerializer.Deserialize<EmailRequestDto>(mensagem);

            if (email == null) throw new ArgumentNullException(nameof(email));


            service.GerarCodigoConfirmacaoAsync(email);
            
        }
    }
}
