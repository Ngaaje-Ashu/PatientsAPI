using DataModels.Models;

namespace PatientsAPI.Controllers;

public class PatientRepository
{
    private static List<PatientDetailDto> _patients = new()
    {
        new PatientDetailDto
        {
            Id = 0,
            FirstName = "Monday",
            LastName = "Oga",
            Gender = "Female",
            DateOfBirth = DateTime.Parse("1997-06-20"),
            DateOfRegistration = DateTime.Parse("2017-12-09")
        },

        new PatientDetailDto
        {
            Id = 1,
            FirstName = "Sunday",
            LastName = "Martins",
            Gender = "Male",
            DateOfBirth = DateTime.Parse("2000-09-11"),
            DateOfRegistration = DateTime.Parse("2023-11-11")
        }
    };

    public List<PatientDetailDto> GetAll()
    {
        // return a list of all patients
        var patients = from p in _patients
            select new PatientDetailDto()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                DateOfBirth = p.DateOfBirth
            };
        return (List<PatientDetailDto>)patients;
    }

    public PatientDetailDto Get(int id)
    {
        // return a patient with the passed Id
        return _patients.FirstOrDefault(p => p.Id == id) ?? throw new InvalidOperationException();
    }

    public void Add(PatientDetailDto patient)
    {
        _patients.Add(patient);
        
    }

    public PatientDetailDto Update(PatientDetailDto patient)
    {
        var patientToUpdate = this.Get(patient.Id);
        patientToUpdate.FirstName = patient.FirstName;
        patientToUpdate.LastName = patient.LastName;
        patientToUpdate.Gender = patient.Gender;
        patientToUpdate.Allergies = patient.Allergies;
        patientToUpdate.DateOfBirth = patient.DateOfBirth;
        patientToUpdate.DateOfRegistration = patient.DateOfRegistration;

        return patientToUpdate;
    }

    public void Delete(int id)
    {
        var patient = new PatientDetailDto();
        var patientToDelete = this.Get(patient.Id);
        _patients.Remove(patientToDelete);
    }
}