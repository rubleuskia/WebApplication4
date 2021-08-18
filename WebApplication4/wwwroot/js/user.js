(function ($) {
    const initialize = () => {
        $(".popup").on('click', function (e) {
            debugger;
            const url = $(this).data('url');

            $.get(url).done((data) => {
                debugger;
                const modalDiv = $('#modal-create-edit-user');
                modalDiv.find(".modal-dialog").html(data);
                modalDiv.modal("show");
            });
        });
    }

    $(() => initialize());
}(jQuery));