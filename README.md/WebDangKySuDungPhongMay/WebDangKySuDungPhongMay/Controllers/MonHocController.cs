using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class MonHocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonHocController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalRecords = await _context.MonHocs.CountAsync();
            var monHocs = await _context.MonHocs
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(monHocs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MonHocViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem mã môn học có bị trùng hay không
                var existingMonHoc = await _context.MonHocs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(mh => mh.MaMonHoc == model.MaMonHoc);

                if (existingMonHoc != null)
                {
                    ModelState.AddModelError("MaMonHoc", "Mã môn học đã tồn tại.");
                    return View("Index", await _context.MonHocs.ToListAsync());
                }

                var monHoc = new MonHoc
                {
                    MaMonHoc = model.MaMonHoc,
                    TenMonHoc = model.TenMonHoc,
                    TinChiLyThuyet = model.TinChiLyThuyet,
                    TinChiThucHanh = model.TinChiThucHanh
                };

                _context.Add(monHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.MonHocs.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MonHocViewModel model)
        {
            if (ModelState.IsValid)
            {
                var monHoc = await _context.MonHocs.FindAsync(model.MaMonHoc);

                if (monHoc == null)
                {
                    return NotFound();
                }

                monHoc.TenMonHoc = model.TenMonHoc;
                monHoc.TinChiLyThuyet = model.TinChiLyThuyet;
                monHoc.TinChiThucHanh = model.TinChiThucHanh;

                _context.Update(monHoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.MonHocs.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var monHoc = await _context.MonHocs.FindAsync(id);
            if (monHoc != null)
            {
                _context.MonHocs.Remove(monHoc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
