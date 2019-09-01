$(function () {

    $(document).on('click', '.btn-enter', function () {
        var form = $('#frmLogin');
        var nome = $('.login-nome').val();
        var senha = $('.login-senha').val();

        $.ajax({
            type: 'GET',
            url: '/Login/Entrar',
            data: {
                nome,
                senha
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    window.location.href = '/Planta/Index'
                    MensagemSucesso(retorno.Mensagem);
                }
                else {
                    aplicaErro(retorno.Mensagem, form);
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao efetuar login', form);
            }
        });
    });

})