$(document).ready(function () {
    var events = [];
    var html = "";
    getMeetings();

    function getMeetings() {
        $.getJSON('/api/calendar/get')
            .done(function (data) {
                $.each(data, function (i, item) {
                    var dateobj = new Date(item.ConfirmedTime);
                    var obj = {
                        Id: item.Id,
                        Title: item.Title,
                        Date: new Date((dateobj.getMonth() + 1) +
                            "/" +
                            dateobj.getUTCDate() +
                            "/" +
                            dateobj.getFullYear()),
                        Time: formatTime(dateobj.getUTCHours(), dateobj.getUTCMinutes())
                }
                    events.push(obj);
                });
                $("#calendar").datepicker({
                    beforeShowDay: function (date) {
                        var result = [true, '', null];
                        var matching = $.grep(events, function (event) {
                            return event.Date.valueOf() === date.valueOf();
                        });

                        if (matching.length) {
                            result = [true, 'highlight', null];
                        }
                        return result;
                    },
                    onSelect: function (dateText) {
                        var date,
                            selectedDate = new Date(dateText),
                            i = 0,
                            selectedEvents = [];

                        while (i < events.length) {
                            date = events[i].Date;

                            if (selectedDate.valueOf() === date.valueOf()) {
                                selectedEvents.push(events[i]);
                            }
                            i++;
                        }
                        if (selectedEvents.length > 0) {
                            i = 0;
                            html = "<h4>Scheduled events</h4>";
                            while (i < selectedEvents.length) {
                                html += (selectedEvents[i].Time +
                                    ' - <a href="/meeting/profile/' +
                                    selectedEvents[i].Id +
                                    '">' +
                                    selectedEvents[i].Title +
                                    '</a> <br />');
                                i++;
                            }
                        } else {
                            html = "";
                        }
                        $("#event-content").html(html);
                    }
                });
            });
    }
});

function formatTime(hours, mins) {
    if (hours < 10)
        hours = "0" + hours;
    if (mins < 10)
        mins = "0" + mins;
    return hours + ":" + mins;
}
