using Onion.Domain.Entities;
using Onion.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Onion.Infrastructure.SqlServerDbService.Services
{
    public class SqlServerDbService : IStudentDbService
    {
        private static readonly ICollection<Student> _students = new List<Student>();
        private static string _connectionString = "Data Source=db-mssql;Initial Catalog=s16996;Integrated Security=True";

        public bool EnrollStudent(Student newStudent, int semestr)
        {
            int queryResult = -1;
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_pro_InsertStudent", connection)
                {
                    CommandTimeout = 5000,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    command.Parameters.Add(new SqlParameter("Id", newStudent.IdStudent));
                    command.Parameters.Add(new SqlParameter("FirstName", newStudent.FirstName));
                    command.Parameters.Add(new SqlParameter("LastName", newStudent.LastName));

                    queryResult = command.ExecuteNonQuery();
                    _students.Add(newStudent);
                }
            }

            if (queryResult == 1)
                return true;

            return false;
        }

        public IEnumerable<Student> GetStudents()
        {
            DataTable dataTable = new DataTable();
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_pro_SelectStudents", connection)
                {
                    CommandTimeout = 5000,
                    CommandType = CommandType.StoredProcedure
                })
                {
                    dataTable.Load(command.ExecuteReader());
                }
            }

            foreach(DataRow row in dataTable.Rows)
            {
                students.Add(new Student()
                {
                    IdStudent = Convert.ToInt32(row[0]),
                    FirstName = row[1].ToString(),
                    LastName = row[2].ToString()
                });
            }

            return students;
        }
    }
}
