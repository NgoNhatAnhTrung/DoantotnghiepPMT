using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebDangKySuDungPhongMay.ViewModels;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class HocKyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HocKyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalRecords = await _context.HocKys.CountAsync();
            var hocKys = await _context.HocKys
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(hocKys);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HocKyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hocKy = new HocKy
                {
                    TenHocKy = model.TenHocKy
                };

                _context.Add(hocKy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.HocKys.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HocKyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hocKy = await _context.HocKys.FindAsync(model.MaHocKy);

                if (hocKy == null)
                {
                    return NotFound();
                }

                hocKy.TenHocKy = model.TenHocKy;

                _context.Update(hocKy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.HocKys.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var hocKy = await _context.HocKys.FindAsync(id);
            if (hocKy != null)
            {
                _context.HocKys.Remove(hocKy);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
