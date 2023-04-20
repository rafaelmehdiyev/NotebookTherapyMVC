$('.add-to-cart').click(function () {
    var id = $(this).data('id');
    $.ajax({
        url: '/Product/AddItemToCart',
        method: 'POST',
        data: { id: id },
        success: function (result) {
            Swal.fire({
                position: 'top',
                title: result.message,
                background: '#28a745',
                customClass: {
                    title: 'text-white',
                    content: 'text-white',
                    popup: 'bg-success'
                },
                showConfirmButton: false,
                timer: 1500
            })
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 401) {
                window.location.href = '/Account/Login';
            }
        }
    });
});