using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PE_wed_part1.DataAccess;
using System.Data;

namespace PE_wed_part1.Pages.Students
{
    public class ListModel : PageModel
    {
        public List<Major> majors;
        public List<Student> students;
        private readonly PePrn221Context context = new PePrn221Context();
        public void OnGet()
        {
            majors = context.Majors.ToList();
            students = context.Students.ToList();
        }

        public void OnPostFilter(string major, string gander, string sortBy)
        {
            majors = context.Majors.ToList();
            bool male = gander == "Male";
            students = context.Students.Where(s=>s.Major == major && s.Male == male).ToList();
            switch (sortBy)
            {
                case "Name":
                    students = students.OrderBy(s=>s.FullName).ToList();
                    break;
                case "Id":
                    students = students.OrderBy(s => s.StudentId).ToList();
                    break;
                case "Dob":
                    students = students.OrderBy(s => s.Dob).ToList();
                    break;
            }
            ViewData["major"] = major;
            ViewData["gander"] = gander;
            ViewData["sortBy"] = sortBy;
        }
    }
}
