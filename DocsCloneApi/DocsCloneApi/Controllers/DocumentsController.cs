using DocsCloneApi.Data;
using DocsCloneApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocsCloneApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetAll() =>
            await _context.Documents.OrderByDescending(d => d.LastModified).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> Get(Guid id)
        {
            var doc = await _context.Documents.FindAsync(id);
            return doc == null ? NotFound() : doc;
        }

        [HttpPost]
        public async Task<ActionResult<Document>> Create(Document doc)
        {
            doc.Id = Guid.NewGuid();
            doc.LastModified = DateTime.UtcNow;
            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = doc.Id }, doc);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Document updated)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return NotFound();

            doc.Title = updated.Title;
            doc.Content = updated.Content;
            doc.LastModified = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc == null) return NotFound();

            _context.Documents.Remove(doc);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
