(function (windows, document) {
    let VentaTotal = (function () {
        let _Nucleo = function () {
            let Elementos_VentaTotal = {
                CodigoONombreDeProducto: 'fCodigoONombreDeProducto',
                CantidadDeProducto: 'fCantidadDeProducto',
                AgregaProducto: 'AgregaProducto',
                formularioProducto: 'formProducto',
                Tabla: 'Tabla'
            };
            let Funcionalidad = '',
                UrlCargaProducto = '',
                UrlAutoCompleteProducto = '',
                TablaVentas = null;
            let _Funcionalidad = function (FuncionalidadSet) {
                if (typeof (FuncionalidadSet) != 'undefined') {
                    Funcionalidad = FuncionalidadSet;
                } else {
                    return Funcionalidad;
                }
            };
            let _UrlCargaProducto = function (UrlCargaProductoSet) {
                if (typeof (UrlCargaProductoSet) != 'undefined') {
                    UrlCargaProducto = UrlCargaProductoSet;
                } else {
                    return UrlCargaProducto;
                }
            };
            let _UrlAutoCompleteProducto = function (UrlAutoCompleteProductoSet) {
                if (typeof (UrlAutoCompleteProductoSet) != 'undefined') {
                    UrlAutoCompleteProducto = UrlAutoCompleteProductoSet;
                } else {
                    return UrlAutoCompleteProducto;
                }
            };
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                if (Funcionalidad == 'INDEX') {
                    GeneraGridDeProducto();
                }
            };
            function GeneraGridDeProducto() {
                TablaVentas = $('#' + Elementos_VentaTotal.Tabla).DataTable({
                    "destroy": true,
                    "responsive": true,
                    "scrollY": 500,
                    "columns": [
                        { "data": "NumeroDeProducto" },
                        { "data": "CodigoDeBarras" },
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadDeProducto" },
                        { "data": "PrecioUnitario" },
                        { "data": "PrecioTotal" },
                        { "defaultContent": "<button type='button' class='detalle btn btn-danger'><i class='far fa-trash-alt'></i></button>" }
                    ],
                    "language": {
                        "url": "/Content/Librerias/DataTables/dataTablesEspañol.json"
                    }
                });
                RemoveProducto('#' + Elementos_VentaTotal.Tabla + ' tbody', TablaVentas);
            }
            var RemoveProducto = function (tbody, table) {
                $(tbody).on('click', 'button.detalle', function () {
                    table.row($(this).parents('tr')).remove().draw();
                    table.row($(this).parents('li')).remove().draw();
                });
            }
            function OcultaCampos() {
                switch (Funcionalidad) {
                    case 'INDEX':
                        break;
                }
            }
            function AgregaEvento() {
                switch (Funcionalidad) {
                    case 'INDEX':
                        $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: UrlAutoCompleteProducto,
                                    type: "POST",
                                    dataType: "json",
                                    data: { Prefix: request.term },
                                    success: function (data) {
                                        response($.map(data, function (item) {
                                            return { label: item.NombreDeProducto, value: item.NombreDeProducto };
                                        }))

                                    }
                                })
                            },
                            messages: {
                                noResults: "", results: ""
                            } 
                        });
                        $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).keypress(function (event) {
                            let keycode = (event.keyCode ? event.keyCode : event.which);
                            if (keycode == 13) {
                                $.ajax({
                                    type: "POST",
                                    url: UrlCargaProducto,
                                    async: false,
                                    data: { NombreOCodigoDeProducto: $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).val() },
                                    success: function (data) {
                                        if (data.Resultado.Resultado) {
                                            $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).blur();
                                            CreaDialog(Elementos_VentaTotal.AgregaProducto, 'Agrega Producto', CreaBotonesDialog(data.Producto), 500, 200);
                                            $('#' + Elementos_VentaTotal.AgregaProducto).dialog('open');
                                            AgregaEventoCantidad(data.Producto);
                                        } else {
                                            alertify.error(data.Resultado.Mensaje);
                                        }
                                    },
                                    error: function () {
                                        alertify.error("Ocurrio un error al realizar la accion de carga de archivo");
                                    }
                                });
                                event.stopPropagation();
                                event.preventDefault();
                            }
                        });
                        break;
                }
            }
            function AgregaEventoCantidad(Producto) {
                $('#' + Elementos_VentaTotal.CantidadDeProducto).keypress(function (event) {
                    let keycode2 = (event.keyCode ? event.keyCode : event.which);
                    if (keycode2 == 13) {
                        AgregaProducto(Producto);
                        event.stopPropagation();
                        event.preventDefault();
                    }
                });
            }
            function CreaBotonesDialog(Producto) {
                return {
                    'Cancelar': {
                        id: 'Cancelar',
                        text: 'Cancelar',
                        class: 'btn btn-danger',
                        click: function () {
                            $('#' + Elementos_VentaTotal.AgregaProducto).dialog('close');
                        }
                    },
                    'Agregar': {
                        id: 'Agregar',
                        text: 'Agregar',
                        class: 'btn btn-primary',
                        click: function () {
                            AgregaProducto(Producto);
                        }
                    }
                }
            }
            function AgregaProducto(Producto) {
                debugger
                let TablaRows = TablaVentas.rows().data();
                let Inserta = true;
                for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                    let ProductoTabla = TablaVentas.rows(i).data()[0];
                    if (ProductoTabla.NumeroDeProducto == Producto.NumeroDeProducto && ProductoTabla.CodigoDeBarras == Producto.CodigoDeBarras) {
                        Inserta = false;
                    }
                }
                if (Inserta) {
                    TablaVentas.row.add({
                        "NumeroDeProducto": Producto.NumeroDeProducto,
                        "CodigoDeBarras": Producto.CodigoDeBarras,
                        "NombreDeProducto": Producto.NombreDeProducto,
                        "CantidadDeProducto": $('#' + Elementos_VentaTotal.CantidadDeProducto).val(),
                        "PrecioUnitario": Producto.PrecioUnitario,
                        "PrecioTotal": parseFloat(Math.round((Producto.PrecioUnitario * $('#' + Elementos_VentaTotal.CantidadDeProducto).val()) * 100) / 100).toFixed(2)
                    }).draw();
                    $('#' + Elementos_VentaTotal.AgregaProducto).dialog('close');
                }
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlAutoCompleteProducto: _UrlAutoCompleteProducto,
                    UrlCargaProducto: _UrlCargaProducto
                },
                Inicio: _Inicio
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlAutoCompleteProducto(ObjetoConfiguracion.UrlAutoCompleteProducto);
            NucleoConfigurado.Configuracion.UrlCargaProducto(ObjetoConfiguracion.UrlCargaProducto);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = VentaTotal;
})(window, document);