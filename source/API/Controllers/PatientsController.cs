using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Npgsql;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly string _connectionString;

    public PatientsController(string connectionString)
    {
        _connectionString = connectionString;
    }

    // GET: api/patients
    [HttpGet]
    public ActionResult<IEnumerable<Patient>> Get()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var patients = connection.Query<Patient>("SELECT * FROM patients").ToList();

        return Ok(patients);
    }

    // GET: api/patients/5
    [HttpGet("{id}")]
    public ActionResult<Patient> Get(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var patient = connection.Query<Patient>("SELECT * FROM patients WHERE id = @id", new { id }).FirstOrDefault();

        if (patient == null)
        {
            return NotFound();
        }

        return Ok(patient);
    }

    // POST: api/patients
    [HttpPost]
    public ActionResult<Patient> Post(Patient patient)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        connection.Execute("INSERT INTO patients (username, password_hash, email) VALUES (@username, @password_hash, @email)", new { username = patient.Username, password_hash = patient.PasswordHash, email = patient.Email });

        return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
    }

    // PUT: api/patients/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, Patient patient)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        connection.Execute("UPDATE patients SET username = @username, password_hash = @password_hash, email = @email WHERE id = @id", new { id = patient.Id, username = patient.Username, password_hash = patient.PasswordHash, email = patient.Email });

        return NoContent();
    }

    // DELETE: api/patients/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        connection.Execute("DELETE FROM patients WHERE id = @id", new { id });

        return NoContent();
    }
}
