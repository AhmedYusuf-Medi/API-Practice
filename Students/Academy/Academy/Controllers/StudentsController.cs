using Api.Models.Base;
using Api.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService) =>
            this.studentService = studentService;

        /// <summary>
        /// Returns all students
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<List<StudentResponseModel>>))]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default) =>
            this.Ok(await studentService.GetAllAsync(cancellationToken));


        /// <summary>
        /// Returns student selected by id if it exists
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<StudentResponseModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result<Exception>))]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Ok(await studentService.GetByIdAsync(id, cancellationToken));

        [HttpGet("explode")]
        public void Explode() => studentService.ThrowServerError();
    }
}
