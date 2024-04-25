using DataModels.Models;

namespace PatientAPIClient.AbstractClasses;

public abstract class UserInput
{
    public static PatientDetailDto GetPatientInfoFromUser()
    {

        // you could do something like this
        var p = new PatientDetailDto();

        Console.WriteLine("Enter patient first name: ");
        p.FirstName = Console.ReadLine() ?? string.Empty;

        // network is slow for me.. but you get the point right?
        // yes sir

        Console.Write("Enter patient last name: ");
        p.LastName = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter patient gender: ");
        p.Gender = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter patient date of birth (YYYY-MM-DD): ");
        p.DateOfBirth = DateTime.Parse(Console.ReadLine());
        Console.Write("Enter patient date of registration (YYYY-MM-DD): ");
        p.DateOfRegistration = DateTime.Parse(Console.ReadLine());

        // so sorry.. I did not see this logic.. concinue with withat you're doing.
        // please continue with how you implemented it before
        Console.WriteLine("Enter patient allergies (comma-separated): ");

        var resp = Console.ReadLine();

        if (resp != null)
        {
            p.Allergies = resp.Split(',').ToList();
        }

        // try this



        return p;
    }
}