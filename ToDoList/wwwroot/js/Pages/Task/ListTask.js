$(".deleteBtn").click(function () {
    let taskId = $(this).data('task-id');
    $.ajax({
        url: '/Task/Delete/' + taskId,
        type: 'POST', 
        success: function (result) {
            alert(`Tarea #${taskId} eliminada con éxito`)
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log(error)
        }
    });
});

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
