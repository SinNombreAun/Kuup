using Mod.Entity;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;

namespace Negocio.Kuup.Clases
{
    class ClsProducto : Interfaces.InterfazGen<ClsProducto>
    {
        ViProducto Producto = new ViProducto();
        public short NumeroDeProducto
        {
            get { return Producto.PRO_NUM_PRODUCTO; }
            set { Producto.PRO_NUM_PRODUCTO = value; }
        }
        public String CodigoDeBarras
        {
            get { return Producto.PRO_CODIGO_BARRAS; }
            set { Producto.PRO_CODIGO_BARRAS = value; }
        }
        public DateTime FechaDeRegistro
        {
            get { return Producto.PRO_FECHA_REGISTRO; }
            set { Producto.PRO_FECHA_REGISTRO = value; }
        }
        public short CantidadDeProductoUltima
        {
            get { return Producto.PRO_CANT_PRODUCTO_ULTIMA; }
            set { Producto.PRO_CANT_PRODUCTO_ULTIMA = value; }
        }
        public short CantidadDeProductoNueva
        {
            get { return Producto.PRO_CANT_PRODUCTO_NUEVA; }
            set { Producto.PRO_CANT_PRODUCTO_NUEVA = value; }
        }
        public short CantidadDeProductoTotal
        {
            get { return Producto.PRO_CANT_PRODUCTO_TOTAL; }
            set { Producto.PRO_CANT_PRODUCTO_TOTAL = value; }
        }

    }
}
