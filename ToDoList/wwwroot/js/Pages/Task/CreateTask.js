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
                    $.confirm({
                        title: 'Operación Exitosa',
                        content: `Nueva tareada creada. Indique si desea agregar otra o volver al inicio.`,
                        buttons: {
                            Agregar: function () {
                                location.reload();
                            },
                            Volver: function () {
                                location.href = '/'
                            }

                        }
                    });
                }
            else {
                $.confirm({
                    title: 'Operación Fallida',
                    content: `Por favor, revise los datos introducidos e intente de nuevo.`,
                    buttons: {
                        Aceptar: function () {
                        },
  
                    }
                });
            }

        },
        error: function (error) {
            alert(error)
        }
    });
});
