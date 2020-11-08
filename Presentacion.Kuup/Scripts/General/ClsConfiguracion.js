var ConfiguracionAjax = {
    ajaxSend: 0,
    ajaxComplete: 0,
    selectores: {
        DivMensaje: "div#IconoCargando > div",
        ContenedorMensaje: "#TextoCargando"
    }, textos: {
        TextoPorDefecto: 'Cargando...',
        TextoAlAbortar: 'Petición abortada, sin conexión...'
    },
    bloquearPantallaUI: function (html) {
        $.blockUI({
            message: html,
            overlayCSS: { backgroundColor: '#E0E0E0' },
            css: {
                top: ($(window).height() - 170) / 2 + 'px',
                left: ($(window).width() - 170) / 2 + 'px',
                border: 'none',
                width: '150px',
                backgroundColor: 'rgba(241,235,226,.8)',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px'
            }
        });
    },
    desBloquearPantallaUI: function () {
        $.unblockUI();
    },
    MensajeCargando: function (toggle, mensaje) {
        var div = $(ConfiguracionAjax.selectores.DivMensaje)
        if (!div.length) {
            console.error("No existe div de icono cargando...");
        }
        if (mensaje) {
            $(ConfiguracionAjax.selectores.ContenedorMensaje, div).html(mensaje);
        } else {
            $(ConfiguracionAjax.selectores.ContenedorMensaje, div).html(ConfiguracionAjax.textos.TextoPorDefecto);
        }
        if (toggle != "undefined") {
            if (toggle) {
                div.css("display", "block");
            } else {
                div.css("display", "none");
            }
        }
    },
    RegistraConfiguracionAjax: function () {
        var that = this
        $.ajaxSetup({
            beforeSend: function () {
                // Mensaje "Cargando..." u otro especificado
                if ($(ConfiguracionAjax.selectores.DivMensaje).is(':visible')) {
                    $(ConfiguracionAjax.selectores.DivMensaje).addClass("iconoVisible");
                }
                that.MensajeCargando(true);
            }
        })
        $(document)
            .ajaxStart(function () {
                //alertify.log("Ajax Start")
                // Mensaje "Cargando..." u otro especificado
                //that.MensajeCargando(true)
            })
            .ajaxSend(function (event, jqxhr, settings) {
                //alertify.log("Ajax Send: " + settings.url)
                var res;
                ConfiguracionAjax.ajaxSend += 1
                //settings.data += "&__RequestVerificationToken=" + encodeURIComponent($('input[name="__RequestVerificationToken"]').val());
                if (settings.configuracion) { // configuración personalizada
                    var c = settings.configuracion
                    if (c.mensaje) {
                        that.MensajeCargando("undefined", c.mensaje)
                    }
                    if (c.bloqueaPantalla) {
                        that.MensajeCargando(false, c.mensaje)
                        that.bloquearPantallaUI($(ConfiguracionAjax.selectores.DivMensaje).clone());
                    }
                }
            })
            .ajaxComplete(function (event, jqxhr, settings) {
                // Oculta mensaje
                ConfiguracionAjax.ajaxComplete += 1
                if ($(ConfiguracionAjax.selectores.DivMensaje).hasClass("iconoVisible") && (ConfiguracionAjax.ajaxSend == ConfiguracionAjax.ajaxComplete)) {
                    $(ConfiguracionAjax.selectores.DivMensaje).removeClass("iconoVisible");
                    that.MensajeCargando(false);
                    ConfiguracionAjax.ajaxComplete = 0
                    ConfiguracionAjax.ajaxSend = 0
                }
                if (settings.configuracion) { // configuración personalizada
                    var c = settings.configuracion
                    if (c.bloqueaPantalla) {
                        that.desBloquearPantallaUI();
                    }
                }
            })
            .ajaxStop(function (event, jqxhr, settings) {
                that.MensajeCargando(false)
                if (settings) { // configuración personalizada
                    var c = settings.configuracion
                    if (c.bloqueaPantalla) {
                        that.desBloquearPantallaUI();
                    }
                }
            })
    },
    init: function (opciones) {
        if (opciones) {
            if (opciones.selectores) {
                $.fn.extend(this.selectores, opciones.selectores);
            }
            if (opciones.textos) {
                $.fn.extend(this.textos, opciones.textos);
            }
            if (opciones.urls) {
                $.fn.extend(this.urls, opciones.urls);
            }
        }
    }
}
/*
* Ejemplo:
  , configuracion: {
    mensaje: "Ejecutando proceso..."
    , sinPruebaConexion: true
    , textoAborto: "Proceso abortado..."
    , bloqueaPantalla: true
  }
*/