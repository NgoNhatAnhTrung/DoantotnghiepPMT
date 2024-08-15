using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class LopHocPhanController : Controller
{
    private readonly ApplicationDbContext _context;

    public LopHocPhanController(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task PopulateDropdownsAsync()
    {
        var hocKys = await _context.HocKys.ToListAsync();
        var namHocs = await _context.NamHocs.ToListAsync();
        var monHocs = await _context.MonHocs.ToListAsync();
        var giangViens = await _context.GiangViens.ToListAsync();

        ViewBag.HocKys = hocKys.Select(hk => new SelectListItem
        {
            Value = hk.MaHocKy.ToString(),
            Text = hk.TenHocKy
        });

        ViewBag.NamHocs = namHocs.Select(nh => new SelectListItem
        {
            Value = nh.MaNamHoc.ToString(),
            Text = $"{nh.NamBatDau}-{nh.NamKetThuc}"
        });

        ViewBag.MonHocs = monHocs.Select(mh => new SelectListItem
        {
            Value = mh.MaMonHoc,
            Text = mh.TenMonHoc
        });

        ViewBag.GiangViens = giangViens.Select(gv => new SelectListItem
        {
            Value = gv.MaGiangVien,
            Text = gv.HoTen
        });
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var totalRecords = await _context.LopHocPhans.CountAsync();
        var lopHocPhans = await _context.LopHocPhans
                            .Include(lhp => lhp.HocKy)
                            .Include(lhp => lhp.NamHoc)
                            .Include(lhp => lhp.MonHoc)
                            .Include(lhp => lhp.GiangVien)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        await PopulateDropdownsAsync();
        return View(lopHocPhans);
    }

    public async Task<IActionResult> Details(string id, int page = 1, int pageSize = 10)
    {
        var lopHocPhan = await _context.LopHocPhans
            .Include(lhp => lhp.SinhVienLopHocPhans)
            .ThenInclude(svlhp => svlhp.SinhVien)
            .Include(lhp => lhp.MonHoc)
            .Include(lhp => lhp.GiangVien)
            .FirstOrDefaultAsync(lhp => lhp.MaLopHocPhan == id);

        if (lopHocPhan == null)
        {
            return NotFound();
        }

        var totalRecords = lopHocPhan.SinhVienLopHocPhans.Count();
        var sinhVienLopHocPhans = lopHocPhan.SinhVienLopHocPhans
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.MaLopHocPhan = lopHocPhan.MaLopHocPhan;
        ViewBag.TenLopHocPhan = lopHocPhan.MonHoc.TenMonHoc;
        ViewBag.SinhVienLopHocPhans = sinhVienLopHocPhans;

        var sinhViens = await _context.SinhViens.ToListAsync();
        ViewBag.SinhViens = sinhViens.Select(sv => new SelectListItem
        {
            Value = sv.MaSinhVien,
            Text = sv.TenSinhVien
        });

        ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSinhVien(string MaLopHocPhan, string SelectedSinhVien)
    {
        if (ModelState.IsValid)
        {
            var lopHocPhan = await _context.LopHocPhans
                .Include(lhp => lhp.SinhVienLopHocPhans)
                .FirstOrDefaultAsync(lhp => lhp.MaLopHocPhan == MaLopHocPhan);

            if (lopHocPhan == null)
            {
                return NotFound();
            }

            if (lopHocPhan.SinhVienLopHocPhans.Count >= lopHocPhan.SoSinhVienToiDa)
            {
                ModelState.AddModelError("MaSinhVien", "Số lượng sinh viên trong lớp học phần đã đạt tối đa.");
            }
            else
            {
                var existing = await _context.SinhVienLopHocPhans
                    .FirstOrDefaultAsync(svlhp => svlhp.MaLopHocPhan == MaLopHocPhan && svlhp.MaSinhVien == SelectedSinhVien);

                if (existing != null)
                {
                    ModelState.AddModelError("MaSinhVien", "Sinh viên đã tồn tại trong lớp học phần này.");
                }
                else
                {
                    var svlhp = new SinhVienLopHocPhan
                    {
                        MaLopHocPhan = MaLopHocPhan,
                        MaSinhVien = SelectedSinhVien
                    };

                    _context.SinhVienLopHocPhans.Add(svlhp);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = MaLopHocPhan });
                }
            }
        }

        return RedirectToAction(nameof(Details), new { id = MaLopHocPhan });
    }

    [HttpPost]
    public async Task<IActionResult> RemoveSinhVien(int id)
    {
        var svlhp = await _context.SinhVienLopHocPhans.FindAsync(id);
        if (svlhp != null)
        {
            _context.SinhVienLopHocPhans.Remove(svlhp);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Details), new { id = svlhp.MaLopHocPhan });
    }

    [HttpPost]
    public async Task<IActionResult> Create(LopHocPhanViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingLopHocPhan = await _context.LopHocPhans
                .AsNoTracking()
                .FirstOrDefaultAsync(lhp => lhp.MaLopHocPhan == model.MaLopHocPhan);

            if (existingLopHocPhan != null)
            {
                ModelState.AddModelError("MaLopHocPhan", "Mã lớp học phần đã tồn tại.");
                await PopulateDropdownsAsync();
                return View("Index", await _context.LopHocPhans.ToListAsync());
            }

            var lopHocPhan = new LopHocPhan
            {
                MaLopHocPhan = model.MaLopHocPhan,
                MaHocKy = model.MaHocKy,
                MaNamHoc = model.MaNamHoc,
                MaMonHoc = model.MaMonHoc,
                MaGiangVien = model.MaGiangVien,
                SoSinhVienToiDa = model.SoSinhVienToiDa
            };

            _context.Add(lopHocPhan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateDropdownsAsync();
        return View("Index", await _context.LopHocPhans.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(LopHocPhanViewModel model)
    {
        if (ModelState.IsValid)
        {
            var lopHocPhan = await _context.LopHocPhans.FindAsync(model.MaLopHocPhan);

            if (lopHocPhan == null)
            {
                return NotFound();
            }

            lopHocPhan.MaHocKy = model.MaHocKy;
            lopHocPhan.MaNamHoc = model.MaNamHoc;
            lopHocPhan.MaMonHoc = model.MaMonHoc;
            lopHocPhan.MaGiangVien = model.MaGiangVien;
            lopHocPhan.SoSinhVienToiDa = model.SoSinhVienToiDa;

            _context.Update(lopHocPhan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateDropdownsAsync();
        return View("Index", await _context.LopHocPhans.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var lopHocPhan = await _context.LopHocPhans.FindAsync(id);
        if (lopHocPhan != null)
        {
            _context.LopHocPhans.Remove(lopHocPhan);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
