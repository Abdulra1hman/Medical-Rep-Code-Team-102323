using System;
using System.Collections.Generic;
using System.Threading;

class User
{
    public string Name { get; set; }
    public string Gmail { get; set; }
    public string Password { get; set; }
    public string Birth { get; set; }
    public string Gender { get; set; }
    public string Location { get; set; }
}

class UserAccountManager
{
    public static Dictionary<string, User> userAccounts = new Dictionary<string, User>();
}

class CreateAccount : User
{
    public void CreateAccountMethod()
    {
        Console.WriteLine("Enter the Full Name:");
        Name = Console.ReadLine();

        bool validEmail = true;
        while (validEmail)
        {
            Console.WriteLine("Enter the Gmail:");
            Gmail = Console.ReadLine();

            if (Gmail.Contains("@gmail.com"))
            {
                validEmail = false;
            }
            else
            {
                Console.WriteLine("Invalid email format! Please enter a valid Gmail address.");
                Console.WriteLine();
            }
        }

        Console.WriteLine("Enter the Password:");
        Password = Console.ReadLine();

        Console.WriteLine("Enter the gender:");
        Gender = Console.ReadLine();

        Console.WriteLine("Enter the birth date:");
        Birth = Console.ReadLine();

        Console.WriteLine("Enter the location:");
        Location = Console.ReadLine();

        UserAccountManager.userAccounts[Gmail] = new User
        {
            Name = Name,
            Gmail = Gmail,
            Password = Password,
            Gender = Gender,
            Birth = Birth,
            Location = Location
        };

        Console.WriteLine("Processing...");
        Thread.Sleep(1000);
        Console.WriteLine();
        Console.WriteLine("Successful registration");
        Console.WriteLine();
    }
}

class Login : User
{
    static bool isLoggedIn = false;

    public bool LogIn()
    {
        if (isLoggedIn)
        {
            Console.WriteLine("You are already logged in.");
            return true;
        }

        bool validEmail = true;
        while (validEmail)
        {
            Console.WriteLine("Enter the Gmail (or type 'back' to return):");
            Gmail = Console.ReadLine();

            if (Gmail.ToLower() == "back")
            {
                return false; 
            }

            if (Gmail.Contains("@gmail.com") && UserAccountManager.userAccounts.ContainsKey(Gmail))
            {
                validEmail = false;
            }
            else
            {
                Console.WriteLine("Invalid email format or account not found! Please enter a valid Gmail address.");
            }
        }

        bool validPassword = false;
        while (!validPassword)
        {
            Console.WriteLine("Enter the Password (or type 'back' to return):");
            Password = Console.ReadLine();

            if (Password.ToLower() == "back")
            {
                return false; 
            }

            if (UserAccountManager.userAccounts[Gmail].Password == Password)
            {
                Console.WriteLine("Processing...");
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("Welcome back");
                Console.WriteLine();
                isLoggedIn = true;
                validPassword = true;
            }
            else
            {
                Console.WriteLine("Incorrect password! Please try again.");
            }
        }
        return isLoggedIn;
    }
}

class Doctor : User
{
    private static List<string> ReceivedMessages = new List<string>();
    private static List<string> BookedAppointments = new List<string>();
    private static List<string> AvailableMedicalReps = new List<string> { "Medical rep 1", "Medical rep 2", "Medical rep 3" };

    public void ReceiveMessagesFromMedicalReps()
    {
        Console.WriteLine("Received Messages from Medical Reps:");
        foreach (var message in MedicalRep.SentMessages)
        {
            ReceivedMessages.Add(message);
            Console.WriteLine(message);
        }
        Console.WriteLine();
    }

    public void ViewBookedAppointmentsWithMedicalReps()
    {
        Console.WriteLine("Your Booked Appointments with Medical Reps:");
        foreach (var appointment in MedicalRep.BookedAppointments)
        {
            BookedAppointments.Add(appointment);
            Console.WriteLine(appointment);
        }
        Console.WriteLine();
    }

    public void ViewAvailableMedicalReps()
    {
        Console.WriteLine("Available Medical Reps:");
        foreach (var rep in AvailableMedicalReps)
        {
            Console.WriteLine(rep);
        }
        Console.WriteLine();
    }

    public void SendNewMessage()
    {
        Console.WriteLine("Enter your message to the medical reps:");
        string message = Console.ReadLine();
        MedicalRep.ReceivedMessages.Add(message); 
        Console.WriteLine("Message sent successfully!");
    }

    public void ViewProfile()
    {
        Console.WriteLine("Doctor Profile:");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Email: {Gmail}");
        Console.WriteLine($"Gender: {Gender}");
        Console.WriteLine($"Birth Date: {Birth}");
        Console.WriteLine($"Location: {Location}");
        Console.WriteLine();
    }
}

class MedicalRep : User
{
    public static List<string> SentMessages = new List<string>();
    public static List<string> ReceivedMessages = new List<string>();
    public static List<string> BookedAppointments = new List<string>();
    private static List<string> AppointmentBooking = new List<string>();

    public void SendNewMessage()
    {
        Console.WriteLine("Enter your message to the doctors:");
        string message = Console.ReadLine();
        SentMessages.Add(message);
        Console.WriteLine("Message sent successfully!");
    }

    public void ViewReceivedMessages()
    {
        Console.WriteLine("Received Messages from Doctors:");
        foreach (var message in ReceivedMessages)
        {
            Console.WriteLine(message);
        }
        Console.WriteLine();
    }

    public void BookAppointment()
    {
        Console.WriteLine("Enter the name of the doctor:");
        string doctorName = Console.ReadLine();
        Console.WriteLine("Choose an available date:");

        foreach (var dd in BookedAppointments)
        {
            Console.WriteLine(dd);
        }
        string date = Console.ReadLine();

        BookedAppointments.Add($"{doctorName}: {date}");
        Console.WriteLine("Appointment booked successfully!");
    }

    public void ViewBookedAppointments()
    {
        Console.WriteLine("Booked Appointments:");
        foreach (var appointment in BookedAppointments)
        {
            Console.WriteLine(appointment);
        }
        Console.WriteLine();
    }

    public void ShowAllDoctors()
    {
        Console.WriteLine("Available Doctors:");
        Console.WriteLine("1. Dr. Mina, Pharmacist");
        Console.WriteLine("2. Dr. Mahmoud, Dermatologist");
        Console.WriteLine("3. Dr. Sara, Pediatrician");
        Console.WriteLine("4. Dr. Ramadan, Neurologist");
        Console.WriteLine("5. Dr. Kareem, Oncologist");
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        bool mainLoop = true;
        while (mainLoop)
        {
            Console.WriteLine("Welcome to the Infinity Team Project");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Please choose: 1- Login   2- Create Account");
            string choiceInput = Console.ReadLine();
            int choice;

            while (!int.TryParse(choiceInput, out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid choice, please choose: 1- Login   2- Create Account");
                choiceInput = Console.ReadLine();
            }

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Login login = new Login();
                    if (!login.LogIn())
                    {
                        continue; 
                    }
                    break;
                case 2:
                    CreateAccount createAccount = new CreateAccount();
                    createAccount.CreateAccountMethod();
                    break;
            }

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Choose: 1- Doctor  2- Medical Rep  3- Exit");
            string userTypeInput = Console.ReadLine();
            int userType;

            while (!int.TryParse(userTypeInput, out userType) || (userType != 1 && userType != 2 && userType != 3))
            {
                Console.WriteLine("Invalid user type, please choose: 1- Doctor  2- Medical Rep  3- Exit");
                userTypeInput = Console.ReadLine();
            }

            Console.WriteLine();

            bool userLoop = true;
            while (userLoop)
            {
                switch (userType)
                {
                    case 1:
                        Doctor doctor = new Doctor();
                        Console.WriteLine("Welcome, Doctor!");
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine("Select an option:");
                        Console.WriteLine("1- Receive messages from medical reps");
                        Console.WriteLine("2- View booked appointments with medical reps");
                        Console.WriteLine("3- View available medical reps");
                        Console.WriteLine("4- Send message to medical reps");
                        Console.WriteLine("5- View Profile");
                        Console.WriteLine("6- Log out");
                        string doctorOptionInput = Console.ReadLine();
                        int doctorOption;

                        while (!int.TryParse(doctorOptionInput, out doctorOption) || (doctorOption < 1 || doctorOption > 6))
                        {
                            Console.WriteLine("Invalid choice, please select a valid option:");
                            doctorOptionInput = Console.ReadLine();
                        }

                        Console.WriteLine();

                        switch (doctorOption)
                        {
                            case 1:
                                doctor.ReceiveMessagesFromMedicalReps();
                                break;
                            case 2:
                                doctor.ViewBookedAppointmentsWithMedicalReps();
                                break;
                            case 3:
                                doctor.ViewAvailableMedicalReps();
                                break;
                            case 4:
                                doctor.SendNewMessage();
                                break;
                            case 5:
                                doctor.ViewProfile();
                                break;
                            case 6:
                                userLoop = false;
                                break;
                        }
                        break;
                    case 2:
                        MedicalRep medicalRep = new MedicalRep();
                        Console.WriteLine("Welcome, Medical Rep!");
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine("Select an option:");
                        Console.WriteLine("1- Send new message to doctors");
                        Console.WriteLine("2- View received messages from doctors");
                        Console.WriteLine("3- Book appointment with doctor");
                        Console.WriteLine("4- View booked appointments");
                        Console.WriteLine("5- Show all available doctors");
                        Console.WriteLine("6- Log out");
                        string medicalRepOptionInput = Console.ReadLine();
                        int medicalRepOption;

                        while (!int.TryParse(medicalRepOptionInput, out medicalRepOption) || (medicalRepOption < 1 || medicalRepOption > 6))
                        {
                            Console.WriteLine("Invalid choice, please select a valid option:");
                            medicalRepOptionInput = Console.ReadLine();
                        }

                        Console.WriteLine();

                        switch (medicalRepOption)
                        {
                            case 1:
                                medicalRep.SendNewMessage();
                                break;
                            case 2:
                                medicalRep.ViewReceivedMessages();
                                break;
                            case 3:
                                medicalRep.BookAppointment();
                                break;
                            case 4:
                                medicalRep.ViewBookedAppointments();
                                break;
                            case 5:
                                medicalRep.ShowAllDoctors();
                                break;
                            case 6:
                                userLoop = false;
                                break;
                        }
                        break;
                    case 3:
                        userLoop = false;
                        mainLoop = false;
                        break;
                }
            }
        }
    }
}
