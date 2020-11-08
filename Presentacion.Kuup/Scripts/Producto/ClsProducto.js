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
                CveEstatus: 'fCveEstatus',
                Guardar: 'Guardar',
                GeneraCodigoDeBarras: 'fGeneraCodigoDeBarras',
                Tabla: 'Tabla',
                TablaMayoreo: 'TablaMayoreo',
                CargaMasiva: 'CargaMasiva',
                AgregarPrecioM: 'AgregarPrecioM',
                AgregaPrecioMayoreo: 'AgregaPrecioMayoreo',
                CantidadMinimaM: 'fCantidadMinimaM',
                CantidadMaxima: 'fCantidadMaxima',
                PrecioDeMayoreo: 'fPrecioDeMayoreo',
                GuardaPrecioM: 'GuardaPrecioM'
            };
            let Funcionalidad = '',
                NumeroDeProductoModel = '',
                CodigoDeBarrasModel = '',
                AplicarPrecioDeMayoreo = '',
                UrlCargaGrid = '',
                UrlDetalle = '',
                UrlCargaMasiva = '',
                UrlDescargaDeArchivo = '',
                UrlGuardaTablaMayoreo = '',
                UrlCargaMayoreo = '',
                TablaM = null;
            let _Funcionalidad = function (FuncionalidadSet) {
                if (typeof (FuncionalidadSet) != 'undefined') {
                    Funcionalidad = FuncionalidadSet;
                } else {
                    return Funcionalidad;
                }
            };
            let _NumeroDeProductoModel = function (NumeroDeProductoModelSet) {
                if (typeof (NumeroDeProductoModelSet) != 'undefined') {
                    NumeroDeProductoModel = NumeroDeProductoModelSet;
                } else {
                    return NumeroDeProductoModel;
                }
            };
            let _CodigoDeBarrasModel = function (CodigoDeBarrasModelSet) {
                if (typeof (CodigoDeBarrasModelSet) != 'undefined') {
                    CodigoDeBarrasModel = CodigoDeBarrasModelSet;
                } else {
                    return CodigoDeBarrasModel;
                }
            };
            let _AplicarPrecioDeMayoreo = function (AplicarPrecioDeMayoreoSet) {
                if (typeof (AplicarPrecioDeMayoreoSet) != 'undefined') {
                    AplicarPrecioDeMayoreo = AplicarPrecioDeMayoreoSet;
                } else {
                    return AplicarPrecioDeMayoreo;
                }
            }
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
            let _UrlGuardaTablaMayoreo = function (UrlGuardaTablaMayoreoSet) {
                if (typeof (UrlGuardaTablaMayoreoSet) != 'undefined') {
                    UrlGuardaTablaMayoreo = UrlGuardaTablaMayoreoSet;
                } else {
                    return UrlGuardaTablaMayoreo;
                }
            }
            let _UrlCargaMayoreo = function (UrlCargaMayoreoSet) {
                if (typeof (UrlCargaMayoreoSet) != 'undefined') {
                    UrlCargaMayoreo = UrlCargaMayoreoSet;
                } else {
                    return UrlCargaMayoreo;
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
                if (Funcionalidad == 'DETALLE') {
                    GeneraGridDeMayoreo();
                    if (AplicarPrecioDeMayoreo == 'SI') {
                        $('#' + Elementos_Producto.TablaMayoreo).parent().parent().parent().parent().parent().hide();
                        $('#TablaMayoreo_info').hide();
                    } else {
                        $.ajax({
                            type: "POST",
                            url: UrlCargaMayoreo,
                            async: false,
                            data: {NumeroDeProducto: NumeroDeProductoModel, CodigoDeBarras: CodigoDeBarrasModel },
                            success: function (data) {
                                if (data.data.length > 0) {
                                    for (var i = 0; i < data.data.length; i++) {
                                        TablaM.row.add({
                                            "NumeroDeMayoreo": data.data[i].NumeroDeMayoreo,
                                            "CantidadMinima": data.data[i].CantidadMinima,
                                            "CantidadMaxima": data.data[i].CantidadMaxima,
                                            "PrecioDeMayoreo": parseFloat(data.data[i].PrecioDeMayoreo).toFixed(2)
                                        }).draw();
                                    }
                                }
                            },
                            error: function () {
                                alertify.error("Ocurrio un error al realizar la accion de carga de archivo");
                            }
                        });
                    }
                    $('#' + Elementos_Producto.GuardaPrecioM).hide();
                }
            };
            function GeneraGridDeMayoreo() {
                TablaM = $('#' + Elementos_Producto.TablaMayoreo).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 300,
                    "scrollX": true,
                    "select": true,
                    "scrollCollapse": false,
                    "fixedColumns": {
                        "leftColumns": 2,
                        "rightColumns": 1
                    },
                    "columns": [
                        { "data": "NumeroDeMayoreo" },
                        { "data": "CantidadMinima" },
                        { "data": "CantidadMaxima" },
                        { "data": "PrecioDeMayoreo" },
                        { "defaultContent": "<button type='button' class='btndelete btn btn-danger'><i class='far fa-trash-alt'></i></button>" }
                    ],
                    "paging": false,
                    "searching": false,
                    "language": LenguajeEN()
                });
                RemoveProducto('#' + Elementos_Producto.TablaMayoreo + ' tbody', TablaM);
            }
            var RemoveProducto = function (tbody, table) {
                $(tbody).on('click', 'button.btndelete', function () {
                    table.row($(this).parents('tr')).remove().draw();
                    let data = table.row($(this).parents('tr')).data();
                    if (typeof (data) == 'undefined') {
                        table.row($(this).parents('li')).remove().draw();
                    }
                    $('#' + Elementos_Producto.GuardaPrecioM).show();
                });
            }
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
                        { "data": "TextoDeEstatus" },
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
                            $('#' + Elementos_Producto.Tabla).parent().parent().parent().parent().parent().hide();
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
                        break;
                    case 'DETALLE':
                        $('#' + Elementos_Producto.CveAviso).change();
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
                        $('#' + Elementos_Producto.AgregarPrecioM).click(function () {
                            CreaDialog(Elementos_Producto.AgregaPrecioMayoreo, 'Registra Precio de Mayoreo', CreaBotonesDialog(), 400, 420);
                            $('#' + Elementos_Producto.AgregaPrecioMayoreo).dialog('open');
                        });
                        $('#' + Elementos_Producto.GuardaPrecioM).click(function () {
                            let TablaRows = TablaM.rows().data();
                            let JsonObj = [];
                            for (var i = 0; i < TablaRows.rows()[0].length; i++) {
                                JsonObj.push(TablaM.rows(i).data()[0]);
                            }
                            let JsonString = '';
                            if (JsonObj.length != 0) {
                                JsonString = JSON.stringify(JsonObj);
                            }
                            $.ajax({
                                type: "POST",
                                url: UrlGuardaTablaMayoreo,
                                async: false,
                                data: {
                                    NumeroDeProducto: NumeroDeProductoModel,
                                    CodigoDeBarras: CodigoDeBarrasModel,
                                    Mayoreos: JsonString
                                },
                                success: function (data) {

                                },
                                error: function () {
                                    alertify.error("Ocurrio un error al realizar la eliminación del registro");
                                }
                            });
                        });
                        break;
                }
            }
            function CreaBotonesDialog() {
                return {
                    'Cancelar': {
                        id: 'Cancelar',
                        text: 'Cancelar',
                        class: 'btn btn-danger',
                        click: function () {
                            $('#' + Elementos_Producto.AgregaPrecioMayoreo).dialog('close');
                            LimpiaMayoreo();
                        }
                    },
                    'Agregar': {
                        id: 'Agregar',
                        text: 'Agregar',
                        class: 'btn btn-primary',
                        click: function () {
                            let Continua = false;
                            if ($('#' + Elementos_Producto.CantidadMinimaM).val() != '') {
                                if (!isNaN($('#' + Elementos_Producto.CantidadMinimaM).val())) {
                                    Continua = true;
                                } else {
                                    alertify.error('El campo Cantidad Minima debe ser de tipo numerico');
                                    Continua = false;
                                }
                            } else {
                                alertify.error('El campo Cantidad Minima es requerido');
                                Continua = false;
                            }
                            if (Continua) {
                                if ($('#' + Elementos_Producto.CantidadMaxima).val() != '') {
                                    if (!isNaN($('#' + Elementos_Producto.CantidadMaxima).val())) {
                                        Continua = true;
                                    } else {
                                        alertify.error('El campo Cantidad Maxima debe ser de tipo numerico');
                                        Continua = false;
                                    }
                                } else {
                                    $('#' + Elementos_Producto.CantidadMaxima).val(0);
                                }
                            }
                            if (Continua) {
                                if ($('#' + Elementos_Producto.CantidadMaxima).val() != 0) {
                                    if (parseInt($('#' + Elementos_Producto.CantidadMaxima).val()) > parseInt($('#' + Elementos_Producto.CantidadMinimaM).val())) {
                                        Continua = true;
                                    } else {
                                        alertify.error('El campo Cantidad Minima no debe ser mayor al campo Cantidad Maxima');
                                        Continua = false;
                                    }
                                }
                            }
                            if (Continua) {
                                if ($('#' + Elementos_Producto.PrecioDeMayoreo).val() != '') {
                                    if ($('#' + Elementos_Producto.PrecioDeMayoreo).val() != 0) {
                                        let TablaRows = TablaM.rows().data();
                                        TablaM.row.add({
                                            "NumeroDeMayoreo": TablaRows.rows()[0].length + 1,
                                            "CantidadMinima": $('#' + Elementos_Producto.CantidadMinimaM).val(),
                                            "CantidadMaxima": $('#' + Elementos_Producto.CantidadMaxima).val(),
                                            "PrecioDeMayoreo": parseFloat($('#' + Elementos_Producto.PrecioDeMayoreo).val()).toFixed(2)
                                        }).draw();
                                        $('#TablaMayoreo_info').show();
                                        $('#' + Elementos_Producto.TablaMayoreo).parent().parent().parent().parent().parent().show();
                                        TablaM.columns.adjust().draw();
                                        $('#' + Elementos_Producto.AgregaPrecioMayoreo).dialog('close');
                                        LimpiaMayoreo();
                                        $('#' + Elementos_Producto.GuardaPrecioM).show();
                                    } else {
                                        alertify.error('El campo Precio de Mayoreo debe ser mayor a 0');
                                    }
                                } else {
                                    alertify.error('El campo Precio de Mayoreo es requerido');
                                }
                            }
                        }
                    }
                }
            }
            function LimpiaMayoreo() {
                $('#' + Elementos_Producto.CantidadMinimaM).val('');
                $('#' + Elementos_Producto.CantidadMaxima).val('');
                $('#' + Elementos_Producto.PrecioDeMayoreo).val('');
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
                    NumeroDeProductoModel: _NumeroDeProductoModel,
                    CodigoDeBarrasModel: _CodigoDeBarrasModel,
                    AplicarPrecioDeMayoreo: _AplicarPrecioDeMayoreo,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlDetalle: _UrlDetalle,
                    UrlCargaMasiva: _UrlCargaMasiva,
                    UrlDescargaDeArchivo: _UrlDescargaDeArchivo,
                    UrlGuardaTablaMayoreo: _UrlGuardaTablaMayoreo,
                    UrlCargaMayoreo: _UrlCargaMayoreo
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
            NucleoConfigurado.Configuracion.NumeroDeProductoModel(ObjetoConfiguracion.NumeroDeProductoModel);
            NucleoConfigurado.Configuracion.CodigoDeBarrasModel(ObjetoConfiguracion.CodigoDeBarrasModel);
            NucleoConfigurado.Configuracion.AplicarPrecioDeMayoreo(ObjetoConfiguracion.AplicarPrecioDeMayoreo);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlDetalle(ObjetoConfiguracion.UrlDetalle);
            NucleoConfigurado.Configuracion.UrlCargaMasiva(ObjetoConfiguracion.UrlCargaMasiva);
            NucleoConfigurado.Configuracion.UrlDescargaDeArchivo(ObjetoConfiguracion.UrlDescargaDeArchivo);
            NucleoConfigurado.Configuracion.UrlGuardaTablaMayoreo(ObjetoConfiguracion.UrlGuardaTablaMayoreo);
            NucleoConfigurado.Configuracion.UrlCargaMayoreo(ObjetoConfiguracion.UrlCargaMayoreo);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Producto;
})(window, document);