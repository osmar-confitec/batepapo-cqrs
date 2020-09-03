
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BatePapo.CrossCutting
{
  public static  class MetodosComuns
    {
        #region " Strings "

        public static string SomenteNumeros(string numeros) => String.Join("", System.Text.RegularExpressions.Regex.Split(string.IsNullOrEmpty(numeros) ? "" : numeros, @"[^\d]"));

        public static bool EGuid(string numero)
        {

            var guidResult = Guid.Empty;
            return Guid.TryParse(numero, out guidResult);

        }

        /// <summary>
        /// Formatar uma string CNPJ
        /// </summary>
        /// <param name="CNPJ">string CNPJ sem formatacao</param>
        /// <returns>string CNPJ formatada</returns>
        /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>

        public static string FormatCNPJ(string CNPJ)
        {
            return Convert.ToUInt64(SomenteNumeros(CNPJ)).ToString(@"00\.000\.000\/0000\-00");
        }

        /// <summary>
        /// Formatar uma string CPF
        /// </summary>
        /// <param name="CPF">string CPF sem formatacao</param>
        /// <returns>string CPF formatada</returns>
        /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>

        public static string FormatCPF(string CPF)
        {
            return Convert.ToUInt64(SomenteNumeros(CPF)).ToString(@"000\.000\.000\-00");
        }


        public static bool ECpf(string cpf)
        {

            cpf = SomenteNumeros(cpf);

            if (string.IsNullOrEmpty(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static bool ValidarEmail(string email)
        {
            bool emailValido = false;
            //Expressão regular retirada de
            //https://msdn.microsoft.com/pt-br/library/01escwtf(v=vs.110).aspx
            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            try
            {
                emailValido = Regex.IsMatch(
                    email,
                    emailRegex);
            }
            catch (RegexMatchTimeoutException)
            {
                emailValido = false;
            }

            return emailValido;

        }
        public static bool ECnpj(string cnpj)
        {
            cnpj = SomenteNumeros(cnpj);

            if (string.IsNullOrEmpty(cnpj))
                return false;


            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);

        }

        #endregion

        #region " Enuns "



        public static T ObterEnumToName<T>(string value)
        {
            var en = (T)Enum.Parse(typeof(T), value, true);
            return en;

        }

        public static string ObterAtributoDescricao<T>(T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
               typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0) return attributes[0].Description;

                else return source.ToString();
            }

            return source.ToString();
        }

        public static Dictionary<int, string> EnumToDictionaryName<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException(" Tipo não é um enum");
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => (int)(object)t, t => t.ToString());
        }

        public static Dictionary<int, string> EnumToDictionaryDescricao<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Tipo não é um enum");
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => (int)(object)t, t => ObterAtributoDescricao(t));
        }


        #endregion

        #region " DateTime "

        public static bool DataValidaSQL(DateTime data)
        {
            if (data.Equals(DateTime.MinValue)
           || data.Equals(DateTime.MaxValue)
           || data < System.Data.SqlTypes.SqlDateTime.MinValue.Value
           || data > System.Data.SqlTypes.SqlDateTime.MaxValue.Value)
                return false;
            return true;
        }


        public static bool MaiorDeIdade(DateTime data)
        {
            var datadif = new DiferencaEntreDatas(DateTime.Now, data);
            return (datadif.Years < 18);
        }

        #endregion

        #region " Generics "

        public static void CopyT1T2DiferentClass<T1, T2>(T1 t1, T2 t2) where T1 : class where T2 : class
        {

            var type1 = t1.GetType();

            var type2 = t2.GetType();

            if (t1 == null)
                t1 = (T1)Activator.CreateInstance(type1);
            if (t2 == null)
                t2 = (T2)Activator.CreateInstance(type2);

            PropertyInfo[] properties = type1.GetProperties();

            foreach (var propertyInfo in properties)
            {
                PropertyInfo property = type1.GetProperty(propertyInfo.Name);
                PropertyInfo propertyt2 = type2.GetProperty(propertyInfo.Name);

                if (propertyt2 == null)
                    continue;

                var val = property.GetValue(t1, null);
                var valt2 = propertyt2.GetValue(t2, null);
                var typeprop = propertyInfo.PropertyType;
                var typepropt2 = propertyt2.PropertyType;


                if (typeprop.IsValueType || typeprop == typeof(string))
                {
                    propertyt2.SetValue(t2, val, null);
                }

                else if (typeprop.GetInterfaces().Contains(typeof(IEnumerable))
                            && typeprop != typeof(byte[])
                            )
                {

                    var typecol = typepropt2.GetTypeInfo().GenericTypeArguments[0];
                    var enumerable = val as IEnumerable<object>;
                    foreach (var item in enumerable)
                    {
                        var objInserir = Activator.CreateInstance(typecol);
                        CopyT1T2(item, objInserir);
                        (valt2 as IList).Add(objInserir);
                    }
                }
                else
                {

                    if (val != null)
                    {
                        /*as class*/
                        /*nova instancia da classe*/
                        var classnew = Activator.CreateInstance(typepropt2);
                        CopyT1T2(val, classnew);
                        propertyt2.SetValue(t2, classnew, null);
                    }

                }

            }

        }

        public static void CopyT1T2<T1, T2>(T1 t1, T2 t2, bool CopiarTodosValores = true) where T1 : class where T2 : class
        {
            var type1 = t1.GetType();

            if (t1 == null)
                t1 = (T1)Activator.CreateInstance(type1);
            if (t2 == null)
                t2 = (T2)Activator.CreateInstance(type1);

            if (!t2.GetType().Equals(t1.GetType()))
                return;

            PropertyInfo[] properties = type1.GetProperties();

            foreach (var propertyInfo in properties)
            {
                PropertyInfo property = type1.GetProperty(propertyInfo.Name);
                var val = property.GetValue(t1, null);
                var valt2 = property.GetValue(t2, null);
                var typeprop = propertyInfo.PropertyType;


                if (typeprop.IsValueType || typeprop == typeof(string))
                {
                    /*não copia todos os valores só os que estão preenchidos*/
                    if (!CopiarTodosValores)
                    {
                        if (val != null)
                        {

                            if (typeprop == typeof(string) && !string.IsNullOrEmpty(val.ToString()))
                            {
                                property.SetValue(t2, val, null);
                            }
                            else if (IsNumeric(val as ValueType))
                            {
                                var numeric = Convert.ToInt64(val);
                                if (numeric > 0 || numeric < 0)
                                    property.SetValue(t2, val, null);
                            }
                            else if (typeprop == typeof(DateTime) && Convert.ToDateTime(val) != default)
                                property.SetValue(t2, val, null);
                        }
                        continue;
                    }
                    property.SetValue(t2, val, null);
                }

                else if (typeprop.GetInterfaces().Contains(typeof(IEnumerable))
                            && typeprop != typeof(byte[])
                            )
                {

                    var typecol = typeprop.GetTypeInfo().GenericTypeArguments[0];
                    var enumerable = val as IEnumerable<object>;
                    foreach (var item in enumerable)
                    {
                        var objInserir = Activator.CreateInstance(typecol);
                        CopyT1T2(item, objInserir);
                        (valt2 as IList).Add(objInserir);
                    }
                }
                else
                {

                    if (val != null)
                    {
                        /*as class*/
                        /*nova instancia da classe*/
                        var classnew = Activator.CreateInstance(typeprop);
                        CopyT1T2(val, classnew);
                        property.SetValue(t2, classnew, null);
                    }

                }


            }

        }

        #endregion

        #region " Segurança "

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
        //   string decrypted = CipherUtility.Decrypt<TripleDESCryptoServiceProvider>(newhash, senhaenvio, palavrachave);
        public static string Encrypt<T>(string value, string password, string salt)
   where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new T();

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public static string Decrypt<T>(string text, string password, string salt)
           where T : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new T();

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static bool VerificarSenhaHash(string senha, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var ComputeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                for (var i = 0; i < ComputeHash.Length; i++)
                    if (ComputeHash[i] != passwordHash[i]) return false;
            }
            return true;
        }

        public static void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }

        }
        #endregion

        #region " Métodos Comparação "
        public static bool IsInteger(ValueType value)
        {
            return (value is SByte || value is Int16 || value is Int32
                    || value is Int64 || value is Byte || value is UInt16
                    || value is UInt32 || value is UInt64
                    || value is BigInteger);
        }

        public static bool IsFloat(ValueType value)
        {
            return (value is float | value is double | value is Decimal);
        }

        public static bool IsNumeric(ValueType value)
        {
            return (value is Byte ||
                    value is Int16 ||
                    value is Int32 ||
                    value is Int64 ||
                    value is SByte ||
                    value is UInt16 ||
                    value is UInt32 ||
                    value is UInt64 ||
                    value is BigInteger ||
                    value is Decimal ||
                    value is Double ||
                    value is Single);
        }
        #endregion

        #region " Linq "


        public static bool TemDados<T>(IEnumerable<T> enumerado)
        {

            return enumerado != null && enumerado.Any();

        }

        public static IQueryable<T> OrderByAscDesc<T>(IQueryable<T> source, string ordering, bool Asc)
        {
            PropertyInfo property;
            var type = typeof(T);
            var parameter = Expression.Parameter(type, "p");
            Expression propertyAccess = null;
            if (ordering.Contains('.'))
            {
                // support to be sorted on child fields.
                String[] childProperties = ordering.Split('.');
                property = typeof(T).GetProperty(childProperties[0]);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
                for (int i = 1; i < childProperties.Length; i++)
                {
                    property = property.PropertyType.GetProperty(childProperties[i]);
                    propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
                }
            }
            else
            {
                property = type.GetProperty(ordering);
                propertyAccess = Expression.MakeMemberAccess(parameter, property);
            }

            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), Asc == true ? "OrderBy" : "OrderByDescending", new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        #endregion


    }
}
