(function (windows, document) {
    let VentaTotal = (function () {
        let _Nucleo = function () {
            let Elementos_VentaTotal = {
                CodigoONombreDeProducto: 'fCodigoONombreDeProducto',
                FolioDeVenta: 'fFolioDeVenta',
                CantidadDeProducto: 'fCantidadDeProducto',
                Paquetes: 'fPaquetes',
                ImporteTotal: 'fImporteTotal',
                AgregaProducto: 'AgregaProducto',
                formularioProducto: 'formProducto',
                VentaTotal: 'VentaTotal',
                RegistraVenta: 'RegistraVenta',
                ImporteRecibido: 'fImporteRecibido',
                ImporteTotalVent: 'fImporteTotalVent',
                MasDeUnProducto: 'MasDeUnProducto',
                ImporteCambio: 'fImporteCambio',
                NombreDeProducto: 'fNombreDeProducto',
                ProductosSelect: 'fProductosSelect',
                NumeroDeTipoDeProducto: 'fNumeroDeTipoDeProducto',
                NumeroDeMarca: 'fNumeroDeMarca',
                Tabla: 'Tabla',
                NombreDeProductoHijo: 'fNombreDeProductoHijo',
                NombreDeProductoHijoCambio: 'fNombreDeProductoHijoCambio',
                BuscaVentaFolio: 'BuscaVentaFolio',
                CambioDeProducto: 'CambioDeProducto'
            };
            let Funcionalidad = '',
                UrlCargaProducto = '',
                UrlAutoCompleteProducto = '',
                UrlProductoATabla = '',
                UrlDeleteProducto = '',
                UrlRegistraVenta = '',
                UrlObtenMarcaPorTipo = '',
                UrlBuscaVentaFolio = '',
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
            };
            let _UrlRegistraVenta = function (UrlRegistraVentaSet) {
                if (typeof (UrlRegistraVentaSet) != 'undefined') {
                    UrlRegistraVenta = UrlRegistraVentaSet;
                } else {
                    return UrlRegistraVenta;
                }
            };
            let _UrlObtenMarcaPorTipo = function (UrlObtenMarcaPorTipoSet) {
                if (typeof (UrlObtenMarcaPorTipoSet) != 'undefined') {
                    UrlObtenMarcaPorTipo = UrlObtenMarcaPorTipoSet;
                } else {
                    return UrlObtenMarcaPorTipo;
                }
            }
            let _UrlBuscaVentaFolio = function (UrlBuscaVentaFolioSet) {
                if (typeof (UrlBuscaVentaFolioSet) != 'undefined') {
                    UrlBuscaVentaFolio = UrlBuscaVentaFolioSet;
                } else {
                    return UrlBuscaVentaFolio;
                }
            };
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                if (Funcionalidad == 'INDEX') {
                    $('#' + Elementos_VentaTotal.ImporteTotal).val(0);
                    GeneraGridDeProducto();
                    $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                } else if (Funcionalidad = 'CAMBIODEVOLUCION') {
                    GeneraGridDevolucionCambio();
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
                    "columnDefs": [{ "targets": [0], "visible": false, "searchable": false }, { "targets": [2], "visible": false, "searchable": false }, { "targets": [3], "visible": false, "searchable": false }],
                    "columns": [
                        { "data": "NumeroDeProducto" },
                        { "data": "CodigoDeBarras" },
                        { "data": "NumeroDeTipoDeProducto" },
                        { "data": "NumeroDeMarca" },
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadDeProducto" },
                        { "data": "PrecioUnitario", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "ImporteDeProducto", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
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
                            for (var ii = 0; ii < data.Registro.length; ii++) {
                                for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                                    if (data.Registro[ii].NumeroDeProducto == TablaVentas.rows(i).data()[0].NumeroDeProducto) {
                                        if (data.Registro[ii].CantidadDeProducto == 0) {
                                            ImporteTotal = parseFloat(ImporteTotal) + parseFloat(TablaVentas.rows(i).data()[0].ImporteDeProducto);
                                            TablaRows.row(i).remove().draw();
                                        } else {
                                            ImporteTotal = parseFloat(ImporteTotal) + parseFloat(TablaVentas.rows(i).data()[0].ImporteDeProducto);
                                            $('#' + Elementos_VentaTotal.Tabla).dataTable().fnUpdate(data.Registro[ii], i, undefined, false);
                                        }
                                    }
                                }
                            }
                            $('#' + Elementos_VentaTotal.ImporteTotal).val((parseFloat($('#' + Elementos_VentaTotal.ImporteTotal).val()) - parseFloat(ImporteTotal)).toFixed(2));
                            TablaVentas.columns.adjust().draw();
                        },
                        error: function () {
                            alertify.error("Ocurrio un error al realizar la eliminación del registro");
                        }
                    });
                });
            }
            function GeneraGridDevolucionCambio() {
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
                    "columnDefs": [{ "targets": [0], "visible": false, "searchable": false }, { "targets": [2], "visible": false, "searchable": false }, { "targets": [3], "visible": false, "searchable": false }],
                    "columns": [
                        { "data": "NumeroDeProducto" },
                        { "data": "CodigoDeBarras" },
                        { "data": "NumeroDeTipoDeProducto" },
                        { "data": "NumeroDeMarca" },
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadDeProducto" },
                        { "data": "PrecioUnitario", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "ImporteDeProducto", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "TextoDeEstatus" },
                        { "defaultContent": "<button type='button' class='btndevolucion btn btn-danger'><i class='far fa-trash-alt'></i></button><button type='button' class='btncambio btn btn-danger'><i class='fa fa-retweet'></i></button>" }
                    ],
                    "paging": false,
                    "searching": false,
                    "language": LenguajeEN()
                });
                AccionBotonesDevolucionCambio('#' + Elementos_VentaTotal.Tabla + ' tbody', TablaVentas);
            }
            var AccionBotonesDevolucionCambio = function (tbody, table) {
                $(tbody).on('click', 'button.btncambio', function () {
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
                            CreaDialog(Elementos_VentaTotal.CambioDeProducto, 'Cambio de Producto', CargaBotonesCambio((data.Registro[0].CantidadDeProducto = 1 ? false : true)), 600, 500);
                            $('#' + Elementos_VentaTotal.CambioDeProducto).dialog('open');
                            data.Registro.forEach((registro, i) => {
                                if (i == 0) {
                                    $('#' + Elementos_VentaTotal.CambioDeProducto + ' #' + Elementos_VentaTotal.NombreDeProducto).append(new Option(registro.NumeroDeProducto + ' / ' + registro.NombreDeProducto, registro.NumeroDeProducto, false, true));
                                }
                                if (i == 1) {
                                    $('#' + Elementos_VentaTotal.CambioDeProducto + ' #' + Elementos_VentaTotal.NombreDeProductoHijo).append(new Option(registro.NumeroDeProducto + ' / ' + registro.NombreDeProducto, registro.NumeroDeProducto, false, true));
                                }
                            });
                            if (data.Registro.length == 2) {
                                $('#' + Elementos_VentaTotal.NombreDeProductoHijo).show();
                                $('#' + Elementos_VentaTotal.NombreDeProductoHijoCambio).show();
                            }
                            AgregaEventoDeProducto();
                        },
                        error: function () {
                            alertify.error("Ocurrio un error al realizar la eliminación del registro");
                        }
                    });
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
                        AgregaEventoDeProducto();
                        $('#' + Elementos_VentaTotal.VentaTotal).click(function () {
                            if ($('#' + Elementos_VentaTotal.ImporteTotal).val() != '') {
                                if (parseFloat($('#' + Elementos_VentaTotal.ImporteTotal).val()) != parseFloat('0')) {
                                    CreaDialog(Elementos_VentaTotal.RegistraVenta, 'Registra Venta', CreaBotonesDialog([], true), 400, 500);
                                    $('#' + Elementos_VentaTotal.RegistraVenta).dialog('open');
                                    $('#' + Elementos_VentaTotal.ImporteTotalVent).val($('#' + Elementos_VentaTotal.ImporteTotal).val());
                                    $('#' + Elementos_VentaTotal.ImporteRecibido).focus();
                                    AgregaEventoCambio();
                                    $('#' + Elementos_VentaTotal.RegistraVenta).on('dialogclose', function (event) {
                                        LimpiaCantidad();
                                        $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                                    });
                                } else {
                                    alertify.error('No se aregistrado ningun Producto o el Importe es 0');
                                }
                            } else {
                                alertify.error('No se aregistrado ningun Producto o el Importe es 0');
                            }
                        });
                        $("#" + Elementos_VentaTotal.NumeroDeTipoDeProducto).change(function () {
                            $.get(UrlObtenMarcaPorTipo, { TipoDeProducto: $("#" + Elementos_VentaTotal.NumeroDeTipoDeProducto).val(), Marca: $("#" + Elementos_VentaTotal.NumeroDeMarca).val() },
                                function (rep) {
                                    var CveTipoMarca = $("#" + Elementos_VentaTotal.NumeroDeMarca);
                                    $("#" + Elementos_VentaTotal.NumeroDeMarca + " > option").val();
                                    if (rep.length) {
                                        for (var i = 0; i < rep.length; i++) {
                                            CveTipoMarca.append($("<option/>").val(rep[i].Value).text(rep[i].Text));
                                        }
                                    }
                                });
                        });
                        break;
                    case "CAMBIODEVOLUCION":
                        $('#' + Elementos_VentaTotal.BuscaVentaFolio).click(function () {
                            $.ajax({
                                type: "POST",
                                url: UrlBuscaVentaFolio,
                                async: false,
                                data: { FolioDeVenta: $('#' + Elementos_VentaTotal.FolioDeVenta).val() },
                                success: function (data) {
                                    if (data.Resultado.Resultado) {
                                        let ImporteTotal = 0;
                                        TablaVentas.clear().draw();
                                        TablaVentas.rows.add(data.Registro).draw();
                                        data.Registro.forEach(registro => ImporteTotal += parseFloat(registro.ImporteDeProducto))
                                        $('#' + Elementos_VentaTotal.ImporteTotal).val(ImporteTotal.toFixed(2));
                                    } else {
                                        alertify.error(data.Resultado.Mensaje);
                                    }
                                },
                                error: function () {
                                    alertify.error("Ocurrio un error al realizar la carga de la Venta");
                                }
                            });

                        });
                        break;
                }
            }
            function ConsultaProducto(NumeroDeProducto) {
                $.ajax({
                    type: "POST",
                    url: UrlCargaProducto,
                    async: true,
                    data: { NombreOCodigoDeProducto: $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).val(), NumeroDeProducto: NumeroDeProducto },
                    success: function (data) {
                        if (data.Resultado.Resultado) {
                            if (data.Productos.length > 1) {
                                $('#' + Elementos_VentaTotal.ProductosSelect + ' option:gt(0)').remove();
                                $.each(data.Productos, function (index, value) {
                                    $('#' + Elementos_VentaTotal.ProductosSelect).append($('<option></option>').attr('value', value.NumeroDeProducto).text(value.CodigoDeBarras + ' - ' + value.NombreDeProducto));
                                });
                                CreaDialog(Elementos_VentaTotal.MasDeUnProducto, 'Selecciona Producto', {
                                    'Cancelar': {
                                        id: 'Cancelar',
                                        text: 'Cancelar',
                                        class: 'btn btn-danger',
                                        click: function () {
                                            $('#' + Elementos_VentaTotal.MasDeUnProducto).dialog('close');
                                        }
                                    },
                                    'Agregar': {
                                        id: 'Agregar',
                                        text: 'Agregar',
                                        class: 'btn btn-primary',
                                        click: function () {
                                            if ($('#' + Elementos_VentaTotal.ProductosSelect).val() != 0) {
                                                ConsultaProducto($('#' + Elementos_VentaTotal.ProductosSelect).val());
                                                $('#' + Elementos_VentaTotal.MasDeUnProducto).dialog('close');
                                            } else {
                                                alertify.error('No se ha seleccionado ningun producto', 0);
                                            }
                                        }
                                    }
                                }, 500, 260);
                                $('#' + Elementos_VentaTotal.MasDeUnProducto).dialog('open');
                            } else {
                                if (data.Producto.CantidadDeProductoTotal > 0) {
                                    let DimencionesX = 500, DimencionesY = 260;
                                    if (data.TienePaquetes) {
                                        $('#' + Elementos_VentaTotal.Paquetes + ' option:gt(0)').remove();
                                        $.each(data.Paquetes, function (index, value) {
                                            $('#' + Elementos_VentaTotal.Paquetes).append($('<option></option>').attr('value', value.NumeroDeProductoPadre + '_' + value.NumeroDeProductoHijo).text(value.NombreDeProductoPadre + ' con ' + value.NombreDeProductoHijo));
                                        });
                                        DimencionesX = 700;
                                        DimencionesY = 350;
                                        $('#' + Elementos_VentaTotal.Paquetes).parent().parent().show();
                                    } else {
                                        $('#' + Elementos_VentaTotal.Paquetes).parent().parent().hide();
                                    }
                                    $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).val('');
                                    $('#' + Elementos_VentaTotal.NumeroDeTipoDeProducto).val('');
                                    $('#' + Elementos_VentaTotal.NumeroDeMarca).val('');
                                    $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).blur();
                                    CreaDialog(Elementos_VentaTotal.AgregaProducto, 'Agrega Producto', CreaBotonesDialog(data.Producto, false), DimencionesX, DimencionesY);
                                    $('#' + Elementos_VentaTotal.AgregaProducto).dialog('open');
                                    $('#' + Elementos_VentaTotal.NombreDeProducto).html(data.Producto.NombreDeProducto);
                                    $('#' + Elementos_VentaTotal.CantidadDeProducto).focus();
                                    AgregaEventoCantidad(data.Producto);
                                    $('#' + Elementos_VentaTotal.AgregaProducto).on('dialogclose', function (event) {
                                        LimpiaCantidad();
                                        $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                                    });
                                } else {
                                    alertify.alert('Aviso Importante', 'Producto sin existencias');
                                }
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
            function AgregaEventoCambio() {
                $('#' + Elementos_VentaTotal.ImporteRecibido).unbind('keydown');
                $('#' + Elementos_VentaTotal.ImporteRecibido).keydown(function (event) {
                    let keycode2 = (event.keyCode ? event.keyCode : event.which);
                    if (keycode2 == 13) {
                        if (!isNaN($('#' + Elementos_VentaTotal.ImporteRecibido).val())) {
                            if ($('#' + Elementos_VentaTotal.ImporteRecibido).val() != '') {
                                $('#' + Elementos_VentaTotal.ImporteCambio).val((parseFloat($('#' + Elementos_VentaTotal.ImporteRecibido).val()) - parseFloat($('#' + Elementos_VentaTotal.ImporteTotalVent).val())).toFixed(2));
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
                                if (!$('#' + Elementos_VentaTotal.Paquetes).is(':hidden')) {
                                    if ($('#' + Elementos_VentaTotal.Paquetes).val() == '') {
                                        alertify.confirm("Confirmación", "Este producto cuenta con un Paquete configurado, ¿Continuar sin paquete?",
                                            function () {
                                                AgregaProducto(Producto);
                                            },
                                            function () {

                                            }
                                        ).set('labels', { ok: 'Si, Continuar sin Paquete', cancel: 'No, Regresar a Paquete' });
                                    } else {
                                        AgregaProducto(Producto);
                                    }
                                } else {
                                    AgregaProducto(Producto);
                                }
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
                                if (!isNaN($('#' + Elementos_VentaTotal.ImporteRecibido).val())) {
                                    if ($('#' + Elementos_VentaTotal.ImporteRecibido).val() != '') {
                                        $.ajax({
                                            type: "POST",
                                            url: UrlRegistraVenta,
                                            async: false,
                                            data: { ImporteEntregado: $('#' + Elementos_VentaTotal.ImporteRecibido).val(), ImporteCambio: $('#' + Elementos_VentaTotal.ImporteCambio).val(), RegistroVenta: JsonString },
                                            success: function (data) {
                                                if (typeof (data.UrlAccount) != 'undefined') {
                                                    windows.location = data.UrlAccount;
                                                }
                                                if (typeof (data.UrlFun) != 'undefined') {
                                                    window.location = data.UrlFun;
                                                }
                                                if (data.Resultado) {
                                                    if (typeof (data.Adicional.MensajeAviso) != 'undefined') {
                                                        if (data.Adicional.MensajeAviso != '') {
                                                            alertify.alert('Aviso Importante', data.Adicional.MensajeAviso);
                                                        }
                                                    }
                                                    alertify.success(data.Mensaje);
                                                    TablaVentas.clear().draw();
                                                    $('#' + Elementos_VentaTotal.RegistraVenta).dialog('close');
                                                    LimpiaCantidad();
                                                    $('#' + Elementos_VentaTotal.ImporteTotal).val(0);
                                                    $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).focus();
                                                    ImprimirTicket(data.Adicional.Ticket);
                                                } else {
                                                    if (data.Adicional.MensajeAviso != '') {
                                                        alertify.alert('Aviso Importante', data.Adicional.MensajeAviso);
                                                    }
                                                    alertify.error(data.Mensaje, 0);
                                                }
                                            },
                                            error: function () {
                                                alertify.error("Ocurrio un error al realizar la carga del Producto");
                                            }
                                        });
                                    } else {
                                        alertify.error('El Importe Recibido es requerido')
                                    }
                                } else {
                                    alertify.error('El Importe Recibido debe ser numerico')
                                }
                            }
                        }
                    }
                }
            }
            function CargaBotonesCambio(MuestraParcial) {
                let Parcial = {
                    id: 'CambioParcial',
                    text: 'Cambio Parcial',
                    class: 'btn btn-primary',
                    click: function () {

                    }
                };
                let Completo = {
                    id: 'CambioCompleto',
                    text: 'Cambio Completo',
                    class: 'btn btn-primary',
                    click: function () {

                    }
                }
                if (MuestraParcial) {
                    return {
                        'CambioParcial': Parcial,
                        'CambioCompleto': Completo,
                        'Cancelar': {
                            id: 'Cancelar',
                            text: 'Cancelar',
                            class: 'btn btn-danger',
                            click: function () {
                                $('#' + Elementos_VentaTotal.CambioDeProducto).dialog('close');
                            }
                        }
                    }
                } else {
                    return {
                        'CambioCompleto': Completo,
                        'Cancelar': {
                            id: 'Cancelar',
                            text: 'Cancelar',
                            class: 'btn btn-danger',
                            click: function () {
                                $('#' + Elementos_VentaTotal.CambioDeProducto).dialog('close');
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
                    data: { NumeroDeProducto: Producto.NumeroDeProducto, Cantidad: _Cantidad, Paquetes: $('#' + Elementos_VentaTotal.Paquetes).val(), RegistrosPrevios: JsonString },
                    success: function (data) {
                        if (data.Registro.length != 0) {
                            for (var i = 0; i < data.Registro.length; i++) {
                                let Index = null;
                                for (var ii = 0; ii < TablaRows.rows()[0].length; ii++) {
                                    if (data.Registro[i].NumeroDeProducto == TablaVentas.rows(ii).data()[0].NumeroDeProducto) {
                                        Index = ii;
                                    }
                                }
                                if (Index == null) {
                                    TablaVentas.row.add({
                                        "NumeroDeProducto": data.Registro[i].NumeroDeProducto,
                                        "CodigoDeBarras": data.Registro[i].CodigoDeBarras,
                                        "NumeroDeTipoDeProducto": data.Registro[i].NumeroDeTipoDeProducto,
                                        "NumeroDeMarca": data.Registro[i].NumeroDeMarca,
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
            function AgregaEventoDeProducto() {
                $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).autocomplete({
                    source: function (request, response) {
                        minLength: 3,
                        $.ajax({
                            url: UrlAutoCompleteProducto,
                            type: "POST",
                            dataType: "json",
                            data: { Prefix: request.term, NumeroDeTipoDeProducto: $('#' + Elementos_VentaTotal.NumeroDeTipoDeProducto).val(), NumeroDeMarca: $('#' + Elementos_VentaTotal.NumeroDeMarca).val() },
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
                $('#' + Elementos_VentaTotal.CodigoONombreDeProducto).keydown(function (event) {
                    let keycode = (event.keyCode ? event.keyCode : event.which);
                    if (keycode == 13) {
                        ConsultaProducto(0);
                    }
                });
            }
            function LimpiaCantidad() {
                $('#' + Elementos_VentaTotal.CantidadDeProducto).val('');
                $('#' + Elementos_VentaTotal.Paquetes).val('');
                $('#' + Elementos_VentaTotal.ImporteCambio).val('');
                $('#' + Elementos_VentaTotal.ImporteTotalVent).val('');
                $('#' + Elementos_VentaTotal.ImporteRecibido).val('');
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlAutoCompleteProducto: _UrlAutoCompleteProducto,
                    UrlCargaProducto: _UrlCargaProducto,
                    UrlProductoATabla: _UrlProductoATabla,
                    UrlDeleteProducto: _UrlDeleteProducto,
                    UrlRegistraVenta: _UrlRegistraVenta,
                    UrlObtenMarcaPorTipo: _UrlObtenMarcaPorTipo,
                    UrlBuscaVentaFolio: _UrlBuscaVentaFolio
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
            NucleoConfigurado.Configuracion.UrlObtenMarcaPorTipo(ObjetoConfiguracion.UrlObtenMarcaPorTipo);
            NucleoConfigurado.Configuracion.UrlBuscaVentaFolio(ObjetoConfiguracion.UrlBuscaVentaFolio);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = VentaTotal;
})(window, document);