$(function () {

    $(document).on('click', '.js-modal-cadastrar-area', function () {
        $('#codigoArea').val(0);
        $('#nomeArea').val('');

        $('#btn-acao-modal').addClass('js-cadastrar-area');
        $('#btn-acao-modal').removeClass('js-editar-area');

        $('#modal-cadastrar-editar-area-partial').find('#titulo-modal').text('Cadastrar Área');

        $('.aviso').hide();

        abrirModal('#modal-cadastrar-editar-area-partial');
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
                    fecharModal('#modal-cadastrar-editar-area-partial');
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

                $('#modal-cadastrar-editar-area-partial').find('#titulo-modal').text('Editar Área');

                $('.aviso').hide();

                abrirModal('#modal-cadastrar-editar-area-partial');
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
                    fecharModal('#modal-cadastrar-editar-area-partial');
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

    $(document).on('click', '.pagina', function () {
        var pagina = $(this).data('pagina');
        renderizarListaArea(pagina);
    });

    setInterval(renderizarStatusSensores, 2000);

    function renderizarStatusSensores() {
        let $areaBody = $('.area-body');

        let $listaSensor = $areaBody.find('.sensor-card');

        for (let i = 0; i < $listaSensor.length; i++) {
            let $sensor = $listaSensor.eq(i);

            let codigoSensor = $sensor.data('codigo-sensor');

            let $status = $sensor.find('.status');

            $.ajax({
                type: 'GET',
                url: `/Sensor/RetornarSensor?Codigo=${codigoSensor}`,

                success: function (retorno) {
                    switch (retorno.Sensor.Status) {
                        case 0:
                            $status.removeClass('status-green');
                            $status.removeClass('status-yellow');
                            $status.removeClass('status-red');

                            $status.addClass('status-green');
                            console.log(`${retorno.Sensor.Nome} --> VERDE`)
                            break;

                        case 1:
                            $status.removeClass('status-green');
                            $status.removeClass('status-yellow');
                            $status.removeClass('status-red');

                            $status.addClass('status-yellow');
                            console.log(`${retorno.Sensor.Nome} --> AMARELO`)
                            break;

                        case 2:
                            $status.removeClass('status-green');
                            $status.removeClass('status-yellow');
                            $status.removeClass('status-red');

                            $status.addClass('status-red');
                            console.log(`${retorno.Sensor.Nome} --> VERMELHO`)
                            break;
                    }
                },
                error: function () {
                    MensagemErroPersonalizada('Ocorreu um erro ao atualizar status');
                }
            });
        }
    }

});

/*function renderizarListaArea(pagina = $('#paginaAtual').val()) {
    var qtdePaginas = $('#qtdePaginas').val();

    if (pagina < 1) {
        pagina = 1;
    }

    if (pagina > qtdePaginas) {
        pagina = qtdePaginas;
    }

    $.ajax({
        type: 'GET',
        url: '/Area/RenderizarListaArea',
        data: {
            codigoPlanta: $('#codigoPlanta').val(),
            pagina
        },
        success: function (retorno) {
            $('#lista-area-partial').html(retorno);
        },
        error: function () {
            MensagemErroPersonalizada('Ocorreu um erro ao listar áreas');
        }
    });
}*/
