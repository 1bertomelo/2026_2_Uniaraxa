using Microsoft.AspNetCore.Mvc;
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
    }
}
