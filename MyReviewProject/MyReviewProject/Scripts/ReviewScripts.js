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
    var id = $(this).attr('id');
    $(this).css("color", "var(--errorpagelink)");
    if ($("i").attr("class") == "fa fa-reply") {
        if ($(".leavecomment").html().indexOf("<blockquote>") < 0) {
            var quote = $("#mydiv" + id).text();
            var user = $("#box" + id).find("a").text();
            quote = $.parseHTML("<blockquote><a>"+ user + "</a>" + quote + "</blockquote> <br>");
            $(".leavecomment").append(quote);
            $("#answerto").append(id);
        }        
    }
    else {
        $(this).css("color", "var(--errorpagelink)");
        $.post("/Review/PostLike", id, function () {
            $(this).css("color", "var(--errorpagelink)");
        });
    }
});


