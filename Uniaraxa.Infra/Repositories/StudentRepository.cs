using System.Data;
using Microsoft.Data.SqlClient;
using Uniaraxa.Domain.Entities;
using Uniaraxa.Domain.Interfaces;

namespace Uniaraxa.Infra.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
            _connectionString = connectionString;
        }

        public void AddStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("INSERT INTO dbo.Students ([Name], Email) OUTPUT INSERTED.Id VALUES (@Name, @Email);", connection);
            AddStudentParameters(command, student);
            connection.Open();
            student.Id = (int)command.ExecuteScalar()!;
        }

        public void DeleteStudent(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("DELETE FROM dbo.Students WHERE Id = @Id;", connection);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            connection.Open();
            command.ExecuteNonQuery();
        }

        public List<Student> GetAllStudents()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT Id, [Name], Email FROM dbo.Students ORDER BY Id;", connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            var students = new List<Student>();
            while (reader.Read())
            {
                students.Add(MapStudent(reader));
            }
            return students;
        }

        public Student? GetStudentById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT Id, [Name], Email FROM dbo.Students WHERE Id = @Id;", connection);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            connection.Open();
            using var reader = command.ExecuteReader();
            return reader.Read() ? MapStudent(reader) : null;
        }

        public void UpdateStudent(Student student)
        {
            ArgumentNullException.ThrowIfNull(student);
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("UPDATE dbo.Students SET [Name] = @Name, Email = @Email WHERE Id = @Id;", connection);
            AddStudentParameters(command, student);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = student.Id;
            connection.Open();
            command.ExecuteNonQuery();
        }

        private static void AddStudentParameters(SqlCommand command, Student student)
        {
            ArgumentNullException.ThrowIfNull(student.Name);
            ArgumentNullException.ThrowIfNull(student.Email);
            if (student.Name.Length > 200 || student.Email.Length > 320)
            {
                throw new ArgumentException("Name deve ter até 200 caracteres e Email até 320.", nameof(student));
            }
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = student.Name;
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 320).Value = student.Email;
        }

        private static Student MapStudent(SqlDataReader reader) => new Student
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Email = reader.GetString(2)
        };
    }
}
