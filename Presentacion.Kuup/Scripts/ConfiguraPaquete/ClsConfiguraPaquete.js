(function (windows, document) {
    let ConfiguraPaquete = (function () {
        let _Nucleo = function () {
            let Elementos_ConfiguraPaquete = {
                CodigoONombreDeProducto: 'fCodigoONombreDeProducto',
                NumeroDeTipoDeProducto: 'fNumeroDeTipoDeProducto',
                NumeroDeMarca: 'fNumeroDeMarca',
                Tabla: 'Tabla',
                TablaFilter: "Tabla_filter",
                CodigoONombreDeProducto2: 'fCodigoONombreDeProducto2',
                NumeroDeTipoDeProducto2: 'fNumeroDeTipoDeProducto2',
                NumeroDeMarca2: 'fNumeroDeMarca2',
                NombreDeProductoPadre: 'fNombreDeProductoPadre',
                CodigoDeBarrasPadre: 'fCodigoDeBarrasPadre',
                NumeroDeProductoPadre: 'fNumeroDeProductoPadre',
                PrecioDeProductoPadre: 'fPrecioDeProductoPadre',
                NombreDeProductoHijo: 'fNombreDeProductoHijo',
                CodigoDeBarrasHijo: 'fCodigoDeBarrasHijo',
                NumeroDeProductoHijo: 'fNumeroDeProductoHijo',
                PrecioDeProductoHijo: 'fPrecioDeProductoHijo',
                CantidadASalir: 'fCantidadASalir',
                ImporteTotal: 'fImporteTotal',
                Configura: 'configura',
                GuardaConfiguracion: 'GuardaConfiguracion',
                btnEditar: 'btnEditar',
                SelectTablas: ["TextoAviso", "TextoCorreoSurtido", "TextoDeEstatus"]
            };
            let Funcionalidad = '',
                UrlCargaGrid = '',
                UrlAutoCompleteProducto = '',
                CombosParaTabla = [],
                UrlObtenMarcaPorTipo = '',
                UrlBuscaPrecioDeProducto = '',
                UrlGuardaConfiguracion = '',
                UrlGuardaConfiguracionEdita = '',
                UrlDetalle = '';
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
            let _UrlAutoCompleteProducto = function (UrlAutoCompleteProductoSet) {
                if (typeof (UrlAutoCompleteProductoSet) != 'undefined') {
                    UrlAutoCompleteProducto = UrlAutoCompleteProductoSet;
                } else {
                    return UrlAutoCompleteProducto;
                }
            };
            let _CombosParaTabla = function (CombosParaTablaSet) {
                if (typeof CombosParaTablaSet != "") {
                    CombosParaTabla = CombosParaTablaSet;
                } else {
                    return CombosParaTabla;
                }
            };
            let _UrlObtenMarcaPorTipo = function (UrlObtenMarcaPorTipoSet) {
                if (typeof (UrlObtenMarcaPorTipoSet) != 'undefined') {
                    UrlObtenMarcaPorTipo = UrlObtenMarcaPorTipoSet;
                } else {
                    return UrlObtenMarcaPorTipo;
                }
            };
            let _UrlBuscaPrecioDeProducto = function (UrlBuscaPrecioDeProductoSet) {
                if (typeof (UrlBuscaPrecioDeProductoSet) != 'undefined') {
                    UrlBuscaPrecioDeProducto = UrlBuscaPrecioDeProductoSet;
                } else {
                    return UrlBuscaPrecioDeProducto;
                }
            };
            let _UrlGuardaConfiguracion = function (UrlGuardaConfiguracionSet) {
                if (typeof (UrlGuardaConfiguracionSet) != 'undefined') {
                    UrlGuardaConfiguracion = UrlGuardaConfiguracionSet;
                } else {
                    return UrlGuardaConfiguracion;
                }
            };
            let _UrlGuardaConfiguracionEdita = function (UrlGuardaConfiguracionEditaSet) {
                if (typeof (UrlGuardaConfiguracionEditaSet) != 'undefined') {
                    UrlGuardaConfiguracionEdita = UrlGuardaConfiguracionEditaSet;
                } else {
                    return UrlGuardaConfiguracionEdita;
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
                    GeneraGridDeConfiguraPaquete();
                }
            };
            function GeneraGridDeConfiguraPaquete() {
                $("#" + Elementos_ConfiguraPaquete.Tabla + " thead tr").clone(true).appendTo("#" + Elementos_ConfiguraPaquete.Tabla + " thead");
                $("#" + Elementos_ConfiguraPaquete.Tabla + " thead tr:eq(1) th").each(function (i) {
                    if (this.id != "boton") {
                        if (Elementos_ConfiguraPaquete.SelectTablas.indexOf(this.id) > -1) {
                            var Elemento = CombosParaTabla[Elementos_ConfiguraPaquete.SelectTablas.indexOf(this.id)];
                            $(this).html(Elemento);
                            $("select", this).on("change", function () {
                                if (table.column(i).search() !== this.value) {
                                    table.column(i).search(this.value).draw();
                                }
                            });
                        } else {
                            var title = $(this).text();
                            $(this).html('<input type="text" style="width: 100%;" placeholder="Buscar ' + title + '" />');
                            $("input", this).on("keydown", function () {
                                let keycode = event.keyCode ? event.keyCode : event.which;
                                if (keycode == 13) {
                                    if (table.column(i).search() !== this.value) {
                                        table.column(i).search(this.value).draw();
                                    }
                                }
                            });
                        }
                    }
                });
                var table = $('#' + Elementos_ConfiguraPaquete.Tabla).DataTable({
                    "processing": true,
                    "serverSide": true,
                    "ajax": {
                        "type": "POST",
                        "url": UrlCargaGrid,
                        "async": false,
                        "datatype": "json"
                    },
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 500,
                    "scrollX": true,
                    "select": true,
                    "scrollCollapse": false,
                    "pageLength": 100,
                    "filter": true,
                    "responsivePriority": 1,
                    "orderCellsTop": true,
                    "fixedHeader": true,
                    "data": null,
                    "order": [[1, 'desc']],
                    "columnDefs": [{ "targets": [7], "visible": false, "searchable": false }, { "targets": [8], "visible": false, "searchable": false }],
                    "columns": [
                        { "data": "CodigoDeBarrasPadre" },
                        { "data": "NombreDeProductoPadre" },
                        { "data": "PrecioDeProductoPadre", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "CodigoDeBarrasHijo" },
                        { "data": "NombreDeProductoHijo" },
                        { "data": "PrecioDeProductoHijo", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "ImporteTotal", "render": $.fn.dataTable.render.number(',', '.', 2, '$') },
                        { "data": "NumeroDeProductoPadre" },
                        { "data": "NumeroDeProductoHijo" },
                        { "defaultContent": "<button type='button' class='detalle btn btn-info'>Detalle</button>"}
                    ],
                    "rowGroup": {
                        "dataSrc": 'NombreDeProductoPadre'
                    },
                    "language": LenguajeEN()
                });
                table.draw();
                $('#' + Elementos_ConfiguraPaquete.TablaFilter).hide();
                DetalleRegistro("#" + Elementos_ConfiguraPaquete.Tabla + " tbody", table);
            }
            var DetalleRegistro = function (tbody, table) {
                $(tbody).on("click", "button.detalle", function () {
                    var data = table.row($(this).parents("tr")).data();
                    if (typeof data == "undefined") {
                        data = table.row($(this).parents("li")).data();
                    }
                    window.location.href = UrlDetalle + '?NumeroDeProductoPadre=' + data.NumeroDeProductoPadre + '&NumeroDeProductoHijo=' + data.NumeroDeProductoHijo;
                });
            };
            function OcultaCampos() {
                switch (Funcionalidad) {
                    case 'ALTA':
                        $('#' + Elementos_ConfiguraPaquete.Configura).hide();
                        $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val(0);
                        $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val(0);
                        $('#' + Elementos_ConfiguraPaquete.CantidadASalir).val(1);
                        break;
                    case 'DETALLE':
                        $('#' + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto).parent().hide();
                        $('#' + Elementos_ConfiguraPaquete.NumeroDeMarca).parent().hide();
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto).parent().hide();
                        $('#' + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto2).parent().hide();
                        $('#' + Elementos_ConfiguraPaquete.NumeroDeMarca2).parent().hide();
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto2).parent().hide();
                        $('#' + Elementos_ConfiguraPaquete.GuardaConfiguracion).hide();
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
                    case 'INDEX':
                        break;
                    case 'ALTA':
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto).autocomplete({
                            minLength: 3,
                            source: function (request, response) {
                                $.ajax({
                                    url: UrlAutoCompleteProducto,
                                    type: "POST",
                                    dataType: "json",
                                    data: { Prefix: request.term, NumeroDeTipoDeProducto: $('#' + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto).val(), NumeroDeMarca: $('#' + Elementos_ConfiguraPaquete.NumeroDeMarca).val()  },
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
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto).keydown(function (event) {
                            let keycode = (event.keyCode ? event.keyCode : event.which);
                            if (keycode == 13) {
                                ConsultaProducto($('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto).val(),true);
                            }
                        });
                        $("#" + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto).change(function () {
                            $.get(UrlObtenMarcaPorTipo, { TipoDeProducto: $("#" + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto).val(), Marca: $("#" + Elementos_ConfiguraPaquete.NumeroDeMarca).val() },
                                function (rep) {
                                    var CveTipoMarca = $("#" + Elementos_ConfiguraPaquete.NumeroDeMarca);
                                    $("#" + Elementos_ConfiguraPaquete.NumeroDeMarca + " > option").val();
                                    if (rep.length) {
                                        for (var i = 0; i < rep.length; i++) {
                                            CveTipoMarca.append($("<option/>").val(rep[i].Value).text(rep[i].Text));
                                        }
                                    }
                                });
                        });
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto2).autocomplete({
                            minLength: 3,
                            source: function (request, response) {
                                $.ajax({
                                    url: UrlAutoCompleteProducto,
                                    type: "POST",
                                    dataType: "json",
                                    data: { Prefix: request.term, NumeroDeTipoDeProducto: $('#' + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto2).val(), NumeroDeMarca: $('#' + Elementos_ConfiguraPaquete.NumeroDeMarca2).val()  },
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
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto2).keydown(function (event) {
                            let keycode = (event.keyCode ? event.keyCode : event.which);
                            if (keycode == 13) {
                                ConsultaProducto($('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto2).val(),false);
                            }
                        });
                        $("#" + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto2).change(function () {
                            $.get(UrlObtenMarcaPorTipo, { TipoDeProducto: $("#" + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto2).val(), Marca: $("#" + Elementos_ConfiguraPaquete.NumeroDeMarca2).val() },
                                function (rep) {
                                    var CveTipoMarca = $("#" + Elementos_ConfiguraPaquete.NumeroDeMarca2);
                                    $("#" + Elementos_ConfiguraPaquete.NumeroDeMarca2 + " > option").val();
                                    if (rep.length) {
                                        for (var i = 0; i < rep.length; i++) {
                                            CveTipoMarca.append($("<option/>").val(rep[i].Value).text(rep[i].Text));
                                        }
                                    }
                                });
                        });
                        $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).change(function () {
                            SumaPreciosDeProductos();
                        });
                        $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).change(function () {
                            SumaPreciosDeProductos();
                        });
                        $('form').off('click', '#' + Elementos_ConfiguraPaquete.GuardaConfiguracion);
                        $('form').on('click', '#' + Elementos_ConfiguraPaquete.GuardaConfiguracion, () => {
                            let EsCorrecto = true;
                            if ($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val() != '') {
                                if (isNaN($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val())) {
                                    alertify.error('El precio del producto padre no es valido');
                                    EsCorrecto = false;
                                } else if ($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val() == '0') {
                                    alertify.error('El precio del producto padre es requerido');
                                    EsCorrecto = false;
                                }
                            } else {
                                alertify.error('El precio del producto padre es requerido');
                                EsCorrecto = false;
                            }
                            if ($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val() != '') {
                                if (isNaN($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val())) {
                                    alertify.error('El precio del producto hijo no es valido');
                                    EsCorrecto = false;
                                }
                            } else {
                                $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val(0);
                            }
                            if ($('#' + Elementos_ConfiguraPaquete.CantidadASalir).val() != '') {
                                if (isNaN($('#' + Elementos_ConfiguraPaquete.CantidadASalir).val())) {
                                    alertify.error('El valor del campo de cantida a salir no es valido');
                                    EsCorrecto = false;
                                }
                            } else {
                                alertify.error('El campo de cantidad a salir es requerido');
                                EsCorrecto = false;
                            }
                            if (EsCorrecto) {
                                $.ajax({
                                    type: "POST",
                                    url: UrlGuardaConfiguracion,
                                    async: false,
                                    data: {
                                        NumeroDeProductoPadre: $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoPadre).val(),
                                        CodigoDeBarrasPadre: $('#' + Elementos_ConfiguraPaquete.CodigoDeBarrasPadre).val(),
                                        PrecioDeProductoPadre: $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val(),
                                        NumeroDeProductoHijo: $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoHijo).val(),
                                        CodigoDeBarrasHijo: $('#' + Elementos_ConfiguraPaquete.CodigoDeBarrasHijo).val(),
                                        PrecioDeProductoHijo: $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val(),
                                        CantidadASalir: $('#' + Elementos_ConfiguraPaquete.CantidadASalir).val()
                                    },
                                    success: function (data) {
                                        debugger
                                        if (data.Resultado) {
                                            debugger
                                            window.location.href = UrlDetalle + '?NumeroDeProductoPadre=' + $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoPadre).val() + '&NumeroDeProductoHijo=' + $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoHijo).val();
                                        } else {
                                            alertify.error(data.Mensaje);
                                        }
                                    },
                                    error: function () {
                                        debugger
                                        alertify.error("Ocurrio un error al realizar la consulta del producto");
                                    }
                                });
                            }
                        });
                        break;
                    case 'DETALLE':
                        $('#' + Elementos_ConfiguraPaquete.btnEditar).click(function () {
                            if (Funcionalidad == 'DETALLE') {
                                Funcionalidad = 'EDITA';
                                $('#' + Elementos_ConfiguraPaquete.NumeroDeTipoDeProducto2).parent().show();
                                $('#' + Elementos_ConfiguraPaquete.NumeroDeMarca2).parent().show();
                                $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto2).parent().show();
                                $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).prop('readonly', false);
                                $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).prop('disabled', false);
                                $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).prop('readonly', false);
                                $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).prop('disabled', false);
                                $('#' + Elementos_ConfiguraPaquete.CantidadASalir).prop('readonly', false);
                                $('#' + Elementos_ConfiguraPaquete.CantidadASalir).prop('disabled', false);
                                $('#' + Elementos_ConfiguraPaquete.btnEditar).hide();
                                $('#' + Elementos_ConfiguraPaquete.GuardaConfiguracion).show();
                            }
                        });
                        $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).change(function () {
                            SumaPreciosDeProductos();
                        });
                        $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).change(function () {
                            SumaPreciosDeProductos();
                        });
                        $('form').off('click', '#' + Elementos_ConfiguraPaquete.GuardaConfiguracion);
                        $('form').on('click', '#' + Elementos_ConfiguraPaquete.GuardaConfiguracion, () => {
                            let EsCorrecto = true;
                            if ($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val() != '') {
                                if (isNaN($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val())) {
                                    alertify.error('El precio del producto padre no es valido');
                                    EsCorrecto = false;
                                } else if ($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val() == '0') {
                                    alertify.error('El precio del producto padre es requerido');
                                    EsCorrecto = false;
                                }
                            } else {
                                alertify.error('El precio del producto padre es requerido');
                                EsCorrecto = false;
                            }
                            if ($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val() != '') {
                                if (isNaN($('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val())) {
                                    alertify.error('El precio del producto hijo no es valido');
                                    EsCorrecto = false;
                                }
                            } else {
                                $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val(0);
                            }
                            if ($('#' + Elementos_ConfiguraPaquete.CantidadASalir).val() != '') {
                                if (isNaN($('#' + Elementos_ConfiguraPaquete.CantidadASalir).val())) {
                                    alertify.error('El valor del campo de cantida a salir no es valido');
                                    EsCorrecto = false;
                                }
                            } else {
                                alertify.error('El campo de cantidad a salir es requerido');
                                EsCorrecto = false;
                            }
                            if (EsCorrecto) {
                                $.ajax({
                                    type: "POST",
                                    url: UrlGuardaConfiguracionEdita,
                                    async: false,
                                    data: {
                                        NumeroDeProductoPadre: $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoPadre).val(),
                                        CodigoDeBarrasPadre: $('#' + Elementos_ConfiguraPaquete.CodigoDeBarrasPadre).val(),
                                        PrecioDeProductoPadre: $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val(),
                                        NumeroDeProductoHijo: $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoHijo).val(),
                                        CodigoDeBarrasHijo: $('#' + Elementos_ConfiguraPaquete.CodigoDeBarrasHijo).val(),
                                        PrecioDeProductoHijo: $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val(),
                                        CantidadASalir: $('#' + Elementos_ConfiguraPaquete.CantidadASalir).val()
                                    },
                                    success: function (data) {
                                        if (data.Resultado) {
                                            window.location.href = UrlDetalle + '?NumeroDeProductoPadre=' + $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoPadre).val() + '&NumeroDeProductoHijo=' + $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoHijo).val();
                                        } else {
                                            alertify.error(data.Mensaje);
                                        }
                                    },
                                    error: function () {
                                        alertify.error("Ocurrio un error al realizar la consulta del producto");
                                    }
                                });
                            }
                        });
                        break;
                }
            }
            function ConsultaProducto(ElementoABuscar, EsPadre) {
                $.ajax({
                    type: "POST",
                    url: UrlBuscaPrecioDeProducto,
                    async: false,
                    data: { CodigoONombreDeProducto: ElementoABuscar },
                    success: function (data) {
                        if (EsPadre) {
                            $('#' + Elementos_ConfiguraPaquete.NombreDeProductoPadre).val(data.NombreDeProducto);
                            $('#' + Elementos_ConfiguraPaquete.CodigoDeBarrasPadre).val(data.CodigoDeBarras);
                            $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoPadre).val(data.NumeroDeProducto);
                            $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val(data.PrecioDeProducto);
                        } else {
                            $('#' + Elementos_ConfiguraPaquete.NombreDeProductoHijo).val(data.NombreDeProducto);
                            $('#' + Elementos_ConfiguraPaquete.CodigoDeBarrasHijo).val(data.CodigoDeBarras);
                            $('#' + Elementos_ConfiguraPaquete.NumeroDeProductoHijo).val(data.NumeroDeProducto);
                            $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val(data.PrecioDeProducto);
                        }
                        SumaPreciosDeProductos();
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la consulta del producto");
                    }
                });
                if (EsPadre) {
                    $('#' + Elementos_ConfiguraPaquete.Configura).show();
                }
            }
            function SumaPreciosDeProductos() {
                let PrecioDeProductoPadre = $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoPadre).val();
                if (PrecioDeProductoPadre == '') {
                    PrecioDeProductoPadre = 0;
                } else {
                    if (isNaN(PrecioDeProductoPadre)) {
                        alertify.error('El Precio del Producto Padre no es un número Valido');
                    } else {
                        PrecioDeProductoPadre = parseFloat(PrecioDeProductoPadre);
                    }
                }
                let PrecioDeProductoHijo = $('#' + Elementos_ConfiguraPaquete.PrecioDeProductoHijo).val();
                if (PrecioDeProductoHijo == '') {
                    PrecioDeProductoHijo = 0;
                } else {
                    if (isNaN(PrecioDeProductoHijo)) {
                        alertify.error('El Precio del Producto Hijo no es un número Valido');
                    } else {
                        PrecioDeProductoHijo = parseFloat(PrecioDeProductoHijo);
                    }
                }
                $('#' + Elementos_ConfiguraPaquete.ImporteTotal).val((PrecioDeProductoPadre + PrecioDeProductoHijo).toFixed(2));
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlAutoCompleteProducto: _UrlAutoCompleteProducto,
                    CombosParaTabla: _CombosParaTabla,
                    UrlObtenMarcaPorTipo: _UrlObtenMarcaPorTipo,
                    UrlBuscaPrecioDeProducto: _UrlBuscaPrecioDeProducto,
                    UrlGuardaConfiguracion: _UrlGuardaConfiguracion,
                    UrlGuardaConfiguracionEdita: _UrlGuardaConfiguracionEdita,
                    UrlDetalle: _UrlDetalle
                },
                Inicio: _Inicio,
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlAutoCompleteProducto(ObjetoConfiguracion.UrlAutoCompleteProducto);
            NucleoConfigurado.Configuracion.CombosParaTabla(ObjetoConfiguracion.CombosParaTabla);
            NucleoConfigurado.Configuracion.UrlObtenMarcaPorTipo(ObjetoConfiguracion.UrlObtenMarcaPorTipo);
            NucleoConfigurado.Configuracion.UrlBuscaPrecioDeProducto(ObjetoConfiguracion.UrlBuscaPrecioDeProducto);
            NucleoConfigurado.Configuracion.UrlGuardaConfiguracion(ObjetoConfiguracion.UrlGuardaConfiguracion);
            NucleoConfigurado.Configuracion.UrlGuardaConfiguracionEdita(ObjetoConfiguracion.UrlGuardaConfiguracionEdita);
            NucleoConfigurado.Configuracion.UrlDetalle(ObjetoConfiguracion.UrlDetalle);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = ConfiguraPaquete;
})(window, document);