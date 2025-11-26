using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DesafioTarget
{
    // --- CLASSES PARTE 1 (VENDAS) ---
    public class Venda
    {
        public string? vendedor { get; set; }
        public decimal valor { get; set; }
    }

    public class DadosVendas
    {
        public List<Venda> vendas { get; set; } = new List<Venda>();
    }

    // --- CLASSES PARTE 2 (ESTOQUE) ---
    public class Produto
    {
        public int codigoProduto { get; set; }
        public string? descricaoProduto { get; set; }
        public int estoque { get; set; }
    }

    public class DadosEstoque
    {
        public List<Produto> estoque { get; set; } = new List<Produto>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // --- EXECUÇÃO PARTE 1 ---
            ExecutarDesafioComissao();
            Console.WriteLine("\n=================================================\n");

            // --- EXECUÇÃO PARTE 2 ---
            ExecutarDesafioEstoque();
            Console.WriteLine("\n=================================================\n");

            // --- EXECUÇÃO PARTE 3 ---
            ExecutarDesafioJuros();
        }

        // =============================================================
        // LÓGICA DA PARTE 1: COMISSÕES
        // =============================================================
        static void ExecutarDesafioComissao()
        {
            Console.WriteLine("--- DESAFIO 1: CÁLCULO DE COMISSÕES ---");

            string jsonVendas = @"
            {
                ""vendas"": [
                    { ""vendedor"": ""João Silva"", ""valor"": 1200.50 },
                    { ""vendedor"": ""João Silva"", ""valor"": 250.30 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 90.75 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 1750.00 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 800.50 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 420.90 }
                ]
            }";

            var dados = JsonSerializer.Deserialize<DadosVendas>(jsonVendas);

            if (dados?.vendas != null)
            {
                var relatorio = dados.vendas
                    .GroupBy(v => v.vendedor)
                    .Select(grupo => new
                    {
                        NomeVendedor = grupo.Key,
                        TotalComissao = grupo.Sum(v => CalcularComissao(v.valor))
                    });

                foreach (var item in relatorio)
                {
                    Console.WriteLine($"Vendedor: {item.NomeVendedor?.PadRight(20)} | Comissão: R$ {item.TotalComissao:F2}");
                }
            }
        }

        static decimal CalcularComissao(decimal valor)
        {
            if (valor < 100) return 0;
            if (valor < 500) return valor * 0.01m;
            return valor * 0.05m;
        }

        // =============================================================
        // LÓGICA DA PARTE 2: ESTOQUE
        // =============================================================
        static void ExecutarDesafioEstoque()
        {
            Console.WriteLine("--- DESAFIO 2: CONTROLE DE ESTOQUE ---");

            string jsonEstoque = @"
            {
                ""estoque"": [
                    { ""codigoProduto"": 101, ""descricaoProduto"": ""Caneta Azul"", ""estoque"": 150 },
                    { ""codigoProduto"": 102, ""descricaoProduto"": ""Caderno Universitário"", ""estoque"": 75 },
                    { ""codigoProduto"": 103, ""descricaoProduto"": ""Borracha Branca"", ""estoque"": 200 }
                ]
            }";

            var dados = JsonSerializer.Deserialize<DadosEstoque>(jsonEstoque);
            var listaProdutos = dados?.estoque ?? new List<Produto>();

            Console.WriteLine("Realizando movimentações...");
            
            MovimentarEstoque(listaProdutos, 101, 50, "Entrada - Compra Fornecedor A");
            MovimentarEstoque(listaProdutos, 101, -10, "Saída - Venda Cliente Balcão");
            MovimentarEstoque(listaProdutos, 999, 5, "Tentativa de erro");
        }

        static void MovimentarEstoque(List<Produto> produtos, int codProduto, int quantidade, string descricao)
        {
            var produto = produtos.FirstOrDefault(p => p.codigoProduto == codProduto);

            if (produto != null)
            {
                Guid idMovimentacao = Guid.NewGuid();
                produto.estoque += quantidade;

                Console.WriteLine($"\n[ID: {idMovimentacao}] - {descricao}");
                Console.WriteLine($"Produto: {produto.descricaoProduto}");
                Console.WriteLine($"Movimentação: {quantidade} un.");
                Console.WriteLine($"Estoque Final: {produto.estoque} un.");
            }
            else
            {
                Console.WriteLine($"\n[ERRO] Produto {codProduto} não encontrado para movimentação: {descricao}");
            }
        }

        // =============================================================
        // LÓGICA DA PARTE 3: CÁLCULO DE JUROS
        // =============================================================
        static void ExecutarDesafioJuros()
        {
            Console.WriteLine("--- DESAFIO 3: CÁLCULO DE JUROS ---");

            // Exemplo: Boleto de R$ 1.000,00 que venceu há 15 dias atrás
            decimal valorOriginal = 1000.00m;
            DateTime dataVencimento = DateTime.Now.AddDays(-15); 
            
            Console.WriteLine($"Data de Vencimento Simulada: {dataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Data de Hoje: {DateTime.Now:dd/MM/yyyy}");

            CalcularJurosBoleto(valorOriginal, dataVencimento);
        }

        static void CalcularJurosBoleto(decimal valorOriginal, DateTime dataVencimento)
        {
            DateTime dataHoje = DateTime.Now;

            // Se a data de hoje for menor ou igual ao vencimento, não há juros
            if (dataHoje.Date <= dataVencimento.Date)
            {
                Console.WriteLine("O boleto está em dia. Sem juros.");
                return;
            }

            // Calcula dias corridos de atraso
            int diasAtraso = (dataHoje.Date - dataVencimento.Date).Days;

            // Regra: 2,5% ao dia (0.025)
            decimal taxaDiaria = 0.025m;
            decimal valorJuros = valorOriginal * taxaDiaria * diasAtraso;
            decimal valorTotal = valorOriginal + valorJuros;

            Console.WriteLine($"\n--- Resultado do Cálculo ---");
            Console.WriteLine($"Dias de atraso: {diasAtraso}");
            Console.WriteLine($"Valor Original: {valorOriginal:C}");
            Console.WriteLine($"Juros Totais: {valorJuros:C}");
            Console.WriteLine($"Valor Final a Pagar: {valorTotal:C}");
        }
    }
}