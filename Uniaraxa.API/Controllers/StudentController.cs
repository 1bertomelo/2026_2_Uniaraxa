using Microsoft.AspNetCore.Mvc;
using Uniaraxa.Domain.Entities;
using Uniaraxa.Service.Services;

namespace Uniaraxa.API.Controllers
{
    public class StudentController: ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
            
        }


        [HttpGet]
        [Route("api/student")]
        public IActionResult GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            if(students is null || !students.Any())
            {
                return NotFound("No students found.");
            }
            return Ok(students);
        }

        [HttpGet]
        [Route("api/student/{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);
            if(student is null)
            {
                return NotFound("Student not found.");
            }
            return Ok(student);
        }

        [HttpPost]
        [Route("api/student")]
        public IActionResult AddStudent([FromBody] Student student)
        {
            _studentService.AddStudent(student);
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }
            , student);
        }
        [HttpDelete]
        [Route("api/student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _studentService.DeleteStudent(id);
            return NoContent();
        }

    }
}
