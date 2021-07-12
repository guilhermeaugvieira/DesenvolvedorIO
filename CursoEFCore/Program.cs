using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoEFCore
{
    public class Program
    {
        public static TipoProduto TipoProduto { get; private set; }

        public static void Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            // InserirDados();
            // InserirDadosEmMassa();
            // ConsultarDados();
            // CadastrarPedido();
            // ConsultarPedidoCarregamentoAdiantado();
            // AtualizarDados();
            RemoverRegistro();
        }

        private static void RemoverRegistro()
        {
            var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);
            // db.Clientes.Remove(cliente);
            // db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);
            cliente.Nome = "Cliente Alterado Passo 2";

            // db.Clientes.Update(cliente);
            // db.Attach(cliente); Ativa o rastreamento do objeto pelo Entity
            // db.Entry(cliente).CurrentValues.SetValues(cliente); Seta novos valores para o objeto do entity rastreado
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos.
                Include(p => p.Itens).
                ThenInclude(p => p.Produto).
                ToList();

            Console.WriteLine(pedidos.Count);
        }
        
        private static void CadastrarPedido()
        {
            var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            var db = new Data.ApplicationContext();
            var consultaPorMetodo = db.Set<Cliente>().Where(c => c.Id >0).Count();
            Console.WriteLine("Registros: " + consultaPorMetodo);

        }
        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                CEP = "99999000",
                Cidade = "Itabaiana",
                Estado = "SE",
                Nome = "Fulano",
                Telefone = "99000001111"
            };

            var db = new Data.ApplicationContext();

            db.AddRange(produto, cliente);
            var registros = db.SaveChanges();
            Console.WriteLine("Total Registros: " + registros);
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var db = new Data.ApplicationContext();

            //db.Produtos.Add(produto);
            db.Set<Produto>().Add(produto);
            var registros = db.SaveChanges();
            Console.WriteLine("Total Registros: " + registros);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
