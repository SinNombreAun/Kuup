(function (windows, document) {
    let ConfiguraPaquete = (function () {
        let _Nucleo = function () {
            let Elementos_ConfiguraPaquete = {
                CodigoONombreDeProducto: 'fCodigoONombreDeProducto',
                Tabla: 'Tabla'
            };
            let Funcionalidad = '',
                UrlCargaGrid = '',
                UrlAutoCompleteProducto = '';
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
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
                if (Funcionalidad == 'INDEX') {
                    GeneraGridDeConfiguraPaquete();
                }
            };
            function GeneraGridDeConfiguraPaquete() {
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
                    "data": null,
                    "order": [[1, 'desc']],
                    "columns": [
                        { "data": "CodigoDeBarrasPadre" },
                        { "data": "NombreDeProductoPadre" },
                        { "data": "PrecioDeProductoPadre" },
                        { "data": "CodigoDeBarrasHijo" },
                        { "data": "NombreDeProductoHijo" },
                        { "data": "PrecioDeProductoHijo" },
                        { "data": "ImporteTotal" }
                    ],
                    "rowGroup": {
                        "dataSrc": 'NombreDeProductoPadre'
                    },
                    "language": LenguajeEN()
                });
                table.draw();
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
                    case 'INDEX':
                        break;
                    case 'ALTA':
                        $('#' + Elementos_ConfiguraPaquete.CodigoONombreDeProducto).autocomplete({
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
                        break;
                    case 'DETALLE':
                        break;
                }
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlAutoCompleteProducto: _UrlAutoCompleteProducto
                },
                Inicio: _Inicio,
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlAutoCompleteProducto(ObjetoConfiguracion.UrlAutoCompleteProducto);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = ConfiguraPaquete;
})(window, document);