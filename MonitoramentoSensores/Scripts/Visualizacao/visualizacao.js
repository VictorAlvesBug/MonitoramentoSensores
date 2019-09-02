$(function () {


    $(document).on('click', '.js-modal-equipamento-detalhes', function () {
        var codigoEquipamento = $(this).data('codigo-equipamento');

        $.ajax({
            type: 'GET',
            url: `/Visualizacao/DetalhesEquipamento?codigoEquipamento=${codigoEquipamento}`,
            success: function (retorno) {
                $('#modal-equipamento-detalhes-partial').html(retorno);
                $('#modal-equipamento-detalhes').modal('show');
            },
            error: function () {
                MensagemErroPersonalizada('Ocorreu um erro ao retornar detalhes do equipamento')
            }
        });

    });

    $(document).on('click', '.pagina', function () {
        var pagina = $(this).data('pagina');
        var codigoPlanta = $('#codigoPlanta').val();

        renderizarListaArea(codigoPlanta, pagina);
    });

});

function renderizarListaArea(codigoPlanta, pagina) {
    var paginaAtual = $('#paginaAtual').val();
    var qtdePaginas = $('#qtdePaginas').val();

    if (pagina != paginaAtual && pagina >= 1 && pagina <= qtdePaginas) {
        $.ajax({
            type: 'GET',
            url: '/Visualizacao/RenderizarAreaPaginada',
            data: {
                codigoPlanta,
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