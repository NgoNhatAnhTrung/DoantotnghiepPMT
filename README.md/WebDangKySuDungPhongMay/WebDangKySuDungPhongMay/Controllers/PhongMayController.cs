using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class PhongMayController : Controller
{
    private readonly ApplicationDbContext _context;

    public PhongMayController(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task PopulateNguoiQuanLyPhongMaysAsync()
    {
        var nguoiQuanLyPhongMays = await _context.NguoiQuanLyPhongMays.ToListAsync();
        ViewBag.NguoiQuanLyPhongMays = nguoiQuanLyPhongMays.Select(nql => new SelectListItem
        {
            Value = nql.MaNguoiQuanLy,
            Text = nql.TenNguoiQuanLy
        });
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var totalRecords = await _context.PhongMays.CountAsync();
        var phongMays = await _context.PhongMays
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        await PopulateNguoiQuanLyPhongMaysAsync();
        return View(phongMays);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PhongMayViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingPhongMay = await _context.PhongMays
                .AsNoTracking()
                .FirstOrDefaultAsync(pm => pm.MaPhongMay == model.MaPhongMay);

            if (existingPhongMay != null)
            {
                ModelState.AddModelError("MaPhongMay", "Mã phòng máy đã tồn tại.");
                await PopulateNguoiQuanLyPhongMaysAsync();
                return View("Index", await _context.PhongMays.ToListAsync());
            }

            var phongMay = new PhongMay
            {
                MaPhongMay = model.MaPhongMay,
                TinhTrangPhongMay = model.TinhTrangPhongMay,
                MaNguoiQuanLy = model.MaNguoiQuanLy,
                TrangThai = model.TrangThai
            };

            _context.Add(phongMay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateNguoiQuanLyPhongMaysAsync();
        return View("Index", await _context.PhongMays.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PhongMayViewModel model)
    {
        if (ModelState.IsValid)
        {
            var phongMay = await _context.PhongMays.FindAsync(model.MaPhongMay);

            if (phongMay == null)
            {
                return NotFound();
            }

            phongMay.TinhTrangPhongMay = model.TinhTrangPhongMay;
            phongMay.MaNguoiQuanLy = model.MaNguoiQuanLy;
            phongMay.TrangThai = model.TrangThai;

            _context.Update(phongMay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulateNguoiQuanLyPhongMaysAsync();
        return View("Index", await _context.PhongMays.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var phongMay = await _context.PhongMays.FindAsync(id);
        if (phongMay != null)
        {
            _context.PhongMays.Remove(phongMay);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
