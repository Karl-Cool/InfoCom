$(document).ready(function () {
    var events = [];
    getMeetings();

    function getMeetings() {
        $.getJSON('/api/calendar/get')
            .done(function (data) {
                $.each(data, function (i, item) {
                    var dateobj = new Date(item.ConfirmedTime);
                    var obj = {
                        Id: item.Id, Title: item.Title, Date: new Date((dateobj.getMonth() + 1)
                        + "/" + dateobj.getUTCDate() + "/" + dateobj.getFullYear())
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
                            event = null;

                        while (i < events.length && !event) {
                            date = events[i].Date;

                            if (selectedDate.valueOf() === date.valueOf()) {
                                event = events[i];
                            }
                            i++;
                        }
                        if (event) {
                            alert(event.Id + " " + event.Title + " " + event.Date);
                        }
                    }
                });
            });
    }
});

