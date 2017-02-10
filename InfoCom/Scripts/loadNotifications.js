var notificationUri = '/api/notification/';
var intSec = 30; // Time in seconds between updates
var time = setInterval(loadNotifications, intSec * 1000);  // Timer

// Function that updates the user notifications if the page is reloaded, and then every 30 seconds according to the timer
$(document).ready(function () {
    loadNotifications();
    resetTimer();
});

// Function that sends a get JSON request to the Notification Api Controller 
function loadNotifications() {
    $.getJSON(notificationUri)
        .done(function (data) {
            // On success, 'data' contains the number of notifcations (if fail 'data' is always 0).
            if (data !== 0) {
                $('#notification-badge').html(data);
                $('#invitations-text').html('Invitations<span class="badge navbar-right">' + data + '</span>');
            }
        });
    resetTimer();
}

// Function that runs the loadNotifications function and resets the timer
function resetTimer() {
    clearInterval(time);
    time = setInterval(loadNotifications, intSec * 1000);
}

//Caret dropdown on hover

$(document).ready(function () {
    $('ul.nav li.dropdown').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(100).fadeIn(100);
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(100).fadeOut(100);
    });
});