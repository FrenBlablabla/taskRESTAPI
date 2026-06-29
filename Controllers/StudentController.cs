using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using najnovijipokusajREST.Data;
using najnovijipokusajREST.Models;

namespace najnovijipokusajREST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("oib")]
        public async Task<IActionResult> GetAllOibs()
        {
            var oibs = await _context.Students
                                     .Where(s => s.Oib != null)
                                     .Select(s => s.Oib)
                                     .ToListAsync();

            return Ok(oibs);
        }
        [HttpGet("details/{oib}")]
        public async Task<IActionResult> GetStudentDetails(string oib)
        {
            var student = await _context.Students
                                         .FirstOrDefaultAsync(s => s.Oib == oib);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }
        [HttpPut("update/{oib}")]
        public async Task<IActionResult> UpdateStudent(string oib, [FromBody] Student updatedStudent)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Oib == oib);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            student.Jmbag = updatedStudent.Jmbag;
            student.Ime = updatedStudent.Ime;
            student.Prezime = updatedStudent.Prezime;
            student.Email = updatedStudent.Email;
            student.DatumUpisa = updatedStudent.DatumUpisa;
            student.Status = updatedStudent.Status;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Student details updated successfully.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
        }
        [HttpGet("courses/{jmbag}")]
        public async Task<IActionResult> GetEnrolledCourses(string jmbag)
        {
            var courses = await _context.UpisaniPredmeti
                                         .Where(up => up.StudentJmbag == jmbag)
                                         .ToListAsync();
            return Ok(courses);
        }

        [HttpGet("exams/{upisPredmetaId}")]
        public async Task<IActionResult> GetExamRegistrations(int upisPredmetaId)
        {
            var exams = await _context.PrijaveIspita
                                      .Where(pi => pi.UpisPredmetaId == upisPredmetaId)
                                      .ToListAsync();
            return Ok(exams);
        }
        [HttpGet("deadlines")]
        public async Task<IActionResult> GetAvailableDeadlines()
        {
            var sampleDeadlines = new List<int> { 101, 102, 103, 104 };
            return Ok(sampleDeadlines);
        }

        [HttpPost("register-exam")]
        public async Task<IActionResult> RegisterExam([FromBody] PrijavaIspita novaPrijava)
        {
            if (novaPrijava == null) return BadRequest("Invalid data.");

            int currentAttempts = await _context.PrijaveIspita
                .Where(p => p.UpisPredmetaId == novaPrijava.UpisPredmetaId)
                .CountAsync();

            novaPrijava.RedniBrojIzlaska = currentAttempts + 1;
            novaPrijava.DatumPrijave = DateTime.Now;
            novaPrijava.Status = "Prijavljen";

            _context.PrijaveIspita.Add(novaPrijava);
            await _context.SaveChangesAsync();

            return Ok("Successfully registered for the exam.");
        }
    }
}