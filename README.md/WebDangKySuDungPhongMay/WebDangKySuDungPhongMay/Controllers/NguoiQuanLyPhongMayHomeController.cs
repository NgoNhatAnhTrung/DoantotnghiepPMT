using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class NguoiQuanLyPhongMayHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NguoiQuanLyPhongMayHomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GVRequests()
        {
            var pendingGVRequests = await _context.PhieuDangKyGVs
                .Include(p => p.GiangVien)
                .Include(p => p.PhongMay)
                .Include(p => p.LopHocPhan)
                .Where(p => p.TrangThai == false)
                .ToListAsync();

            return View(pendingGVRequests);
        }

        public async Task<IActionResult> SVRequests()
        {
            var pendingSVRequests = await _context.PhieuDangKySVs
                .Include(p => p.SinhVien)
                .Include(p => p.PhongMay)
                .Where(p => p.TrangThai == false)
                .ToListAsync();

            return View(pendingSVRequests);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveGVRequest(int id)
        {
            var request = await _context.PhieuDangKyGVs.FindAsync(id);
            if (request != null)
            {
                request.TrangThai = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(GVRequests));
        }

        [HttpPost]
        public async Task<IActionResult> RejectGVRequest(int id)
        {
            var request = await _context.PhieuDangKyGVs.FindAsync(id);
            if (request != null)
            {
                _context.PhieuDangKyGVs.Remove(request);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(GVRequests));
        }

        [HttpPost]
        public async Task<IActionResult> ApproveSVRequest(int id)
        {
            var request = await _context.PhieuDangKySVs.FindAsync(id);
            if (request != null)
            {
                request.TrangThai = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(SVRequests));
        }

        [HttpPost]
        public async Task<IActionResult> RejectSVRequest(int id)
        {
            var request = await _context.PhieuDangKySVs.FindAsync(id);
            if (request != null)
            {
                _context.PhieuDangKySVs.Remove(request);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(SVRequests));
        }

        public async Task<IActionResult> GVHistory()
        {
            var approvedGVRequests = await _context.PhieuDangKyGVs
                .Include(p => p.GiangVien)
                .Include(p => p.PhongMay)
                .Include(p => p.LopHocPhan)
                .Where(p => p.TrangThai == true)
                .ToListAsync();

            return View(approvedGVRequests);
        }

        public async Task<IActionResult> SVHistory()
        {
            var approvedSVRequests = await _context.PhieuDangKySVs
                .Include(p => p.SinhVien)
                .Include(p => p.PhongMay)
                .Where(p => p.TrangThai == true)
                .ToListAsync();

            return View(approvedSVRequests);
        }

    }
}
