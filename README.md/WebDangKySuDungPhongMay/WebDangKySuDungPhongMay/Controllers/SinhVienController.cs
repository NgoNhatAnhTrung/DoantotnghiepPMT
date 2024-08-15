using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalRecords = await _context.SinhViens.CountAsync();
            var sinhViens = await _context.SinhViens
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(sinhViens);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SinhVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check for existing MaSinhVien
                var existingSinhVien = await _context.SinhViens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(sv => sv.MaSinhVien == model.MaSinhVien);

                // Check for existing MaGiangVien
                var existingGiangVien = await _context.GiangViens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(gv => gv.MaGiangVien == model.MaSinhVien);

                // Check for existing MaNguoiQuanLy
                var existingNguoiQuanLy = await _context.NguoiQuanLyPhongMays
                    .AsNoTracking()
                    .FirstOrDefaultAsync(nql => nql.MaNguoiQuanLy == model.MaSinhVien);

                // If any of the IDs are already in use, add a model error
                if (existingSinhVien != null)
                {
                    ModelState.AddModelError("MaSinhVien", "Mã sinh viên đã tồn tại.");
                }
                else if (existingGiangVien != null)
                {
                    ModelState.AddModelError("MaSinhVien", "Mã sinh viên trùng với mã giảng viên.");
                }
                else if (existingNguoiQuanLy != null)
                {
                    ModelState.AddModelError("MaSinhVien", "Mã sinh viên trùng với mã người quản lý.");
                }
                else
                {
                    var sinhVien = new SinhVien
                    {
                        MaSinhVien = model.MaSinhVien,
                        TenSinhVien = model.TenSinhVien,
                        MatKhau = model.MatKhau,
                        NgaySinh = model.NgaySinh,
                        GioiTinh = model.GioiTinh,
                        Email = model.Email,
                        SoDienThoai = model.SoDienThoai,
                        TrangThai = model.TrangThai
                    };

                    _context.Add(sinhVien);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View("Index", await _context.SinhViens.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(SinhVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sinhVien = await _context.SinhViens.FindAsync(model.MaSinhVien);

                if (sinhVien == null)
                {
                    return NotFound();
                }

                sinhVien.TenSinhVien = model.TenSinhVien;
                sinhVien.NgaySinh = model.NgaySinh;
                sinhVien.GioiTinh = model.GioiTinh;
                sinhVien.Email = model.Email;
                sinhVien.SoDienThoai = model.SoDienThoai;
                sinhVien.TrangThai = model.TrangThai;

                if (!string.IsNullOrWhiteSpace(model.MatKhau))
                {
                    sinhVien.MatKhau = model.MatKhau;
                }

                _context.Update(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.SinhViens.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
