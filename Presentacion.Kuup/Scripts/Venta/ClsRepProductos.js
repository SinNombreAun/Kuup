(function (windows, document) {
    let Productos = (function () {
        let _Nucleo = function () {
            let Elementos_RepProductos = {
                ProductosVendidos: 'ProductosVendidos',
                Ventas: 'Ventas',
                FechaInicial: 'fFechaInicial',
                FechaFinal: 'fFechaFinal',
                Consulta: 'Consulta',
                Guardar: 'Guardar',
                GeneraReporte: 'GeneraReporte'
            };
            let Funcionalidad = '',
                UrlCargaGrid = '',
                UrlGeneraReporte = '',
                FechaInicialLimite = '',
                TablaProductos = null;
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
            let _UrlGeneraReporte = function (UrlGeneraReporteSet) {
                if (typeof (UrlGeneraReporteSet) != 'undefined') {
                    UrlGeneraReporte = UrlGeneraReporteSet;
                } else {
                    return UrlGeneraReporte;
                }
            }
            let _FechaInicialLimite = function (FechaInicialLimiteSet) {
                if (typeof (FechaInicialLimiteSet) != 'undefined') {
                    FechaInicialLimite = FechaInicialLimiteSet;
                } else {
                    return FechaInicialLimite;
                }
            }
            let _Inicio = function () {
                OcultaCampos();
                AgregaEvento();
                EjecutaEventosIniciales();
                $('.datetimepicker').datepicker({
                    minDate: FechaInicialLimite
                });
                if (Funcionalidad == 'INDEX') {
                    GeneraGridDeProductosVendidos();
                }
            };
            function GeneraGridDeProductosVendidos() {
                TablaProductos = $('#' + Elementos_RepProductos.ProductosVendidos).DataTable({
                    "destroy": true,
                    "responsive": false,
                    "scrollY": 300,
                    "scrollX": true,
                    "scrollCollapse": false,
                    "lengthChange": false,
                    "pageLength": 10000000,
                    "order": [[4, 'desc']],
                    "columns": [
                        { "data": "NumeroDeProducto" },
                        { "data": "CodigoDeBarras" },
                        { "data": "NombreDeProducto" },
                        { "data": "CantidadVendida" },
                        { "data": "TextoDeEstatus" }
                    ],
                    "language": LenguajeEN()
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
                    case 'INDEX':
                        $('#' + Elementos_RepProductos.FechaInicial).change(function () {
                            $('#' + Elementos_RepProductos.FechaFinal).datepicker("destroy");
                            $('#' + Elementos_RepProductos.FechaFinal).datepicker({
                                minDate: $('#' + Elementos_RepProductos.FechaInicial).val()
                            });
                        });
                        $('#' + Elementos_RepProductos.Consulta).click(function () {
                            $.ajax({
                                type: "POST",
                                url: UrlCargaGrid,
                                //async: false,
                                data: { fFechaInicial: $('#' + Elementos_RepProductos.FechaInicial).val(), fFechaFinal: $('#' + Elementos_RepProductos.FechaFinal).val() },
                                success: function (data) {
                                    debugger
                                    TablaProductos.clear().draw();
                                    if (data.data.length > 0) {
                                        for (var i = 0; i < data.data.length; i++) {
                                            TablaProductos.row.add({
                                                "NumeroDeProducto": data.data[i].NumeroDeProducto,
                                                "CodigoDeBarras": data.data[i].CodigoDeBarras,
                                                "NombreDeProducto": data.data[i].NombreDeProducto,
                                                "CantidadVendida": data.data[i].CantidadVendida,
                                                "TextoDeEstatus": data.data[i].TextoDeEstatus
                                            }).draw();
                                        }
                                    }
                                },
                                error: function () {
                                    alertify.error("Ocurrio un error al realizar la accion de Consulta");
                                }
                            });
                        });
                        $('#' + Elementos_RepProductos.GeneraReporte).click(function () {
                            let UrlGeneraReporteParam = "/?fFechaInicial=" + $('#' + Elementos_RepProductos.FechaInicial).val()
                                + "&fFechaFinal=" + $('#' + Elementos_RepProductos.FechaFinal).val() 
                                + "&Tipo=PDF" 
                            window.open(UrlGeneraReporte + UrlGeneraReporteParam, "_blank");
                        });
                        break;
                    case 'ALTA':
                        break;
                    case 'DETALLE':
                        break;
                }
            }
            return {
                Configuracion: {
                    Funcionalidad: _Funcionalidad,
                    UrlCargaGrid: _UrlCargaGrid,
                    UrlGeneraReporte: _UrlGeneraReporte,
                    FechaInicialLimite: _FechaInicialLimite
                },
                Inicio: _Inicio,
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.Funcionalidad(ObjetoConfiguracion.Funcionalidad);
            NucleoConfigurado.Configuracion.UrlCargaGrid(ObjetoConfiguracion.UrlCargaGrid);
            NucleoConfigurado.Configuracion.UrlGeneraReporte(ObjetoConfiguracion.UrlGeneraReporte);
            NucleoConfigurado.Configuracion.FechaInicialLimite(ObjetoConfiguracion.FechaInicialLimite);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Productos;
})(window, document);