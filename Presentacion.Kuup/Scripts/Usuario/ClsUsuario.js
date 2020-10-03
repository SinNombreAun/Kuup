(function (windows, document) {
    let Usuario = (function () {
        let _Nucleo = function () {
            let Elementos_Usuario = {
                form: 'formUsuario',
                NombreDePersona: 'fNombreDePersona',
                ApellidoPaterno: 'fApellidoPaterno',
                ApellidoMaterno: 'fApellidoMaterno',
                NombreDeUsuario: 'fNombreDeUsuario',
                CorreoDeUsuario: 'CorreoDeUsuario',
                PasswordUsuario: 'fPasswordUsuario',
                PasswordUsuarioConfirm: 'fPasswordUsuarioConfirm',
                Guardar: 'Guardar'
            };
            function AltaAgenda() {
                let Resultado;
                let Registro = 'fFechaDeInicioDeEvento=' + $('#' + Elementos_Agenda.FechaDeInicioDeEvento).val() + '&';
                if ($('#' + Elementos_Agenda.TodoElDia).is(':checked')) {
                    Registro += 'fFechaDeFinDeEvento=' + $('#' + Elementos_Agenda.FechaDeInicioDeEvento).val() + '&';
                } else {
                    Registro += 'fFechaDeFinDeEvento=' + $('#' + Elementos_Agenda.FechaDeFinDeEvento).val() + '&';
                }
                Registro += 'fCveDeNotifica=' + $('#' + Elementos_Agenda.CveDeNotifica).val() + '&';
                Registro += 'fDescripcion=' + $('#' + Elementos_Agenda.Descripcion).val() + '&';
                $.ajax({
                    type: "POST",
                    url: URLAltaAgenda,
                    async: false,
                    data: Registro,
                    success: function (data) {
                        Resultado = data;
                        if (data.Resultado) {
                            alertify.success(data.Mensaje);
                        } else {
                            alertify.error(data.Mensaje);
                        }
                    },
                    error: function () {
                        alertify.error("Ocurrio un error al realizar la accion Alta");
                    }
                });
                return Resultado;
            }
            return {
                Configuracion: {
                    URLAltaAgenda: _URLAltaAgenda,
                    URLGetAgenda: _URLGetAgenda,
                    URLDeleteAgenda: _URLDeleteAgenda
                },
                Inicio: _Inicio,
                ShowHideFechaFinDeEvento: _ShowHideFechaFinDeEvento
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.URLAltaAgenda(ObjetoConfiguracion.URLAltaAgenda);
            NucleoConfigurado.Configuracion.URLGetAgenda(ObjetoConfiguracion.URLGetAgenda);
            NucleoConfigurado.Configuracion.URLDeleteAgenda(ObjetoConfiguracion.URLDeleteAgenda);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.Objeto = Agenda;
})(window, document);