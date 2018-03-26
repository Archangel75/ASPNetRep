$(document).ready(function () {
});

$(".submitcomment").click(function () {
    var comment = $(".leavecomment").text();
    //var check = comment.toString().replace(/\s/g, '');
    if (comment.length > 0) {
        if ($("#wrongcomment").css("display") == "block")
            $("#wrongcomment").css("display", "none");
        var id = $("#answerto").text();
        if (typeof id == 'undefined' || id == "")
            id = -1;
        var revId = $("#hidId").val();
        $.post("/Review/PostComment", { comment: comment, id: id, ReviewId: revId }, function (success) {
            if (success > 0) {
                $(".leavecomment").text() = "";
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
            quote = $.parseHTML("<blockquote><a>"+ user + "</a>" + quote + "</blockquote> <br>");
            $(".leavecomment").append(quote);
            $("#answerto").append(ElemId);
        }        
    }
    else {
        $.post("/Review/PostLike", { id: ElemId }, function () {
            $(this).css("color", "var(--errorpagelink)");
        });
    }
});


