





const html_confirmacao = `
        <div class="modal fade" id="modalSimNao" tabindex="-1" role="dialog" aria-labelledby="modalSimNaoLabel" aria-hidden="true" style="z-index:9999">
            <div class="modal-dialog">
                <div class="modal-content">

                    <div class="modal-header">
                        <h4 id="modalSimNaoLabel" class="modal-titulo"></h4>
                        <button type="button" class="close float-right" data-dismiss="modal" aria-label="Close" data-toggle="tooltip" data-placement="left" title="Fechar">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>

                    <div class="modal-body">
                        <p id="modalSimNaoTexto"> </p>
                    </div>

                    <div class="modal-footer">
                        <a class="btn btn-blue js-modalSimNao-ok">Confirmar</a>
                        <a class="btn btn-black js-modalSimNao-cancelar">Cancelar</a>
                    </div>

                </div>
            </div>
        </div> `;
const html_confirmacao_tres_opcoes = `
        <div class="modal fade" id="modalSimNao" tabindex="-1" role="dialog" aria-labelledby="modalSimNaoLabel" aria-hidden="true" style="z-index:9999">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 id="modalSimNaoLabel" class="modal-titulo"></h4>
                        <button type="button" class="close float-right" data-dismiss="modal" aria-label="Close" data-toggle="tooltip" data-placement="left" title="Fechar">
                            <i class="fa fa-times"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p id="modalSimNaoTexto"> </p>
                    </div> 
                    <div class="modal-footer">
                        <a class="btn btn-blue js-modalSimNao-ok1">Texto 1</a>
                        <a class="btn btn-green js-modalSimNao-ok2">Texto 2</a>
                        <a class="btn btn-black js-modalSimNao-cancelar">Cancelar</a>
                    </div> 
                </div>
            </div>
        </div> `;

function ExibirModalConfirmacao(titulo, corpo, cbSucesso, cbCancelar, cbValida) {
    var $modal = $(html_confirmacao);

    $("#modalSimNao").remove();

    $('#modalSimNaoLabel', $modal).text(titulo)
    $('#modalSimNaoTexto', $modal).html(corpo)

    $("main").append($modal);

    $("#modalSimNao").modal("show");

    $(".js-modalSimNao-ok").click(function () {

        if (cbValida) {
            if (cbValida()) {
                $("#modalSimNao").modal("hide");

                if (cbSucesso)
                    cbSucesso();
            }
            return;
        }

        $("#modalSimNao").modal("hide");

        if (cbSucesso)
            cbSucesso();
    });
    $(".js-modalSimNao-cancelar").click(function () {
        $("#modalSimNao").modal("hide"); 1

        if (cbCancelar)
            cbCancelar();
    });
}

function ExibirModalConfirmacaoTresOpcoes(titulo, corpo, textoOpcao1, textoOpcao2, cbSucesso1, cbSucesso2, cbCancelar) {
    var $modal = $(html_confirmacao_tres_opcoes);

    $("#modalSimNao").remove();

    $('#modalSimNaoLabel', $modal).text(titulo)
    $('#modalSimNaoTexto', $modal).html(corpo)

    $("main").append($modal);

    $("#modalSimNao").modal("show");


    $(".js-modalSimNao-ok1").text(textoOpcao1)
    $(".js-modalSimNao-ok2").text(textoOpcao2)


    $(".js-modalSimNao-ok1").click(function () {
        $("#modalSimNao").modal("hide");

        if (cbSucesso1)
            cbSucesso1();
    });

    $(".js-modalSimNao-ok2").click(function () {
        $("#modalSimNao").modal("hide");

        if (cbSucesso2)
            cbSucesso2();
    });

    $(".js-modalSimNao-cancelar").click(function () {
        $("#modalSimNao").modal("hide"); 1

        if (cbCancelar)
            cbCancelar();
    });
}