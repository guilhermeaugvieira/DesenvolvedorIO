using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
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
        }

        public static void ConsultarDados()
        {
            var db = new Data.ApplicationContext();
            var consultaPorMetodo = db.Set<Cliente>().Where(c => c.Id >0).Count();
            Console.WriteLine("Registros: " + consultaPorMetodo);

        }
        public static void InserirDadosEmMassa()
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

        public static void InserirDados()
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
