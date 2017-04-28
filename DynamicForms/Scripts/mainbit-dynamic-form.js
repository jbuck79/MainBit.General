$(function () {

    /*
    $('form[action^=/my-form-url]').each(function () {
        var form = $(this);
        form.find('button[type=submit]').click(function () {
            // process btn click
        });
    });
    */

    // https://www.cloudconstruct.com/blog/submitting-a-form-via-ajax-with-the-custom-forms-module

    $('form[action$="/MainBit.General/Form/Submit"]').submit(function (event) {
        event.preventDefault ? event.preventDefault() : event.returnValue = false;

        var form = $(this),
            postData = form.serialize(),
            url = form.attr('action'),
            notifyElements = form.parents('.mb-request-notifies').add(form.find('.mb-request-notifies')),
            stateElements = form.parents('.mb-request-states').add(form.find('.mb-request-states')),
            stateClasses = {
                submitting: 'mb-request-submitting',
                submitted: 'mb-request-submitted'
            };

        $.ajax({
            url: url,
            type: "POST",
            data: postData + "&isajax=true",        //add this parameter so we can trigger our custom code
            beforeSend: function before(e) {
                stateElements.addClass(stateClasses.submitting);
                form.find(':input').attr("disabled", true);
            },
            complete: function after(e) {
                stateElements.removeClass(stateClasses.submitting);
                form.find(':input').removeAttr("disabled");
            },
            success: function (data) {
                if (data.messages != undefined) {
                    var messages = $.map(data.messages, function (message) { return message.Message.Text });
                    stateElements.addClass(stateClasses.submitted);
                    notifyElements.html(messages[0]);
                } else {
                    //parse the model out returned for errors
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                $(form).append("<ul><li>There was a problem with your request. Please review the information and submit again.</li></ul>");
            }
        });
    });
})