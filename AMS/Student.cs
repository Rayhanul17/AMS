using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    public class Student
    {
        AmsDbContext amsDbContext = new AmsDbContext();
        
        public void MainMenu(int id)
        {
            while (true)
            {
                Console.Write("Do You Want to Give Attendance?\n1. Yes\nX. Log Out\nYour Choice: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    GiveAttendance(id);
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

        void GiveAttendance(int studentId)
        {
            var courseIdList = amsDbContext.CourseStudent.Where(c => c.UserId == studentId).ToList();

            if (courseIdList != null)
            {
                foreach (var item in courseIdList)
                {
                    var course = amsDbContext.Course.Where(i => i.Id == item.CourseId).FirstOrDefault();
                    Console.WriteLine($"{course.Id}. {course.CourseName}");
                }
                Console.Write("Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                Schedule schedule = amsDbContext.Schedule.Where(i => i.CourseId == courseId).FirstOrDefault();

                if (schedule != null)
                {
                    try
                    {
                        TimeSpan from = TimeSpan.Parse(schedule.FromTime);
                        TimeSpan to = TimeSpan.Parse(schedule.ToTime);
                        TimeSpan now = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));


                        if ((TimeSpan.Compare(from, now) <= 0) && (TimeSpan.Compare(to, now) >= 0))
                        {
                            List<ClassTimeTable> classTimeTables = amsDbContext.ClassTimeTable.Where(i => i.ScheduleId == schedule.Id).ToList();
                            string date = DateTime.Now.ToString("dd-MM-yyyy");
                            for (int i = 0; i <= classTimeTables.Count; i++)
                            {
                                if (i != classTimeTables.Count)
                                {
                                    if (classTimeTables[i].ClassDate == date)
                                    {
                                        var attendance = amsDbContext.Attendances.Where(a => a.TimingId == classTimeTables[i].Id && a.StudentId == studentId).FirstOrDefault();
                                        if (attendance != null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Already Submitted Attendance");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            return;
                                        }
                                        else
                                        {
                                            amsDbContext.Attendances.Add(new Attendance()
                                            {
                                                StudentId = studentId,
                                                TimingId = classTimeTables[i].Id,
                                                IsAttend = true
                                            });
                                            amsDbContext.SaveChanges();
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("Submitted Attendance");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Thread.Sleep(500);
                                            Console.Clear();
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Schedule Not Found");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Schedule Not Found");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Some thing went wrong!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    

                }
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Course Not Found");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
