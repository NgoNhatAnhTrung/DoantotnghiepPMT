using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class MayTinhController : Controller
{
    private readonly ApplicationDbContext _context;

    public MayTinhController(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task PopulatePhongMaysAsync()
    {
        var phongMays = await _context.PhongMays.ToListAsync();
        ViewBag.PhongMays = phongMays.Select(pm => new SelectListItem
        {
            Value = pm.MaPhongMay,
            Text = pm.MaPhongMay
        });
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var totalRecords = await _context.MayTinhs.CountAsync();
        var mayTinhs = await _context.MayTinhs
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

        ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        await PopulatePhongMaysAsync();
        return View(mayTinhs);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MayTinhViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingMayTinh = await _context.MayTinhs
                .AsNoTracking()
                .FirstOrDefaultAsync(mt => mt.MaMay == model.MaMay);

            if (existingMayTinh != null)
            {
                ModelState.AddModelError("MaMay", "Mã máy đã tồn tại.");
                await PopulatePhongMaysAsync();
                return View("Index", await _context.MayTinhs.ToListAsync());
            }

            var mayTinh = new MayTinh
            {
                MaMay = model.MaMay,
                MaPhong = model.MaPhong,
                TinhTrangMayTinh = model.TinhTrangMayTinh,
                TrangThai = model.TrangThai
            };

            _context.Add(mayTinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulatePhongMaysAsync();
        return View("Index", await _context.MayTinhs.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MayTinhViewModel model)
    {
        if (ModelState.IsValid)
        {
            var mayTinh = await _context.MayTinhs.FindAsync(model.MaMay);

            if (mayTinh == null)
            {
                return NotFound();
            }

            mayTinh.MaPhong = model.MaPhong;
            mayTinh.TinhTrangMayTinh = model.TinhTrangMayTinh;
            mayTinh.TrangThai = model.TrangThai;

            _context.Update(mayTinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        await PopulatePhongMaysAsync();
        return View("Index", await _context.MayTinhs.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var mayTinh = await _context.MayTinhs.FindAsync(id);
        if (mayTinh != null)
        {
            _context.MayTinhs.Remove(mayTinh);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
