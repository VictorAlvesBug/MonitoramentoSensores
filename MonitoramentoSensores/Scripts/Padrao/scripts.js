
function esconderAlerts() {
    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);
}

function mostrarMensagem(mensagem, mensagemConsole, icone, classe) {
    console.log(mensagemConsole);
    $("#alert-helper").html(
        "<div style='display:none;' class='alert alert-" + classe + " alert-dismissable erro-dashboard' style='z-index:9999999999;'>" +
        "<i class='fa fa-" + icone + " margin'></i>" +
        "<i aria-hidden='true' data-dismiss='alert' class='close fa fa-times btn-close-float-message'></i> &nbsp;" +
        mensagem +
        "</div>");
    $("#alert-helper .alert").fadeIn("slow");
    esconderAlerts();
}

function MensagemErroPersonalizada(mensagemPadrao, mensagemConsole = '') {
    mostrarMensagem(mensagemPadrao, mensagemConsole, "ban", "danger");
};

function MensagemErro(mensagemConsole = '') {
    mostrarMensagem("Ops! Ocorreu um erro.", mensagemConsole, "ban", "danger");
};

function MensagemSucesso(mensagemPadrao, mensagemConsole = '') {
    mostrarMensagem(mensagemPadrao, mensagemConsole, "check", "success");
};

function aplicaErro(mensagem, divErro) {
    if (typeof mensagem == "object") {
        divErro.find('.aviso').fadeIn(500).html(mensagem.join('<br/>'));
    }
    else if (mensagem != null) {
        divErro.find('.aviso').fadeIn(500).text(mensagem);
    }
    else {
        alert("Ocorreu um erro");
    }
}