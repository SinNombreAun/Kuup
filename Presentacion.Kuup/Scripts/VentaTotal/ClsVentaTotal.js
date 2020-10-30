(function (windows, document) {
    let VentaTotal = (function () {
        let _Nucleo = function () {
            let Elementos_VentaTotal = {
                CodigoONombreDeProducto: 'fCodigoONombreDeProducto',
                CantidadDeProducto: 'fCantidadDeProducto',
                Paquetes: 'fPaquetes',
                ImporteTotal: 'fImporteTotal',
                AgregaProducto: 'AgregaProducto',
                formularioProducto: 'formProducto',
                VentaTotal: 'VentaTotal',
                RegistraVenta: 'RegistraVenta',
                ImporteRecibido: 'fImporteRecibido',
                ImporteTotalVent: 'fImporteTotalVent',
                ImporteCambio: 'fImporteCambio',
                Tabla: 'Tabla'
            };
            let Funcionalidad = '',
                UrlCargaProducto = '',
                UrlAutoCompleteProducto = '',
                UrlProductoATabla = '',
                UrlDeleteProducto = '',
                UrlRegistraVenta = '';
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
            let _UrlProductoATabla = function (UrlProductoATablaSet) {
                if (typeof (UrlProductoATablaSet) != 'undefined') {
                    UrlProductoATabla = UrlProductoATablaSet;
                } else {
                    return UrlProductoATabla;
                }
            };
            let _UrlDeleteProducto = function (UrlDeleteProductoSet) {
                if (typeof (UrlDeleteProductoSet) != 'undefined') {
                    UrlDeleteProducto = UrlDeleteProductoSet;
                } else {
                    return UrlDeleteProducto;
                }
            }
            let _UrlRegistraVenta = function (UrlRegistraVentaSet) {
                if (typeof (UrlRegistraVentaSet) != 'undefined') {
                    UrlRegistraVenta = UrlRegistraVentaSet;
                } else {
                    return UrlRegistraVenta;
                }
            }
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                if (Funcionalidad == 'INDEX') {
                    $('#' + Elementos_VentaTotal.ImporteTotal).val(0);
                    GeneraGridDeProducto();
                    $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                }
            };
            function GeneraGridDeProducto() {
                TablaVentas = $('#' + Elementos_VentaTotal.Tabla).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 500,
                    "scrollX": true,
                    "select": true,
                    "fixedColumns": {
                        "leftColumns": 2,
                        "rightColumns": 2
                    },
                    "columnDefs": [{ "targets": [0], "visible": false, "searchable": false }],
                    "columns": [
                        { "data": "NumeroDeProducto" },
                        { "data": "CodigoDeBarras" },
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadDeProducto" },
                        { "data": "PrecioUnitario" },
                        { "data": "ImporteDeProducto" },
                        { "defaultContent": "<button type='button' class='btndelete btn btn-danger'><i class='far fa-trash-alt'></i></button>" }
                    ],
                    "paging": false,
                    "searching": false,
                    "language": LenguajeEN()
                });
                RemoveProducto('#' + Elementos_VentaTotal.Tabla + ' tbody', TablaVentas);
            }
            var RemoveProducto = function (tbody, table) {
                $(tbody).on('click', 'button.btndelete', function () {
                    let data = table.row($(this).parents('tr')).data();
                    if (typeof (data) == 'undefined') {
                        data = table.row($(this).parents('li')).data();
                    }
                    let TablaRows = TablaVentas.rows().data();
                    let JsonObj = [];
                    for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                        JsonObj.push(TablaVentas.rows(i).data()[0]);
                    }
                    let JsonString = '';
                    if (JsonObj.length != 0) {
                        JsonString = JSON.stringify(JsonObj);
                    }
                    let JsonData = '';
                    JsonData = JSON.stringify(data);
                    $.ajax({
                        type: "POST",
                        url: UrlDeleteProducto,
                        async: false,
                        data: { RegistroPrevio: JsonData, RegistrosPrevios: JsonString },
                        success: function (data) {
                            let ImporteTotal = 0;
                            TablaRows = TablaVentas.rows().data();
                            for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                                for (var ii = 0; ii < data.Registro.length; ii++) {
                                    if (data.Registro[ii].CodigoDeBarras == TablaVentas.rows(i).data()[0].CodigoDeBarras) {
                                        ImporteTotal = parseFloat(ImporteTotal) + parseFloat(TablaVentas.rows(i).data()[0].ImporteDeProducto);
                                        TablaRows.row(i).remove().draw();
                                    }
                                }
                            }
                            $('#' + Elementos_VentaTotal.ImporteTotal).val((parseFloat($('#' + Elementos_VentaTotal.ImporteTotal).val()) - parseFloat(ImporteTotal)).toFixed(2));
                            TablaVentas.columns.adjust().draw();
                        },
                        error: function () {
                            alertify.error("Ocurrio un error al realizar la eliminación del registro");
                        }
                    });;
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
                                        try {
                                            response($.map(data, function (item) {
                                                return { label: item.NombreDeProducto, value: item.NombreDeProducto };
                                            }));
                                        } catch (e) {
                                            //Excepcion controlada 
                                        }
                                    }
                                })
                            },
                            messages: {
                                noResults: "", results: ""
                            }
                        });
                        $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).keydown(function (event) {
                            let keycode = (event.keyCode ? event.keyCode : event.which);
                            if (keycode == 13) {
                                ConsultaProducto();
                            }
                        });
                        $('#' + Elementos_VentaTotal.VentaTotal).click(function () {
                            if ($('#' + Elementos_VentaTotal.ImporteTotal).val() != '') {
                                if (parseFloat($('#' + Elementos_VentaTotal.ImporteTotal).val()) != parseFloat('0')) {
                                    CreaDialog(Elementos_VentaTotal.RegistraVenta, 'Registra Venta', CreaBotonesDialog([], true), 400, 500);
                                    $('#' + Elementos_VentaTotal.RegistraVenta).dialog('open');
                                    $('#' + Elementos_VentaTotal.ImporteTotalVent).val($('#' + Elementos_VentaTotal.ImporteTotal).val());
                                    $('#' + Elementos_VentaTotal.ImporteRecibido).focus();
                                    AgregaEventoCambio();
                                    $('#' + Elementos_VentaTotal.AgregaProducto).on('dialogclose', function (event) {
                                        LimpiaCantidad();
                                        $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                                    });
                                } else {
                                    alertify.error('No se aregistrado ningun Producto o el Importe es');
                                }
                            } else {
                                alertify.error('No se aregistrado ningun Producto o el Importe es');
                            }
                        });
                        break;
                }
            }
            function ConsultaProducto() {
                $.ajax({
                    type: "POST",
                    url: UrlCargaProducto,
                    async: false,
                    data: { NombreOCodigoDeProducto: $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).val() },
                    success: function (data) {
                        if (data.Resultado.Resultado) {
                            let DimencionesX = 500, DimencionesY = 200;
                            if (data.TienePaquetes) {
                                $('#' + Elementos_VentaTotal.Paquetes + ' option:gt(0)').remove();
                                $.each(data.Paquetes, function (index, value) {
                                    $('#' + Elementos_VentaTotal.Paquetes).append($('<option></option>').attr('value', value.NumeroDeProductoPadre + '_' + value.NumeroDeProductoHijo).text(value.NombreDeProductoPadre + ' con ' + value.NombreDeProductoHijo));
                                });
                                DimencionesX = 700;
                                DimencionesY = 300;
                                $('#' + Elementos_VentaTotal.Paquetes).parent().parent().show();
                            } else {
                                $('#' + Elementos_VentaTotal.Paquetes).parent().parent().hide();
                            }
                            $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).val('');
                            $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).blur();
                            CreaDialog(Elementos_VentaTotal.AgregaProducto, 'Agrega Producto', CreaBotonesDialog(data.Producto, false), DimencionesX, DimencionesY);
                            $('#' + Elementos_VentaTotal.AgregaProducto).dialog('open');
                            $('#' + Elementos_VentaTotal.CantidadDeProducto).focus();
                            AgregaEventoCantidad(data.Producto);
                            $('#' + Elementos_VentaTotal.AgregaProducto).on('dialogclose', function (event) {
                                LimpiaCantidad();
                                $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                            });
                        } else {
                            alertify.error(data.Resultado.Mensaje);
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la validacion del Producto");
                    }
                });
            }
            function AgregaEventoCambio() {
                $('#' + Elementos_VentaTotal.ImporteRecibido).unbind('keydown');
                $('#' + Elementos_VentaTotal.ImporteRecibido).keydown(function (event) {
                    let keycode2 = (event.keyCode ? event.keyCode : event.which);
                    if (keycode2 == 13) {
                        if (!isNaN($('#' + Elementos_VentaTotal.ImporteRecibido).val())) {
                            if ($('#' + Elementos_VentaTotal.ImporteRecibido).val() != '') {
                                $('#' + Elementos_VentaTotal.ImporteCambio).val(parseFloat($('#' + Elementos_VentaTotal.ImporteRecibido).val()) - parseFloat($('#' + Elementos_VentaTotal.ImporteTotalVent).val()));
                            }
                        }
                    }
                });
            }
            function AgregaEventoCantidad(Producto) {
                $('#' + Elementos_VentaTotal.CantidadDeProducto).unbind('keydown');
                $('#' + Elementos_VentaTotal.CantidadDeProducto).keydown(function (event) {
                    let keycode2 = (event.keyCode ? event.keyCode : event.which);
                    if (keycode2 == 13) {
                        AgregaProducto(Producto);
                    }
                });
            }
            function CreaBotonesDialog(Producto, EsRegistroVenta) {
                if (!EsRegistroVenta) {
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
                } else {
                    return {
                        'Cancelar': {
                            id: 'Cancelar',
                            text: 'Cancelar',
                            class: 'btn btn-danger',
                            click: function () {
                                $('#' + Elementos_VentaTotal.RegistraVenta).dialog('close');
                            }
                        },
                        'Agregar': {
                            id: 'Agregar',
                            text: 'Agregar',
                            class: 'btn btn-primary',
                            click: function () {
                                let TablaRows = TablaVentas.rows().data();
                                let JsonObj = [];
                                for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                                    JsonObj.push(TablaVentas.rows(i).data()[0]);
                                }
                                let JsonString = '';
                                if (JsonObj.length != 0) {
                                    JsonString = JSON.stringify(JsonObj);
                                }
                                $.ajax({
                                    type: "POST",
                                    url: UrlRegistraVenta,
                                    async: false,
                                    data: { RegistroVenta: JsonString },
                                    success: function (data) {
                                        if (data.Resultado) {
                                            if (data.Adicional != null) {
                                                alertify.alert('Aviso Importante', data.Adicional);
                                            } else {
                                                alertify.success(data.Mensaje);
                                            }
                                            TablaVentas.clear().draw();
                                            $('#' + Elementos_VentaTotal.RegistraVenta).dialog('close');
                                            LimpiaCantidad();
                                            $('#' + Elementos_VentaTotal.ImporteTotal).val(0);
                                            $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                                        } else {
                                            if (data.Adicional != null) {
                                                alertify.alert('Aviso Importante', data.Adicional);
                                            } else {
                                                alertify.error(data.Mensaje);
                                            }
                                        }
                                    },
                                    error: function () {
                                        alertify.error("Ocurrio un error al realizar la carga del Producto");
                                    }
                                });
                            }
                        }
                    }
                }
            }
            function AgregaProducto(Producto) {
                let TablaRows = TablaVentas.rows().data();
                let JsonObj = [];
                for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                    JsonObj.push(TablaVentas.rows(i).data()[0]);
                }
                let JsonString = '';
                if (JsonObj.length != 0) {
                    JsonString = JSON.stringify(JsonObj);
                }
                let _Cantidad = $('#' + Elementos_VentaTotal.CantidadDeProducto).val();
                if (_Cantidad == '') {
                    _Cantidad = 1;
                } else if (isNaN(_Cantidad)) {
                    alertify.error("El campo de Cantidad debe ser numerico");
                }
                $.ajax({
                    type: "POST",
                    url: UrlProductoATabla,
                    async: false,
                    data: { NombreOCodigoDeProducto: Producto.CodigoDeBarras, Cantidad: _Cantidad, Paquetes: $('#' + Elementos_VentaTotal.Paquetes).val(), RegistrosPrevios: JsonString },
                    success: function (data) {
                        if (data.Registro.length != 0) {
                            for (var i = 0; i < data.Registro.length; i++) {
                                let Index = null;
                                for (var ii = 0; ii < TablaRows.rows()[0].length; ii++) {
                                    if (data.Registro[i].CodigoDeBarras == TablaVentas.rows(ii).data()[0].CodigoDeBarras) {
                                        Index = ii;
                                    }
                                }
                                if (Index == null) {
                                    TablaVentas.row.add({
                                        "NumeroDeProducto": data.Registro[i].NumeroDeProducto,
                                        "CodigoDeBarras": data.Registro[i].CodigoDeBarras,
                                        "NombreDeProducto": data.Registro[i].NombreDeProducto,
                                        "CantidadDeProducto": data.Registro[i].CantidadDeProducto,
                                        "PrecioUnitario": data.Registro[i].PrecioUnitario,
                                        "ImporteDeProducto": data.Registro[i].ImporteDeProducto
                                    }).draw();
                                } else {
                                    $('#' + Elementos_VentaTotal.Tabla).dataTable().fnUpdate(data.Registro[i], Index, undefined, false);
                                }
                                LimpiaCantidad();
                                $('#' + Elementos_VentaTotal.AgregaProducto).dialog('close');
                                $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                                TablaVentas.columns.adjust().draw();
                            }
                            let ImporteTotal = 0;
                            for (var ii = 0; ii < TablaRows.rows()[0].length; ii++) {
                                ImporteTotal = parseFloat(ImporteTotal) + parseFloat(TablaVentas.rows(ii).data()[0].ImporteDeProducto);
                            }
                            $('#' + Elementos_VentaTotal.ImporteTotal).val(ImporteTotal.toFixed(2));
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la carga del Producto");
                    }
                });
            }
            function LimpiaCantidad() {
                $('#' + Elementos_VentaTotal.CantidadDeProducto).val('');
                $('#' + Elementos_VentaTotal.Paquetes).val('');
                $('#' + Elementos_VentaTotal.ImporteCambio).val();
                $('#' + Elementos_VentaTotal.ImporteTotalVent).val();
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlAutoCompleteProducto: _UrlAutoCompleteProducto,
                    UrlCargaProducto: _UrlCargaProducto,
                    UrlProductoATabla: _UrlProductoATabla,
                    UrlDeleteProducto: _UrlDeleteProducto,
                    UrlRegistraVenta: _UrlRegistraVenta
                },
                Inicio: _Inicio
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlAutoCompleteProducto(ObjetoConfiguracion.UrlAutoCompleteProducto);
            NucleoConfigurado.Configuracion.UrlCargaProducto(ObjetoConfiguracion.UrlCargaProducto);
            NucleoConfigurado.Configuracion.UrlProductoATabla(ObjetoConfiguracion.UrlProductoATabla);
            NucleoConfigurado.Configuracion.UrlDeleteProducto(ObjetoConfiguracion.UrlDeleteProducto);
            NucleoConfigurado.Configuracion.UrlRegistraVenta(ObjetoConfiguracion.UrlRegistraVenta);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = VentaTotal;
})(window, document);