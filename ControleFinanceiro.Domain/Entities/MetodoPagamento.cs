namespace ControleFinanceiro.Domain.Entities
{
    public enum MetodoPagamento
    {
        Dinheiro = 1,
        Transferencia = 2,
        CartaoCredito = 3,
        CartaoDebito = 4
    }

    public static class MetodoPagamentoExtensions
    {
        public static bool PossuiLiquidacaoAutomatica(this MetodoPagamento metodo)
        {
            return metodo == MetodoPagamento.CartaoCredito || metodo == MetodoPagamento.CartaoDebito;
        }
    }
}
