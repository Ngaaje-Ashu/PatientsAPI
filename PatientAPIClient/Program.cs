using System.Net.Http.Json;

namespace PatientAPIClient;

public class Program
{
    static async Task Main(string[] args)
    {
        var dog = new Dog();
        var cat = new Cat
        {
            Color = "Blue"
        };

        cat.Color = "Red";
        var user = new User()
        {
            Id = "234",
            FirstName = "Ashu",
            LastName = "Ngaaje",
            UserName = "nashua",
            Password = "123456789"
        };

        var userDto = Mapper.Map(user);
        Console.WriteLine(userDto.LastName);
        Console.ReadKey();


        //ShowAnimalColor.MakeAnimalsSpeak(dog, cat);
        //var input = new ConsoleInput();
        //var output = new DebugOutput();

        //var patientApiClient = new PatientApiClient(output, input);


        //while (true)
        //{
        //    Console.WriteLine("Choose an action:");
        //    Console.WriteLine("1. Get all patients");
        //    Console.WriteLine("2. Get patient by ID");
        //    Console.WriteLine("3. Add new patient");
        //    Console.WriteLine("4. Update patient");
        //    Console.WriteLine("5. Delete patient");
        //    Console.WriteLine("6. Exit");

        //    var choice = Console.ReadLine();

        //    switch (choice)
        //    {
        //        case "1":
        //            await patientApiClient.GetAllPatients(); //GetAllPatients(httpClient, baseUrl);
        //            break;
        //        case "2":
        //            await patientApiClient.GetPatientById();
        //            break;
        //        case "3":
        //            await patientApiClient.AddNewPatient();

        //            break;
        //        case "4":
        //            await patientApiClient.UpdatePatient();
        //            break;
        //        case "5":
        //            await patientApiClient.DeletePatient();
        //            break;
        //        case "6":
        //            return;
        //        default:
        //            Console.WriteLine("Invalid choice.");
        //            break;
        //    }
        //}

    }

}

public record UserDto(string Id, string FirstName, string LastName);

public class User
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public abstract class Animal
{
    public string Color
    {
        get;
        set;
    }

    public abstract void Speak();
}

public class Cat : Animal
{
    public int NumberOfWhiskers { get; set; }
    public override void Speak()
    {
        Console.WriteLine("Meouwww");
    }
}


public class Dog : Animal
{
    public bool IsNgongDog { get; set; }
    public override void Speak()
    {
        Console.WriteLine("Bark");
    }
}

public static class ShowAnimalColor
{
    public static void ShowColor(Animal animal)
    {
        Console.WriteLine(animal.Color);
        Console.ReadKey();
    }

    public static void MakeAnimalsSpeak(params Animal[] animals)
    {
        animals.ToList().ForEach(a => a.Speak());
    }
}

public static class Mapper
{
    public static UserDto Map(User user)
    {
        return new UserDto(user.Id, user.FirstName, user.LastName);
    }
}