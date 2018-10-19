using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using memo.Data;
using memo.Models;
using Microsoft.AspNetCore.Identity;
//using Rotativa;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace memo.Controllers
{
    public class MemosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public MemosController(ApplicationDbContext context,
                              SignInManager<IdentityUser> signInManager,
                             UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index(int id = 1, int type = 0)
        {
            //return View(await _context.Memo.ToListAsync());
            Console.WriteLine("Memo Index Type {0}", type);
            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(User);
                //return View(_context.Memo.ToList().Where(m => m.OwnerId == id));
                int size = 5;
                int skip = (id - 1) * size;
                var countTask = _context.Memo
                  .Where(m => m.OwnerId == userId)
                  .CountAsync();


                var count = await countTask;

                ViewData["Page"] = id; //pass the curent page to view
                ViewData["Count"] = count; //pass the count to view
                ViewData["Type"] = type;
                if (type == 0)
                {
                    var dataTask = _context.Memo
                        .OrderBy(m => m.Date)
                        .Where(m => m.OwnerId == userId)
                        .Skip(skip)
                        .Take(size)
                        .ToListAsync();
                    var results = await dataTask;
                    return View(results);

                }
                else
                {
                    var dataTask = _context.Memo
                 .OrderByDescending(m => m.Date)
                 .Where(m => m.OwnerId == userId)
                 .Skip(skip)
                 .Take(size)
                 .ToListAsync();
                    var results = await dataTask;
                    return View(results);

                }
            }
            return View(null);

        }

        public async Task<IActionResult> Remind(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var memo = await _context.Memo
                .FirstOrDefaultAsync(m => m.memoId == id);
            if (memo == null)
            {
                return NotFound();
            }

            var email = _userManager.GetUserName(User);

            //instantiate mimemessage
            var message = new MimeMessage();
            //From Address
            message.From.Add(new MailboxAddress("Memo Application", "mailmemodp1@gmail.com"));
            //To Address
            message.To.Add(new MailboxAddress("Memo Application", email));
            //Subject
            message.Subject = "This is a reminder from Memo Application";
            //Body
            string title = memo.Title;
            string details = memo.Details;
            DateTime date = memo.Date;

            message.Body = new TextPart("plain")
            {
                Text = "Please check your memo for further details." + "\n" + "Title: "
                + title + "\n" + "Details: " + details + "\n" + "Due On: " + date + "\n"


            };


            //configure and send mail
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("mailmemodp1@gmail.com", "Pa55w.rd");
                client.Send(message);
                client.Disconnect(true);
            }

            return View();

        }

        // Report View
        //public ActionResult GenerateReport() 
        //{
        //    if (_signInManager.IsSignedIn(User))
        //    {
        //        var id = _userManager.GetUserId(User);
        //        return View(_context.Memo.ToList().Where(m => m.OwnerId == id)); 
        //    }
        //return NoContent();
        //}

        //public ActionResult GetAll()
        //{
        //    return NoContent();
        //}

        //public ActionAsPdf PrintAll()
        //{
        //    var q = new ActionAsPdf("GetAll");
        //    return q;
        //}

        public ActionResult SearchByDateRange(DateTime? fromDate, DateTime? toDate)
        {
            var memos = from m in _context.Memo
                        select m;
            var id = _userManager.GetUserId(User);
            if (!fromDate.HasValue) fromDate = DateTime.Now.Date;
            if (!toDate.HasValue) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
            if (toDate < fromDate) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var result = memos.Where(s => (s.Date >= fromDate && s.Date < toDate) &&
                            (s.OwnerId == id)).ToList();
                            
            return View(result);
        }

        public ActionResult NotificationWidget(DateTime? fromDate, DateTime? toDate)
        {
            var memos = from m in _context.Memo
                        select m;
            var id = _userManager.GetUserId(User);
            if (!fromDate.HasValue) fromDate = DateTime.Now.Date;
            if (!toDate.HasValue) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
            if (toDate < fromDate) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var result = memos.Where(s => (s.Date >= fromDate && s.Date < toDate) &&
                            (s.OwnerId == id)).ToList();
                            
            return View(result);
        }

        public ActionResult SearchByCalendar(DateTime? fromDate, DateTime? toDate)
        {
            var memos = from m in _context.Memo
                        select m;
            var id = _userManager.GetUserId(User);
            if (!fromDate.HasValue) fromDate = DateTime.Now.Date;
            if (!toDate.HasValue) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
            if (toDate < fromDate) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            var result = memos.Where(s => (s.Date >= fromDate && s.Date < toDate) &&
                            (s.OwnerId == id)).ToList();
                            
            return View(result);
        }

        public async Task<IActionResult> SearchByTitle(string searchString)
        {
            var memos = from m in _context.Memo
                        select m;
            if ((_signInManager.IsSignedIn(User)) && (searchString != null))
            {
                var id = _userManager.GetUserId(User);
                
                memos = memos.Where(s => (s.Title.ToLower().Contains(searchString.ToLower())) &&
                            (s.OwnerId == id));
            }
            return View(await memos.ToListAsync());

        }

        public ActionResult SearchTitle()
        {
            return null;
        }

        // GET: Memos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memo = await _context.Memo
                .FirstOrDefaultAsync(m => m.memoId == id);
            if (memo == null)
            {
                return NotFound();
            }

            return View(memo);
        }

        // GET: Memos/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Memos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("memoId,OwnerId,Date,Title,Details")] Memo memo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memo);
        }

        // GET: Memos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memo = await _context.Memo.FindAsync(id);
            if (memo == null)
            {
                return NotFound();
            }
            return View(memo);
        }

        // POST: Memos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("memoId,OwnerId,Date,Title,Details")] Memo memo)
        {
            if (id != memo.memoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemoExists(memo.memoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(memo);
        }

        // GET: Memos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memo = await _context.Memo
                .FirstOrDefaultAsync(m => m.memoId == id);
            if (memo == null)
            {
                return NotFound();
            }

            return View(memo);
        }

        // POST: Memos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memo = await _context.Memo.FindAsync(id);
            _context.Memo.Remove(memo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemoExists(int id)
        {
            return _context.Memo.Any(e => e.memoId == id);
        }
    }
}
