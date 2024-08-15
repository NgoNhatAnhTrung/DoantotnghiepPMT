using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class SinhVienHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienHomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sinhVienId = User.Identity.Name; // Assuming the username is the same as MaSinhVien

            var sinhVienPhieuDangKySVs = await _context.PhieuDangKySVs
                .Include(p => p.PhongMay)
                .Include(p => p.SinhVien)
                .Where(p => p.MaSinhVien == sinhVienId)
                .ToListAsync();

            return View(sinhVienPhieuDangKySVs);
        }

        public async Task<IActionResult> Register()
        {
            var phongMays = await _context.PhongMays.ToListAsync();
            ViewBag.PhongMays = phongMays.Select(pm => new SelectListItem
            {
                Value = pm.MaPhongMay,
                Text = pm.MaPhongMay
            }).ToList();

            var viewModel = new PhieuDangKySVViewModel
            {
                NgayDangKy = DateTime.Now
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(PhieuDangKySVViewModel model)
        {
            if (ModelState.IsValid)
            {
                var phieuDangKySV = new PhieuDangKySV
                {
                    MaSinhVien = User.Identity.Name, // Get the current logged-in user
                    NgayDangKy = DateTime.Now, // Set the registration date to the current date
                    NgaySuDung = model.NgaySuDung,
                    ThoiGianBatDau = model.ThoiGianBatDau,
                    ThoiGianKetThuc = model.ThoiGianKetThuc,
                    LyDo = model.LyDo,
                    GhiChu = model.GhiChu,
                    TrangThai = false,
                    MaPhongMay = model.MaPhongMay
                };

                _context.PhieuDangKySVs.Add(phieuDangKySV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var phongMays = await _context.PhongMays.ToListAsync();
            ViewBag.PhongMays = phongMays.Select(pm => new SelectListItem
            {
                Value = pm.MaPhongMay,
                Text = pm.MaPhongMay
            }).ToList();

            return View(model);
        }

        public async Task<IActionResult> Notification()
        {
            var thongBaos = await _context.ThongBaos.Include(t => t.NguoiQuanLyPhongMay).ToListAsync();
            return View(thongBaos);
        }
    }
}
