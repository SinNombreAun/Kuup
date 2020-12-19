(function (windows, document) {
    let Surtido = (function () {
        let _Nucleo = function () {
            let Elementos_Surtido = {
                form: 'formSurtido',
                CodigoONombreDeProducto: 'fCodigoONombreDeProducto',
                FolioDeSurtido: 'fFolioDeSurtido',
                CveDeAplicaSurtido: 'fCveDeAplicaSurtido',
                NumeroDeProveedor: 'fNumeroDeProveedor',
                NumeroDeUsuario: 'fNumeroDeUsuario',
                NumeroDeProducto: 'fNumeroDeProducto',
                CodigoDeBarras: 'fCodigoDeBarras',
                CantidadPrevia: 'fCantidadPrevia',
                CantidadNueva: 'fCantidadNueva',
                PrecioUnitario: 'fPrecioUnitario',
                CostoTotal: 'fCostoTotal',
                FechaDeSurtido: 'fFechaDeSurtido',
                CveDeEstatus: 'fCveDeEstatus',
                TextoDeAplicaProveedor: 'fTextoDeAplicaProveedor',
                NombreDeProveedor: 'fNombreDeProveedor',
                NombreDeUsuario: 'fNombreDeUsuario',
                NombreDeProducto: 'fNombreDeProducto',
                TextoDeEstatus: 'fTextoDeEstatus',
                ProductosSelect: 'fProductosSelect',
                Tabla: 'Tabla',
                TablaSurtido: 'TablaSurtido',
                Agregar: 'Agregar',
                Guardar: 'Guardar'
            };
            let Funcionalidad = '',
                UrlAccion = '',
                UrlCargaProducto = '',
                UrlAutoCompleteProducto = '',
                UrlCargaGrid = '',
                UrlDetalle = '',
                TablaS = null
            let _Funcionalidad = function (FuncionalidadSet) {
                if (typeof (FuncionalidadSet) != 'undefined') {
                    Funcionalidad = FuncionalidadSet;
                } else {
                    return Funcionalidad;
                }
            };
            let _UrlAccion = function (UrlAccionSet) {
                if (typeof (UrlAccionSet) != 'undefined') {
                    UrlAccion = UrlAccionSet;
                } else {
                    return UrlAccion;
                }
            }
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
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
                if (Funcionalidad == 'INDEX') {
                    GeneraGridDeSurtido();
                }
                if (Funcionalidad == 'ALTA') {
                    GeneraGridParaSurtido();
                }
            };
            function GeneraGridParaSurtido() {
                TablaS = $('#' + Elementos_Surtido.TablaSurtido).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 300,
                    "scrollX": true,
                    "scrollCollapse": false,
                    "fixedColumns": {
                        "leftColumns": 3,
                        "rightColumns": 1
                    },
                    "columnDefs": [{ "targets": [0], "visible": false, "searchable": false }, { "targets": [1], "visible": false, "searchable": false }],
                    "columns": [
                        { "data": "NumeroDeProducto" },
                        { "data": "CodigoDeBarras" },
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadPrevia" },
                        { "data": "CantidadNueva" },
                        { "data": "CantidadTotal" },
                        { "defaultContent": "<button type='button' class='btndelete btn btn-danger'><i class='far fa-trash-alt'></i></button>" }
                    ],
                    "paging": false,
                    "searching": false,
                    "language": LenguajeEN()
                });
                RemoveProducto('#' + Elementos_Surtido.TablaSurtido + ' tbody', TablaS);
            }
            var RemoveProducto = function (tbody, table) {
                $(tbody).on('click', 'button.btndelete', function () {
                    table.row($(this).parents('li')).remove().draw();
                    table.row($(this).parents('tr')).remove().draw();
                });
            }
            function GeneraGridDeSurtido() {
                var table = $('#' + Elementos_Surtido.Tabla).DataTable({
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": "POST",
                        "url": UrlCargaGrid,
                        "async": true,
                        "datatype": "json"
                    },
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 500,
                    "scrollX": true,
                    "scrollCollapse": false,
                    "pageLength": 100,
                    "filter": true,
                    "responsivePriority": 1,
                    "data": null,
                    "order": [[0, 'desc']],
                    "columns": [
                        { "data": "FolioDeSurtido" },
                        { "data": "NombreDeProducto" },
                        { "data": "NombreDeQuienSurtio" },
                        { "data": "CantidadNueva" },
                        { "data": "FechaDeSurtido" },
                        { "data": "TextoDeEstatus" }
                    ],
                    "rowGroup": {
                        "dataSrc": 'FolioDeSurtido'
                    },
                    "language": LenguajeEN()
                });
                table.draw();
                DetalleRegistro('#' + Elementos_Surtido.Tabla + ' tbody', table);
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
            function OcultaCampos() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        break;
                    case 'IMPORTAR':
                        break;
                }
            }
            function EjecutaEventosIniciales() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        break;
                    case 'DETALLE':
                        break;
                }
            }
            function AgregaEvento() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        $('#' + Elementos_Surtido.CodigoONombreDeProducto).autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: UrlAutoCompleteProducto,
                                    type: "POST",
                                    dataType: "json",
                                    data: { Prefix: request.term },
                                    async: false,
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
                        $('#' + Elementos_Surtido.CodigoONombreDeProducto).keydown(function (event) {
                            let keycode = (event.keyCode ? event.keyCode : event.which);
                            if (keycode == 13) {
                                AgregaProductoASutido(0);
                            }
                        });
                        $('#' + Elementos_Surtido.Agregar).click(function () {
                            LimpiaValidaciones();
                            if ($('#' + Elementos_Surtido.NombreDeProducto).val() != '' && $('#' + Elementos_Surtido.CantidadPrevia).val()) {
                                if ($('#' + Elementos_Surtido.CantidadNueva).val() == '') {
                                    $('.rcantidad').html('El campo Cantidad Nueva es obligatorio');
                                } else {
                                    if (!isNaN($('#' + Elementos_Surtido.CantidadNueva).val())) {
                                        TablaS.row.add({
                                            "NumeroDeProducto": $('#' + Elementos_Surtido.NumeroDeProducto).val(),
                                            "CodigoDeBarras": $('#' + Elementos_Surtido.CodigoDeBarras).val(),
                                            "NombreDeProducto": $('#' + Elementos_Surtido.NombreDeProducto).val(),
                                            "CantidadPrevia": $('#' + Elementos_Surtido.CantidadPrevia).val(),
                                            "CantidadNueva": $('#' + Elementos_Surtido.CantidadNueva).val(),
                                            "CantidadTotal": parseInt($('#' + Elementos_Surtido.CantidadPrevia).val()) + parseInt($('#' + Elementos_Surtido.CantidadNueva).val())
                                        }).draw();
                                        LimpiarCampos();
                                        $('#' + Elementos_Surtido.Guardar).show();
                                    } else {
                                        $('.rcantidad').html('El campo Cantidad Nueva debe ser un número.');
                                    }
                                }
                            } else {
                                alertify.error('No sé a seleccionado ningún producto',0);
                            }
                        });
                        $('#' + Elementos_Surtido.Guardar).click(function () {
                            let TablaRows = TablaS.rows().data();
                            let JsonObj = [];
                            for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                                JsonObj.push(TablaS.rows(i).data()[0]);
                            }
                            let JsonStrin = JSON.stringify(JsonObj);
                            $.ajax({
                                type: "POST",
                                url: UrlAccion,
                                async: false,
                                data: { ProductosSurtidos: JsonStrin },
                                success: function (data) {
                                    if (typeof (data.UrlAccount) != 'undefined') {
                                        windows.location = data.UrlAccount;
                                    }
                                    if (typeof (data.UrlFun) != 'undefined') {
                                        window.location = data.UrlFun;
                                    }
                                    if (data.Resultado.Resultado) {
                                        alertify.success(data.Resultado.Mensaje);
                                        if (TablaS != null) {
                                            TablaS.clear().draw();
                                            $('#' + Elementos_Surtido.Guardar).hide();
                                        }
                                    } else {
                                        alertify.error(data.Resultado.Mensaje, 0);
                                    }
                                },
                                error: function () {
                                    alertify.error("Ocurrio un error al realizar la accion de carga de archivo");
                                }
                            });
                        });
                        break;
                    case 'DETALLE':
                        break;
                }
            }
            function AgregaProductoASutido(NumeroDeProducto) {
                $.ajax({
                    type: "POST",
                    url: UrlCargaProducto,
                    async: false,
                    data: { NombreOCodigoDeProducto: $('#' + Elementos_Surtido.CodigoONombreDeProducto).val(), NumeroDeProducto: NumeroDeProducto },
                    success: function (data) {
                        if (data.Resultado.Resultado) {
                            if (data.Productos.length > 1) {
                                $('#' + Elementos_Surtido.ProductosSelect + ' option:gt(0)').remove();
                                $.each(data.Productos, function (index, value) {
                                    $('#' + Elementos_Surtido.ProductosSelect).append($('<option></option>').attr('value', value.NumeroDeProducto).text(value.CodigoDeBarras + ' - ' + value.NombreDeProducto));
                                });
                                CreaDialog(Elementos_Surtido.MasDeUnProducto, 'Selecciona Producto', {
                                    'Cancelar': {
                                        id: 'Cancelar',
                                        text: 'Cancelar',
                                        class: 'btn btn-danger',
                                        click: function () {
                                            $('#' + Elementos_Surtido.MasDeUnProducto).dialog('close');
                                        }
                                    },
                                    'Agregar': {
                                        id: 'Agregar',
                                        text: 'Agregar',
                                        class: 'btn btn-primary',
                                        click: function () {
                                            if ($('#' + Elementos_Surtido.ProductosSelect).val() != 0) {
                                                AgregaProductoASutido($('#' + Elementos_Surtido.ProductosSelect).val());
                                                $('#' + Elementos_Surtido.MasDeUnProducto).dialog('close');
                                            } else {
                                                alertify.error('No se ha seleccionado ningun producto', 0);
                                            }
                                        }
                                    }
                                }, 500, 260);
                                $('#' + Elementos_Surtido.MasDeUnProducto).dialog('open');
                            } else {
                                $('#' + Elementos_Surtido.NumeroDeProducto).val(data.Producto.NumeroDeProducto);
                                $('#' + Elementos_Surtido.CodigoDeBarras).val(data.Producto.CodigoDeBarras);
                                $('#' + Elementos_Surtido.NombreDeProducto).val(data.Producto.NombreDeProducto);
                                $('#' + Elementos_Surtido.CantidadPrevia).val(data.Producto.CantidadDeProductoTotal);
                            }
                        } else {
                            alertify.error(data.Resultado.Mensaje);
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la validacion del Producto");
                    }
                });
            }
            function LimpiarCampos() {
                $('#' + Elementos_Surtido.CodigoONombreDeProducto).val('');
                $('#' + Elementos_Surtido.NumeroDeProducto).val('');
                $('#' + Elementos_Surtido.CodigoDeBarras).val('');
                $('#' + Elementos_Surtido.NombreDeProducto).val('');
                $('#' + Elementos_Surtido.CantidadPrevia).val('');
                $('#' + Elementos_Surtido.CantidadNueva).val('');
            }
            function LimpiaValidaciones() {
                $('.rcantidad').html('');
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlAccion: _UrlAccion,
                    UrlAutoCompleteProducto: _UrlAutoCompleteProducto,
                    UrlCargaProducto: _UrlCargaProducto,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlDetalle: _UrlDetalle,
                },
                Inicio: _Inicio,
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlAccion(ObjetoConfiguracion.UrlAccion);
            NucleoConfigurado.Configuracion.UrlAutoCompleteProducto(ObjetoConfiguracion.UrlAutoCompleteProducto);
            NucleoConfigurado.Configuracion.UrlCargaProducto(ObjetoConfiguracion.UrlCargaProducto);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlDetalle(ObjetoConfiguracion.UrlDetalle);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Surtido;
})(window, document);