using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assignment3_5105.Models;
using MySql.Data.MySqlClient;
//this is from Christine Bittle's Blog code from gitHub as reference on Nov 11 and 12 2020
//I also have utilized Christine Bittle's youtube videos for modules 6, 7, 8 and 9 provided via Blackboard on Nov 11 and 12 2020 for educational purposes

namespace Assignment3_5105.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDBContext schooldatabase = new SchoolDBContext();
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {

            MySqlConnection Conn = schooldatabase.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Authors

            List<Teacher> schoolteachers = new List<Teacher>{};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int teacherId = (int)ResultSet["teacherId"];
                string teacherfname = (string)ResultSet["teacherfname"];
                string teacherlname = (string)ResultSet["teacherlname"];
                string employeenumber = (string)ResultSet["employeenumber"];
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.teacherId = teacherId;
                NewTeacher.teacherfname = teacherfname;
                NewTeacher.teacherlname = teacherlname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;

                //Add the Author Name to the List
                schoolteachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return schoolteachers;
        }
        [HttpGet]
        public Teacher ShowTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = schooldatabase.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int teacherId = (int)ResultSet["teacherId"];
                string teacherfname = (string)ResultSet["teacherfname"];
                string teacherlname = (string)ResultSet["teacherlname"];
                string employeenumber = (string)ResultSet["employeenumber"];
                DateTime hiredate = (DateTime)ResultSet["hiredate"];
                decimal salary = (decimal)ResultSet["salary"];

                NewTeacher.teacherId = teacherId;
                NewTeacher.teacherfname = teacherfname;
                NewTeacher.teacherlname = teacherlname;
                NewTeacher.employeenumber = employeenumber;
                NewTeacher.hiredate = hiredate;
                NewTeacher.salary = salary;
            }


            return NewTeacher;
        }

    }
}

// The reason the bottom one is an int is because we are only looking for one teacher
// the first one is IEnumerable because that returns multiple things and we were looking for a list
