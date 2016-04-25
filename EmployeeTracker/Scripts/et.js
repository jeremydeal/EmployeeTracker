$(function () {

    // AJAX SEARCH FOR EMPLOYEES/INDEX
    $("form[data-et-ajax='true']").on('submit', function(e) {
        e.preventDefault();    
        var $form = $(this);

        // close autocomplete if necessary
        var $autocomplete = $form.children("input[type=search]").attr("autocomplete", "off");

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        // send Ajax request to the given action
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-et-target"));
            var $newHtml = $(data)
            $target.replaceWith($newHtml);
            $newHtml.effect("hightlight");
        });

        // prevent default submit action
        return false;        
    });

    // AUTOCOMPLETE FOR EMPLOYEES/INDEX
    var createAutoComplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-et-autocomplete"),
            select: submitAutoCompleteForm
        };

        $input.autocomplete(options);
    }
    var submitAutoCompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);

        var $form = $input.parents("form:first");
        $form.submit();
    }
    $('input[data-et-autocomplete]').each(createAutoComplete);

});