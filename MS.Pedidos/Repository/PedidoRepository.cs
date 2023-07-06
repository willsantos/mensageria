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

        public async Task<Pedido> GetById(Guid id)
        {
            try
            {
                var objetoEncontrado = await _appDbContext.Pedidos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                return objetoEncontrado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
