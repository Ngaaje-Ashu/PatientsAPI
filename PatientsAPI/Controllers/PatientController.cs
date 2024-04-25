using System.Reflection.Metadata.Ecma335;
using DataModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatientsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientRepository _patientRepository;

        public PatientController(PatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }
        
        // GET: PatientList
        [HttpGet]
        public IEnumerable<PatientDetailDto> Get()
        {
            return this._patientRepository.GetAll();
        }

        //GET: api/PatientDetailDto/{id}
        [HttpGet("{id}", Name = "GetPatientsById")]
        public IActionResult Get(int id)
        {
            var patient = this._patientRepository.Get(id);
            if (patient == null)
            {
                return this.NotFound();
            }

            return this.Ok(patient);
        }

        // POST: PatientController/Create
        [HttpPost(Name = "CreatePatient")]
        public IActionResult Post(PatientDetailDto patient)
        {
            this._patientRepository.Add(patient);
            return this.Created(nameof(Get), patient);
        }

        //PUT: PatientController/Update
        [HttpPut("{id}", Name = "UpdatePatient")]
        public IActionResult Put(PatientDetailDto updatedPatient)
        {
            this._patientRepository.Update(updatedPatient);
            return this.Ok();
        }

        //DELETE: DeletePatientbyID
        [HttpDelete("{id}", Name = "DeletePatientById")]
        public IActionResult Delete(int id)
        {
            this._patientRepository.Delete(id);

            return this.NoContent();
        }
        
    }
}


