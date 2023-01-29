using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1_Bosianek.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Lab1_Bosianek.Rest.Context;

namespace Lab1_Bosianek.Rest.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public PeopleController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    [HttpGet]
    public IEnumerable<Person> Get()
    {
        var people = GetPeople();
        return people;
    }
    private IEnumerable<Person> GetPeople()
    {
        var people = new List<Person>();
        using (var connection = new SqlConnection(_configuration.GetConnectionString("AzureDb")))
        {
            var sql = "SELECT PersonId, FirstName, LastName FROM People";
            connection.Open();
            using SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var person = new Person()
                {
                    PersonId = (int)reader["PersonId"],
                    FirstName = reader["FirstName"].ToString()!,
                    LastName = reader["LastName"].ToString()!,
                };
                people.Add(person);
            }
        }
        return people;
    }
}
