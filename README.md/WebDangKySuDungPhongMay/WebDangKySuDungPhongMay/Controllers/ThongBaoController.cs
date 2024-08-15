using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebDangKySuDungPhongMay.Models;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class ThongBaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThongBaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the list of notifications
        public async Task<IActionResult> Index()
        {
            var thongBaos = await _context.ThongBaos.Include(t => t.NguoiQuanLyPhongMay).ToListAsync();
            return View(thongBaos);
        }

        // Handle the form submission for creating a new notification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThongBao thongBao)
        {
            if (ModelState.IsValid)
            {
                thongBao.ThoiGianDang = DateTime.Now;
                thongBao.MaNguoiQuanLy = User.Identity.Name; // Assuming the username is the MaNguoiQuanLy

                _context.Add(thongBao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // Handle the form submission for editing an existing notification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ThongBao thongBao)
        {
            if (id != thongBao.MaThongBao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    thongBao.ThoiGianDang = DateTime.Now;
                    _context.Update(thongBao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThongBaoExists(thongBao.MaThongBao))
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
            return RedirectToAction(nameof(Index));
        }

        // Handle the form submission for deleting an existing notification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var thongBao = await _context.ThongBaos.FindAsync(id);
            _context.ThongBaos.Remove(thongBao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThongBaoExists(int id)
        {
            return _context.ThongBaos.Any(e => e.MaThongBao == id);
        }
    }
}
