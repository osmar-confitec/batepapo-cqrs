using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatePapo.CrossCutting
{
    public static class MetodosComunsExtensao
    {


        #region " Linq "

        public static IQueryable<T> OrderByAscDesc<T>(this IQueryable<T> source, string ordering, bool Asc)
                 => MetodosComuns.OrderByAscDesc<T>(source, ordering, Asc);

        public static bool TemDados<T>(this IEnumerable<T> enumerado) => MetodosComuns.TemDados(enumerado);

        #endregion

        #region " Enuns "

        public static string ObterAtributoDescricao<T>(this T source)
        {
            return MetodosComuns.ObterAtributoDescricao<T>(source);
        }

        public static T ObterEnumToName<T>(this string value)
        {
            var enu = MetodosComuns.ObterEnumToName<T>(value);
            return enu;
        }


        public static Dictionary<int, string> EnumToDictionaryName<T>()
        {
            return MetodosComuns.EnumToDictionaryName<T>();
        }

        public static Dictionary<int, string> EnumToDictionaryDescricao<T>()
        {
            return MetodosComuns.EnumToDictionaryDescricao<T>();
        }


        #endregion

        #region " String "


        public static bool EGuid(this string numero) => MetodosComuns.EGuid(numero);

        /// <summary>
        /// Formatar uma string CNPJ
        /// </summary>
        /// <param name="CNPJ">string CNPJ sem formatacao</param>
        /// <returns>string CNPJ formatada</returns>
        /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>

        public static string FormatCNPJ(this string CNPJ) => MetodosComuns.FormatCNPJ(CNPJ);

        /// <summary>
        /// Formatar uma string CPF
        /// </summary>
        /// <param name="CPF">string CPF sem formatacao</param>
        /// <returns>string CPF formatada</returns>
        /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>

        public static string FormatCPF(this string CPF) => MetodosComuns.FormatCPF(CPF);


        public static bool ValidarEmail(this string email) => MetodosComuns.ValidarEmail(email);

        public static bool ECnpj(this string cnpj) => MetodosComuns.ECnpj(cnpj);

        public static bool ECpf(this string cpf) => MetodosComuns.ECpf(cpf);

        public static string SomenteNumeros(this string numeros) => MetodosComuns.SomenteNumeros(numeros);

        #endregion

        #region " DateTime "


        public static bool MaiorDeIdade(this DateTime data) => MetodosComuns.MaiorDeIdade(data);

        public static bool DataValidaSQL(this DateTime data) => MetodosComuns.DataValidaSQL(data);

        #endregion

        #region " Segurança "

        public static void CriarSenhaHash(this string senha, out byte[] senhaHash, out byte[] senhaSalt) => MetodosComuns.CriarSenhaHash(senha, out senhaHash, out senhaSalt);

        public static void VerificarSenhaHash(this string senha, byte[] passwordHash, byte[] passwordSalt) => MetodosComuns.VerificarSenhaHash(senha, passwordHash, passwordSalt);


        #endregion
    }
}
