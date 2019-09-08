$(function () {

    $(document).on('click', '.js-modal-cadastrar-planta', function () {
        $('#codigoPlanta').val(0);
        $('#nomePlanta').val('');
        $('#paisPlanta').val(0);
        $('#cepPlanta').val('');
        $('#numeroPlanta').val('');

        limparRetornoCEP();

        $('#btn-acao-modal').addClass('js-cadastrar-planta');
        $('#btn-acao-modal').removeClass('js-editar-planta');

        $('#titulo-modal').text('Cadastrar Planta');

        $('.aviso').hide();

        abrirModal('#modal-cadastrar-editar-planta-partial');
    });

    $(document).on('click', '.js-cadastrar-planta', function () {
        $.ajax({
            type: 'POST',
            url: '/Planta/CadastrarPlanta',
            data: {
                Nome: $('#nomePlanta').val(),
                Pais: $('#paisPlanta').val(),
                CEP: $('#cepPlanta').val(),
                CEPValido: $("#cepValido").val(),
                Numero: $('#numeroPlanta').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaPlanta();
                    fecharModal('#modal-cadastrar-editar-planta-partial');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmPlanta'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao cadastrar planta', $('#frmPlanta'));
            }
        });
    });

    $(document).on('click', '.js-modal-editar-planta', function () {

        var codigo = $(this).data('codigo');

        $.ajax({
            type: 'GET',
            url: `/Planta/RetornarPlanta?Codigo=${codigo}`,
            success: function (retorno) {
                $('#codigoPlanta').val(retorno.Planta.Codigo);
                $('#nomePlanta').val(retorno.Planta.Nome);
                $('#paisPlanta').val(retorno.Planta.Pais);
                $('#cepPlanta').val(retorno.Planta.CEP);
                $('#numeroPlanta').val(retorno.Planta.Numero);

                exibirDadosRetornoCEP();

                $('#btn-acao-modal').addClass('js-editar-planta');
                $('#btn-acao-modal').removeClass('js-cadastrar-planta');

                $('#titulo-modal').text('Editar Planta');

                $('.aviso').hide();

                abrirModal('#modal-cadastrar-editar-planta-partial');
            },
            error: function () {
                MensagemErroPersonalizada('Ocorreu um erro ao retornar planta');
            }
        });
    });

    $(document).on('click', '.js-editar-planta', function () {
        $.ajax({
            type: 'POST',
            url: '/Planta/EditarPlanta',
            data: {
                Codigo: $('#codigoPlanta').val(),
                Nome: $('#nomePlanta').val(),
                Pais: $('#paisPlanta').val(),
                CEP: $('#cepPlanta').val(),
                CEPValido: $("#cepValido").val(),
                Numero: $('#numeroPlanta').val()
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    MensagemSucesso(retorno.Mensagem);
                    renderizarListaPlanta();
                    fecharModal('#modal-cadastrar-editar-planta-partial');
                }
                else {
                    aplicaErro(retorno.Mensagem, $('#frmPlanta'));
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao editar planta', $('#frmPlanta'));
            }
        });
    });

    $(document).on('click', '.js-excluir-planta', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');
        var pais = $(this).data('pais');

        ExibirModalConfirmacao("Excluir Planta", `Deseja realmente excluir a planta <b>${nome} (${pais})</b>`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Planta/ExcluirPlanta?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaPlanta();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao excluir planta');
                    }
                });
            });
    });

    $(document).on('click', '.js-duplicar-planta', function () {

        var codigo = $(this).data('codigo');
        var nome = $(this).data('nome');
        var pais = $(this).data('pais');

        ExibirModalConfirmacao("Duplicar Planta", `Deseja realmente duplicar a planta <b>${nome} (${pais})</b>`,
            function () {
                $.ajax({
                    type: 'POST',
                    url: `/Planta/DuplicarPlanta?Codigo=${codigo}`,
                    success: function (retorno) {
                        if (retorno.Sucesso) {
                            MensagemSucesso(retorno.Mensagem);
                            renderizarListaPlanta();
                        }
                        else {
                            MensagemErroPersonalizada(retorno.Mensagem);
                        }
                    },
                    error: function () {
                        MensagemErroPersonalizada('Ocorreu um erro ao duplicar planta');
                    }
                });
            });
    });

    //$('.cep-mask').mask("99999-999");

    $(document).on('keyup', '.cep-mask', function () {
        exibirDadosRetornoCEP();
    });

    $(document).on('click', '.pagina', function () {
        var pagina = $(this).data('pagina');
        renderizarListaPlanta(pagina);
    });

});

function renderizarListaPlanta(pagina = $('#paginaAtual').val()) {
    var qtdePaginas = $('#qtdePaginas').val();

    if (pagina < 1) {
        pagina = 1;
    }

    if (pagina > qtdePaginas) {
        pagina = qtdePaginas;
    }

    $.ajax({
        type: 'GET',
        url: '/Planta/RenderizarListaPlanta',
        data: {
            pagina
        },
        success: function (retorno) {
            $('#lista-planta-partial').html(retorno);
        },
        error: function () {
            MensagemErroPersonalizada('Ocorreu um erro ao listar plantas');
        }
    });
}

function exibirDadosRetornoCEP() {
    if ($('#cepPlanta').val().length == 8/*9*/) {
        $.ajax({
            url: 'https://viacep.com.br/ws/' + $('#cepPlanta').val() + '/json/unicode/',
            dataType: 'json',
            success: function (resposta) {
                if (resposta.logradouro) {
                    $("#cepValido").val('True');
                    $("#logradouroPlanta").val(resposta.logradouro);
                    $("#complementoPlanta").val(resposta.complemento);
                    $("#bairroPlanta").val(resposta.bairro);
                    $("#cidadePlanta").val(resposta.localidade);
                    $("#ufPlanta").val(resposta.uf);

                    $(".retorno-cep").show();
                }
                else {
                    limparRetornoCEP();
                }
            }
        });
    }
    else {
        limparRetornoCEP();
    }
}

function limparRetornoCEP() {
    $("#cepValido").val('False');
    $("#logradouroPlanta").val("");
    $("#complementoPlanta").val("");
    $("#bairroPlanta").val("");
    $("#cidadePlanta").val("");
    $("#ufPlanta").val("");

    $(".retorno-cep").hide();
}