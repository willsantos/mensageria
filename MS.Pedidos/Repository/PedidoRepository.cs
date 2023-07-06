﻿using Microsoft.EntityFrameworkCore;
using MS.Pedidos.Entities;
using MS.Pedidos.Interfaces.Repository;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;

        public PedidoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Post(Pedido Pedido)
        {
            var pedidoCadastrado = _appDbContext.Add(Pedido); 
            await _appDbContext.SaveChangesAsync();            
        }

        public async Task<Pedido> GetById(Guid Id)
        {
            var objetoEncontrado = await _appDbContext.Set<Pedido>().FindAsync(Id);
            return objetoEncontrado;
        }
           
        public Task<IEnumerable<PedidoDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> GetPedido()
        {
            throw new NotImplementedException();
        }

        public async Task Pacth(Pedido pedidoAtualizado)
        {            
             _appDbContext.Pedidos.Update(pedidoAtualizado);
            await _appDbContext.SaveChangesAsync();
        }
    
        public Task Put()
        {
            throw new NotImplementedException();
        }

        public Task Remove()
        {
            throw new NotImplementedException();
        }

        public Task Pacth()
        {
            throw new NotImplementedException();
        }
    }
}
