$("#createTask").click(function (e) {
    e.preventDefault()
    let taskData = $("#taskForm").serialize()
    console.log(taskData)
    $.ajax({

        url: "/Task/Create",
        type: "POST",
        data: taskData,
        success: function (data) {
            if (data == "Success") {
                alert("Tarea Creada con exito")
                location.reload();
            }
            else {
                alert("Operación Fracasó")
            }

        },
        error: function (error) {
            alert(error)
        }
    });
});
