﻿using MVC.Business.Interfaces;
using MVC.Business.Models;
using MVC.Business.Models.Validations;

namespace MVC.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotifier notifier) : base(notifier)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task<bool> Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return false;

            if (_fornecedorRepository.Buscar(p => p.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return false;
            }

            await _fornecedorRepository.Adicionar(fornecedor);
            return true;
        }

        public async Task<bool> Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return false;

            if (_fornecedorRepository.Buscar(p => p.Documento == fornecedor.Documento && p.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");
                return false;
            }

            await _fornecedorRepository.Atualizar(fornecedor);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados!");
                return false;
            }

            var endereco = await _enderecoRepository.ObterEnderecoPorFonecedor(id);

            if (endereco != null)
                await _enderecoRepository.Remover(endereco.Id);

            await _fornecedorRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}