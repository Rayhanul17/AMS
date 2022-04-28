using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS
{
    
    public class Admin
    {
        
        AmsDbContext amsDbContext = new AmsDbContext();
        User user = new User();
        Role role = new Role();
        

        public void MainMenu()
        {
            while (true)
            {
                List<Role> roles = amsDbContext.Role.ToList();

                foreach (Role role in roles)
                    Console.WriteLine($"{role.Id}. Create {role.RoleTitle}");

                Console.WriteLine("4. Create Course\nX. Exit");
                Console.Write("Your Choice: ");
                string option = Console.ReadLine();

                if (option == "1" || option == "2" || option == "3")
                {
                    try
                    {
                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("UserName: ");
                        string userName = Console.ReadLine();

                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        int roleId = int.Parse(option);

                        User? user = amsDbContext.User.Where(x => x.UserName == userName).FirstOrDefault();
                        if(user != null)
                        {
                            Console.WriteLine("Chose Another Username!");
                            continue;
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Data!");

                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }

                else if (option == "4")
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

                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("Success!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Data!");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else if (option.ToLower() == "x")
                {

                    Console.WriteLine("Program Terminated!");
                    break;
                }

                //else if(option == "o" || option == "O")
                //{
                //    Console.WriteLine("Logged Out");

                //    Console.Clear();
                //    logManagement.LogIn();
                //}

                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please Enter Valid Key!");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
            }
        }
    }
}
