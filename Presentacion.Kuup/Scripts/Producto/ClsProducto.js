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
                GeneraCodigoDeBarras: 'fGeneraCodigoDeBarras',
                Tabla: 'Tabla',
                CargaMasiva: 'CargaMasiva'
            };
            let Funcionalidad = '',
                UrlCargaGrid = '',
                UrlDetalle = '',
                UrlCargaMasiva = '',
                UrlDescargaDeArchivo = '';
            let _Funcionalidad = function (FuncionalidadSet) {
                if (typeof (FuncionalidadSet) != 'undefined') {
                    Funcionalidad = FuncionalidadSet;
                } else {
                    return Funcionalidad;
                }
            };
            let _UrlCargaGrid = function (UrlCargaGridSet) {
                if (typeof (UrlCargaGridSet) != 'undefined') {
                    UrlCargaGrid = UrlCargaGridSet;
                } else {
                    return UrlCargaGrid;
                }
            };
            let _UrlDetalle = function (UrlDetalleSet) {
                if (typeof (UrlDetalleSet) != 'undefined') {
                    UrlDetalle = UrlDetalleSet;
                } else {
                    return UrlDetalle;
                }
            };
            let _UrlCargaMasiva = function (UrlCargaMasivaSet) {
                if (typeof (UrlCargaMasivaSet) != 'undefined') {
                    UrlCargaMasiva = UrlCargaMasivaSet;
                } else {
                    return UrlCargaMasiva;
                }
            };
            let _UrlDescargaDeArchivo = function (UrlDescargaDeArchivoSet) {
                if (typeof (UrlDescargaDeArchivoSet) != 'undefined') {
                    UrlDescargaDeArchivo = UrlDescargaDeArchivoSet;
                } else {
                    return UrlDescargaDeArchivo;
                }
            }
            let TablaImportar = null;
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
                if (Funcionalidad == 'INDEX') {
                    GeneraGridDeProducto();
                }
            };
            function GeneraGridDeProducto() {
                var table = $('#' + Elementos_Producto.Tabla).DataTable({
                    "ajax": {
                        "method": "GET",
                        "url": UrlCargaGrid + "?Grid=true"
                    },
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 500,
                    "scrollX": true,
                    "select": true,
                    "scrollCollapse": false,
                    "fixedColumns": {
                        "leftColumns": 2,
                        "rightColumns": 1
                    },
                    "columnDefs": [ { "targets": [0], "visible": false, "searchable": false } ],
                    "columns": [
                        { "data": "NumeroDeProducto" },
                        { "data": "CodigoDeBarras" },
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadDeProductoTotal" },
                        { "data": "PrecioUnitario" },
                        { "data": "TextoAviso" },
                        { "data": "TextoCorreoSurtido" },
                        { "data": "TextoAplicaMayoreo" },
                        { "data": "CantidadMinimaMayoreo" },
                        { "data": "PrecioMayoreo" },
                        { "data": "TextoEstatus" },
                        { "defaultContent": "<button type='button' class='detalle btn btn-info'>Detalle</button>" }
                    ],
                    "language": LenguajeEN()
                });
                DetalleRegistro('#' + Elementos_Producto.Tabla + ' tbody', table);
            }
            var DetalleRegistro = function (tbody, table) {
                $(tbody).on('click', 'button.detalle', function () {
                    var data = table.row($(this).parents('tr')).data();
                    if (typeof (data) == 'undefined') {
                        data = table.row($(this).parents('li')).data();
                    }
                    window.location.href = UrlDetalle + '?NumeroDeProducto=' + data.NumeroDeProducto;
                });
            }
            _CreaTabla = function (data) {
                $('#' + Elementos_Producto.Tabla).show();
                TablaImportar = $('#' + Elementos_Producto.Tabla).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "data": data.data,
                    "scrollX": true,
                    "scrollCollapse": true,
                    "fixedColumns": {
                        "leftColumns": 1,
                        "rightColumns": 1
                    },
                    "columnDefs": [{ "targets": [5], "visible": false, "searchable": false }, { "targets": [7], "visible": false, "searchable": false }, { "targets": [10], "visible": false, "searchable": false }],
                    "columns": [
                        { "data": "CodigoDeBarras" },
                        { "data": "NombreDeProducto" },
                        { "data": "Descripcion" },
                        { "data": "CantidadDeProductoTotal" },
                        { "data": "PrecioUnitario" },
                        { "data": "CveAviso" },
                        { "data": "TextoAviso" },
                        { "data": "CveCorreoSurtido" },
                        { "data": "TextoCorreoSurtido" },
                        { "data": "CantidadMinima" },
                        { "data": "CveAplicaMayoreo" },
                        { "data": "TextoAplicaMayoreo" },
                        { "data": "CantidadMinimaMayoreo" },
                        { "data": "PrecioMayoreo" },
                        { "data": "Observaciones" }
                    ],
                    "language": LenguajeEN()
                });
            }
            _LimpiaTabla = function () {
                if (TablaImportar != null) {
                    TablaImportar.clear().draw();
                    $('#' + Elementos_Producto.Tabla).parent().parent().parent().parent().parent().hide();
                }
            }
            _CargaMasivaDeRegistros = function () {
                let TablaRows = TablaImportar.rows().data();
                let JsonObj = [];
                for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                    JsonObj.push(TablaImportar.rows(i).data()[0]);
                }
                let JsonStrin = JSON.stringify(JsonObj);
                $.ajax({
                    type: "POST",
                    url: UrlCargaMasiva,
                    async: false,
                    data: { strinjson: JsonStrin },
                    success: function (data) {
                        if (data.Resultado.Resultado) {
                            TablaImportar.clear().draw();
                            $('#' + Elementos_Producto.Tabla).parent().parent().parent().parent().parent().hide();;
                            alertify.success("Datos cargados correctamente");
                        } else {
                            alertify.error(data.Resultado.Mensaje);
                            let url = UrlDescargaDeArchivo + "?";
                            url += "NombreDeArchivo=" + data.NombreDeArchivo + "&Extencion=" + data.Extencion
                            window.open(url, "_blank");
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la accion de carga de archivo");
                    }
                });
            }
            function OcultaCampos() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        $('#' + Elementos_Producto.CveCorreoSurtido).parent().hide();
                        $('#' + Elementos_Producto.CantidadMinima).parent().hide();
                        $('#' + Elementos_Producto.NumeroDeProveedor).parent().hide();

                        $('#' + Elementos_Producto.CantidadMinimaMayoreo).parent().hide();
                        $('#' + Elementos_Producto.PrecioMayoreo).parent().hide();
                        break;
                    case 'IMPORTAR':
                        $('#' + Elementos_Producto.CargaMasiva).hide();
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
                    alertify.confirm("Codigo de Barras", "Este producto no cuenta con Codigo de Barras, ¿Desas que el sistema genere un Codigo de Barras para este producto?",
                        function () {
                            $('#' + Elementos_Producto.GeneraCodigoDeBarras).val(2);
                            $('form').submit();
                        },
                        function () {
                            $('#' + Elementos_Producto.GeneraCodigoDeBarras).val(1);
                            $('form').submit();
                            alertify.warning("Proceso de Alta sin generación de Código de Barras");
                        }
                    ).set('labels', { ok: 'Si, Generar Codigo', cancel: 'No, Generar Codigo' });
                } else {
                    $('#' + Elementos_Producto.GeneraCodigoDeBarras).val(1);
                    $('form').submit();
                }
            });
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlDetalle: _UrlDetalle,
                    UrlCargaMasiva: _UrlCargaMasiva,
                    UrlDescargaDeArchivo: _UrlDescargaDeArchivo
                },
                Inicio: _Inicio,
                CreaTabla: _CreaTabla,
                CargaMasivaDeRegistros: _CargaMasivaDeRegistros,
                LimpiaTabla: _LimpiaTabla
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlDetalle(ObjetoConfiguracion.UrlDetalle);
            NucleoConfigurado.Configuracion.UrlCargaMasiva(ObjetoConfiguracion.UrlCargaMasiva);
            NucleoConfigurado.Configuracion.UrlDescargaDeArchivo(ObjetoConfiguracion.UrlDescargaDeArchivo);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Producto;
})(window, document);