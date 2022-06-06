
namespace AMS
{
    public class LogManagement 
    {

        AmsDbContext amsDbContext = new AmsDbContext();
        User user = new User();
        Teacher teacher = new Teacher();
        Student student = new Student();
        Admin admin = new Admin();
        Role role = new Role();


        public void LogIn()
        {
            Console.Write("UserName: ");
            var userName = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

           
            List<User> users = amsDbContext.User.ToList();

            for (int i = 0; i <= users.Count; i++)
            {
                if (i == users.Count)
                {
                    Console.WriteLine("Login Failed!");
                    break;
                }
                if (users[i].UserName == userName)
                {
                    if (users[i].Password == password)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Login Success");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(500);
                        Console.Clear();

                        role = amsDbContext.Role.Where(x => x.Id == users[i].RoleId).FirstOrDefault();
                        user = amsDbContext.User.FirstOrDefault(x => x.UserName == userName);
                       
                        LogInfo(user.Name, role.RoleTitle);

                        if (users[i].RoleId == 1)
                        {
                            admin.MainMenu();

                        }

                        else if (users[i].RoleId == 2)
                        {
                            teacher.MainMenu(user.Id);
                        }

                        else if (users[i].RoleId == 3)
                        {
                            student.MainMenu(user.Id);

                        }

                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Login Failed!");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
            }
        }

        void LogInfo(string name, string role)
        {
            Console.Title = $"{name} at Attendance System as {role}";
        }
    }
}
