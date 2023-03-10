using Microsoft.EntityFrameworkCore;
using SchoolDb.Data;
using SchoolDb.Models;
namespace SchoolDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new SchoolDbContext();

            string[] options = { "Hämta avdelningar", "Hämta elever", "Hämta Klass", "Hämta senaste månadens betyg", "Hämta kurser", "Lägg till elev", "Lägg till personal"};
            string body = "Välkommen";
            while(true)
            {
                ViewUI HomeView = new ViewUI("Home", options, body);
                int selectedOptions = HomeView.Run();
                switch (selectedOptions)
                {
                    case 0:
                        GetStaffRoles();
                        break;
                    case 1:
                        ShowStudents();
                        break;
                    case 2:
                        ShowClass();
                        break;
                    case 3:
                        GetLastMonthGrades();
                        break;
                    case 4:
                        GetSubjects();
                        break;
                    case 5:
                        CreateStudent();
                        break;
                    case 6:
                        CreateStaff();
                        break;
                }
            }
            
            void GetStaffRoles()
            {
                
                string[] options = new string[context.StaffRoles.Count() + 1];
                int i = 0;

                var staffRoles = context.StaffRoles.ToList();
                body = "";
                foreach (var sr in staffRoles)
                {
                    body += $"{sr.Title} {GetstaffAmount(sr)}st\n";
                }

                int GetstaffAmount(StaffRole sr)
                {
                    var myStaff = context.staff.Where(s => s.FkStaffRoleId == sr.StaffRoleId);
                    int staffCount = myStaff.Count();
                    return staffCount;
                }
            }
            void ShowStudents()
            {
                var myStudents = from s in context.Students
                                 select s;
                
                string[] options = { "Förnamn", "Efternamn"};
                ViewUI StudentsView = new ViewUI("Hur ska listan sorteras?", options, "");
                int selectedOptions = StudentsView.Run();
                switch (selectedOptions)
                {
                    case 0:         
                        string[] optionsB = { "Stigande", "Fallande" };
                        ViewUI OptionView = new ViewUI("Hur ska listan sorteras?", optionsB, "");
                        int selectedOptionsB = OptionView.Run();
                        switch (selectedOptionsB)
                        {
                            case 0:
                                myStudents = myStudents.OrderBy(s => s.Fname);
                                break;
                            case 1:
                                myStudents = myStudents.OrderByDescending(s => s.Fname);
                                //myStudents = myStudents.OrderBy(s => s.Fname);
                                break;  
                        }
                        break;
                    case 1:
                        string[] optionsC = { "Stigande", "Fallande" };
                        ViewUI OptionViewB = new ViewUI("Hur ska listan sorteras?", optionsC, "");
                        int selectedOptionsC = OptionViewB.Run();
                        switch (selectedOptionsC)
                        {
                            case 0:
                                myStudents = myStudents.OrderBy(s => s.Lname);
                                break;
                            case 1:
                                myStudents = myStudents.OrderByDescending(s => s.Lname);
                                //myStudents = myStudents.OrderBy(s => s.Fname);
                                break;
                        }
                        break;
                }               
                body = "Elever\n";
                Console.WriteLine("Loading...");
                foreach (var Student in myStudents)
                {
                    body += $"Namn: {Student.Fname} {Student.Lname}  Personnummer: {Student.SocialNum}\n";
                }
                
            }
            void ShowClass()
            {
                var myClasses = from c in context.Classes
                                select c;

                string[] options = new string[myClasses.Count()];
                int i = 0;
                foreach (var Class in myClasses)
                {                
                    options[i] = Class.Title;
                    i++;
                }
                ViewUI ShowClassView = new ViewUI("Välj Klass", options, "");
                int selectedOption = ShowClassView.Run();

                var myClassStudents = from s in context.Students
                                      where s.FkClassId == (selectedOption + 1) select s;

                body = "";
                Console.WriteLine("Loading...");
                foreach (var student in myClassStudents)
                {
                    
                    body += $"{student.Fname} {student.Lname} \n";
                }

                               
            }
            void GetLastMonthGrades()
            {
                var gradeList = context.Grades.FromSqlRaw("SELECT * FROM Grade WHERE DateAdded >= DateAdd(Day, DateDiff(Day, 0, GetDate())-30, 0)");
                var studentList = context.Students.FromSqlRaw("SELECT * FROM Student").ToList();
                var subjectList = context.Subjects.FromSqlRaw("SELECT * FROM Subject").ToList();
                body = "";
                foreach (var g in gradeList)
                {
                    body += $"Elev: {studentList[g.FkStudentId - 1].Fname} {studentList[g.FkStudentId - 1].Lname}  Ämne: {subjectList[g.FkSubjectId - 1].Title}   Betyg: {g.Grade1}   Datum: {g.DateAdded}\n";
                }
            }
            void GetSubjects()
            {
                var subjectList = context.Subjects.FromSqlRaw("SELECT * FROM Subject").ToList();
                
                body = "";
                int i = 0;
                foreach (var s in subjectList)
                {

                    var gradeList = context.Grades.FromSqlRaw($"SELECT * FROM Grade WHERE FK_SubjectId = {i + 1}");
                    float avarageGrade = 0;
                    foreach(var g in gradeList)
                    {
                        avarageGrade = avarageGrade + g.Grade1;

                    }
                    avarageGrade = (avarageGrade / gradeList.Count());
                    
                    body += $" {s.Title}   Snittbetyg: {avarageGrade}\n";
                    i++;
                }
            }
            void CreateStudent()
            {
                Console.Clear();
                Console.Write("Förnamn: ");
                string firstName = Console.ReadLine();
                Console.Write("Efternamn: ");
                string lastName = Console.ReadLine();
                Console.Write("Personnummer: ");
                string socialNumber = Console.ReadLine();

                Console.WriteLine("Loading...");
                var classList = from c in context.Classes
                                   select c;
                string[] options = new string[classList.Count()];
                int i = 0;
                foreach (var c in classList)
                {
                    options[i] = c.Title;
                    i++;
                }
                ViewUI ShowClassView = new ViewUI("Klass", options, "");
                int selectedOption = ShowClassView.Run();
                int classId = (selectedOption + 1);
                //ToDo fixa raden under
                context.Database.ExecuteSqlRaw($"INSERT INTO Student (FName, LName, SocialNum, FK_ClassId) VALUES ('{firstName}', '{lastName}', '{socialNumber}', '{classId}')");
                body = "Elev tillagd!";
            }
            void CreateStaff()
            {
                Console.Clear();
                staff staff = new();
                Console.Write("Förnamn: ");
                staff.Fname = Console.ReadLine();
                Console.Write("Efternamn: ");
                staff.Lname = Console.ReadLine();


                Console.WriteLine("Loading...");
                var myStaffRoles = from sr in context.StaffRoles
                              select sr;
                string[] options = new string[myStaffRoles.Count()];
                int i = 0;
                foreach (var StaffRole in myStaffRoles)
                {
                    options[i] = StaffRole.Title;
                    i++;
                }
                ViewUI CreateStaffView = new ViewUI("Roll", options, "");
                int selectedOption = CreateStaffView.Run();

                staff.FkStaffRoleId = selectedOption + 1;

                Console.WriteLine("Loading...");
                context.Add(staff);
                context.SaveChanges();

                body = "Personal tillagd!";

            }           
        }
    }
}