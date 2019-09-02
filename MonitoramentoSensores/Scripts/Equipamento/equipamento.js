$(function () {

    $(document).on('click', '.js-modal-cadastrar-equipamento', function () {
        $('#codigoEquipamento').val(0);
        $('#nomeEquipamento').val('');

        $('#btn-acao-modal').addClass('js-cadastrar-equipamento');
        $('#btn-acao-modal').removeClass('js-editar-equipamento');

        $('#titulo-modal').text('Cadastrar Equipamento');

        $('.aviso').hide();

        $('#modal-cadastrar-editar-equipamento-partial').modal('show');
    });

    $(document).on('click', '.js-cadastrar-equipamento', function () {
        $.ajax({
            type: 'POST',
            url: '/Equipamento/CadastrarEquipamento',
            data: {
                CodigoArea: $('#codigoArea').val(),
                Nome: $('#nomeEquipamento').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaEquipamento();
                $('#modal-cadastrar-editar-equipamento-partial').modal('hide');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmEquipamento'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao cadastrar equipamento', $('#frmEquipamento'));
            }
        });
    });

    $(document).on('click', '.js-modal-editar-equipamento', function () {

        var codigo = $(this).data('codigo');

        $.ajax({
            type: 'GET',
            url: `/Equipamento/RetornarEquipamento?Codigo=${codigo}`,
            success: function (retorno) {
                $('#codigoEquipamento').val(retorno.Equipamento.Codigo);
                $('#nomeEquipamento').val(retorno.Equipamento.Nome);

                $('#btn-acao-modal').addClass('js-editar-equipamento');
                $('#btn-acao-modal').removeClass('js-cadastrar-equipamento');

                $('#titulo-modal').text('Editar Equipamento');

                $('.aviso').hide();

                $('#modal-cadastrar-editar-equipamento-partial').modal('show');
            },
            error: function () {
                MensagemErroPersonalizada('Ocorreu um erro ao retornar equipamento');
            }
        });
    });

    $(document).on('click', '.js-editar-equipamento', function () {
        $.ajax({
            type: 'POST',
            url: '/Equipamento/EditarEquipamento',
            data: {
                Codigo: $('#codigoEquipamento').val(),
                CodigoArea: $('#codigoArea').val(),
                Nome: $('#nomeEquipamento').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaEquipamento();
                $('#modal-cadastrar-editar-equipamento-partial').modal('hide');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmEquipamento'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao editar equipamento', $('#frmEquipamento'));
            }
        });
    });

    $(document).on('click', '.js-excluir-equipamento', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');

        ExibirModalConfirmacao("Excluir Equipamento", `Deseja realmente excluir o equipamento <b>${nome}</b>`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Equipamento/ExcluirEquipamento?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaEquipamento();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao excluir equipamento');
                    }
                });
            });
    });

    $(document).on('click', '.js-duplicar-equipamento', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');

        ExibirModalConfirmacao("Duplicar Equipamento", `Deseja realmente duplicar o equipamento <b>${nome}</b>`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Equipamento/DuplicarEquipamento?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaEquipamento();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao duplicar equipamento');
                    }
                });
            });
    });

});

function renderizarListaEquipamento() {
    var codigoArea = $('#codigoArea').val();

    $.ajax({
        type: 'GET',
        url: `/Equipamento/RenderizarListaEquipamento?CodigoArea=${codigoArea}`,
        success: function (retorno) {
            $('#lista-equipamento-partial').html(retorno);
        },
        error: function () {
            MensagemErroPersonalizada('Ocorreu um erro ao listar equipamentos');
        }
    });
}