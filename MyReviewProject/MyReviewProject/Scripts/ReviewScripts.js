$(document).ready(function () {
});

function unescapedHTML(id, escapedHTML) {
    var a = escapedHTML.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;/g, '&');
    a = $.parseHTML(a);
    return $("#" + id).html(a);
}

$(".submitcomment").click(function () {
    var comment = $(".leavecomment").html();
    if (comment.length > 0) {
        if ($("#wrongcomment").css("display") == "block")
            $("#wrongcomment").css("display", "none");
        var id = $("#answerto").text();
        if (typeof id == 'undefined' || id == "")
            id = -1;
        var ReviewId = $("#hidId").val();
        $.post("/Review/PostComment", { comment: comment, id: id, ReviewId: ReviewId }, function (success) {
            if (success > 0) {
                $(".leavecomment").empty();
            }            
        });
    }
    else {
        $("#wrongcomment").css("display","block");
    }    
});

$("i").click(function () {
    var ElemId = $(this).attr('id');    
    if ($(this).attr("class") == "fa fa-reply") {
        if ($(".leavecomment").html().indexOf("<blockquote>") < 0) {
            $(this).css("color", "var(--errorpagelink)");
            var quote = $("#mydiv" + ElemId).text();
            var user = $("#box" + ElemId).find("a").text();
            quote = $.parseHTML("<blockquote><a>" + user + "</a>" + quote + "</blockquote> <br>");
            $("#answerto").append(ElemId);            
            $(".leavecomment").append(quote);
            $(".leavecomment").append("<br>");          
        }        
    }
    else {
        $.post("/Review/PostLike", { id: ElemId }, function () {
            $(this).css("color", "var(--errorpagelink)");
        });
    }
});