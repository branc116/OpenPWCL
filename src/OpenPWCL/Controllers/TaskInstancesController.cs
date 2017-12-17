using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenPWCL.Data;

namespace OpenPWCL.Controllers
{
    public class TaskInstancesController : Controller
    {
        private readonly TasksContext _context;

        public TaskInstancesController(TasksContext context)
        {
            _context = context;
        }

        // GET: TaskInstances
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaskInstances.ToListAsync());
        }

        // GET: TaskInstances/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInstance = await _context.TaskInstances
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskInstance == null)
            {
                return NotFound();
            }

            return View(taskInstance);
        }

        // GET: TaskInstances/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskInstances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InputParams,Finished,Resoult")] TaskInstance taskInstance)
        {
            if (ModelState.IsValid)
            {
                taskInstance.Id = Guid.NewGuid();
                _context.Add(taskInstance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskInstance);
        }

        // GET: TaskInstances/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInstance = await _context.TaskInstances.SingleOrDefaultAsync(m => m.Id == id);
            if (taskInstance == null)
            {
                return NotFound();
            }
            return View(taskInstance);
        }

        // POST: TaskInstances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,InputParams,Finished,Resoult")] TaskInstance taskInstance)
        {
            if (id != taskInstance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskInstance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskInstanceExists(taskInstance.Id))
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
            return View(taskInstance);
        }

        // GET: TaskInstances/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskInstance = await _context.TaskInstances
                .SingleOrDefaultAsync(m => m.Id == id);
            if (taskInstance == null)
            {
                return NotFound();
            }

            return View(taskInstance);
        }

        // POST: TaskInstances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var taskInstance = await _context.TaskInstances.SingleOrDefaultAsync(m => m.Id == id);
            _context.TaskInstances.Remove(taskInstance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public JsonResult GetTaskInstance(Guid taskId)
        {
            var instance = _context.Tasks
                .Include(i => i.TaskInstances)
                .FirstOrDefault(i => i.Id == taskId)
                .TaskInstances
                .FirstOrDefault(i => !i.Finished);
            return Json(instance);
        }
        //POST: TaskInstances/TaskInstaceSolved/5
        [HttpPost]
        public JsonResult TaskInstanceSolved([FromRoute]Guid id, [FromQuery]string result)
        {
            var instance = _context.TaskInstances.FirstOrDefault(i => i.Id == id);
            instance.Finished = true;
            instance.Resoult = result;
            _context.SaveChanges();
            return GetTaskInstance(instance.TaskId);
        }

        private bool TaskInstanceExists(Guid id)
        {
            return _context.TaskInstances.Any(e => e.Id == id);
        }
    }
}
