using System;
using System.Globalization;
using System.Text;

namespace CompreAqui.Domain.Tools
{
    public static class EncryptingTool
    {
        /// <summary>
        /// Funcao Para Criptografar ou retornar o conteudo descriptografado
        /// </summary>
        /// <param name="conteudo">String a Ser Criptograda</param>
        /// <returns>String Criptografada</returns>
        public static string Criptografa(this string conteudo)
        {
            // Criptografa os Dados
            try
            {
                conteudo = TiraAcentos(conteudo);
                Byte[] b = Encoding.ASCII.GetBytes(conteudo);
                string retorno = Convert.ToBase64String(b);
                return retorno;
            }
            catch (Exception erro)
            {

                throw new ArgumentException("Erro ao tentar criptografar senha", erro);
            }

        }
        /// <summary>
        /// Funcao Para Descriptografar string Criptografada
        /// </summary>
        /// <param name="conteudo">String a Ser DesCriptograda</param>
        /// <returns>String DesCriptografada</returns>
        public static string Descriptografa(this string conteudo)
        {
            // DescCriptografa os Dados
            try
            {
                Byte[] b = Convert.FromBase64String(conteudo);
                string retorno = Encoding.ASCII.GetString(b);
                return retorno;
            }
            catch (Exception erro)
            {
                throw new ArgumentException("Erro ao tentar descriptografar senha", erro);
            }
        }

        /// <summary>
        /// Remove acentos de uma string
        /// </summary>
        /// <param name="texto">String de origem a ser removido os acentos</param>
        /// <returns>String com os acentos removidos</returns>
        public static string TiraAcentos(string texto)
        {
            string stFormD = texto.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (var value in stFormD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(value);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(value);
                }
            }
            return (sb.ToString());
        }
    }
}
