// Constantes
const cnsToast = Swal.mixin({
    toast: true,
    timer: 5000,
    width: '300px',
    position: 'bottom-end',
    allowOutsideClick: false,
    allowEnterKey: false,
    timerProgressBar: true,
    showCloseButton: true,
    showConfirmButton: false
});

const cnsConfirm = Swal.mixin({
    width: '350px',
    confirmButtonColor: '#17a2b8',
    denyButtonColor: '#6c757d',
    confirmButtonText: '<span class="fas fa-check mr-2"></span> Si',
    denyButtonText: '<span class="fas fa-times mr-2"></span> No',
    customClass: {
        confirmButton: 'btn btn-info btn-sm my-0',
        denyButton: 'btn btn-secondary btn-sm my-0'
    },
    allowOutsideClick: false,
    allowEnterKey: false,
    showCloseButton: true,
    showDenyButton: true,
    focusDeny: true
});

const cnsAlert = Swal.mixin({
    width: '350px',
    confirmButtonColor: '#17a2b8',
    confirmButtonText: '<span class="fas fa-check mr-2"></span> Aceptar',
    customClass: {
        confirmButton: 'btn btn-info btn-sm my-0'
    },
    allowOutsideClick: false,
    allowEnterKey: false,
    showCloseButton: true
});

const cnsDataTableOptions = {
    sort: false,
    autoWidth: false,
    responsive: true,
    iDisplayLength: 50,
    language: { url: "/Content/plugins/datatables/jquery.dataTables.es.json" },
    dom: "<'row'<'col-sm - 12'f>><'row'<'col-sm-12't>><'row'<'col-sm-5'i><'col-sm-7'p>>"
};

const cnsHeadersFetch = {
    'Accept': 'application/json',
    'Content-Type': 'application/json'
}

// Funciones
const fnUrlBase = () => $('meta[name=URL_BASE]').attr('content');

const fnCrearTag = (psTipo, pobjOpciones = null) => {
    let vobjTag = $(`<${psTipo}></${psTipo}>`);
    if (pobjOpciones == null) return vobjTag;

    if (pobjOpciones.sId) vobjTag.attr("id", pobjOpciones.sId);
    if (pobjOpciones.sClase) vobjTag.addClass(pobjOpciones.sClase);
    if (pobjOpciones.sTipo) vobjTag.attr("type", pobjOpciones.sTipo);
    if (pobjOpciones.lActivo) vobjTag.attr("checked", pobjOpciones.lActivo);
    if (pobjOpciones.sTexto) vobjTag.text(pobjOpciones.sTexto);
    if (pobjOpciones.sTitulo) vobjTag.attr("title", pobjOpciones.sTitulo);
    if (pobjOpciones.sIcono) vobjTag.prepend(fnCrearTag("i").addClass(pobjOpciones.sIcono));
    if (pobjOpciones.lstData) pobjOpciones.lstData.map(vobjData => vobjTag.attr(vobjData.sNombre, vobjData.sValor));
    return vobjTag;
};

const fnCrearDropdown = (plstOpciones) => {
    let vobjBoton = fnCrearTag("button", { sClase: "btn btn-info btn-xs dropdown-toggle", sIcono: "fas fa-cog" })
        .attr("data-toggle", "dropdown").attr("aria-haspopup", "true").attr("aria-expanded", "false");

    let vobjMenu = fnCrearTag("div").addClass("dropdown-menu dropdown-menu-right")
    plstOpciones.map((vobjOpcion) => vobjMenu = vobjMenu.append(fnCrearTag("button", vobjOpcion)));

    return fnCrearTag("div").addClass("btn-group").append(vobjBoton).append(vobjMenu);
};

function fnEvento(psAccion, psIdClase, pobjFuncion) {
    let vobjElemento = document.getElementById(psIdClase);
    if (vobjElemento) {
        vobjElemento.addEventListener(psAccion, pobjFuncion);
    } else {
        vobjElemento = document.getElementsByClassName(psIdClase);
        for (let vobjItem of vobjElemento) {
            vobjItem.addEventListener(psAccion, pobjFuncion);
        }
    }
}

function fnRedondear(pnNumero, pnDecimales) {
    var vnFlotante = parseFloat(pnNumero);
    var vnResultado = Math.round(vnFlotante * Math.pow(10, pnDecimales)) / Math.pow(10, pnDecimales);
    return vnResultado;
}

function fnDeshabilitarPegado(psIdClase) {
    $(document).on("paste ", psIdClase, function (poEvent) {
        poEvent.preventDefault();
    });
}

const fnGet = (psRuta, pobjParametros) => new Promise((resolve, reject) => {
    fetch(`${fnUrlBase()}${psRuta}?${new URLSearchParams(pobjParametros)}`, {
            method: "GET",
            headers: cnsHeadersFetch
        })
        .then(pobjHttpResponse => {
            if (pobjHttpResponse.status == 200) {
                resolve(pobjHttpResponse.json());
            } else if (pobjHttpResponse.status == 401) {
                fnError("Lo sentimos, su sesión ha finalizado. Inicie sesión nuevamente.");
                fnProcesando(false);
                setTimeout(function () { window.location.replace(fnUrlBase());; }, 5000);
            } else if (pobjHttpResponse.status == 403) {
                fnError("Lo sentimos, usted no tiene permiso para ejecutar la acción requerida.") // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            } else if (pobjHttpResponse.status == 404) {
                fnError("Lo sentimos, el recurso al que intenta acceder no existe o no se encuentra disponible."); // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            }
        })
        .catch(psError => {
            fnProcesando(false);
            reject(psError);
        })
});

const fnGetFile = (psRuta, pobjParametros, psMensajeError = "") => {
    if (psMensajeError == "")
        psMensajeError = "Ocurrió un problema al descargar el archivo."

    fetch(`${fnUrlBase()}${psRuta}?${new URLSearchParams(pobjParametros)}`, {
        method: "GET",
        headers: cnsHeadersFetch
    }).then(pobjResp => {
        let vsArchivoNombre = "DownloadSpine.png";
        let voDisposition = pobjResp.headers.get('Content-Disposition');

        if (!voDisposition || voDisposition === null || voDisposition.indexof('attachment') === -1) {
            fnProcesando(false);
            fnAdvertencia(psMensajeError);
            return;
        }

        let vobjFileNameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
        let varrMatches = vobjFileNameRegex.exec(voDisposition);
        if (varrMatches != null && varrMatches[1]) {
            vsArchivoNombre = varrMatches[1].replace(/['"]/g, '');
        }

        pobjResp.blob().then(pobjBlob => {
            let vsUrl = window.URL.createObjectURL(pobjBlob);
            let vobjEnlace = document.createElement('a');
            vobjEnlace.href = vsUrl;
            vobjEnlace.download = vsArchivoNombre;
            document.body.appendChild(vobjEnlace); // we need to append the element to the dom -> otherwise it will not work in firefox
            vobjEnlace.click();
            vobjEnlace.remove();
            window.URL.revokeObjectURL(vsUrl);
        });

        fnProcesando(false);
    }).catch(() => {
        fnProcesando(false);
        fnAdvertencia(psMensajeError);
    });
};

const fnPost = (psRuta, pobjObjeto) => new Promise((resolve, reject) => {
    fetch(`${fnUrlBase()}${psRuta}`, {
            method: "POST",
            headers: cnsHeadersFetch,
            body: JSON.stringify(pobjObjeto)
        })
        .then(pobjHttpResponse => {
            if (pobjHttpResponse.status == 200) {
                resolve(pobjHttpResponse.json());
            } else if (pobjHttpResponse.status == 401) {
                fnError("Lo sentimos, su sesión ha finalizado. Inicie sesión nuevamente.");
                fnProcesando(false);
                setTimeout(function () { window.location.replace(fnUrlBase());; }, 5000);
            } else if (pobjHttpResponse.status == 403) {
                fnError("Lo sentimos, usted no tiene permiso para ejecutar la acción requerida.") // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            } else if (pobjHttpResponse.status == 404) {
                fnError("Lo sentimos, el recurso al que intenta acceder no existe o no se encuentra disponible."); // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            }
        })
        .catch(psError => {
            fnProcesando(false);
            reject(psError);
        })
});

const fnPostForm = (psRuta, psForm) => new Promise((resolve, reject) => {
    fetch(`${fnUrlBase()}${psRuta}`, {
        method: "POST",
        headers: cnsHeadersFetch,
        body: $(psForm).serialize()
    }).then(vobjResult => vobjResult.json())
        .then(pobjApiRpta => resolve(pobjApiRpta))
        .catch(psError => {
            fnProcesando(false);
            reject(psError);
        })
});

const fnPut = (psRuta, pobjObjeto) => new Promise((resolve, reject) => {
    fetch(`${fnUrlBase()}${psRuta}`, {
            method: "PUT",
            headers: cnsHeadersFetch,
            body: JSON.stringify(pobjObjeto)
        })
        .then(pobjHttpResponse => {
            if (pobjHttpResponse.status == 200) {
                resolve(pobjHttpResponse.json());
            } else if (pobjHttpResponse.status == 401) {
                fnError("Lo sentimos, su sesión ha finalizado. Inicie sesión nuevamente.");
                fnProcesando(false);
                setTimeout(function () { window.location.replace(fnUrlBase());; }, 5000);
            } else if (pobjHttpResponse.status == 403) {
                fnError("Lo sentimos, usted no tiene permiso para ejecutar la acción requerida.") // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            } else if (pobjHttpResponse.status == 404) {
                fnError("Lo sentimos, el recurso al que intenta acceder no existe o no se encuentra disponible."); // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            }
        })
        .catch(psError => {
            fnProcesando(false);
            reject(psError);
        })
});



const fnPatch = (psRuta, pobjObjeto) => new Promise((resolve, reject) => {
    fetch(`${fnUrlBase()}${psRuta}`, {
            method: "PATCH",
            headers: cnsHeadersFetch,
            body: JSON.stringify(pobjObjeto)
        })
        .then(pobjHttpResponse => {
            if (pobjHttpResponse.status == 200) {
                resolve(pobjHttpResponse.json());
            } else if (pobjHttpResponse.status == 401) {
                fnError("Lo sentimos, su sesión ha finalizado. Inicie sesión nuevamente.");
                fnProcesando(false);
                setTimeout(function () { window.location.replace(fnUrlBase());; }, 5000);
            } else if (pobjHttpResponse.status == 403) {
                fnError("Lo sentimos, usted no tiene permiso para ejecutar la acción requerida.") // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            } else if (pobjHttpResponse.status == 404) {
                fnError("Lo sentimos, el recurso al que intenta acceder no existe o no se encuentra disponible."); // <br/>Recurso: '" + psRuta + "'");
                fnProcesando(false);
            }
        })
        .catch(psError => {
            fnProcesando(false);
            reject(psError);
        })
});

const fnDelete = (psRuta, pobjParametros) => new Promise((resolve, reject) => {
    fetch(`${fnUrlBase()}${psRuta}?${new URLSearchParams(pobjParametros)}`, {
        method: "DELETE",
        headers: cnsHeadersFetch
    }).then(pobjResp => pobjResp.json())
        .then(pobjApiRpta => resolve(pobjApiRpta))
        .catch(psError => {
            fnProcesando(false);
            reject(psError);
        })
});



function fnGetFileOld(psRuta, pobjParametros, psMensajeError = "") {
    return $.ajax({
        type: "GET",
        url: fnUrlBase() + psRuta,
        data: pobjParametros,
        xhrFields: { responseType: "blob" },
        success: function (data, textStatus, request) {
            var vsContentType = request.getResponseHeader("content-type");
            //if (vsContentType == "application/octet-stream") {
            var vsFileName = "";
            var voDisposition = request.getResponseHeader('Content-Disposition');
            if (voDisposition != null) {
                if (voDisposition && voDisposition.indexOf('attachment') !== -1) {
                    var voFilenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                    var varrMatches = voFilenameRegex.exec(voDisposition);
                    if (varrMatches != null && varrMatches[1]) {
                        vsFileName = varrMatches[1].replace(/['"]/g, '');
                    }
                }
                var vsArchivoNombre = vsFileName; // request.getResponseHeader("sArchivoNombre");
                var voControAnchor = document.createElement("a");
                var vsUrl = window.URL.createObjectURL(data);
                voControAnchor.href = vsUrl;
                voControAnchor.download = vsArchivoNombre;
                voControAnchor.click();
                window.URL.revokeObjectURL(vsUrl);
            } else {
                if (psMensajeError == "")
                    psMensajeError = "Ocurrió un problema al descargar el archivo."
                fnAdvertencia(psMensajeError);
            }
            //}
            fnProcesando(false);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            fnProcesando(false);
            var vobjApiRpta = JSON.parse(xhr.getResponseHeader("sData"));
            fnVerificarApiRpta(vobjApiRpta);
        }
    });
}

const fnGetLoad = (psIdContenedor, psRuta) => new Promise((resolve, reject) => {
    fetch(`${fnUrlBase()}${psRuta}`, {
        method: "GET",
    }).then(pobjResponse => pobjResponse.text())
        .then(psHTML => {
            $(psIdContenedor).html(psHTML);
            resolve();
        })
        .catch(psError => {
            fnProcesando(false);
            reject(psError);
        })
});



//function fnGetLoad(psIdContenedor, psRuta, pfnFuncion) {
//    $(psIdContenedor).load(fnUrlBase() + psRuta, function () {
//        pfnFuncion();
//    });
//}


//function fnGetLoad(psIdContenedor, psRuta, pfnFuncion) {
//    $(psIdContenedor).load(fnUrlBase() + psRuta, function () {
//        pfnFuncion();
//    });
//}
function fnVerificarApiRpta(pobjApiRpta) {
    if (pobjApiRpta.iTipo == 0) {
        let vsError = pobjApiRpta.sMensaje;
        if (pobjApiRpta.nError > 0)
            vsError = vsError + ". (" + pobjApiRpta.nError + ")";
        fnError(vsError);
        return false;
    } else if (pobjApiRpta.iTipo == 2) {
        fnAdvertencia(pobjApiRpta.sMensaje);
        return false;
    } else {
        return true;
    }
}

function fnStringToDate(psFecha, psFormato = "DD/MM/YYYY") {
    return new Date(moment(psFecha, psFormato).format("YYYY-MM-DDThh:mm:ss"));
}
function fnDateToString(pobjFecha, psFormato = "DD/MM/YYYY") {
    return moment(pobjFecha).format(psFormato);
}
function fnDateDiff(pobjFechaIni, pobjFechaFin, psTiempo = 'd') {
    return pobjFechaFin.diff(pobjFechaIni, psTiempo);
}

function fnValidarEmail(psEmail) {
    var vsExpresion = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if (vsExpresion.test(psEmail.trim())) {
        return true
    } else {
        return false
    }
};
function fnAutoHeight(psIdentificador) {
    $(psIdentificador).each(function () {
        this.setAttribute('style', 'height:30px;overflow-y:hidden;resize:none;');  //(this.scrollHeight)
    }).on('input', function () {
        this.style.height = '30px'; //'auto'  :  '30px'
        if (this.scrollHeight > 0)
            this.style.height = (this.scrollHeight + 2) + 'px';
    });
}

function fnLimpiarDiv(psIdDiv) {
    $(psIdDiv + " input:text," + psIdDiv + " textarea").each(function () {
        if ($(this).attr("id").toString().includes("_dtp") == 1) {
            $(this).datepicker("setDate", new Date());
        } else {
            $(this).val("");
        }
    });

    $(psIdDiv + " input:checkbox").each(function () {
        if ($(this).attr("class") != undefined) {
            if ($(this).attr("class").toString().includes("switchery") == 0) {
                $(this).prop("checked", false);
            }
        } else {
            $(this).prop("checked", false);
        }
    });

    $(psIdDiv + " select").each(function () {
        $(this).val("");
    });

    $(psIdDiv + " .select2-hidden-accessible").each(function (index, object) { $(object).select2("val", "") });
}
function fnHabilitarDiv(psIdDiv, plHabilitar = true) {
    // Funcion que habilita o deshabilita los controles que se encuentra dentro de un div
    // psIdDiv: selector de div
    // plHabilitar: indica el estado de los controles
    // ************************************************************************
    $(psIdDiv).find("input, select, button").each(function () {
        if ($(this).attr("id").includes("_chk") == 0) {
            $(this).prop("disabled", !plHabilitar);
        } else {
            if ($(this).attr("class") != undefined) {
                if ($(this).attr("class").toString().includes("switchery") == 0) {
                    $(this).prop("disabled", !plHabilitar);
                }
            } else {
                $(this).prop("disabled", !plHabilitar);
            }
        }
    });
}
function fnLlenarDiv(psIdDiv, pobjObjeto) {
    // Función que obtiene los valores de un objeto y los muestra en los
    // controles de un div
    // psIdDiv: selector de div
    // pobjObjeto: objeto que se desea llenar
    // ************************************************************************
    $(psIdDiv).find("input, select, textarea").each(function () {
        if ($(this).prop("name") != "") {
            let vsIdControl = $(this).attr("id");
            let vsName = $(this).attr("name");
            if (vsIdControl.includes("txt")) {
                let varrSplit = vsName.split("|");
                if (varrSplit.length == 1) {
                    $(this).val(pobjObjeto[vsName]);
                } else if (varrSplit.length == 2) {
                    let vsPropertyValor = varrSplit[0];
                    let vnPropertyDecimales = parseInt(varrSplit[1]);
                    if (pobjObjeto[vsPropertyValor] != undefined && pobjObjeto[vsPropertyValor] != null) {
                        $(this).val(pobjObjeto[vsPropertyValor].toFixed(vnPropertyDecimales));
                    }
                }
            } else if (vsIdControl.includes("chk")) {
                if ($(this).attr("class") != undefined) {
                    if ($(this).attr("class").toString().includes("switchery") == 0) {
                        $(this).prop("checked", pobjObjeto[vsName]);
                    }
                } else {
                    $(this).prop("checked", pobjObjeto[vsName]);
                }
            } else if (vsIdControl.includes("cbo")) {
                let varrSplit = vsName.split("|");
                if (varrSplit.length == 2) {
                    let vsPropertyValue = varrSplit[0];
                    let vsProperyText = varrSplit[1];
                    if ($("#" + vsIdControl).find("option[value=" + pobjObjeto[vsPropertyValue] + "]").length > 0) {
                        $(this).val(pobjObjeto[vsPropertyValue]);
                    } else {
                        if ($("#" + vsIdControl + " option").length == 0) {
                            $(this).append("<option value='" + pobjObjeto[vsPropertyValue] + "'>" + pobjObjeto[vsProperyText] + "</option>");
                            $(this).val(pobjObjeto[vsPropertyValue]);
                        } else if ($("#" + vsIdControl + " option").length == 1) {
                            if ($("#" + vsIdControl + " option:first").val() != "") { // - SELECCIONE -
                                $("#" + vsIdControl + " option:first").remove();
                            }
                            $(this).append("<option value='" + pobjObjeto[vsPropertyValue] + "'>" + pobjObjeto[vsProperyText] + "</option>");
                            $(this).val(pobjObjeto[vsPropertyValue]);
                        }
                    }
                } else if (varrSplit.length == 1) {
                    $(this).val(pobjObjeto[vsName]);
                }
                if ($(this).attr("class").includes("select2") > 0) {
                    $(this).trigger("change");
                }
            } else if (vsIdControl.includes("dtp")) {
                try {
                    $(this).datepicker('setDate', pobjObjeto[vsName]);
                } catch (e) {
                    alert(e.message);
                }
            }
        }
    });
}
function fnLlenarObjeto(psIdDiv, pobjObjeto) {
    // Función que obtiene los valores de los controles de un div y los almacena
    // en el objeto
    // psIdDiv: selector de div
    // pobjObjeto: objeto que se desea llenar
    // ************************************************************************
    $(psIdDiv).find("input, select, textarea").each(function () {
        if ($(this).prop("name") != "") {
            let vsIdControl = $(this).attr("id");
            let vsName = $(this).attr("name");
            // console.log("id='" + vsIdControl + "', name='" + vsName + "'");
            if (vsIdControl.includes("txt")) {
                let varrSplit = vsName.split("|");
                if (varrSplit.length == 1) {
                    pobjObjeto[vsName] = $(this).val();
                } else if (varrSplit.length == 2) {
                    let vsPropertyValor = varrSplit[0];
                    pobjObjeto[vsPropertyValor] = $(this).val();
                }
            } else if (vsIdControl.includes("chk")) {
                if ($(this).attr("class") != undefined) {
                    if ($(this).attr("class").toString().includes("switchery") == 0) {
                        pobjObjeto[vsName] = $(this).prop("checked");
                    }
                } else {
                    pobjObjeto[vsName] = $(this).prop("checked");
                }
            } else if (vsIdControl.includes("cbo")) {
                let varrSplit = vsName.split("|");
                if (varrSplit.length == 2) {
                    let vsPropertyValue = varrSplit[0];
                    let vsPropertyTexto = varrSplit[1];
                    pobjObjeto[vsPropertyValue] = $(this).val();
                    pobjObjeto[vsPropertyTexto] = $("#" + vsIdControl + " option:selected").text();
                } else if (varrSplit.length == 1) {
                    pobjObjeto[vsName] = $(this).val();
                }
            } else if (vsIdControl.includes("dtp")) {
                if ($(this).val() != "") {
                    let varrSplit = vsName.split("|");
                    if (varrSplit.length == 2 && varrSplit[1] == "s") {
                        let vsPropertyValor = varrSplit[0];
                        pobjObjeto[vsPropertyValor] = moment($(this).val(), 'DD/MM/YYYY').format('YYYYMMDD');
                    } else if (varrSplit.length == 2 && varrSplit[1] == "d") {
                        let vsPropertyValor = varrSplit[0];
                        pobjObjeto[vsPropertyValor] = moment($(this).val(), 'DD/MM/YYYY').format('YYYY-MM-DD');
                    } else if (varrSplit.length == 1) {
                        pobjObjeto[vsName] = moment($(this).val(), 'DD/MM/YYYY').format('YYYY-MM-DD');
                    }
                }
            }
        }
    });
}

function fnMayusculas(psIdentificador = "") {
    if (psIdentificador == "")
        psIdentificador = ".mayusculas";
    $(document).on('input', psIdentificador, function () {
        var start = this.selectionStart;
        var end = this.selectionEnd;
        this.value = this.value.toUpperCase();
        this.setSelectionRange(start, end);
    });
}
function fnMinusculas(psIdClase) {
    $(document).on('input', psIdClase, function () {
        this.value = this.value.toLowerCase();
    });
}
function fnAlfabetico(psIdentificador = "", plEspacios = true, psEspeciales = "") {
    if (psIdentificador == "") {
        psIdentificador = ".alfabetico";
    }
    fnDeshabilitarPegado(psIdentificador);
    $(document).on("keypress", psIdentificador, function (poEvent) {
        return fnValidarTecla(poEvent, false, true, plEspacios, psEspeciales);
    });
}
function fnNumerico(psIdentificador = "") {
    if (psIdentificador == "") {
        psIdentificador = ".numerico";
    }
    fnDeshabilitarPegado(psIdentificador);
    $(document).on("keypress", psIdentificador, function (poEvent) {
        return fnValidarTecla(poEvent, true, false, false, '');
    });
}
function fnDecimal(psIdentificador = "", pnDecimales = 2) {
    if (psIdentificador == "") {
        psIdentificador = ".decimal";
    }
    fnDeshabilitarPegado(psIdentificador);
    $(document).on("keypress", psIdentificador, function (poEvent) {
        return fnValidarTecla(poEvent, true, false, false, '.');
    });
    $(document).on("blur", psIdentificador, function () {
        var vsValor = $(this).val();
        if (!isNaN(Number(vsValor)) && !isNaN(Number(pnDecimales)))
            $(this).val(parseFloat(Number(vsValor)).toFixed(pnDecimales));
    });
}
function fnEntero(psIdentificador = "") {
    if (psIdentificador === "") {
        psIdentificador = ".entero";
    }
    fnDecimal(psIdentificador, 0);
}
function fnAlfanumerico(psIdentificador = "", psSimbolos = "", plEspacios = true) {
    if (psIdentificador == "") {
        psIdentificador = ".alfanumerico";
    }
    fnDeshabilitarPegado(psIdentificador);
    $(document).on("keypress", psIdentificador, function (poEvent) {
        return fnValidarTecla(poEvent, true, true, plEspacios, psSimbolos);
    });
}
function fnLeftTrim(psIndentificador = "") {
    if (psIndentificador == "") {
        psIdentificador = ".leftTrim";
    }
    $(psIdentificador).on('input', function () {
        this.value = this.value.leftTrim();
    });
}
function fnCampoPersonalizado(psIdClase, plNumeros, plLetras, plEspacioBlanco, psCaracteresEspeciales) {
    $(document).on("keypress", psIdClase, function (poEvent) {
        return fnValidarTecla(poEvent, plNumeros, plLetras, plEspacioBlanco, psCaracteresEspeciales);
    });
}
function fnValidarTecla(pobjEvent, plNumeros, plLetras, plEspacioBlanco, psCaracteresEspeciales = '') {
    var vsTecla = window.event ? window.event.keyCode : pobjEvent.which;
    return fnValidarCampo(String.fromCharCode(vsTecla), plNumeros, plLetras, plEspacioBlanco, psCaracteresEspeciales);
}
function fnValidarCampo(psValor, plNumeros, plLetras, plEspacioBlanco, psCaracteresEspeciales) {
    var vsRegExpresion = "^([";
    if (plNumeros === true) vsRegExpresion += "0-9";
    if (plLetras === true) vsRegExpresion += "A-Za-zñÑáéíóúÁÉÍÓÚ";
    if (plEspacioBlanco === true) vsRegExpresion += "\\s";
    vsRegExpresion += "\\b";
    if (psCaracteresEspeciales == undefined) psCaracteresEspeciales = '';
    if (psCaracteresEspeciales.indexOf("-") !== -1 && psCaracteresEspeciales.length > 0)
        psCaracteresEspeciales = psCaracteresEspeciales.replace("-", "\\-");
    vsRegExpresion += psCaracteresEspeciales + "])*$";

    var voRegExpresion = new RegExp(vsRegExpresion);
    return voRegExpresion.test(psValor);
}

function fnLlenarCombo(psIdCombo, plstData, psPropiedadValor, psPropiedadMostrar, psAdicionalValor = "", psAdicionalMostrar = "") {
    $(psIdCombo).empty();
    if (psAdicionalMostrar != "")
        $(psIdCombo).append("<option value = '" + psAdicionalValor + "'>" + psAdicionalMostrar + "</option>");

    $.each(plstData, function (i, voItem) {
        $(psIdCombo).append('<option value=' + voItem[psPropiedadValor] + '>' + voItem[psPropiedadMostrar] + '</option>');
    });
}

function fnSwitchery(psIdControl, plValue = true) {
    var vobjSwitchery = new Switchery(document.querySelector(psIdControl), {
        color: '#1ab394' //'#1c84c6'
        , secondaryColor: '#ed5565'
        , jackColor: '#fff'
        , jackSecondaryColor: null
        , className: 'switchery'
        , disabled: false
        , disabledOpacity: 0.6
        , speed: '0.1s'
        //, size: 'large'
    });
    vobjSwitchery.val = function (plValor) {
        if (plValor != null && plValor != undefined) {
            if (vobjSwitchery.isChecked() != plValor)
                vobjSwitchery.bindClick();
        } else {
            return vobjSwitchery.isChecked();
        }
    };
    vobjSwitchery.val(plValue);
    return vobjSwitchery;
}
function fnDatePicker(psIdentificador, pobjFecha = null) {
    var vdtpFecha = $(psIdentificador).datepicker({
        theme: 'bootstrap',
        pickTime: false,
        autoclose: true,
        language: 'es',
        format: 'dd/mm/yyyy'
    });
    vdtpFecha.val = function (pobjFecha) {
        if (pobjFecha !== null && pobjFecha !== undefined) {
            vdtpFecha.datepicker("setDate", fnDateToString(pobjFecha, "DD/MM/YYYY"));
        } else {
            return vdtpFecha.datepicker('getDate');
        }
    };
    if (pobjFecha !== null) {
        vdtpFecha.val(pobjFecha);
    }
    return vdtpFecha;
}
function fnSelect2(pobjSettings) {

    if (pobjSettings.sIdCombo == undefined || pobjSettings.sIdCombo == "")
        alert("fnSelect2() => No se ha inicializado el atributo 'sIdCombo'");
    else if (pobjSettings.sUrl == undefined || pobjSettings.sUrl == "")
        alert("fnSelect2() => No se ha inicializado el atributo 'sUrl'");
    else if (pobjSettings.sAtributoValor == undefined || pobjSettings.sAtributoValor == "")
        alert("fnSelect2() => No se ha inicializado el atributo 'sAtributoValor'");
    else if (pobjSettings.sAtributoMostrar == undefined || pobjSettings.sAtributoMostrar == "")
        alert("fnSelect2() => No se ha inicializado el atributo 'sAtributoMostrar'");
    else if (pobjSettings.objParametros == undefined || pobjSettings.objParametros == null)
        alert("fnSelect2() => No se ha inicializado el atributo 'objParametros'");
    else if (pobjSettings.sParametroBusqueda == undefined || pobjSettings.sParametroBusqueda == "")
        alert("fnSelect2() => No se ha inicializado el atributo 'sParametroBusqueda'");

    if (pobjSettings.sAtributoAdicional == undefined)
        pobjSettings.sAtributoAdicional = "";
    if (pobjSettings.sPlaceholder == undefined)
        pobjSettings.sPlaceholder = "";

    $(pobjSettings.sIdCombo).empty();
    $(pobjSettings.sIdCombo).select2({
        allowClear: true,
        theme: "bootstrap",
        width: '100%',
        triggerChange: false,
        title: false,
        language: {
            noResults: function () {
                return "No hay resultado";
                /*if (pobjSettings.objNoResultado == undefined) {
                    return "No hay resultado";
                } else {
                    return "<div class='container-fluid'>" +
                        "<div class='row'>" +
                        "<div class='col-md-6 text-left s2Padleft'><label style='font-weight: normal; padding-top: 8px;'>No hay resultados </label></div>" +
                        "<div class='col-md-6 text-right s2Padright'><a href='javascript:void(0)' onclick='" + psNoResultadoFuncion + "' class='btn btn-success btn-sm'>Agregar Nuevo</a></div>" +
                        "</div ></div >";
                }*/
            },
            searching: function () {
                return "Buscando...";
            }
        },
        ajax: {
            url: fnUrlBase() + pobjSettings.sUrl,
            dataType: 'json',
            delay: 250,
            data: function (params) {
                if (pobjSettings.sParametroBusqueda != "")
                    pobjSettings.objParametros[pobjSettings.sParametroBusqueda] = params.term.trim().toUpperCase();
                return pobjSettings.objParametros;
            },
            processResults: function (voRpta) {
                var resultado = $.map(voRpta.objDatos, function (item, index) {
                    return { id: item[pobjSettings.sAtributoValor], text: item[pobjSettings.sAtributoMostrar], textAdd: pobjSettings.sAtributoAdicional, data: item };
                });
                return {
                    results: resultado
                };
            },
            cache: false
        },
        placeholder: pobjSettings.sPlaceholder,
        escapeMarkup: function (markup) { return markup; },
        minimumInputLength: 3,
        templateResult: function (repo) {
            if (repo.loading) {
                return "Consultando ...";
            }
            else {
                var markup = "<div class='container-fluid'>" +
                    "<div class='row'>" +
                    "<div class='col-xs-8 col-sm-8 col-md-8 col-lg-8 s2Padleft'>" + repo.text + "</div>" +
                    "<div class='col-xs-4 col-sm-4 col-md-4 col-lg-4 s2Padright text-right'><strong>" + repo.textAdd + "</strong></div>" +
                    "</div>" +
                    "</div>";
                return markup;
            }
        },
        templateSelection: function (repo) {
            return repo.text;
        }


    });
}
function fnSelectValor(psIdCombo, psValor, psTexto) {
    $(psIdCombo).empty();
    var vobjOption = new Option(psTexto, psValor, false, false);
    $(psIdCombo).append(vobjOption).trigger("change");
}
function fnLimpiarSelectDos(psIdSelectDos) {
    $(psIdSelectDos).empty();
    $("#select2-" + psIdSelectDos.replace("#", "") + "-container").removeAttr('title');
    $(psIdSelectDos).trigger('change');
}

function fnMensaje(psMensaje, psTitulo = "") {
    if (psTitulo == "")
        psTitulo = "Mensaje";

    cnsToast.fire({
        icon: 'success',
        title: psTitulo,
        html: `<p class="mb-0">${psMensaje}</p>`
    });
}
function fnAdvertencia(psAdvertencia, psTitulo = "") {
    if (psTitulo == "")
        psTitulo = "Advertencia";

    cnsToast.fire({
        icon: 'warning',
        title: psTitulo,
        html: `<p class="mb-0">${psAdvertencia}</p>`
    });
}
function fnError(psError, psTitulo = "") {
    if (psTitulo == "")
        psTitulo = "Error";

    cnsToast.fire({
        icon: 'error',
        title: psTitulo,
        html: `<p class="mb-0">${psError}</p>`
    });
}
function fnPregunta(psPregunta, pfnFuncionSi, pfnFuncionNo = null) {
    cnsConfirm.fire({
        icon: "question",
        title: "Confirmar",
        html: `<p class="mb-0">${psPregunta}</p>`
    }).then((result) => {
        if (result.isConfirmed)
            pfnFuncionSi();
        else if (result.isDenied && pfnFuncionNo != null)
            pfnFuncionNo();
    });
}
function fnAlerta(psMensaje, pfnFuncion) {
    cnsAlert.fire({
        icon: "info",
        title: "Información",
        html: `<p class="mb-0">${psMensaje}</p>`
    }).then((result) => {
        if (result.isConfirmed)
            pfnFuncion();
    });
}
function fnProcesando(plMostrar = true, psMensaje = "") {
    // Función que muestra u oculta un div superpuesto con un mensaje de procesamiento dado.
    // plMostrar: indica la visibilidad del div
    // Mensaje del div
    // ************************************************************************
    if (plMostrar) {
        if (psMensaje == "") {
            psMensaje = "Procesando... por favor, espere";
        }

        if (jQuery('body').find('#resultLoading').attr('id') != 'resultLoading') {
            jQuery('body').append("<div id='resultLoading' style='display:none'><div><img src='" + fnUrlBase() + '/Content/img/' + "ajax-loader_0.gif'><div id='divLoader'>" + psMensaje + "</div></div><div class='bg'></div></div>");
        }
        else {
            $("#divLoader").text(psMensaje);
        }

        jQuery('#resultLoading').css({
            'width': '100%',
            'height': '100%',
            'position': 'fixed',
            'z-index': '10000000',
            'top': '0',
            'left': '0',
            'right': '0',
            'bottom': '0',
            'margin': 'auto'
        });

        jQuery('#resultLoading .bg').css({
            'background': '#000000',
            'opacity': '0.7',
            'width': '100%',
            'height': '100%',
            'position': 'absolute',
            'top': '0'
        });

        jQuery('#resultLoading>div:first').css({
            'width': '250px',
            'height': '75px',
            'text-align': 'center',
            'position': 'fixed',
            'top': '0',
            'left': '0',
            'right': '0',
            'bottom': '0',
            'margin': 'auto',
            'font-size': '16px',
            'z-index': '10',
            'color': '#ffffff'

        });
        jQuery('#resultLoading .bg').height('100%');
        jQuery('#resultLoading').fadeIn(300);
        jQuery('body').css('cursor', 'wait');
    } else {
        jQuery('#resultLoading .bg').height('100%');
        jQuery('#resultLoading').fadeOut(300);
        jQuery('body').css('cursor', 'default');
    }
}