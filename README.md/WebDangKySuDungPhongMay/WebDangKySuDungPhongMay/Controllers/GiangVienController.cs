using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class GiangVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GiangVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalRecords = await _context.GiangViens.CountAsync();
            var giangViens = await _context.GiangViens
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(giangViens);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiangVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check for existing MaGiangVien
                var existingGiangVien = await _context.GiangViens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(gv => gv.MaGiangVien == model.MaGiangVien);

                // Check for existing MaNguoiQuanLy
                var existingNguoiQuanLy = await _context.NguoiQuanLyPhongMays
                    .AsNoTracking()
                    .FirstOrDefaultAsync(nql => nql.MaNguoiQuanLy == model.MaGiangVien);

                // Check for existing MaSinhVien
                var existingSinhVien = await _context.SinhViens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(sv => sv.MaSinhVien == model.MaGiangVien);

                // If any of the IDs are already in use, add a model error
                if (existingGiangVien != null)
                {
                    ModelState.AddModelError("MaGiangVien", "Mã giảng viên đã tồn tại.");
                }
                else if (existingNguoiQuanLy != null)
                {
                    ModelState.AddModelError("MaGiangVien", "Mã giảng viên trùng với mã người quản lý.");
                }
                else if (existingSinhVien != null)
                {
                    ModelState.AddModelError("MaGiangVien", "Mã giảng viên trùng với mã sinh viên.");
                }
                else
                {
                    var giangVien = new GiangVien
                    {
                        MaGiangVien = model.MaGiangVien,
                        HoTen = model.HoTen,
                        MatKhau = model.MatKhau,
                        NgaySinh = model.NgaySinh,
                        GioiTinh = model.GioiTinh,
                        Email = model.Email,
                        SoDienThoai = model.SoDienThoai,
                        TrangThai = model.TrangThai
                    };

                    _context.Add(giangVien);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("Index", await _context.GiangViens.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GiangVienViewModel model)
        {
            if (ModelState.IsValid)
            {
                var giangVien = await _context.GiangViens.FindAsync(model.MaGiangVien);

                if (giangVien == null)
                {
                    return NotFound();
                }

                giangVien.HoTen = model.HoTen;
                giangVien.NgaySinh = model.NgaySinh;
                giangVien.GioiTinh = model.GioiTinh;
                giangVien.Email = model.Email;
                giangVien.SoDienThoai = model.SoDienThoai;
                giangVien.TrangThai = model.TrangThai;

                if (!string.IsNullOrWhiteSpace(model.MatKhau))
                {
                    giangVien.MatKhau = model.MatKhau;
                }

                _context.Update(giangVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.GiangViens.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var giangVien = await _context.GiangViens.FindAsync(id);
            if (giangVien != null)
            {
                _context.GiangViens.Remove(giangVien);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
