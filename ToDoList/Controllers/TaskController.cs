using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList;
using System.Data.SqlClient;

namespace MVCEjemplo.Controllers
{
    public class TaskController : Controller
    {

        //Mostrar la vista Create
        public IActionResult Create()
        {
            return View();
        }

        //Sobrecarga que permite crear tareas
        [HttpPost]
        public string Create(TaskModel task)
        {
            DBService DB = new();
            string createTask = DB.CreateTask(task);

            return createTask;
        }

        public IActionResult TestConnection()
        {
            DBService DB = new();
            bool isConnectionSuccessful = DB.TestConnection();

            if (isConnectionSuccessful)
            {
                ViewBag.MyString = "Database connection test successful.";
            }
            else
            {
                ViewBag.MyString = "Database connection test failed.";
            }
            return View();
        }

        public IActionResult List()
        {
            DBService DB = new();
            List<TaskModel> TasksTable = DB.RetrieveTasks();

            return View(TasksTable);
        }

        public IActionResult ListByCompletion(bool isCompleted)
        {
            DBService DB = new();
            List<TaskModel> TasksTable = DB.RetrieveTasks(isCompleted);

            return View(TasksTable);

        }
        public IActionResult SearchTask(string searchParam)
        {
            DBService DB = new();
            List<TaskModel> TasksTable = DB.SearchTask(searchParam);

            return View(TasksTable);

        }

        public string Delete(string id)
        {
            DBService DB = new();
            string deleteTask = DB.DeleteTask(id);
            return deleteTask;
        } 

        public IActionResult Edit(string id) 
        {
            DBService DB = new();
            TaskModel task = DB.RetrieveById(id);
            return View(task);
        }

        public string EditTask(TaskModel task)
        {
            DBService DB = new();
            string editTask = DB.EditTask(task);
            return editTask;
        }

    }

}
