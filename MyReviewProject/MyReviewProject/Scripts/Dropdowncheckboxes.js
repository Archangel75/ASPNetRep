﻿/*
	Dropdown with Multiple checkbox select with jQuery - May 27, 2013
	(c) 2013 @ElmahdiMahmoud
	license: https://www.opensource.org/licenses/mit-license.php
*/
//дропает
$(".dropdownCat dt a").on("click", function() {
    $(".dropdownCat dd ul").slideToggle("fast");
});
//прячет
$(".dropdownCat dd ul li a").on("click", function() {
    $(".dropdownCat dd ul").hide();
});
//получает значение
function getSelectedCatValue(id) {
  return $("#" + id)
    .find(".dropdownCat dt a span.value")
    .html();
}

$(document).bind("click", function(e) {
  var $clicked = $(e.target);
  if (!$clicked.parents().hasClass("dropdownCat")) $(".dropdownCat dd div ul").hide();
  
});

//Categories
$('.mutliSelectCat input').on('click', function () {
        var title = $(this).closest('.mutliSelectCat').find('input').val(),
        title = $(this).val();
        $.get("/Review/GetSubCategories", { catname: title }, function (data) {
            $("#subCat").html(data.result);
        });
        $('.multiSelCat span').remove();
        var html = '<span title="' + title + '">' + title + '</span>';
        $('.multiSelCat').append(html);
        $(".hidaCat").hide();
        
});

//SubCategories
//дропает
$(".dropdownSub dt a").on("click", function () {
    $(".dropdownSub dd ul").slideToggle("fast");
});
//прячет
$(".dropdownSub dd ul li a").on("click", function () {
    $(".dropdownSub dd ul").hide();
});
//получает значение
function getSelectedSubValue(id) {
    return $("#" + id)
        .find(".dropdownSub dt a span.value")
        .html();
}
//прячет когда кликаем в сторону
$(document).bind("click", function (e) {
    var $clicked = $(e.target);
    if (!$clicked.parents().hasClass("dropdownSub") ) $(".dropdownSub dd div ul").hide();
});

//SubCategories
$('.mutliSelectSub input').on('click', function () {
    var title2 = $(this).closest('.mutliSelectSub').find('input').val(),
        title2 = $(this).val();
    $('.multiSelSub span').remove();
    var html2 = '<span title="' + title2 + '">' + title2 + '</span>';
    $('.multiSelSub').append(html2);
    $(".hidaSub").hide();
});



$('#createSubject').on('click', function () {
    var catName = document.getElementsByClassName('multiSelCat')[0];
    var subCatName = document.getElementsByClassName('multiSelSub')[0];
    var subjName = document.getElementById('subjName');
    $.post("/Review/CreateSubject", { subcatId: subCatName.id, subjname: subjName }, function () {
        var modal = document.getElementById('SubjectModal');
        modal.style.display = "block";
    });
});