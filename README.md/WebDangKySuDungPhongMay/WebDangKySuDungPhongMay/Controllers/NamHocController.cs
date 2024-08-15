using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class NamHocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NamHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalRecords = await _context.NamHocs.CountAsync();
            var namHocs = await _context.NamHocs
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(namHocs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NamHocViewModel model)
        {
            if (ModelState.IsValid)
            {
                var namHoc = new NamHoc
                {
                    NamBatDau = model.NamBatDau,
                    NamKetThuc = model.NamKetThuc
                };

                _context.Add(namHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.NamHocs.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NamHocViewModel model)
        {
            if (ModelState.IsValid)
            {
                var namHoc = await _context.NamHocs.FindAsync(model.MaNamHoc);

                if (namHoc == null)
                {
                    return NotFound();
                }

                namHoc.NamBatDau = model.NamBatDau;
                namHoc.NamKetThuc = model.NamKetThuc;

                _context.Update(namHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.NamHocs.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var namHoc = await _context.NamHocs.FindAsync(id);
            if (namHoc != null)
            {
                _context.NamHocs.Remove(namHoc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
