$(document).ready(function () {
    // Agregar evento 'focus' a los elementos con la clase 'input'
    $(".input").focus(function () {
        // Remover la clase 'focus' de todos los elementos con la clase 'inputBox'
        $(".inputBox").removeClass("focus");
        // Agregar la clase 'focus' al elemento padre (.inputBox) del elemento con la clase 'input' que recibió el foco
        $(this).parent().addClass("focus");
    });

    // Agregar evento 'click' a todos los elementos en el documento
    $(document).click(function (event) {
        // Verificar si el evento de clic ocurrió fuera de los elementos con la clase 'inputBox'
        if (!$(event.target).closest(".inputBox").length) {
            // Si ocurrió fuera de los elementos con la clase 'inputBox', removemos la clase 'focus' de todos los elementos con esa clase
            $(".inputBox").removeClass("focus");
        }
    });
});

document.addEventListener('DOMContentLoaded', async function () {
    $("#btnGuardar").click(async function () {
        let v_id = $("#txtid").val();
        if (v_id == "" || v_id == null || v_id == "undefined") {
            v_id = 0;
        }
        var parametros = {
            p_opcion: "INSERT",
            p_id: v_id,
            p_nombre_perfil: $("#txtNombrePerfil").val(),
            p_descripcion: $("#txtDescripcion").val(),
            p_estado: document.getElementById("switchestado").checked,
            p_id_usuario: 0
        };
        await setPerfil(parametros);
    });
});

const setPerfil = async (parametros) => {
    try {
        const responseRequest = await postAjax("/Configuraciones/setPerfil", parametros);
        messageToastUtil(responseRequest.responsemessage, responseRequest.messagetype);
        var a = 1;
    } catch (error) {
        messageToastUtil(error, "ERROR");
    }
}