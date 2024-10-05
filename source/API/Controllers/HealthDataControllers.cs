using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Npgsql;

[ApiController]
[Route("api/[controller]")]
public class HealthDataController : ControllerBase
{
    private readonly string _connectionString;

    public HealthDataController(string connectionString)
    {
        _connectionString = connectionString;
    }

    // GET: api/healthdata
    [HttpGet]
    public ActionResult<IEnumerable<HealthData>> Get()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var healthData = connection.Query<HealthData>("SELECT * FROM health_data").ToList();

        return Ok(healthData);
    }

    // GET: api/healthdata/5
    [HttpGet("{id}")]
    public ActionResult<HealthData> Get(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        var healthData = connection.Query<HealthData>("SELECT * FROM health_data WHERE id = @id", new { id }).FirstOrDefault();

        if (healthData == null)
        {
            return NotFound();
        }

        return Ok(healthData);
    }

    // POST: api/healthdata
    [HttpPost]
    public ActionResult<HealthData> Post(HealthData healthData)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        connection.Execute("INSERT INTO health_data (patient_id, data) VALUES (@patient_id, @data)", new { patient_id = healthData.PatientId, data = healthData.Data });

        return CreatedAtAction("GetHealthData", new { id = healthData.Id }, healthData);
    }

    // PUT: api/healthdata/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, HealthData healthData)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        connection.Execute("UPDATE health_data SET patient_id = @patient_id, data = @data WHERE id = @id", new { id = healthData.Id, patient_id = healthData.PatientId, data = healthData.Data });

        return NoContent();
    }

    // DELETE: api/healthdata/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        connection.Execute("DELETE FROM health_data WHERE id = @id", new { id });

        return NoContent();
    }
}
