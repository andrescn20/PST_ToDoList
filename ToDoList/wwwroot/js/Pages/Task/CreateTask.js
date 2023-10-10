$("#createTask").click(function (e) {
    e.preventDefault()
    let taskData = $("#taskForm").serialize()
    console.log(taskData)
    $.ajax({

        url: "/Task/Create",
        type: "POST",
        data: taskData,
        success: function (data) {
            console.log(data)

        },
        error: function (error) {
            console.error(error);
        }
    });
});
