using ConsoleTestApp.Services;
using DateRangeAccounting.API.Models;
using System;

namespace ConsoleTestApp
{
    internal class Program
    {
        private static string _appPath;
        private static string _token;

        private static UserService _userService;
        private static HttpClientService _clientService;
        private static DateRangeService _dateService;

        private static void Main()
        {
            AppPathMenu();

            _clientService = new HttpClientService();
            _userService = new UserService(_appPath, _clientService);
            _dateService = new DateRangeService(_appPath, _clientService);

            Menu();
            while (true)
            {
                try
                {
                    PerformOperation(MenuSelection());
                }
                catch (Exception)
                {
                }
            }
        }

        private static void AppPathMenu()
        {
            Console.WriteLine("Api address");

            _appPath = Console.ReadLine();
        }

        private static void Menu()
        {
            var names = Enum.GetNames(typeof(MenuOption));

            for (var i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]}");
            }
        }

        private static MenuOption MenuSelection()
        {
            var names = Enum.GetNames(typeof(MenuOption));

            while (true)
            {
                var selection = Console.ReadLine();

                if (int.TryParse(selection, out var result) && result >= 0 && result <= names.Length)
                {
                    return (MenuOption)(result - 1);
                }

                Console.WriteLine("Input error, try again");
            }
        }

        private static void PerformOperation(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.Register:
                    RegistrationMenu();
                    break;
                case MenuOption.Login:
                    LoginMenu();
                    break;
                case MenuOption.GetUserInfo:
                    GetUserMenu();
                    break;
                case MenuOption.GetDatesByRange:
                    GetDatesByRange();
                    break;
                case MenuOption.AddDate:
                    AddDate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
        }

        private static void RegistrationMenu()
        {
            Console.WriteLine("Login:");
            string userName = Console.ReadLine();

            Console.WriteLine("Password:");
            string password = Console.ReadLine();

            var registerResult = _userService.Register(userName, password);
            
            _token = _userService.GetToken(userName, password);
        }

        private static void LoginMenu()
        {
            Console.WriteLine("Login:");
            var userName = Console.ReadLine();

            Console.WriteLine("Password:");
            var password = Console.ReadLine();

            _token = _userService.GetToken(userName, password);

            Console.WriteLine();
        }

        private static void GetUserMenu()
        {
            if (_token == null) Console.WriteLine("Unauthorized");

            var userInfo = _userService.GetUserInfo(_token);
            Console.WriteLine("User:");
            Console.WriteLine(userInfo.Email);
        }

        private static void GetDatesByRange()
        {
            if (_token == null) Console.WriteLine("Unauthorized");

            Console.WriteLine("Beginning date:");

            DateTime start;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out start))
                    break;
                Console.WriteLine("Input error, try again");
            }

            Console.WriteLine("Ending date:");

            DateTime end;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out end))
                    break;
                Console.WriteLine("Input error, try again");
            }

            var dates = _dateService.Find(new DateRangeViewModel{ Start = start, End = end });
            
            if (dates == null) return;
            foreach (var date in dates)
            {
                Console.WriteLine($"{date.Start} - {date.End}");
            }
        }

        private static void AddDate()
        {
            if (_token == null) Console.WriteLine("Unauthorized");

            Console.WriteLine("Beginning date:");

            DateTime start;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out start))
                    break;
                Console.WriteLine("Input error, try again");
            }

            Console.WriteLine("Ending date:");

            DateTime end;

            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out end))
                    break;
                Console.WriteLine("Input error, try again");
            }

            _dateService.Add(_token, new DateRangeViewModel{Start = start, End = end});

            Console.WriteLine();
        }
        
        private enum MenuOption
        {
            Register,
            Login,
            GetUserInfo,
            GetDatesByRange,
            AddDate
        }
    }
}
