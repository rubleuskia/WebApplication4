(function ($) {
    const subscribe = () => {
        $(".popup").on('click', function (e) {
            showPopup(this);
        });
    }

    const showPopup = (reff) => {
        const url = $(reff).data('url');

        $.get(url).done(function (data) {
            debugger;
            const modal = $('#modal-create-edit-user');
            modal.find(".modal-dialog").html(data);
            modal.modal("show");
        });
    }

    $(function () {
        subscribe();
    });
}(jQuery));