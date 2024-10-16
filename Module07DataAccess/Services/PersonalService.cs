using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module07DataAccess.Model;
using MySql.Data.MySqlClient;

namespace Module07DataAccess.Services
{
    public class PersonalService
    {
        private readonly string _connectionString;

        public PersonalService()
        {
            var dbService = new DatabaseConnectionService();
            _connectionString = dbService.GetConnectionString();
        }

        public async Task<List<Personal>>GetAllPersonalAsync()
        {
            var personalService = new List<Personal>();
            using (var conn = new MySqlConnection( _connectionString ))
            {
                await conn.OpenAsync();
                
                //retrieve data
                var cmd = new MySqlCommand("SELECT * FROM tblpersonal", conn);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        personalService.Add(new Personal
                         {
                            ID = reader.GetInt32("ID"),
                            Name = reader.GetString("Name"),
                            Gender = reader.GetString("Gender"),
                            ContactNo = reader.GetString("Contactno"),
                        });
                    }
                }
            }
            return personalService;
        }
    }
}
