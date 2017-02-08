// Loads data into the popup window when deactivate meeting button is clicked
function deactivateMeetingModal(id) {
    var userId = id;
    $('#confirm-deactivate-meeting-button').data("id", userId);
}

// Function that sends an ajax delete request to the Meetings Api Controller to deactivate the selected meeting
$('#confirm-deactivate-meeting-button').click(function () {
    var id = $(this).data("id");
    $.ajax({
        type: "DELETE",
        url: "/Api/Meetings/" + id,
        contentType: "application/json"
    })
    .done(function (data) {
        window.location.href = data.RedirectUrl;
    });
});