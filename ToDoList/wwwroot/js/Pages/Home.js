$("#completeBtn").click(function () {
    const isCompleted = {
        isCompleted: true
    }
    $.ajax({
        url: "/Task/ListByCompletion",
        type: "POST",
        data:(isCompleted),
        success: function (response) {
            console.log(response)
        },
        error: function (error) {
            console.error(error);
        }
    });
});
