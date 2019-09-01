$(function () {

    $(document).on('click', '.js-modal-cadastrar-sensor', function () {
        $('#codigoSensor').val(0);
        $('#nomeSensor').val('');
        $('#enderecoSensor').val('');

        $('#btn-acao-modal').addClass('js-cadastrar-sensor');
        $('#btn-acao-modal').removeClass('js-editar-sensor');

        $('#titulo-modal').text('Cadastrar Sensor');

        $('.aviso').hide();

        $('#modal-cadastrar-editar-sensor-partial').modal('show');
    });

    $(document).on('click', '.js-cadastrar-sensor', function () {
        $.ajax({
            type: 'POST',
            url: '/Sensor/CadastrarSensor',
            data: {
                CodigoEquipamento: $('#codigoEquipamento').val(),
                Nome: $('#nomeSensor').val(),
                Endereco: $('#enderecoSensor').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaSensor();
                    $('#modal-cadastrar-editar-sensor-partial').modal('hide');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmSensor'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao cadastrar sensor', $('#frmSensor'));
            }
        });
    });

    $(document).on('click', '.js-modal-editar-sensor', function () {

        var codigo = $(this).data('codigo');

        $.ajax({
            type: 'GET',
            url: `/Sensor/RetornarSensor?Codigo=${codigo}`,
            success: function (retorno) {
                $('#codigoSensor').val(retorno.Sensor.Codigo);
                $('#nomeSensor').val(retorno.Sensor.Nome);
                $('#enderecoSensor').val(retorno.Sensor.Endereco);

                $('#btn-acao-modal').addClass('js-editar-sensor');
                $('#btn-acao-modal').removeClass('js-cadastrar-sensor');

                $('#titulo-modal').text('Editar Sensor');

                $('.aviso').hide();

                $('#modal-cadastrar-editar-sensor-partial').modal('show');
            },
            error: function () {
                MensagemErroPersonalizada('Ocorreu um erro ao retornar sensor');
            }
        });
    });

    $(document).on('click', '.js-editar-sensor', function () {
        $.ajax({
            type: 'POST',
            url: '/Sensor/EditarSensor',
            data: {
                Codigo: $('#codigoSensor').val(),
                CodigoEquipamento: $('#codigoEquipamento').val(),
                Nome: $('#nomeSensor').val(),
                Endereco: $('#enderecoSensor').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaSensor();
                    $('#modal-cadastrar-editar-sensor-partial').modal('hide');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmSensor'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao editar sensor', $('#frmSensor'));
            }
        });
    });

    $(document).on('click', '.js-excluir-sensor', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');
        var endereco = $(this).data('endereco');

        ExibirModalConfirmacao("Excluir Sensor", `Deseja realmente excluir o sensor <b>${nome}</b> (Endereço: <b>${endereco}</b>)`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Sensor/ExcluirSensor?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaSensor();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao excluir sensor');
                    }
                });
            });
    });

    $(document).on('click', '.js-duplicar-sensor', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');
        var endereco = $(this).data('endereco');

        ExibirModalConfirmacao("Duplicar Sensor", `Deseja realmente duplicar o sensor <b>${nome}</b> (Endereço: <b>${endereco}</b>)`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Sensor/DuplicarSensor?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaSensor();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao duplicar sensor');
                    }
                });
            });
    });

});

function renderizarListaSensor() {
    var codigoEquipamento = $('#codigoEquipamento').val();

    $.ajax({
        type: 'GET',
        url: `/Sensor/RenderizarListaSensor?CodigoEquipamento=${codigoEquipamento}`,
        success: function (retorno) {
            $('#lista-sensor-partial').html(retorno);
        },
        error: function () {
            MensagemErroPersonalizada('Ocorreu um erro ao listar sensores');
        }
    });
}