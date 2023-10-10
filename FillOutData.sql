INSERT INTO Tasks (Title, Description, RegisteredAt, IsCompleted, CompletedAt)
VALUES
    ('Crear Tarea', 'Task 1 Description', '2023-01-01 10:00:00', 1, '2023-01-02 15:30:00'),
    ('Modificar Tarea', 'Task 2 Description', '2023-01-03 09:45:00', 0, '2023-01-03 09:45:00'), -- Provide a valid CompletedAt value
    ('Eliminar Tarea', 'Task 3 Description', '2023-01-04 14:20:00', 1, '2023-01-05 11:10:00'),
    ('Mostrar Tareas', 'Task 4 Description', '2023-01-06 08:00:00', 0, '2023-01-06 08:00:00'), -- Provide a valid CompletedAt value
    ('Mostrar Pendientes', 'Task 5 Description', '2023-01-07 16:30:00', 1, '2023-01-08 12:45:00');
