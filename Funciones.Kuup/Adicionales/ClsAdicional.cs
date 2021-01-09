using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Web.Mvc;
using Mod.Entity;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web.Management;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Web.Security.AntiXss;
using System.Web.WebPages;
using System.Data.SqlTypes;
using System.Web.Configuration;
using System.Web;
using System.Collections;
using System.Data.Entity.Validation;

namespace Funciones.Kuup.Adicionales
{
    public static class ClsAdicional
    {
        #region Adicionales
        [System.Diagnostics.DebuggerHidden()]
        public static T Convert<T>(this String input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    if (input == "")
                    {
                        if (typeof(T) != typeof(String))
                        {
                            input = "0";
                        }
                    }
                    return (T)converter.ConvertFromString(input);
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }
        public static class ClsGetHostNameAndIP
        {
            [System.Diagnostics.DebuggerHidden()]
            public static String GetIPv4Address(String sHostNameOrAddress)
            {
                try
                {
                    IPAddress[] aIPHostAddresses = Dns.GetHostAddresses(sHostNameOrAddress);

                    foreach (IPAddress ipHost in aIPHostAddresses)
                    {
                        if (ipHost.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ipHost.ToString();
                        }
                    }
                    foreach (IPAddress ipHost in aIPHostAddresses)
                    {
                        if (ipHost.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            IPHostEntry ihe = Dns.GetHostEntry(ipHost);
                            foreach (IPAddress ipEntry in ihe.AddressList)
                            {
                                if (ipEntry.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                {
                                    return ipEntry.ToString();
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return String.Empty;
            }
            [System.Diagnostics.DebuggerHidden()]
            public static String GetHostName(String UserHostAddres)
            {
                return System.Net.Dns.GetHostEntry(GetIPv4Address(UserHostAddres)).HostName;
            }
        }
        public static T Deserializar<T>(String JsonString)
        {
            return JsonConvert.DeserializeObject<T>(JsonString);
        }
        #endregion
        #region Resultado
        public class ClsResultado
        {
            private bool _Resultado;
            private String _Mensaje;
            private Object _Adicional;
            public bool Resultado
            {
                get { return _Resultado; }
                set { _Resultado = value; }
            }
            public String Mensaje
            {
                get { return _Mensaje; }
                set { _Mensaje = value; }
            }
            public Object Adicional
            {
                get { return _Adicional; }
                set { _Adicional = value; }
            }
            public ClsResultado() { }
            public ClsResultado(bool Resultado, String Mensaje)
            {
                _Resultado = Resultado;
                _Mensaje = Mensaje;
            }
            public ClsResultado(bool Resultado, String Mensaje, Object Adicional)
            {
                _Resultado = Resultado;
                _Mensaje = Mensaje;
                _Adicional = Adicional;
            }
            public static ClsResultado ValidaLista(List<ClsResultado> ListaResultados)
            {
                ClsResultado resultado = new ClsResultado(true, String.Empty);
                if (ListaResultados.Exists(x => x.Resultado == false))
                {
                    resultado.Resultado = false;
                    resultado.Mensaje = "Resultado con errores";
                    resultado.Adicional = ListaResultados.FindAll(x => x.Resultado == false).ToList();
                }
                return resultado;
            }
            public String MensajeController()
            {
                String JsonResultado = JsonConvert.SerializeObject(this);
                return JsonResultado;
            }
        }
        #endregion
        #region Archivos
        public static class ClsArchivos
        {
            private static readonly String _RutaCodigoDeBarras = "~/Imagenes/CodigoBarras/";
            public static String RutaCodigoDeBarras
            {
                get { return _RutaCodigoDeBarras; }
            }
            private static readonly String _Descarga = "~/App_Data/Download/";
            public static String Descarga
            {
                get { return _Descarga; }
            }
            private static readonly String _Tickets = "~/App_Data/Ticket/";
            public static String Tickets
            {
                get { return _Tickets; }
            }
            public static String LeeArchivo(String ruta)
            {
                String line = String.Empty, allLine = String.Empty;
                using (StreamReader file = new StreamReader(ruta, System.Text.Encoding.GetEncoding(1252), false))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        allLine += line + Environment.NewLine;
                    }
                }
                return allLine.TrimEnd('\r', '\n');
            }
            public static bool CrearArchivo(String ruta)
            {
                using (File.Create(ruta))
                {
                    if (FileExists(ruta, false))
                    {
                        return true;
                    }
                }
                return false;
            }
            public static bool FileExists(String ruta, bool isDirectory, bool CreaElemento = false)
            {
                if (!isDirectory)
                {
                    if (File.Exists(ruta))
                    {
                        return true;
                    }
                    else
                    {
                        if (CreaElemento)
                        {
                            return CrearArchivo(ruta);
                        }
                    }
                }
                else
                {
                    if (Directory.Exists(ruta))
                    {
                        return true;
                    }
                    else
                    {
                        if (CreaElemento)
                        {
                            return CrearDirectorio(ruta);
                        }
                    }
                }
                return false;
            }
            public static bool EliminaArchivo(String ruta)
            {
                File.Delete(ruta);
                if (!FileExists(ruta, false))
                {
                    return true;
                }
                return false;
            }
            public static bool CrearDirectorio(String ruta)
            {
                Directory.CreateDirectory(ruta);
                if (FileExists(ruta, true))
                {
                    return true;
                }
                return false;
            }
            public static void EscribeArchivo(String ruta, String Info)
            {
                using (StreamWriter File = new StreamWriter(ruta, false, System.Text.Encoding.GetEncoding(1252)))
                {
                    File.Write(Info);
                }
            }
            public static void EscribreArchivoCSV(String ruta, List<String> Titulos, List<List<Object>> Informacion)
            {
                if (Titulos.Count != 0 && Informacion.FirstOrDefault().Count != 0)
                {
                    if (Titulos.Count == Informacion.FirstOrDefault().Count)
                    {
                        using (var Stream = new StreamWriter(ruta, false, System.Text.Encoding.GetEncoding(1252)))
                        {
                            System.Text.StringBuilder Textos = new System.Text.StringBuilder();
                            Textos.AppendLine(String.Join(",", Titulos));
                            foreach (var Linea in Informacion)
                            {
                                List<String> LineaFormato = new List<String>();
                                var lstString = Linea.OfType<String>();
                                foreach (var Elemento in lstString)
                                {
                                    var Item = Elemento;
                                    if (Item.Contains(Environment.NewLine))
                                    {
                                        Item = Item.Replace(Environment.NewLine, " ");
                                    }
                                    if (Item.Contains(@""""))
                                    {
                                        Item = Item.Replace(@"""", @"""""");
                                    }
                                    if (Item.Contains(","))
                                    {
                                        Item = @"""" + Item + @"""";
                                    }
                                    LineaFormato.Add(Item);
                                }
                                Textos.AppendLine(String.Join(",", LineaFormato));
                            }
                            Stream.Write(Textos.ToString());
                        }
                    }
                }
            }
            private static List<List<String>> ImportarCSV(String ruta, bool PermiteNulos = true)
            {
                List<List<String>> Datos = new List<List<String>>();
                if (FileExists(ruta, false))
                {
                    String ContenidoCSV = LeeArchivo(ruta);
                    if (ContenidoCSV.Length > 0)
                    {
                        foreach (var lineaCSV in ContenidoCSV.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                        {
                            if (lineaCSV.Trim().Length > 0)
                            {
                                List<String> registroFila = new List<String>();
                                registroFila = LineaCsvToList(lineaCSV, ',', PermiteNulos);
                                if (registroFila.Count > 0)
                                {
                                    Datos.Add(registroFila);
                                }
                            }
                        }
                    }
                }
                return Datos;
            }
            private static List<String> LineaCsvToList(String lineaCsv, Char Separador, bool PermiteNulos = true)
            {
                List<String> Resultado = new List<String>();
                if (lineaCsv == null)
                {
                    // El valor de la linea es null
                }
                if (lineaCsv.Length == 0)
                {
                    return Resultado;
                }
                if (!(Separador == ',' || Separador == ';' || Separador == '.' || Separador == '|' || Separador == '-' || Separador == '_' || Separador == '/' || Separador == '\\'))
                {
                    // El separador no es valido
                }
                Char dobleComilla = Convert<Char>(@"""");
                Regex objRegEx = null;
                MatchCollection objMatchCollection = null;
                String Aux = String.Empty, patron = String.Empty;
                if (Separador == ',' || Separador == '_')
                {
                    patron = @"((?:[^""" + Separador + @"]|(?:""(?:\\{2}|\\""|[^""])*?""))+)|(?=" + Separador + @"{2}|" + Separador + @"$)";
                }
                else
                {
                    patron = @"((?:[^""\" + Separador + @"]| (?: ""(?:\\{ 2}|\\"" |[^""])*? ""))+)| (?=\" + Separador + @"{ 2}|\" + Separador + @"$)";
                }
                try
                {
                    objRegEx = new Regex(patron, RegexOptions.IgnoreCase);
                    objMatchCollection = objRegEx.Matches(lineaCsv.Trim());
                    if (lineaCsv.Trim().StartsWith(Separador.ToString()))
                    {
                        Resultado.Add(String.Empty);
                    }
                    foreach (Match objMatch in objMatchCollection)
                    {
                        Aux = objMatch.Value.Substring(0, objMatch.Length);
                        if (Aux.Trim().StartsWith(dobleComilla.ToString(), StringComparison.CurrentCulture) && Aux.Trim().EndsWith(dobleComilla.ToString(), StringComparison.CurrentCulture))
                        {
                            Aux = Aux.Trim();
                            Aux = Aux.Replace(Convert<Char>(dobleComilla.ToString() + dobleComilla.ToString()), dobleComilla);
                            Aux = Aux.Replace(dobleComilla, Convert<Char>(String.Empty));
                        }
                        if (Aux.Trim() != String.Empty || PermiteNulos)
                        {
                            Resultado.Add(Aux);
                        }
                    }
                }
                catch (Exception e)
                {
                    // Mensaje de error
                }
                finally
                {
                    if (objRegEx != null)
                    {
                        objRegEx = null;
                    }
                    if (objMatchCollection != null)
                    {
                        objMatchCollection = null;
                    }
                    patron = String.Empty;
                    Aux = String.Empty;
                }
                if (Resultado.Count == 1)
                {
                    Resultado.RemoveAll(x => x.Trim() == String.Empty);
                }
                return Resultado;
            }
            private static bool ComparaLista(List<String> listaA, List<String> listaB)
            {
                bool Iguales = false;
                int Indice = 0;
                int ElementosIguales = 0;
                if (listaA.Count == listaB.Count)
                {
                    foreach (var ElementoA in listaA)
                    {
                        if (ElementoA.Trim().ToUpper() == listaB[Indice].Trim().ToUpper())
                        {
                            ElementosIguales++;
                        }
                        Indice++;
                    }
                    if (listaA.Count() == ElementosIguales)
                    {
                        Iguales = true;
                    }
                }
                return Iguales;
            }
            public static ClsResultado ImportarCSVConvierteAEntidad(String ruta, Func<List<List<String>>,
                List<Dictionary<String, String>>, ClsResultado> DelegadoValidaCamposParaImportacion,
                Func<Dictionary<String, String>, List<Object>> DelegadoConvierteListaAEntidad,
                ref IEnumerable<Object> ListaDeEntidad)
            {
                ClsResultado Resultado = new ClsResultado(true, String.Empty);
                List<ClsResultado> lstResultado = new List<ClsResultado>();
                List<List<String>> Info = new List<List<String>>();
                List<List<String>> Info2 = new List<List<String>>();
                List<List<String>> Info3 = new List<List<String>>();
                List<List<String>> InfoTemp = new List<List<String>>();
                try
                {
                    Info3 = ImportarCSV(ruta);
                    if (Info3.Count > 1)
                    {
                        Info3.RemoveAt(0);
                    }
                    else
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "Los datos del archivo no pudieron ser validados debido a que vienen vacios, verifique";
                    }
                }
                catch (Exception e)
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = e.Message;
                }
                Info.Add(Info3.FirstOrDefault());
                Info2.Add(Info3.FirstOrDefault());
                int RegistrosDescatados = 0;
                try
                {
                    bool Encontrado = false;
                    List<Object> lista = new List<Object>();
                    foreach (var elem1 in Info3)
                    {
                        Encontrado = false;
                        foreach (var elem2 in Info2)
                        {
                            if (ComparaLista(elem1, elem2))
                            {
                                Encontrado = true;
                                break;
                            }
                        }
                        bool FilaConContenido = false;
                        foreach (var celda in elem1)
                        {
                            if (!String.IsNullOrEmpty(celda))
                            {
                                FilaConContenido = true;
                                break;
                            }
                        }
                        if (!FilaConContenido)
                        {
                            break;
                        }
                        if (!Encontrado)
                        {
                            Info.Add(elem1);
                            Info2 = new List<List<String>>();
                            Info2.AddRange(Info);
                        }
                        RegistrosDescatados = Info3.Count() - Info.Count();
                        List<Dictionary<String, String>> lstInfo = new List<Dictionary<String, String>>();
                        InfoTemp = new List<List<String>>();
                        InfoTemp.Add(elem1);
                        Resultado = DelegadoValidaCamposParaImportacion(InfoTemp, lstInfo);
                        if (lstInfo.FirstOrDefault().ContainsKey("Secuencial"))
                        {
                            lstInfo.FirstOrDefault()["Secuencial"] = Info2.Count().ToString();
                        }
                        lstResultado.Add(Resultado);
                        if (lstInfo.Count() > 0)
                        {
                            foreach (var item in lstInfo)
                            {
                                lista.AddRange(DelegadoConvierteListaAEntidad(item));
                            }
                        }
                        else
                        {
                            Resultado.Resultado = false;
                            Resultado.Mensaje = "Informacion a validar no es valida para ser capturada";
                        }
                    }
                    if (lista.Count > 0)
                    {
                        ListaDeEntidad = lista;
                    }
                    if (lstResultado.Exists(x => x.Resultado == false))
                    {
                        Resultado = ClsResultado.ValidaLista(lstResultado);
                    }
                    else
                    {
                        Resultado.Mensaje = "Archivo listo para cargar";
                    }
                    return Resultado;
                }
                catch (Exception e)
                {

                }
                return new ClsResultado();
            }
        }
        #endregion
        #region Valida
        public static class ClsValida
        {
            public static ClsResultado ValidaCampoStringCapacidad(String Campo, String CampoTexto, bool EsRequerido, int Capacidad, String ValorDeDato, int row, ref Dictionary<String, String> Registros)
            {
                ClsResultado Resultado = new ClsResultado(true, String.Empty);
                if (String.IsNullOrEmpty(ValorDeDato) && EsRequerido)
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "El campo " + CampoTexto + " es requerido";
                    Registros.Add(Campo, String.Empty);
                    return Resultado;
                }
                else if(string.IsNullOrEmpty(ValorDeDato) && !EsRequerido)
                {
                    Registros.Add(Campo, String.Empty);
                }
                else if (!String.IsNullOrEmpty(ValorDeDato))
                {
                    Resultado = ClsAdicional.ClsValida.ValidaCapacidad(CampoTexto, ValorDeDato, typeof(String), Capacidad, 0);
                    Registros.Add(Campo, ValorDeDato);
                }
                return Resultado;
            }
            public static ClsResultado ValidaCampoNumericoYCapacidad(String Campo, String CampoTexto, bool EsRequerido, int Capacidad, int CapacidadDecimal, String ValorDeDato, Type TipoAConvertir, int row, ref Dictionary<String, String> Registros)
            {
                ClsResultado Resultado = new ClsResultado(true, String.Empty);
                if (!String.IsNullOrEmpty(ValorDeDato))
                {
                    Resultado = ValidaTipoDeDato(CampoTexto, ValorDeDato, TipoAConvertir);
                    if (Resultado.Resultado)
                    {
                        Resultado = ValidaCapacidad(CampoTexto, ValorDeDato, TipoAConvertir, Capacidad, CapacidadDecimal);
                        if (Resultado.Resultado)
                        {
                            Registros.Add(Campo, ValorDeDato);
                        }
                        else
                        {
                            Registros.Add(Campo, ValorDeDato);
                        }
                    }
                }
                else
                {
                    if (EsRequerido)
                    {
                        Registros.Add(Campo, "0");
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "El campo " + Campo + " es requerido";
                    }
                    else
                    {
                        Registros.Add(Campo, "0");
                    }
                }
                return Resultado;
            }
            public static ClsResultado ValidaClave(String Campo, String CampoCve, String CampoTexto, byte NumeroDeClave, String ValorDeDato, int row, ref Dictionary<String, String> Registros)
            {
                ClsResultado Resultado = new ClsResultado(true, String.Empty);
                if (!String.IsNullOrEmpty(ValorDeDato))
                {
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        List<Claves> Claves = (from q in db.Claves where q.CVE_NUM_CLAVE == NumeroDeClave && q.CVE_NOM_CLAVE == ValorDeDato.Trim().ToUpper() select q).ToList();
                        if (Claves.Count() == 1)
                        {
                            Registros.Add(Campo, Claves.FirstOrDefault().CVE_NOM_CLAVE);
                            Registros.Add(CampoCve, Claves.FirstOrDefault().CVE_NUM_SEC_CLAVE.ToString());
                        }
                        else
                        {
                            Registros.Add(Campo, String.Empty);
                            Registros.Add(CampoCve, "0");
                            Resultado.Resultado = false;
                            Resultado.Mensaje = "El campo " + CampoTexto + " no fue encontrado dentro de las claves";
                        }
                    }
                }
                else
                {
                    Registros.Add(Campo, String.Empty);
                    Registros.Add(CampoCve, "0");
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "El campo " + CampoTexto + " es requerido";
                }
                return Resultado;
            }
            public static ClsResultado ValidaCapacidad(String Campo, String Valor, Type TipoDeDato, int CapacidadNumeroEntero, int CapacidadNumeroDecimal)
            {
                ClsResultado Resultado = new ClsResultado(true, String.Empty);
                if (TipoDeDato == typeof(byte) && TipoDeDato == typeof(short))
                {
                    if (Valor.Length > CapacidadNumeroEntero)
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "La capacidad del campo " + Campo + " a registrar excede lo aceptado";
                    }
                }
                else if (TipoDeDato == typeof(decimal))
                {
                    if (Valor.Split('.')[0].Length > CapacidadNumeroEntero)
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "La capacidad del campo " + Campo + " a registrar excede lo aceptado, de parte del entero";
                    }
                    if (Valor.Split(',').Length == 2)
                    {
                        if (Valor.Split(',')[1].Length > CapacidadNumeroDecimal)
                        {
                            Resultado.Resultado = false;
                            if (!String.IsNullOrEmpty(Resultado.Mensaje))
                            {
                                Resultado.Mensaje += "La capacidad del campo " + Campo + " a registrar excede lo aceptado, de parte del decimal";
                            }
                            else
                            {
                                Resultado.Mensaje = "La capacidad del campo  " + Campo + "a registrar excede lo aceptado, de parte del decimal";
                            }
                        }
                    }
                }
                else if (TipoDeDato == typeof(String))
                {
                    if (Valor.Length > CapacidadNumeroEntero)
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "La capacidad del campo " + Campo + " a registrar excede lo aceptado";
                    }
                }
                return Resultado;
            }
            public static ClsResultado ValidaTipoDeDato(String Campo, String Valor, Type TipoAConvertir)
            {
                ClsResultado Resultrado = new ClsResultado(true, String.Empty);
                if (typeof(short) == TipoAConvertir)
                {
                    short outval = 0;
                    if (!short.TryParse(Valor, out outval))
                    {
                        Resultrado.Resultado = false;
                        Resultrado.Mensaje = "El tipo de dato no es aceptado para este campo " + Campo;
                        return Resultrado;
                    }
                }
                if (typeof(byte) == TipoAConvertir)
                {
                    byte outval = 0;
                    if (!byte.TryParse(Valor, out outval))
                    {
                        Resultrado.Resultado = false;
                        Resultrado.Mensaje = "El tipo de dato no es aceptado para este campo " + Campo;
                        return Resultrado;
                    }
                }
                if (typeof(decimal) == TipoAConvertir)
                {
                    decimal outval = 0;
                    if (!decimal.TryParse(Valor, out outval))
                    {
                        Resultrado.Resultado = false;
                        Resultrado.Mensaje = "El tipo de dato no es aceptado para este campo " + Campo;
                        return Resultrado;
                    }
                }
                return Resultrado;
            }
        }
        #endregion
        #region ManejaError
        public class ClsManejaError
        {
            public String MensajeErrorException(Exception exception, String NombreDeUsuario, String TipoDeExcepcion, String CodigoDeError)
            {
                String Mensaje = "Tipo de Excepción" + Environment.NewLine +
                    "Mensaje: " + exception.Message + Environment.NewLine +
                    "Versión: " + "" + Environment.NewLine +
                    "Usuario: " + NombreDeUsuario + Environment.NewLine +
                    "Fecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine;
                if (HttpContext.Current.Request.UrlReferrer != null)
                {
                    Mensaje += "Referrer : " + HttpContext.Current.Request.UrlReferrer.PathAndQuery + Environment.NewLine;
                }
                Mensaje += "Request URL: " + HttpContext.Current.Request.Url.PathAndQuery + Environment.NewLine;
                List<String> Parametros = new List<string>();
                int Cont = 0;
                switch (HttpContext.Current.Request.HttpMethod)
                {
                    case "GET":

                        foreach (var Param in HttpContext.Current.Request.QueryString)
                        {
                            if (Param != null)
                            {
                                Parametros.Add("* Parametro: " + Param + " = " + HttpContext.Current.Request.QueryString[Cont]);
                                Cont++;
                            }
                            if (Parametros.Count() > 0)
                            {
                                Mensaje += "Parametros por GET" + Environment.NewLine +
                                    String.Join(Environment.NewLine, Parametros);
                            }
                        }
                        break;
                    case "POST":
                        foreach (var Param in HttpContext.Current.Request.Form)
                        {
                            if (Param != null)
                            {
                                Parametros.Add("* Parametro: " + Param + " = " + HttpContext.Current.Request.Form[Cont]);
                                Cont++;
                            }
                            if (Parametros.Count() > 0)
                            {
                                Mensaje += "Parametros por POST" + Environment.NewLine +
                                    String.Join(Environment.NewLine, Parametros);
                            }
                        }
                        break;
                }
                Mensaje += "Método de Petición: " + HttpContext.Current.Request.HttpMethod + Environment.NewLine +
                    "Petición AJAX: " + (HttpContext.Current.Request.Headers.Get("X-Requested-With") == "XMLHttpRequest" ? "SI" : "NO") + Environment.NewLine +
                    "User Agent: " + HttpContext.Current.Request.UserAgent + Environment.NewLine +
                    "Explorador: " + HttpContext.Current.Request.Browser.Id + " " + HttpContext.Current.Request.Browser.Version + Environment.NewLine +
                    "Equipo: " + Environment.MachineName + Environment.NewLine +
                    "Usuario de Equipo: " + Environment.UserName + Environment.NewLine +
                    "Información de la Excepción" + Environment.NewLine;
                foreach (DictionaryEntry DataEx in exception.Data)
                {
                    Mensaje += DataEx.Key.ToString() + " = " + DataEx.Value.ToString() + Environment.NewLine;
                }
                Mensaje += "Codigo de Error: " + CodigoDeError + Environment.NewLine +
                    "Stack: " + exception.StackTrace + Environment.NewLine +
                    "Hast Code: " + exception.GetHashCode().ToString() + Environment.NewLine +
                    "Target: " + exception.TargetSite.ToString() + Environment.NewLine +
                    "Source: " + exception.Source + Environment.NewLine +
                    "Versión Sistema Operativo: " + Environment.OSVersion.VersionString + Environment.NewLine +
                    "Procesadores: " + Environment.ProcessorCount.ToString() + Environment.NewLine +
                    "IP Address: " + HttpContext.Current.Request.ServerVariables.Get("REMOTE_ADDR");
                return Mensaje;
            }
        }
        #endregion
        #region CargaCombos
        public class ClsCargaCombo
        {
            public static List<SelectListItem> CargaComboClave(byte NumeroDeClave, String ValorPorDefecto)
            {
                byte ValorPorDefectoN = Convert<byte>(ValorPorDefecto);
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    return (from q in db.Claves where q.CVE_NUM_CLAVE == NumeroDeClave select new SelectListItem { Text = q.CVE_NUM_SEC_CLAVE.ToString() + " / " + q.CVE_NOM_CLAVE, Value = q.CVE_NUM_SEC_CLAVE.ToString(), Selected = q.CVE_NUM_SEC_CLAVE == ValorPorDefectoN }).ToList();
                }
            }
            public static String CargaComboClaveParaTabla(byte NumeroDeClave,String idDeCombo)
            {
                String SelectText = String.Format("<select  id='{0}'>", idDeCombo);
                using(DBKuupEntities db = new DBKuupEntities())
                {
                    SelectText += "<option value=''>--SELECCIONE--</option>";
                    foreach (Claves Clave in (from q in db.Claves where q.CVE_NUM_CLAVE == NumeroDeClave select q).ToList())
                    {
                        SelectText += String.Format("<option value='{1}'>{0} / {1}</option>", Clave.CVE_NUM_SEC_CLAVE, Clave.CVE_NOM_CLAVE);
                    }
                }
                SelectText += "</select>";
                return SelectText;
            }
            public static List<SelectListItem> CargaComboProveedor(Nullable<byte> NumeroDeProveedor)
            {
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    return (from q in db.Proveedor where q.PRV_NUM_PROVEEDOR == NumeroDeProveedor select new SelectListItem { Text = q.PRV_NUM_PROVEEDOR.ToString() + " / " + q.PRV_NOM_PROVEEDOR, Value = q.PRV_NUM_PROVEEDOR.ToString(), Selected = q.PRV_NUM_PROVEEDOR == NumeroDeProveedor }).ToList();
                }
            }
            public static List<Object> AutoCompleteProducto(String Prefix)
            {
                var Productos = new List<Object>();
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    Parametro Parametro = (from q in db.Parametro where q.PAR_NOM_PARAMETRO == "ActivaLike" select q).FirstOrDefault();
                    if (Parametro != null)
                    {
                        if (Parametro.PAR_VALOR_PARAMETRO == "SI")
                        {
                            Productos = (from q in db.Producto where q.PRO_CODIGO_BARRAS.ToUpper().Contains(Prefix.ToUpper()) && q.PRO_CVE_ESTATUS == 1 select new { NombreDeProducto = q.PRO_NOM_PRODUCTO, CodigoDeBarras = q.PRO_CODIGO_BARRAS }).ToList<Object>();
                            if(Productos.Count() == 0)
                            {
                                Productos = (from q in db.Producto where q.PRO_NOM_PRODUCTO.ToUpper().Contains(Prefix.ToUpper()) && q.PRO_CVE_ESTATUS == 1 select new { NombreDeProducto = q.PRO_NOM_PRODUCTO, CodigoDeBarras = q.PRO_CODIGO_BARRAS }).ToList<Object>();
                            }
                        }
                        else
                        {
                            Productos = (from q in db.Producto where q.PRO_CODIGO_BARRAS.ToUpper().StartsWith(Prefix.ToUpper()) && q.PRO_CVE_ESTATUS == 1 select new { NombreDeProducto = q.PRO_NOM_PRODUCTO, CodigoDeBarras = q.PRO_CODIGO_BARRAS }).ToList<Object>();
                            if(Productos.Count() == 0)
                            {
                                Productos = (from q in db.Producto where q.PRO_NOM_PRODUCTO.ToUpper().StartsWith(Prefix.ToUpper()) && q.PRO_CVE_ESTATUS == 1 select new { NombreDeProducto = q.PRO_NOM_PRODUCTO, CodigoDeBarras = q.PRO_CODIGO_BARRAS }).ToList<Object>();
                            }
                        }
                    }
                    else
                    {
                        Productos = (from q in db.Producto where q.PRO_CODIGO_BARRAS.ToUpper().StartsWith(Prefix.ToUpper()) && q.PRO_CVE_ESTATUS == 1 select new { NombreDeProducto = q.PRO_NOM_PRODUCTO, CodigoDeBarras = q.PRO_CODIGO_BARRAS }).ToList<Object>();
                        if (Productos.Count() == 0)
                        {
                            Productos = (from q in db.Producto where q.PRO_NOM_PRODUCTO.ToUpper().StartsWith(Prefix.ToUpper()) && q.PRO_CVE_ESTATUS == 1 select new { NombreDeProducto = q.PRO_NOM_PRODUCTO, CodigoDeBarras = q.PRO_CODIGO_BARRAS }).ToList<Object>();
                        }
                    }
                }
                return Productos;
            }
        }
        #endregion
    }
}
