#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestEF.Data;
using TestEF.Models;

namespace TestEF.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            var expenses = await _context.Expense.ToListAsync();
            foreach (var expense in expenses)
            {
                expense.Category = await _context.ExpenseCategory.FirstAsync(x => x.Id == expense.CategoryId);
            }

            expenses = PreSortExpenses(new(expenses));

            return View(expenses);
        }

        private List<Expense> PreSortExpenses(List<Expense> expenses)
		{
            var sortBy = Request.Cookies["sortExpensesBy"];
            var isReverse = Request.Cookies["isReverseExpenses"]?.Equals("true");


            switch (sortBy)
            {
                case nameof(Expense.Label): expenses = expenses.OrderBy(e => e.Label).ToList(); break;
                case nameof(Expense.Description): expenses = expenses.OrderBy(e => e.Description).ToList(); break;
                case nameof(Expense.Amount): expenses = expenses.OrderBy(e => e.Amount).ToList(); break;
                case nameof(Expense.Category): expenses = expenses.OrderBy(e => e.Category.Name).ToList(); break;
                case nameof(Expense.Date): expenses = expenses.OrderBy(e => e.Date).ToList(); break;
            }


            if (isReverse == true)
                expenses.Reverse();

            return expenses;
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            var expenseCategories = _context.ExpenseCategory.ToList();
            ViewData["Categories"] = expenseCategories;
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Label,Description,Amount,CategoryId")] Expense expense)
        {
            expense.Category = await _context.ExpenseCategory.FirstOrDefaultAsync(x => x.Id == expense.CategoryId);

            // TODO: Data validation
            if (expense.Category != null)
            {
                _context.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            var expenseCategories = _context.ExpenseCategory.ToList();
            ViewData["Categories"] = expenseCategories;
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Label,Description,Amount,CategoryId")] Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            // TODO: Data validation
            if (true)
            {
                try
                {
                    expense.Category = await _context.ExpenseCategory.FirstOrDefaultAsync(x => x.Id == expense.CategoryId);
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
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
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expense.FindAsync(id);
            _context.Expense.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.Id == id);
        }
    }
}
