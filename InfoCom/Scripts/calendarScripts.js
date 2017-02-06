$(document).ready(function () {
    var events = [
    { Title: "Five K for charity", Date: new Date("02/13/2017") },
    { Title: "Dinner", Date: new Date("02/25/2017") },
    { Title: "Meeting with manager", Date: new Date("03/01/2017") }
    ];

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
                event = null;

            while (i < events.length && !event) {
                date = events[i].Date;

                if (selectedDate.valueOf() === date.valueOf()) {
                    event = events[i];
                }
                i++;
            }
            if (event) {
                alert(event.Title);
            }
        }
    });
});