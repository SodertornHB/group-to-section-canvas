$('.submitbutton').on('click', function (e) {
    let Id = e.target.name;
    console.log(e.target.name);

    $('#txtFeedback-' + Id).text('(Sparar...)')
    payload = {
        'groupId': Id
    }

    var url = '/lti/update-group-to-section';
    if ('@Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")' == 'Production') {
        url = '/canvas-integration' + url;
    }

    $.ajax({
        type: "POST",
        url: url,
        data: payload,
        success: function () {
            $('#txtFeedback-' + Id).text('Sparad!')
        },
        error: function (response) {
            $('#txtFeedback-' + Id).text(response.responseText);
        }
    });
});