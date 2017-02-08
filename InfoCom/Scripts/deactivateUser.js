// Loads data into the popup window when delete user button is clicked
function deactivateUserModal(id) {
    var userId = id;
    $('#confirm-deactivate-user-button').data("id", userId);
}

// Function that sends an ajax delete request to the User Api Controller to deactivate the selected user
$('#confirm-deactivate-user-button').click(function () {
    var id = $(this).data("id");
    $.ajax({
        type: "DELETE",
        url: "/Api/Users/" + id,
        contentType: "application/json"
    })
    .done(function (data) {
        window.location.href = data.RedirectUrl;
    });
});