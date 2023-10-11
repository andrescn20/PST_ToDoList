using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList
{
    public class DBService
    {
        readonly static string connectionString = "Server=Andres-HP;Database=PTS_TodoList;Trusted_Connection=True;";
        SqlConnection connection;

        public DBService()
        {
            connection = new SqlConnection(connectionString);
        }
        public void openConnection()
        {
            connection.Open();
        }

        public void closeConnection()
        {
            connection.Close();
        }
        public bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<TaskModel> RetrieveTasks()
        {
            List<TaskModel> tasks = new List<TaskModel>();

            string sqlQuery = "SELECT * FROM Tasks";
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TaskModel task = new TaskModel
                        {
                            Id = (int)reader["id"],
                            Title = (string)reader["title"],
                            Description = (string)reader["description"],
                            RegisteredAt = (DateTime)reader["registeredAt"],
                            IsCompleted = (bool)reader["isCompleted"],
                            CompletedAt = reader.IsDBNull(reader.GetOrdinal("completedAt"))
                                     ? null  
                                     : (DateTime)reader["completedAt"],
                        };

                        tasks.Add(task);
                    }

                }
            }
            return tasks;
        }
        public List<TaskModel> RetrieveTasks(bool isCompleted)
        {
            List<TaskModel> tasks = new List<TaskModel>();

            string sqlQuery = "SELECT * FROM Tasks WHERE isCompleted = @IsCompleted";
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.Add("@IsCompleted", SqlDbType.Bit).Value = isCompleted;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TaskModel task = new TaskModel
                        {
                            Id = (int)reader["id"],
                            Title = (string)reader["title"],
                            Description = (string)reader["description"],
                            RegisteredAt = (DateTime)reader["registeredAt"],
                            IsCompleted = (bool)reader["isCompleted"],
                            CompletedAt = reader.IsDBNull(reader.GetOrdinal("completedAt"))
                                     ? null
                                     : (DateTime)reader["completedAt"],
                        };

                        tasks.Add(task);
                    }

                }
            }
            return tasks;
        }
        public List<TaskModel> SearchTask(string searchParam)
        {
            List<TaskModel> tasks = new List<TaskModel>();

            string sqlQuery = "SELECT * FROM Tasks WHERE title LIKE '%' + @SearchParam + '%' " +
                "OR  description LIKE '%' + @SearchParam + '%';";

            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.Add("@SearchParam", SqlDbType.VarChar).Value = searchParam;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TaskModel task = new TaskModel
                        {
                            Id = (int)reader["id"],
                            Title = (string)reader["title"],
                            Description = (string)reader["description"],
                            RegisteredAt = (DateTime)reader["registeredAt"],
                            IsCompleted = (bool)reader["isCompleted"],
                            CompletedAt = reader.IsDBNull(reader.GetOrdinal("completedAt"))
                                     ? null
                                     : (DateTime)reader["completedAt"],
                        };

                        tasks.Add(task);
                    }

                }
            }
            return tasks;
        }
        public string CreateTask(TaskModel task)
        {
                try
                {
                    connection.Open();

                    DateTime registeredDate = DateTime.Now;

                    string insertQuery = "INSERT INTO Tasks (title, description, registeredAt, isCompleted, completedAt) " +
                            "VALUES (@Title, @Description, @RegisteredAt, @IsCompleted, @CompletedAt)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {

                        // Add parameters to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@Title", task.Title);
                        command.Parameters.AddWithValue("@Description", task.Description);
                        command.Parameters.AddWithValue("@RegisteredAt", registeredDate);
                        command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                        if (task.CompletedAt == null)
                        {
                            command.Parameters.AddWithValue("@CompletedAt", DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@CompletedAt", task.CompletedAt);
                        }


                        // Execute the SQL command
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Data was successfully inserted
                            return "Success"; // Redirect to a success page or the list of items
                        }
                        else
                        {
                            // No rows were affected, so insertion failed
                            return "failed"; // Return to the create view with an error message
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it as needed
                    return "An error occurred: " + ex.Message; // Return to the create view with an error message
                }
            
        }
        public string DeleteTask(string id)
        {
            try
            {
                connection.Open();

                string insertQuery = "DELETE FROM Tasks WHERE id = @Id";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {

                    command.Parameters.AddWithValue("@Id", id );

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Success"; 
                    }
                    else
                    {
                        return "failed";
                    }
                }
            }
            catch (Exception ex)
            {
                return "An error occurred: " + ex.Message; 
            }

        }
        public string CompleteTask(string id)
        {
            try
            {
                connection.Open();
                DateTime completedDate = DateTime.Now;

                string insertQuery = "UPDATE Tasks SET isCompleted = @IsCompleted, completedAt = @completedAt WHERE id = @Id;";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {


                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@IsCompleted", true);
                    command.Parameters.AddWithValue("@CompletedAt", completedDate);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "failed";
                    }
                }
            }
            catch (Exception ex)
            {
                return "An error occurred: " + ex.Message;
            }

        }
        public TaskModel RetrieveById(string id)
        {
            TaskModel task = null;

            string sqlQuery = "SELECT * FROM Tasks WHERE id = @Id";
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        task = new TaskModel
                        {
                            Id = (int)reader["id"],
                            Title = (string)reader["title"],
                            Description = (string)reader["description"],
                            RegisteredAt = (DateTime)reader["registeredAt"],
                            IsCompleted = (bool)reader["isCompleted"],
                            CompletedAt = reader.IsDBNull(reader.GetOrdinal("completedAt"))
                                     ? null
                                     : (DateTime)reader["completedAt"],
                        };
                    }

                }
            }
            return task;
        }

        public string EditTask(TaskModel task)
        {

            try
            {
                connection.Open();

                string insertQuery = "UPDATE Tasks SET" +
                    "    title = @Title," +
                    "    description = @Description," +
                    "    registeredAt = @RegisteredAt," +
                    "    isCompleted = @IsCompleted," +
                    "    completedAt = @CompletedAt " +
                    "WHERE    Id = @TaskId;" ;
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {

                    command.Parameters.AddWithValue("@TaskID", task.Id);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@RegisteredAt", task.RegisteredAt);
                    command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
                    if (task.CompletedAt == null || !task.IsCompleted)
                    {
                        command.Parameters.AddWithValue("@CompletedAt", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CompletedAt", task.CompletedAt);
                    }


                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "failed";
                    }
                }
            }
            catch (Exception ex)
            {
                return "An error occurred: " + ex.Message;
            }

        }



    }
}

