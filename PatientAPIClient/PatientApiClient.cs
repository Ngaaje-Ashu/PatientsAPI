using System.Diagnostics;
using System.Net.Http.Json;
using System.Net.Http;
using System.Runtime.CompilerServices;
using PatientAPIClient.AbstractClasses;
using DataModels.Models;

namespace PatientAPIClient;

public class PatientApiClient : ApiClient
{
    private readonly IOutput _output;
    private readonly IInput _input;

    public PatientApiClient(IOutput output, IInput input)
    {
        this._output = output;
        this._input = input;
        //this.BaseUrl = new Uri($"{DefaultAddress}/Patient");
    }


    public async Task GetAllPatients()
    {

        // let me give you a tip: PatientApiClient has a "dependency" on Console
        // we don't want that.. we want the depency to be inverted i.e. all dependencies are injected
        //
        try
        {
            HttpResponseMessage response = await this.Client.GetAsync("Patient");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                List<PatientDetailDto>? patients = await response.Content.ReadFromJsonAsync<List<PatientDetailDto>>();
                this._output.WriteLine("All patients:\n");
                foreach (var patient in patients)
                {
                    this._output.WriteLine(
                        $"ID: {patient.Id}\n" +
                        $"First Name: {patient.FirstName}\n" +
                        $"Last Name: {patient.LastName}\n" +
                        $"Date of Birth: {patient.DateOfBirth}\n" +
                        $"Gender: {patient.Gender}\n" +
                        $"Allergies: {patient.Allergies}\n" +
                        $"Date of Registration: {patient.DateOfRegistration}\n\n"
                    );
                }
            }
        }
        catch (HttpRequestException e)
        {
            this._output.WriteLine($"\nHTTP request error: {e.Message}\n");
        }
    }

    // continue with the rest please.. I will call you after

    public async Task GetPatientById()
    {
        this._output.Write("Enter patient ID: ");
        var id = int.Parse(this._input.ReadLine());
        try
        {
            HttpResponseMessage response = await this.Client.GetAsync( $"Patient/{id}");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                PatientDetailDto? patient = await response.Content.ReadFromJsonAsync<PatientDetailDto>();
                this._output.WriteLine($"Patient info with id {id}:\n");
                this._output.WriteLine(
                    $"ID: {patient.Id}\n" +
                    $"First Name: {patient?.FirstName}\n" +
                    $"Last Name: {patient?.LastName}\n" +
                    $"Date of Birth: {patient?.DateOfBirth}\n" +
                    $"Gender: {patient?.Gender}\n" +
                    $"Allergies: {patient?.Allergies}\n" +
                    $"Date of Registration: {patient?.DateOfRegistration}\n\n"
                );

            }


        }
        catch (HttpRequestException e)
        {
            this._output.WriteLine($"\nHTTP request error: {e.Message}\n");
        }
    }

    public async Task AddNewPatient()
    {

        PatientDetailDto newPatient = UserInput.GetPatientInfoFromUser();

        try
        {
            HttpResponseMessage response = await this.Client.PostAsJsonAsync("Patient", newPatient);
            response.EnsureSuccessStatusCode();

            this._output.WriteLine("New patient added successfully.");
        }
        catch (HttpRequestException e)
        {
            this._output.WriteLine($"\nHTTP request error: {e.Message} \n");
        }
    }

    public async Task UpdatePatient()
    {
        this._output.Write("Enter patient ID to update: ");
        var id = int.Parse(this._input.ReadLine());

        this._output.Write("Enter new patient first name: ");
        var firstName = this._input.ReadLine();
        this._output.Write("Enter new patient last name: ");
        var lastName = this._input.ReadLine();
        this._output.Write("Enter new patient gender: ");
        var gender = this._input.ReadLine();
        this._output.Write("Enter new patient date of birth (YYYY-MM-DD): ");
        var dateOfBirth = DateTime.Parse(this._input.ReadLine());
        this._output.Write("Enter new patient date of registration (YYYY-MM-DD): ");
        var dateOfRegistration = DateTime.Parse(this._input.ReadLine());
        this._output.WriteLine("Enter new patient allergies (comma-separated): ");
        var allergiesInput = this._input.ReadLine();
        var allergies = allergiesInput.Split(',');

        var updatedPatient = new
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            DateOfRegistration = dateOfRegistration,
            Allergies = allergies
        };

        try
        {
            HttpResponseMessage response = await this.Client.PutAsJsonAsync($"Patient/{id}", updatedPatient);
            response.EnsureSuccessStatusCode();

            this._output.WriteLine("Patient updated successfully.");
        }
        catch (HttpRequestException e)
        {
            this._output.WriteLine($"\nHTTP request error: {e.Message} \n");
        }
    }

     public async Task DeletePatient()
    {
        this._output.Write("Enter patient ID to delete: ");
        var id = int.Parse(this._input.ReadLine());

        try
        {
            HttpResponseMessage response = await this.Client.DeleteAsync($"Patient/{id}");
            response.EnsureSuccessStatusCode();

            this._output.WriteLine("Patient deleted successfully.");
        }
        catch (HttpRequestException e)
        {
            this._output.WriteLine($"\nHTTP request error: {e.Message}\n");
        }
    }
}


public interface IInput
{
   string? ReadLine();
}

public class ConsoleInput : IInput
{
    public string ReadLine()
    {
        return Console.ReadLine();
        //return null;
    }
}
public interface IOutput
{
    public void Write(string message);
    public void WriteLine(string message);

}

public class DebugOutput : IOutput
{
    public void Write(string message)
    {
        Debug.Write(message);
    }

    public void WriteLine(string message)
    {
        Debug.WriteLine(message);
    }
}

public class ConsoleOutput : IOutput
{
    public void Write(string message)
    {
        Console.Write(message);
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}

// let me show u how resharper can be leveraged to create derived classes
//public class MessageBoxOutput : IOutput
//{
//    public void WriteLine(string message)
//    {
//        //MessageBox since we are using a Console App.. and the MessageBox class is defined in a windows forms project
//        // it's not the best option for now.. but I hope you get the idea
//        throw NotImplementedException("This implementeeation does not work.. use other dervied types")
//    }
//}