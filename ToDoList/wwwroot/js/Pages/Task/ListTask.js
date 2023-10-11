$(".deleteBtn").click(function () {
    let taskId = $(this).data('task-id');
    console.log(taskId)

    $.confirm({
        title: 'Atención: está a punto de borrar una Tarea.',
        content: 'Seleccione "Confirmar" para continuar:',
        buttons: {
            confirmar: function () {
                deleteTask(taskId)
                $.confirm({
                    title: 'Operación Exitosa',
                    content: `Tarea #${taskId} eliminada con éxito`,
                    buttons: {
                        confirmar: function () {
                            location.reload();
                        },
                    }
                });
            },
            cancelar: function () {
                $.alert('Cancelado. La tarea no fue borrada');
            },
        }
    });
})

function deleteTask(taskId) {
    $.ajax({
        url: '/Task/Delete/' + taskId,
        type: 'POST',
        success: function (result) {
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    });

}

$(".editBtn").click(function () {
    let taskId = $(this).data('task-id');
    window.location.href = '/Task/Edit/' + taskId;
});
$(".completeBtn").click(function () {
    let taskId = $(this).data('task-id');
    $.ajax({
        url: '/Task/Complete/' + taskId,
        type: 'POST',
        success: function (result) {
            alert(`Tarea #${taskId} completada con éxito`)
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    });
});
