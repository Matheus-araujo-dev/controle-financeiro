using System.Linq;

namespace ControleFinanceiro.Shared.Utils
{
    public static class DocumentoValidator
    {
        public static bool IsValid(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento)) return false;
            var digits = new string(documento.Where(char.IsDigit).ToArray());
            return digits.Length switch
            {
                11 => IsValidCpf(digits),
                14 => IsValidCnpj(digits),
                _ => false
            };
        }

        private static bool IsValidCpf(string cpf)
        {
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0])) return false;
            int[] mult1 = {10,9,8,7,6,5,4,3,2};
            int[] mult2 = {11,10,9,8,7,6,5,4,3,2};
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - '0') * mult1[i];
            int rem = sum % 11;
            int dig1 = rem < 2 ? 0 : 11 - rem;
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += ((i == 9 ? dig1 : cpf[i] - '0')) * mult2[i];
            rem = sum % 11;
            int dig2 = rem < 2 ? 0 : 11 - rem;
            return cpf.EndsWith($"{dig1}{dig2}");
        }

        private static bool IsValidCnpj(string cnpj)
        {
            if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0])) return false;
            int[] mult1 = {5,4,3,2,9,8,7,6,5,4,3,2};
            int[] mult2 = {6,5,4,3,2,9,8,7,6,5,4,3,2};
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (cnpj[i] - '0') * mult1[i];
            int rem = sum % 11;
            int dig1 = rem < 2 ? 0 : 11 - rem;
            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += ((i == 12 ? dig1 : cnpj[i] - '0')) * mult2[i];
            rem = sum % 11;
            int dig2 = rem < 2 ? 0 : 11 - rem;
            return cnpj.EndsWith($"{dig1}{dig2}");
        }
    }
}
