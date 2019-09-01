$(function () {

    $(document).on('click', '.js-modal-cadastrar-area', function () {
        $('#codigoArea').val(0);
        $('#nomeArea').val('');

        $('#btn-acao-modal').addClass('js-cadastrar-area');
        $('#btn-acao-modal').removeClass('js-editar-area');

        $('#titulo-modal').text('Cadastrar Área');

        $('.aviso').hide();

        $('#modal-cadastrar-editar-area-partial').modal('show');
    });

    $(document).on('click', '.js-cadastrar-area', function () {
        $.ajax({
            type: 'POST',
            url: '/Area/CadastrarArea',
            data: {
                CodigoPlanta: $('#codigoPlanta').val(),
                Nome: $('#nomeArea').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaArea();
                    $('#modal-cadastrar-editar-area-partial').modal('hide');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmArea'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao cadastrar área', $('#frmArea'));
            }
        });
    });

    $(document).on('click', '.js-modal-editar-area', function () {

        var codigo = $(this).data('codigo');

        $.ajax({
            type: 'GET',
            url: `/Area/RetornarArea?Codigo=${codigo}`,
            success: function (retorno) {
                $('#codigoArea').val(retorno.Area.Codigo);
                $('#nomeArea').val(retorno.Area.Nome);

                $('#btn-acao-modal').addClass('js-editar-area');
                $('#btn-acao-modal').removeClass('js-cadastrar-area');

                $('#titulo-modal').text('Editar Área');

                $('.aviso').hide();

                $('#modal-cadastrar-editar-area-partial').modal('show');
            },
            error: function () {
                MensagemErroPersonalizada('Ocorreu um erro ao retornar área');
            }
        });
    });

    $(document).on('click', '.js-editar-area', function () {
        $.ajax({
            type: 'POST',
            url: '/Area/EditarArea',
            data: {
                Codigo: $('#codigoArea').val(),
                CodigoPlanta: $('#codigoPlanta').val(),
                Nome: $('#nomeArea').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaArea();
                    $('#modal-cadastrar-editar-area-partial').modal('hide');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmArea'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao editar área', $('#frmArea'));
            }
        });
    });

    $(document).on('click', '.js-excluir-area', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');

        ExibirModalConfirmacao("Excluir Área", `Deseja realmente excluir a área <b>${nome}</b>`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Area/ExcluirArea?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaArea();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao excluir área');
                    }
                });
            });
    });

    $(document).on('click', '.js-duplicar-area', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');

        ExibirModalConfirmacao("Duplicar Área", `Deseja realmente duplicar a área <b>${nome}</b>`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Area/DuplicarArea?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaArea();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao duplicar área');
                    }
                });
            });
    });

});

function renderizarListaArea() {
    var codigoPlanta = $('#codigoPlanta').val();

    $.ajax({
        type: 'GET',
        url: `/Area/RenderizarListaArea?CodigoPlanta=${codigoPlanta}`,
        success: function (retorno) {
            $('#lista-area-partial').html(retorno);
        },
        error: function () {
            MensagemErroPersonalizada('Ocorreu um erro ao listar áreas');
        }
    });
}