using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class NguoiQuanLyPhongMayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NguoiQuanLyPhongMayController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var totalRecords = await _context.NguoiQuanLyPhongMays.CountAsync();
            var nguoiQuanLys = await _context.NguoiQuanLyPhongMays
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(nguoiQuanLys);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NguoiQuanLyPhongMayViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check for existing MaNguoiQuanLy
                var existingNguoiQuanLy = await _context.NguoiQuanLyPhongMays
                    .AsNoTracking()
                    .FirstOrDefaultAsync(nql => nql.MaNguoiQuanLy == model.MaNguoiQuanLy);

                // Check for existing MaGiangVien
                var existingGiangVien = await _context.GiangViens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(gv => gv.MaGiangVien == model.MaNguoiQuanLy);

                // Check for existing MaSinhVien
                var existingSinhVien = await _context.SinhViens
                    .AsNoTracking()
                    .FirstOrDefaultAsync(sv => sv.MaSinhVien == model.MaNguoiQuanLy);

                // If any of the IDs are already in use, add a model error
                if (existingNguoiQuanLy != null)
                {
                    ModelState.AddModelError("MaNguoiQuanLy", "Mã người quản lý đã tồn tại.");
                }
                else if (existingGiangVien != null)
                {
                    ModelState.AddModelError("MaNguoiQuanLy", "Mã người quản lý trùng với mã giảng viên.");
                }
                else if (existingSinhVien != null)
                {
                    ModelState.AddModelError("MaNguoiQuanLy", "Mã người quản lý trùng với mã sinh viên.");
                }
                else
                {
                    var nguoiQuanLy = new NguoiQuanLyPhongMay
                    {
                        MaNguoiQuanLy = model.MaNguoiQuanLy,
                        TenNguoiQuanLy = model.TenNguoiQuanLy,
                        MatKhau = model.MatKhau,
                        NgaySinh = model.NgaySinh,
                        GioiTinh = model.GioiTinh,
                        Email = model.Email,
                        SoDienThoai = model.SoDienThoai,
                        TrangThai = model.TrangThai
                    };

                    _context.Add(nguoiQuanLy);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View("Index", await _context.NguoiQuanLyPhongMays.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(NguoiQuanLyPhongMayViewModel model)
        {
            if (ModelState.IsValid)
            {
                var nguoiQuanLy = await _context.NguoiQuanLyPhongMays.FindAsync(model.MaNguoiQuanLy);

                if (nguoiQuanLy == null)
                {
                    return NotFound();
                }

                nguoiQuanLy.TenNguoiQuanLy = model.TenNguoiQuanLy;
                nguoiQuanLy.NgaySinh = model.NgaySinh;
                nguoiQuanLy.GioiTinh = model.GioiTinh;
                nguoiQuanLy.Email = model.Email;
                nguoiQuanLy.SoDienThoai = model.SoDienThoai;
                nguoiQuanLy.TrangThai = model.TrangThai;

                if (!string.IsNullOrWhiteSpace(model.MatKhau))
                {
                    nguoiQuanLy.MatKhau = model.MatKhau;
                }

                _context.Update(nguoiQuanLy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.NguoiQuanLyPhongMays.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var nguoiQuanLy = await _context.NguoiQuanLyPhongMays.FindAsync(id);
            if (nguoiQuanLy != null)
            {
                _context.NguoiQuanLyPhongMays.Remove(nguoiQuanLy);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
