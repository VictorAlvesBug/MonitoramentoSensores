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

});