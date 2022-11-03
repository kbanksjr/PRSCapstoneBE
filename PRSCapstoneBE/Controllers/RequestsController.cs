using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSCapstoneBE.Models;

//Heart of PRS, together with a collection of RequestLine rows make up user's request of products.
//Provides grouping for all of the products being requested, user that created the request and total amount of all products being requested.

namespace PRSCapstoneBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        //creating static strings for statuses
                
        public static string REVIEW = "REVIEW";
        public static string APPROVED = "APPROVED";
        public static string REJECTED = "REJECTED";
        public static string PAID = "PAID";
        public static string NEW = "NEW";


        private readonly AppDbContext _context;

        public RequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.Include(x => x.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.Include(x => x.RequestLines)!.ThenInclude(x => x.Product)
                                                   .Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        //Get Reviews method, GET: /api/requests/reviews/{userId} . Gets request in "REVIEW" status and not owned by the user with the PK of id.
        [HttpGet("reviews/{userId}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestReview(int userid)
        {
            
            var grr = await _context.Users.FindAsync(userid);
            if (grr is null)
            {
                return NotFound();
            }

            return await _context.Requests.Where(u => u.UserId != userid && u.Status == REVIEW).ToListAsync();

        }


        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // PUT: /api/requests/review/5
        [HttpPut("review/{id}")]
        public async Task<IActionResult> Review(int id, Request request)
        {
            request.Status = (request.Total <= 50) ? APPROVED : REVIEW;
            return await PutRequest(id, request);

            /* Used ternary above to simplify code
             * Method before still works but prone to more bugs
            if(request.Total <= 50)
            {
                request.Status = APPROVED;
                var reviewx = await PutRequest(id, request);
                return reviewx;
            }
            request.Status = REVIEW;
            var reviewy = await PutRequest(id, request);
            return reviewy;
            */


        }

        // PUT: /api/requests/approve/5
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id, Request request)
        {
            request.Status = APPROVED;
            return await PutRequest(id, request);

        }

        // PUT: /api/requests/reject/5
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id, Request request)
        {
            request.Status = REJECTED;
            return await PutRequest(id, request);

        }

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
