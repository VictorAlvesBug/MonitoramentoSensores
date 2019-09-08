﻿$(function () {

    $(document).on('keydown', '.login-nome', function (e) {
        $('.login-senha').val('');
        if (e.keyCode == 13) {
            $('.login-senha').focus();
        }
    });

    $(document).on('keydown', '.login-senha', function (e) {
        if (e.keyCode == 13) {
            $('.btn-enter').click();
        }
    });

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
            beforeSend: function () {
                document.querySelector('.loader-container').classList.add('mostrar');
            },
            success: function (retorno) {
                if (retorno.Sucesso) {
                    window.location.href = '/Planta/Index'
                }
                else {
                    aplicaErro('Ocorreu um erro ao efetuar login', form);
                }
            },
            error: function () {
                aplicaErro('Ocorreu um erro ao efetuar login', form);
            },
            complete: function () {
                document.querySelector('.loader-container').classList.remove('mostrar');
            }
        });
    });

})