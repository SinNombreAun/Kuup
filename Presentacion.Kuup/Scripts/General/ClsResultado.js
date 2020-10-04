(function (windows, document) {
    let Resultado = (function () {
        let _Nucleo = function () {
            let ObjetoResultado = '';
            let _ObjetoResultado = function (ObjetoResultadoSet) {
                if (typeof (ObjetoResultadoSet) != 'undefined') {
                    ObjetoResultado = ObjetoResultadoSet;
                } else {
                    return ObjetoResultado;
                }
            };
            let _GeneraMensaje = function () {
                if (ParseaJson()) {
                    if (ObjetoResultado.Adicional != null) {
                        alertify.log(ObjetoResultado.Mensaje);
                    } else if (ObjetoResultado.Resultado != true) {
                        alertify.error(ObjetoResultado.Mensaje,0);
                    } else {
                        alertify.success(ObjetoResultado.Mensaje);
                    }
                }
            };
            function ParseaJson() {
                if (ObjetoResultado != '') {
                    ObjetoResultado = jQuery.parseJSON(ObjetoResultado);
                    if (ObjetoResultado == null) {
                        ObjetoResultado = '';
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return false;
                }
            }
            return {
                Configuracion: {
                    ObjetoResultado: _ObjetoResultado
                },
                GeneraMensaje: _GeneraMensaje
            }
        }
        let _Constructor = function (ObjetoConfiguracion) {
            let NucleoConfigurado = _Nucleo();
            NucleoConfigurado.Configuracion.ObjetoResultado(ObjetoConfiguracion.ObjetoResultado);
            return NucleoConfigurado;
        }
        return {
            Nucleo: _Nucleo,
            Constructor: _Constructor
        }
    })();
    window.ObjetoResult = Resultado;
})(window, document);