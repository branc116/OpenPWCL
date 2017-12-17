using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenPWCL.Data;
using OpenPWCL.Models;
using Newtonsoft.Json;

namespace OpenPWCL.Controllers
{
    public class TasksController : Controller
    {
        private readonly TasksContext _context;

        public TasksController(TasksContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tasks.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(i => i.TaskInstances)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JavascriptCode")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.Id = Guid.NewGuid();
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,JavascriptCode")] Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
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
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tasks = await _context.Tasks.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AddTaskInstances(Guid id, string InputParams, Guid TaskId)
        {
            if (TaskId == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(i => i.TaskInstances)
                .SingleOrDefaultAsync(m => m.Id == TaskId);
            

            if (tasks == null)
            {
                return NotFound();
            }
            var parameters = Newtonsoft.Json.Linq.JArray
                .Parse(InputParams)
                .Select(i => new TaskInstance
                {
                    Finished = false,
                    Id = Guid.NewGuid(),
                    InputParams = i.ToString(Formatting.None),
                    TaskId = TaskId
                });
            tasks.TaskInstances.AddRange(parameters);
            await _context.SaveChangesAsync();
            return View("Details", tasks);
        }
        [HttpGet]
        public async Task<JsonResult> GetTaskToSolve()
        {
            var best = await _context.Tasks
                .OrderBy(i => i.Priority)
                .Include(i => i.TaskInstances)
                .FirstOrDefaultAsync(i => i.TaskInstances
                    .Any(j => !j.Finished));
            return Json(best);
        } 
        private bool TasksExists(Guid id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
