using Funciones.Kuup.Adicionales;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Funciones.Kuup.Tecket
{
    public class ClsTicket
    {
        public static System.Drawing.Font printFont;
        public static StreamReader streamToPrint;
        public static StringBuilder line = new StringBuilder();
        public static int max = 40, maxCaracteres = 44;
        public byte[] Serverbyte { get; private set; }
        int cort;
        String ticket = String.Empty, parte1, parte2;
        public String RutaDeTicket { get; set; }
        public String NombreDeTicket { get; set; }
        public static String LineasCaracter(String Caracter)
        {
            return line.AppendLine(String.Empty.PadLeft(maxCaracteres, Convert.ToChar(Caracter))).ToString();
        }
        public static void EncabezadoVenta()
        {
            String LineEncavesado = "Articulo            Cant  P.Unit       Valor";
            line.AppendLine(LineEncavesado);
        }
        public void TextoIzquierda(string par1)
        {
            max = par1.Length;
            if (max > 40)
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);
            }
            else
            {
                parte1 = par1;
            }
            line.AppendLine(ticket = parte1);
        }
        public void TextoDerecha(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 40)
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);
            }
            else
            {
                parte1 = par1;
            }
            max = 40 - par1.Length;
            for (int i = 0; i < max; i++)
            {
                ticket += " ";
            }
            line.AppendLine(ticket += parte1 + "\n");
        }
        public void TextoCentro(string par1)
        {
            ticket = "";
            max = par1.Length;
            if (max > 40)
            {
                cort = max - 40;
                parte1 = par1.Remove(40, cort);
            }
            else
            {
                parte1 = par1;
            }
            max = (int)(40 - parte1.Length) / 2;
            for (int i = 0; i < max; i++)
            {
                ticket += " ";
            }
            line.AppendLine(ticket += parte1 + "\n");
        }
        public void TextoExtremos(string par1, string par2)
        {
            max = par1.Length;
            if (max > 18)
            {
                cort = max - 18;
                parte1 = par1.Remove(18, cort);
            }
            else
            {
                parte1 = par1;
            }
            ticket = parte1;
            max = par2.Length;
            if (max > 18)
            {
                cort = max - 18;
                parte2 = par2.Remove(18, cort);
            }
            else
            {
                parte2 = par2;
            }
            max = 40 - (parte1.Length + parte2.Length);
            for (int i = 0; i < max; i++)
            {
                ticket += " ";
            }
            line.AppendLine(ticket += parte2 + "\n");
        }
        public void AgregaTotales(string par1, decimal total)
        {
            max = par1.Length;
            if (max > 25)
            {
                cort = max - 25;
                parte1 = par1.Remove(25, cort);
            }
            else
            {
                parte1 = par1;
            }
            ticket = par1;
            parte2 = "$" + total.ToString();
            max = maxCaracteres - (parte1.Length + parte2.Length);
            ticket += String.Empty.PadRight(max, ' ');
            line.AppendLine(ticket += parte2 + "\n");
        }
        public void AgregaArticulo(String Articulo, decimal precio, int cant, decimal subtotal)
        {
            if (cant.ToString().Length <= 3 && precio.ToString("c").Length <= 10 && subtotal.ToString("c").Length <= 11)
            {
                String elementos = "";
                bool bandera = false;

                if (Articulo.Length > 44)
                {
                    elementos += cant.ToString().PadLeft(3,' ');

                    elementos += precio.ToString().PadLeft(10, ' ');

                    elementos += subtotal.ToString().PadLeft(11, ' ');

                    int CaracterActual = 0;
                    for (int Longtext = Articulo.Length; Longtext > 20; Longtext++)
                    {
                        if (bandera == false)
                        {
                            line.AppendLine((Articulo.Substring(CaracterActual, 20) + elementos));
                            bandera = true;
                        }
                        else
                        {
                            if(CaracterActual + 20 < Articulo.Length)
                            {
                                line.AppendLine(Articulo.Substring(CaracterActual, 20));
                            }
                            else
                            {
                                if (Articulo.Length - CaracterActual > 0)
                                {
                                    line.AppendLine(Articulo.Substring(CaracterActual, Articulo.Length - CaracterActual).PadRight(20, ' '));
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        CaracterActual += 20;
                    }
                    //line.AppendLine(Articulo.Substring(CaracterActual, Articulo.Length - CaracterActual));
                }
                else
                {
                    elementos = Articulo.PadRight(20, ' ');
                    elementos +=  cant.ToString().PadLeft(3,' ');

                    elementos += precio.ToString().PadLeft(10, ' ');

                    elementos +=  subtotal.ToString().PadLeft(11, ' ');
                    line.AppendLine(elementos);
                }
            }
            else
            {
                //  MessageBox.Show("Valores fuera de rango");
            }
        }
        public void ImprimirTiket(String Impresora,ref ClsAdicional.ClsResultado Resultado)
        {
            if (ClsAdicional.ClsArchivos.FileExists(RutaDeTicket, true, true))
            {
                if (ClsAdicional.ClsArchivos.FileExists(RutaDeTicket + NombreDeTicket, false, true))
                {
                    ClsAdicional.ClsArchivos.EscribeArchivo(RutaDeTicket + NombreDeTicket, line.ToString());
                    line = new StringBuilder();
                    try
                    {
                        using (streamToPrint = new StreamReader(RutaDeTicket + NombreDeTicket, Encoding.GetEncoding(1252), false))
                        {
                            printFont = new Font("Arial", 7);
                            PrintDocument printDocument = new PrintDocument();
                            printDocument.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                            printDocument.PrinterSettings.PrinterName = Impresora;
                            printDocument.DocumentName = NombreDeTicket;
                            printDocument.Print();
                        }
                    }
                    catch (Exception e)
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = String.Format("Excepción de tipo: {0} Mensaje: {1} Código de Error: {2}", e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString());
                    }
                }
            }
        }
        public void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            int count = 0;
            float leftMargin = 0;
            float topMargin = 0;
            String line = null;

            float linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);

            while (count < linesPerPage && ((line = streamToPrint.ReadLine()) != null))
            {
                float yPos = topMargin + (count * printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }

            if (line != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
        public String RegresaTextoTicket()
        {
            String ticket = line.ToString();
            line = new StringBuilder();
            return ticket;
        }
    }
}
