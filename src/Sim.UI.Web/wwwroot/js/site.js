
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $(".viewbutton").on("click", function () {
        var customerId = $(this).data('id');
        var secao = $(this).data('secao');
        var acao = $(this).data('acao');
        var url = "/" + secao + "/" + acao + "?id=" + customerId;        
        $("#viewmodalData").empty();
        $.get(url, function (data) {
            var tempdata = data;
            $("#viewmodalData").html(tempdata);
        });        
    });

    $('.modal').modal();

    $('.dropdown-trigger').dropdown(
        {
            hover:true,
            restringirWidth:false,
            coverTrigger:false
        });

    $('select').formSelect();

    $('.collapsible').collapsible({ accordion:true });

    $('.sidenav').sidenav();

    $('.tabs').tabs();

    $('.datepicker').datepicker();    

    $('.tooltipped').tooltip();

    $('.scrollspy').scrollSpy();
       
    M.updateTextFields();

    var toggler = document.getElementsByClassName("caret");
    var i;

    for (i = 0; i < toggler.length; i++) {
        toggler[i].addEventListener("click", function () {
            this.parentElement.querySelector(".nested").classList.toggle("active");
            this.classList.toggle("caret-down");
        });
    }

    /*
    $('.sidenav li').click(() => {
        $('.sidenav').sidenav('close');
    })*/

});

