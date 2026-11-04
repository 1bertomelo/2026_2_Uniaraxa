
using Uniaraxa.Domain.Entities;

namespace Uniaraxa.Domain.Interfaces
{
    public interface IStudentRepository
    {
        public void AddStudent(Student student);
        public void UpdateStudent(Student student);
        public void DeleteStudent(int id);
        public Student GetStudentById(int id);
        public List<Student> GetAllStudents();
    }
}
