////////////////////////////////////////////////////////////////////////////////
//
//  src:        https://github.com/open-environment/open-waters
//  author:     Open Environment Software
//  intent:     Handles tab clicking and postback
//  requires:   jquery
//
////////////////////////////////////////////////////////////////////////////////

$(document).ready(function () {
    //handles tab click event
    $(".tabs-menu a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".tab-content").not(tab).css("display", "none");
        $(tab).fadeIn();
    });

    //used for postback on tabs to bring to correct tab (in coordination with code-behind)
    var st = $(this).find("input[id*='hdnSelectedTab']").val();
    if (st == null)
        st = 1;
    $('[id$=tabby' + st + '] > a').trigger('click');

});
