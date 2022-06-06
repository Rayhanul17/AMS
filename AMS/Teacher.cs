using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    public class Teacher
    {
        AmsDbContext amsDbContext = new AmsDbContext();
        public void MainMenu(int teacherId)
        {
            while (true)
            {
                Console.WriteLine("1. Check Attendance Report\nX. Log Out");
                Console.Write("Your Choice: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    CheckAttendanceReport(teacherId);
                }

                else if (option.ToLower() == "x")
                {
                    Console.Title = "Attendance Management System by ©RayhanSoft";
                    Console.WriteLine("Log Out Successfull!");
                    break;
                }

                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please Enter Valid Key!");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            }
        } 

        void CheckAttendanceReport(int teacherId)
        {
            var courseTeachers = amsDbContext.CourseTeacher.Where(c => c.TeacherId == teacherId).ToList();

            if (courseTeachers != null)
            {
                foreach (var item in courseTeachers)
                {
                    var course = amsDbContext.Course.Where(i => i.Id == item.CourseId).FirstOrDefault();
                    Console.WriteLine($"{course.Id}. {course.CourseName}");
                }
                Console.Write("Course ID: ");
                int courseId;
                try
                {
                    courseId = int.Parse(Console.ReadLine());
                }
                catch
                {
                    return;
                }

                List<CourseStudent> courseStudents = amsDbContext.CourseStudent.Where(a => a.CourseId == courseId).ToList();
                List<User> students = new List<User>();
                if(courseStudents != null)
                {
                    foreach(var item in courseStudents)
                    {
                        var student = amsDbContext.User.Where( i => i.Id == item.UserId).FirstOrDefault();
                        //Console.WriteLine($"{student.Id}. {student.Name}");
                        students.Add(student);
                        
                        
                    }
                    var scheduleId = amsDbContext.Schedule.Where(a => a.CourseId == courseId).FirstOrDefault().Id;
                    List<ClassTimeTable> timeTables = amsDbContext.ClassTimeTable.Where(i => i.ScheduleId == scheduleId).ToList();
                    Console.Write("{0,-15}","Date");
                    foreach (var timeTable in timeTables)
                        Console.Write($"{timeTable.ClassDate.Substring(0,5)}\t");
                    Console.WriteLine();
                    foreach (User item in students)
                    {
                        //var attendances = amsDbContext.Attendances.Where(i => i.StudentId == student.Id).ToList();
                        Console.Write("{0,-15} ",item.Name);
                        foreach (var timeTable in timeTables)
                        {
                            Attendance attendance = amsDbContext.Attendances.Where(i => i.TimingId == timeTable.Id && i.StudentId == item.Id).FirstOrDefault();
                            if (attendance != null)
                                Console.Write("✓\t");
                            else
                                Console.Write("x\t");
                            
                        }
                        Console.WriteLine();
                    }

                }

            }
        }
    }
}
