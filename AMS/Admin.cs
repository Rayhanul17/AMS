

namespace AMS
{
    
    public class Admin
    {
        
        AmsDbContext amsDbContext = new AmsDbContext();
        User user = new User();
        Role role = new Role();
        

        public void MainMenu()
        {
            bool condition = true;
            while (condition)
            {


                Console.WriteLine(@"1. Create User
2. Create Course
3. Assign Teacher to Course
4. Assign Student to Course
5. Make Schedule
X. Log Out");
                Console.Write("Your Choice: ");
                string option = Console.ReadLine();

                switch (option.ToLower())
                {
                    case ("1"):
                        CreateUser();
                        break;

                    case ("2"):
                        CreateCourse();
                        break;

                    case ("3"):
                        TeacherToCourse();
                        break;                  


                    case ("4"):
                        StudentToCourse();
                        break;

                    case "5":
                        CreateSchedule();
                        break;

                    case ("x"):
                        Console.WriteLine("Log Out Successfull!");
                        condition = false;
                        break;

                    default:
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please Enter Valid Key!");
                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }
                }
            }
        }

        void CreateUser()
        {
            try
            {
                List<Role> roles = amsDbContext.Role.ToList();

                Console.WriteLine("User Role: ");
                foreach (Role role in roles)
                    Console.WriteLine($"{role.Id}. {role.RoleTitle}");

                Console.Write("Your Choice: ");
                int roleId = int.Parse(Console.ReadLine());

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("UserName: ");
                string userName = Console.ReadLine();

                Console.Write("Password: ");
                string password = Console.ReadLine();



                User? user = amsDbContext.User.Where(x => x.UserName == userName).FirstOrDefault();
                if (user != null)
                {
                    Console.WriteLine("Chose Another Username!");
                    return;
                }
                amsDbContext.User.Add(new User
                {
                    RoleId = roleId,
                    Name = name,
                    UserName = userName,
                    Password = password
                });

                amsDbContext.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid Data!");

                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        void CreateCourse()
        {
            try
            {
                Console.Write("CourseTitle: ");
                string courseName = Console.ReadLine();

                Console.Write("Fees: ");
                decimal fees = decimal.Parse(Console.ReadLine());

                amsDbContext.Course.Add(new Course
                {
                    CourseName = courseName,
                    Fees = fees
                });

                amsDbContext.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Invalid Data!");
                Console.ForegroundColor = ConsoleColor.White;
                
            }
        }

        void StudentToCourse()
        {
            try
            {
                List<Course> courses = amsDbContext.Course.ToList();
                List<User> users = amsDbContext.User.Where(x => x.RoleId == 3).ToList();

                Console.WriteLine("Course List");
                foreach (Course course in courses)
                    Console.WriteLine($"{course.Id}. {course.CourseName}");
                Console.Write("Course Id: ");
                int courseId= int.Parse(Console.ReadLine());

                foreach (User user in users)
                    Console.WriteLine($"{user.Id}. {user.UserName}");
                Console.Write("Student Id: ");
                int userId = int.Parse(Console.ReadLine());


                Course existingCourse = amsDbContext.Course.Where(c => c.Id == courseId).FirstOrDefault();
                User existingStudent = amsDbContext.User.Where(c => c.Id == userId).FirstOrDefault();

                existingCourse.Users = new List<CourseStudent>()
                {
                    new CourseStudent(){ User = existingStudent },
                    //new CourseStudent(){ User = new User(){ Name = "Sifat", UserName= "sifat", Password= "123", RoleId=3 } }
                };
                amsDbContext.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.Clear();    
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Already Student included on this course!");
                Console.ForegroundColor = ConsoleColor.White;

            }
        }

        void TeacherToCourse()
        {
            try
            {
                List<Course> courses = amsDbContext.Course.ToList();

                List<User> users = amsDbContext.User.Where(x => x.RoleId == 2).ToList();

                Console.WriteLine("Course List");
                foreach (Course course in courses)
                    Console.WriteLine($"{course.Id}. {course.CourseName}");
                Console.Write("Course Id: ");
                int courseId = int.Parse(Console.ReadLine());

                Console.WriteLine("Teacher List");
                foreach (User user in users)
                    Console.WriteLine($"{user.Id}. {user.UserName}");
                Console.Write("Teacher Id: ");
                int userId = int.Parse(Console.ReadLine());

                var courseTeacher = amsDbContext.CourseTeacher.Where(a => a.TeacherId == userId && a.CourseId == courseId).FirstOrDefault();
                if (courseTeacher != null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Already Teacher included on this course!");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
                else
                {
                    amsDbContext.CourseTeacher.Add(new CourseTeacher
                    {
                        TeacherId = userId,
                        CourseId = courseId,
                    });

                    amsDbContext.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfully Assigned");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(500);
                    Console.Clear();
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Something went wrong!");
                Console.ForegroundColor = ConsoleColor.White;

            }
        }

        void CreateSchedule()
        {
            try
            {
                List<Course> courses = amsDbContext.Course.ToList();

                Console.WriteLine("Course List");
                foreach (Course course in courses)
                    Console.WriteLine($"{course.Id}. {course.CourseName}");
                Console.Write("Course Id: ");
                int courseId = int.Parse(Console.ReadLine());

                Schedule schedule = amsDbContext.Schedule.Where(s => s.CourseId == courseId).FirstOrDefault();

                if( schedule != null)
                {
                    Console.ForegroundColor= ConsoleColor.Yellow;
                    Console.WriteLine("Already Scheduled");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
                else
                {
                    Console.Write("Class Start Date(DD-MM-YYYY): ");
                    string d = Console.ReadLine();
                    DateTime startDate = DateTime.Parse(d);

                    Console.Write("Tatal Class: ");
                    int totalClass = int.Parse(Console.ReadLine());
                    Console.WriteLine("Time must be in 24 hour format & not touch or cross 00:00 ");
                    Console.Write("Class Start Time(HH:mm): ");
                    string classStart = Console.ReadLine();
                    Console.Write("Class Finish Time(HH:mm): ");
                    string classFinish = Console.ReadLine();

                     
                    if(TimeSpan.Compare(TimeSpan.Parse(classStart), TimeSpan.Parse(classFinish)) >= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Does not fullfill timing condition!");
                        Console.ForegroundColor = ConsoleColor.White;
                        return;
                    }
                    amsDbContext.Schedule.Add(new Schedule
                    {
                        CourseId = courseId,
                        StartDate = startDate.ToString("dd-MM-yyyy"),
                        TotalClass = totalClass,
                        FromTime = classStart,
                        ToTime = classFinish
                    });

                    amsDbContext.SaveChanges();

                    int scheduleId = amsDbContext.Schedule.OrderBy(p => p.Id).Last().Id;
                    int countClass = 0;
                    int i = 0;

                    Console.WriteLine("Which days of week selected for class(0 2 4) : ");
                    foreach (DayOfWeek item in Enum.GetValues(typeof(DayOfWeek)))
                        Console.Write($"><{(int)item} for {item}><");
                    Console.WriteLine();

                    string[] days = Console.ReadLine().Split(' ');

                    int[] weekOfDay = new int[days.Length];

                    foreach (string day in days)
                        weekOfDay[i++] = int.Parse(day);

                    while (countClass != totalClass)
                    {

                        for (int j = 0; j < weekOfDay.Length; j++)
                        {
                            if ((int)startDate.DayOfWeek == weekOfDay[j])
                            {
                                amsDbContext.ClassTimeTable.Add(new ClassTimeTable
                                {
                                    ScheduleId = scheduleId,
                                    ClassDate = startDate.ToString("dd-MM-yyyy")
                                });
                                amsDbContext.SaveChanges();
                                countClass++;
                            }
                        }
                        startDate = startDate.AddDays(1);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Creating Schedule Successfull!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Thread.Sleep(500);
            Console.Clear();
        }
    }
}
