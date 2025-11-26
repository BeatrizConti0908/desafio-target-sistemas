# Desafio Técnico - Target Sistemas

Este repositório contém a solução para o desafio técnico da vaga de **Desenvolvedor(a) de Sistemas Jr.**

## 🚀 Tecnologias Utilizadas
* **Linguagem:** C# (.NET 8.0)
* **Formato de Dados:** JSON
* **Ferramentas:** Visual Studio Code

## 📋 Funcionalidades Implementadas

O projeto foi estruturado em uma aplicação de console (`Console Application`) que resolve os três problemas propostos:

### 1. Cálculo de Comissões
Processamento de um JSON de vendas para calcular a comissão de cada vendedor seguindo as regras de negócio:
* Vendas < R$ 100,00: Sem comissão.
* Vendas < R$ 500,00: 1% de comissão.
* Vendas >= R$ 500,00: 5% de comissão.

### 2. Controle de Estoque
Sistema de movimentação de produtos que permite:
* Entrada e saída de mercadorias.
* Geração automática de ID único para cada movimentação (Guid).
* Validação de produto existente.
* Atualização de saldo em tempo real.

### 3. Cálculo de Juros
Módulo financeiro que calcula juros simples baseados em datas:
* Cálculo de dias corridos de atraso.
* Aplicação de taxa de 2,5% ao dia.

## ⚙️ Como Rodar o Projeto

1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado.
2. Clone este repositório.
3. Abra o terminal na pasta do projeto.
4. Execute o comando:

```bash
dotnet run