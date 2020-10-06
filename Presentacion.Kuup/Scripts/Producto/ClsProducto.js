(function (windows, document) {
    let Producto = (function () {
        let _Nucleo = function () {
            let Elementos_Producto = {
                form: 'formProducto',
                NumeroDeProducto: 'fNumeroDeProducto',
                CodigoDeBarras: 'fCodigoDeBarras',
                FechaDeRegistro: 'fFechaDeRegistro',
                CantidadDeProductoUltima: 'fCantidadDeProductoUltima',
                CantidadDeProductoNueva: 'fCantidadDeProductoNueva',
                CantidadDeProductoTotal: 'fCantidadDeProductoTotal',
                NombreDeProducto: 'fNombreDeProducto',
                Descripcion: 'fDescripcion',
                CveAviso: 'fCveAviso',
                CveCorreoSurtido: 'fCveCorreoSurtido',
                CantidadMinima: 'fCantidadMinima',
                NumeroDeProveedor: 'fNumeroDeProveedor',
                PrecioUnitario: 'fPrecioUnitario',
                CveAplicaMayoreo: 'fCveAplicaMayoreo',
                CantidadMinimaMayoreo: 'fCantidadMinimaMayoreo',
                PrecioMayoreo: 'fPrecioMayoreo',
                CveEstatus: 'fCveEstatus',
                Guardar: 'Guardar',
                GeneraCodigoDeBarras: 'fGeneraCodigoDeBarras'
            };
            let Funcionalidad = '',
                URLGeneraCodigoDeBarras = '';
            let _Funcionalidad = function (FuncionalidadSet) {
                if (typeof (FuncionalidadSet) != 'undefined') {
                    Funcionalidad = FuncionalidadSet;
                } else {
                    return Funcionalidad;
                }
            };
            let _URLGeneraCodigoDeBarras = function (URLGeneraCodigoDeBarrasSet) {
                if (typeof (URLGeneraCodigoDeBarrasSet) != 'undefined') {
                    URLGeneraCodigoDeBarras = URLGeneraCodigoDeBarrasSet;
                } else {
                    return URLGeneraCodigoDeBarras;
                }
            };
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
            };
            function OcultaCampos() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        $('#' + Elementos_Producto.CveCorreoSurtido).parent().hide();
                        $('#' + Elementos_Producto.CantidadMinima).parent().hide();
                        $('#' + Elementos_Producto.NumeroDeProveedor).parent().hide();

                        $('#' + Elementos_Producto.CantidadMinimaMayoreo).parent().hide();
                        $('#' + Elementos_Producto.PrecioMayoreo).parent().hide();
                        break;
                }
            }
            function EjecutaEventosIniciales() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        $('#' + Elementos_Producto.CveAviso).change();
                        $('#' + Elementos_Producto.CveAplicaMayoreo).change();
                        break;
                    case 'DETALLE':
                        $('#' + Elementos_Producto.CveAviso).change();
                        $('#' + Elementos_Producto.CveAplicaMayoreo).change();
                        break;
                }
            }
            function AgregaEvento() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        $('#' + Elementos_Producto.CveAviso).change(function () {
                            if (this.value == 1) {
                                $('#' + Elementos_Producto.CveCorreoSurtido).parent().show();
                                $('#' + Elementos_Producto.CantidadMinima).parent().show();
                            } else {
                                $('#' + Elementos_Producto.CveCorreoSurtido).parent().hide();
                                $('#' + Elementos_Producto.CveCorreoSurtido).val('');
                                $('#' + Elementos_Producto.CantidadMinima).parent().hide();
                                $('#' + Elementos_Producto.CantidadMinima).val('');
                            }
                        });
                        $('#' + Elementos_Producto.CveAplicaMayoreo).change(function () {
                            if (this.value == 1) {
                                $('#' + Elementos_Producto.CantidadMinimaMayoreo).parent().show();
                                $('#' + Elementos_Producto.PrecioMayoreo).parent().show();
                            } else {
                                $('#' + Elementos_Producto.CantidadMinimaMayoreo).parent().hide();
                                $('#' + Elementos_Producto.CantidadMinimaMayoreo).val('');
                                $('#' + Elementos_Producto.PrecioMayoreo).parent().hide();
                                $('#' + Elementos_Producto.PrecioMayoreo).val('');
                            }
                        });
                        break;
                    case 'DETALLE':
                        $('#' + Elementos_Producto.CveAviso).change(function () {
                            if (this.value == 1) {
                                $('#' + Elementos_Producto.CveCorreoSurtido).parent().show();
                                $('#' + Elementos_Producto.CantidadMinima).parent().show();
                            } else {
                                $('#' + Elementos_Producto.CveCorreoSurtido).parent().hide();
                                $('#' + Elementos_Producto.CveCorreoSurtido).val('');
                                $('#' + Elementos_Producto.CantidadMinima).parent().hide();
                                $('#' + Elementos_Producto.CantidadMinima).val('');
                            }
                        });
                        $('#' + Elementos_Producto.CveAplicaMayoreo).change(function () {
                            if (this.value == 1) {
                                $('#' + Elementos_Producto.CantidadMinimaMayoreo).parent().show();
                                $('#' + Elementos_Producto.PrecioMayoreo).parent().show();
                            } else {
                                $('#' + Elementos_Producto.CantidadMinimaMayoreo).parent().hide();
                                $('#' + Elementos_Producto.CantidadMinimaMayoreo).val('');
                                $('#' + Elementos_Producto.PrecioMayoreo).parent().hide();
                                $('#' + Elementos_Producto.PrecioMayoreo).val('');
                            }
                        });
                        break;
                }
            }
            $('#' + Elementos_Producto.Guardar).click(function () {
                if ($('#' + Elementos_Producto.CodigoDeBarras).val() == '' && $('#' + Elementos_Producto.NombreDeProducto).val()) {
                    alertify.confirm("Este producto no cuenta con Codigo de Barras, ¿Desas que el sistema genere un Codigo de Barras para este producto?",
                        function () {
                            $('#' + Elementos_Producto.GeneraCodigoDeBarras).val(2);
                        },
                        function () {
                            alertify.log("Proceso de Alta Cancelado");
                        }
                    );
                } else {
                    $('#' + Elementos_Producto.GeneraCodigoDeBarras).val(1);
                    $('form').submit();
                }
            });
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    URLGeneraCodigoDeBarras: _URLGeneraCodigoDeBarras
                },
                Inicio: _Inicio
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.URLGeneraCodigoDeBarras(ObjetoConfiguracion.URLGeneraCodigoDeBarras);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Producto;
})(window, document);