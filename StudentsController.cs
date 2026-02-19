using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StudentMicroservice
{
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// CRUD operations for Student entity.
    /// </summary>
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all students.
        /// </summary>
        /// <returns>List of students</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return _context.Students.ToList();
        }

        /// <summary>
        /// Get a student by ID.
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>Student object</returns>
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();
            return student;
        }

        /// <summary>
        /// Create a new student.
        /// </summary>
        /// <param name="student">Student object</param>
        /// <returns>Created student</returns>
        [HttpPost]
        public ActionResult<Student> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        /// <summary>
        /// Update an existing student.
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <param name="student">Student object</param>
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest();
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Delete a student by ID.
        /// </summary>
        /// <param name="id">Student ID</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();
            _context.Students.Remove(student);
            _context.SaveChanges();
            return NoContent();
        }
    }
}