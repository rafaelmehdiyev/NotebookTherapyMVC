function AddToFav(e) {
    var productId = e.getAttribute('data-id');
    console.log($(e).siblings(".remove-from-fav"))
    $.ajax({
        url: '/Product/AddToFavourite',
        method: 'POST',
        data: { id: productId },
        success: function (result) {
            if (result.success) {
                console.log(result)
                $(e).siblings(".remove-from-fav").attr("data-id", result.data.id)
                $(e).siblings(".remove-from-fav").attr("data-product-id", productId)
                $(e).siblings(".remove-from-fav").css("pointer-events", "auto")
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 401) {
                window.location.href = '/Account/Login';
            }
        }
    });
}
function RemoveFromFav(e) {
    var removeId = e.getAttribute('data-id');
    var productId = e.getAttribute('data-product-id');
    $.ajax({
        url: '/Product/RemoveFromFavourite',
        method: 'POST',
        data: { id: removeId },
        success: function (result) {
            if (result.success) {
                console.log(result);
                $(e).siblings(".add-to-fav").attr("data-id", productId)
                /*$(".remove-from-fav").css("pointer-events", "none")*/
                $(e).siblings(".add-to-fav").css("pointer-events", "auto")
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 401) {
                window.location.href = '/Account/Login';
            }
        }
    });
}