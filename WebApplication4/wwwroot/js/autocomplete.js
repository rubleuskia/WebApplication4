(function ($, autocomplete) {
    const init = (inputId, targetId) => {
        const input = $(inputId);
        const url = input.data('url');
        input.autocomplete({
            source: (request, response) => {
                $.ajax({
                    url: url,
                    data: { "prefix": request.term },
                    type: "POST",
                    success: (data) => {
                        response(data);
                    },
                    error: (response) => {
                        console.error(response.responseText);
                    },
                    failure: (response) => {
                        console.error(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $(targetId).val(i.item.val);
            },
            minLength: 0
        })
        .focus(function() {
            $(this).autocomplete("search");
        });
    }
    autocomplete.init = init;
}(jQuery, window.autocomplete = window.autocomplete || {}));