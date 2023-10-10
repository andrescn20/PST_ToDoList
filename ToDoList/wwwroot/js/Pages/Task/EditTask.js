$("#editTask").click(function (e) {
    e.preventDefault()
    let taskData = $("#editTaskForm").serialize()
    console.log(taskData)
    $.ajax({

        url: "/Task/EditTask",
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
