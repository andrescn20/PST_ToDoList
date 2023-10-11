$("#editTask").click(function (e) {
    e.preventDefault()
    let taskData = $("#editTaskForm").serialize()
    $.ajax({

        url: "/Task/EditTask",
        type: "POST",
        data: taskData,
        success: function (data) {
            if (data == "Success") {
                alert("Tarea Actualizada con exito")
                window.location.href = '/Task/List'
            }
            else {
                alert("Operación Fracasó")
            }
        },
        error: function (error) {
            console.error(error);
        }
    });
});
