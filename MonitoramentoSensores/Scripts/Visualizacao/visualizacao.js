$(function () {


    $(document).on('click', '.js-modal-equipamento-detalhes', function () {
        var codigoEquipamento = $(this).data('codigo-equipamento');

        $.ajax({
            type: 'GET',
            url: `/Visualizacao/DetalhesEquipamento?codigoEquipamento=${codigoEquipamento}`,
            success: function (retorno) {
                $('#modal-equipamento-detalhes-partial').html(retorno);
                abrirModal('#modal-equipamento-detalhes');
            },
            error: function () {
                MensagemErroPersonalizada('Ocorreu um erro ao retornar detalhes do equipamento')
            }
        });

    });

    $(document).on('click', '.pagina', function () {
        var pagina = $(this).data('pagina');

        renderizarListaArea(pagina);
    });

    setInterval(renderizarStatusEquipamentos, 2000);

    function renderizarStatusEquipamentos() {
        let $boxBody = $('.box-body');

        let $listaEquipamento = $boxBody.find('.equipamento-content');

        for (let i = 0; i < $listaEquipamento.length; i++) {
            let $equipamento = $listaEquipamento.eq(i);

            let codigoEquipamento = $equipamento.data('codigo-equipamento');

            let $status = $equipamento.find('.equipamento-status');

            $.ajax({
                type: 'GET',
                url: `/Visualizacao/RetornarStatusEquipamentoAsync?Codigo=${codigoEquipamento}`,
                success: function (retorno) {
                    switch (retorno.Status) {
                        case 0:
                            $status.removeClass('equipamento-status-green');
                            $status.removeClass('equipamento-status-yellow');
                            $status.removeClass('equipamento-status-red');

                            $status.addClass('equipamento-status-green');
                            break;

                        case 1:
                            $status.removeClass('equipamento-status-green');
                            $status.removeClass('equipamento-status-yellow');
                            $status.removeClass('equipamento-status-red');

                            $status.addClass('equipamento-status-yellow');
                            break;

                        case 2:
                            $status.removeClass('equipamento-status-green');
                            $status.removeClass('equipamento-status-yellow');
                            $status.removeClass('equipamento-status-red');

                            $status.addClass('equipamento-status-red');
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

function mostrarOpcoesArea(codigoArea) {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    for (var i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        if (openDropdown.classList.contains('show')) {
            openDropdown.classList.remove('show');
        }
    }
    document.getElementById(`opcoes-area-${codigoArea}`).classList.toggle("show");
}

// Close the dropdown menu if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

function renderizarListaArea(pagina = $('#paginaAtual').val()) {
    var qtdePaginas = $('#qtdePaginas').val();

    if (pagina == null || pagina == undefined) {
        pagina = 1;
    }

    if (pagina >= 1 && pagina <= qtdePaginas) {
        $.ajax({
            type: 'GET',
            url: '/Visualizacao/RenderizarAreaPaginada',
            data: {
                codigoPlanta: $('#codigoPlanta').val(),
                pagina
            },
            success: function (retorno) {
                $('#lista-area-paginada-partial').html(retorno);
            },
            error: function () {
                MensagemErroPersonalizada('Ocorreu um erro ao paginar lista de áreas');
            }
        });
    }
}