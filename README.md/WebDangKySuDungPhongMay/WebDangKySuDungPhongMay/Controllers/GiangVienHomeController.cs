using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebDangKySuDungPhongMay.Converters;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class GiangVienHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GiangVienHomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var giangVienId = User.Identity.Name; // Assuming the username is the same as MaGiangVien

            var lopHocPhans = await _context.LopHocPhans
                .Include(l => l.MonHoc)
                .Include(lhp => lhp.HocKy)
                .Include(lhp => lhp.NamHoc)
                .Include(lhp => lhp.GiangVien)
                .Where(l => l.MaGiangVien == giangVienId)
                .ToListAsync();

            var phongMays = await _context.PhongMays.ToListAsync();
            ViewBag.PhongMays = phongMays.Select(pm => new SelectListItem
            {
                Value = pm.MaPhongMay,
                Text = pm.MaPhongMay
            }).ToList();

            return View(lopHocPhans);
        }

        [HttpPost]
        public async Task<IActionResult> Register(PhieuDangKyGVViewModel model)
        {
            if (ModelState.IsValid)
            {
                var phieuDangKyGV = new PhieuDangKyGV
                {
                    MaGiangVien = User.Identity.Name, // Get the current logged-in user
                    NgayDangKy = DateTime.Now, // Set the registration date to the current date
                    NgaySuDung = model.NgaySuDung,
                    TietBatDau = model.TietBatDau,
                    TietKetThuc = model.TietKetThuc,
                    LyDo = model.LyDo,
                    GhiChu = model.GhiChu,
                    TrangThai = false,
                    MaPhongMay = model.MaPhongMay,
                    MaLopHocPhan = model.MaLopHocPhan
                };

                _context.PhieuDangKyGVs.Add(phieuDangKyGV);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var phongMays = await _context.PhongMays.ToListAsync();
            ViewBag.PhongMays = phongMays.Select(pm => new SelectListItem
            {
                Value = pm.MaPhongMay,
                Text = pm.MaPhongMay
            }).ToList();

            var lopHocPhans = await _context.LopHocPhans
                .Include(l => l.MonHoc)
                .Include(lhp => lhp.HocKy)
                .Include(lhp => lhp.NamHoc)
                .Include(lhp => lhp.GiangVien)
                .Where(l => l.MaGiangVien == User.Identity.Name)
                .ToListAsync();

            return View("Index", lopHocPhans); // Return to the index view with the model data
        }

        public async Task<IActionResult> History()
        {
            var maGiangVien = User.Identity.Name;
            var phieuDangKyGVs = await _context.PhieuDangKyGVs
                .Where(p => p.MaGiangVien == maGiangVien)
                .Include(p => p.PhongMay)
                .Include(p => p.LopHocPhan)
                .ThenInclude(lhp => lhp.MonHoc)
                .Include(p => p.LopHocPhan)
                .ThenInclude(lhp => lhp.HocKy)
                .Include(p => p.LopHocPhan)
                .ThenInclude(lhp => lhp.NamHoc)
                .ToListAsync();

            return View(phieuDangKyGVs);
        }
        public async Task<IActionResult> Notification()
        {
            var thongBaos = await _context.ThongBaos.Include(t => t.NguoiQuanLyPhongMay).ToListAsync();
            return View(thongBaos);
        }

        [HttpGet]
        public async Task<IActionResult> Timetable(string maLopHocPhan)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(l => l.MonHoc)
                .Include(l => l.PhieuDangKyGVs)
                .Where(l => l.MaLopHocPhan == maLopHocPhan)
                .FirstOrDefaultAsync();

            if (lopHocPhan == null)
            {
                return NotFound();
            }

            var timetableEntries = lopHocPhan.PhieuDangKyGVs.Where(l => l.TrangThai == true)
                .Select(p => new LopHocPhanTimetableViewModel.TimetableEntry
                {
                    DayOfWeek = DayOfWeekConverter.ConvertToVietnamese(p.NgaySuDung.DayOfWeek),
                    Date = p.NgaySuDung,
                    StartTime = p.TietBatDau, // Assuming periods are in hours
                    EndTime = p.TietKetThuc
                })
               
                .OrderBy(e => e.Date)
                .ThenBy(e => e.StartTime)
                .ToList();

            var viewModel = new LopHocPhanTimetableViewModel
            {
                MaLopHocPhan = lopHocPhan.MaLopHocPhan,
                TenMonHoc = lopHocPhan.MonHoc?.TenMonHoc,
                TimetableEntries = timetableEntries
            };

            return PartialView("_TimetablePartial", viewModel);
        }

    }
}

