function AddToFav(e) {
    var addId = e.getAttribute('data-id');
    $.ajax({
        url: '/Product/AddToFavourite',
        method: 'POST',
        data: { id: addId },
        success: function (result) {
            if (result.success) {
                $(".add-to-fav[data-id='" + addId + "']").replaceWith(`<button onclick="RemoveFromFav(this)" class="px-3 rounded-circle fw-bold text-white remove-from-fav" data-id="${result.data.id}" data-product-id=${addId}"><i class="fa-solid fa-heart"></i></button>`);
                console.log(result)
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
                $(".remove-from-fav[data-id='" + removeId + "']").replaceWith(`<button class="px-3 rounded-circle fw-bold text-white add-to-fav" onclick="AddToFav(this)" data-id="${productId}"><i class="fa-regular fa-heart"></i></button>`);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 401) {
                window.location.href = '/Account/Login';
            }
        }
    });
}