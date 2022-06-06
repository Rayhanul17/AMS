using AMS;

Console.Title = "Attendance Management System by ©RayhanSoft";
string option;
do
{
    LogManagement logManagement = new LogManagement();
    logManagement.LogIn();
    Thread.Sleep(500);
    Console.Clear();
    Console.Write("Press 1 To Login Again & Another Key To Exit: ");
    option = Console.ReadLine();
}
while (option == "1");




