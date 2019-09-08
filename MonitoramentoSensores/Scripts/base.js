$(function () {
    $(document).on('ready', function () {
        //$('[data-toggle="tooltip"]').tooltip();

        if ($('#nomeUsuario').val() == 0 && !window.location.href.includes('/Login/Index')) {
            window.location.href = '/Login/Index';
        }
    })

    $(document).on('mousemove', '[data-toggle="tooltip"]', function () {
        var title = $(this).attr('data-title');
        $(this).attr('title', title);
    });

    $(document).on('click', '.close', function () {
        this.closest('.meu-modal-container').classList.remove('mostrar');
    });

    $(document).on('click', '.meu-modal-container', function (e) {

        var meuModalContainer = this.closest('.meu-modal-container');
        if (meuModalContainer && !meuModalContainer.classList.contains('nao-ignoravel') && e.target.classList.contains('meu-modal-container'))
            meuModalContainer.classList.remove('mostrar');
    });
});

function abrirModal(seletor) {
    var modalElement = document.querySelector(seletor);
    if (modalElement)
        modalElement.classList.add('mostrar');
}

function fecharModal(seletor) {
    var modalElement = document.querySelector(seletor);
    if (modalElement)
        modalElement.classList.remove('mostrar');
}