using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_5105.Models;
using System.Diagnostics;

namespace Assignment3_5105.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
            //always insert variable from second statement into brackets beside view
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.ShowTeacher(id);
            return View(SelectedTeacher);
        }
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.ShowTeacher(id);
            return View(NewTeacher);
        }
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        public ActionResult New()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber, DateTime hiredate, decimal salary)
        {
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(teacherfname);
            Debug.WriteLine(teacherlname);
            Debug.WriteLine(employeenumber);
            Debug.WriteLine(hiredate);
            Debug.WriteLine(salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = teacherfname;
            NewTeacher.teacherlname = teacherlname;
            NewTeacher.employeenumber = employeenumber;
            NewTeacher.hiredate = hiredate;
            NewTeacher.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("list");
        }
        //GET: /Teacher/Update/{id}
        /// <summary>
        /// Routes to Author Update page. Gets information from school database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.ShowTeacher(id);

            return View(SelectedTeacher);
        }

        /// <summary>
        /// A post request that contains current information with new values. Redirects to the Teacher Show page.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teacherfname"></param>
        /// <param name="teacherlname"></param>
        /// <param name="employeenumber"></param>
        /// <param name="hiredate"></param>
        /// <param name="salary"></param>
        /// <returns>A webpage with the current intoformation on the Teacher.</returns>
        //POST: Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, DateTime hiredate, decimal salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.teacherfname = teacherfname;
            TeacherInfo.teacherlname = teacherlname;
            TeacherInfo.employeenumber = employeenumber;
            TeacherInfo.hiredate = hiredate;
            TeacherInfo.salary = salary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);
            return RedirectToAction("Show/" + id);
        }
    }
}