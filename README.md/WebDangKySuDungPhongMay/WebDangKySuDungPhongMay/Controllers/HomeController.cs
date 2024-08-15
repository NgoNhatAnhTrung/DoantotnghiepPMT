using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebDangKySuDungPhongMay.Models;
using WebDangKySuDungPhongMay.ViewModels;

namespace WebDangKySuDungPhongMay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Home");
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "NguoiQuanLyPhongMay");
            }
            else if (User.IsInRole("GiangVien"))
            {
                return RedirectToAction("Index", "GiangVienHome");
            }
            else if (User.IsInRole("SinhVien"))
            {
                return RedirectToAction("Index", "SinhVienHome");
            }
            else if (User.IsInRole("NguoiQuanLyPhongMay"))
            {
                return RedirectToAction("Index", "NguoiQuanLyPhongMayHome");
            }

            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.TenDangNhap == model.Username && a.MatKhau == model.Password);
                if (admin != null)
                {
                    await SignInUser(admin.TenDangNhap, "Admin", admin.TenHienThi);
                    return RedirectToAction("Index", "Home");
                }

                var giangVien = await _context.GiangViens
                    .FirstOrDefaultAsync(gv => gv.MaGiangVien == model.Username && gv.MatKhau == model.Password);
                if (giangVien != null)
                {
                    await SignInUser(giangVien.MaGiangVien, "GiangVien", giangVien.HoTen);
                    return RedirectToAction("Index", "GiangVienHome");
                }

                var sinhVien = await _context.SinhViens
                    .FirstOrDefaultAsync(sv => sv.MaSinhVien == model.Username && sv.MatKhau == model.Password);
                if (sinhVien != null)
                {
                    await SignInUser(sinhVien.MaSinhVien, "SinhVien", sinhVien.TenSinhVien);
                    return RedirectToAction("Index", "SinhVienHome");
                }

                var nguoiQuanLy = await _context.NguoiQuanLyPhongMays
                    .FirstOrDefaultAsync(nql => nql.MaNguoiQuanLy == model.Username && nql.MatKhau == model.Password);
                if (nguoiQuanLy != null)
                {
                    await SignInUser(nguoiQuanLy.MaNguoiQuanLy, "NguoiQuanLyPhongMay", nguoiQuanLy.TenNguoiQuanLy);
                    return RedirectToAction("Index", "NguoiQuanLyPhongMayHome");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }

        private async Task SignInUser(string username, string role, string subname)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("DisplayName", subname)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }
      
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userName = User.Identity.Name;

            var admin = await _context.Admins.FirstOrDefaultAsync(u => u.TenDangNhap == userName);
            if (admin != null)
            {
                if (admin.MatKhau != model.CurrentPassword)
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác");
                    return View(model);
                }

                admin.MatKhau = model.NewPassword;
                await _context.SaveChangesAsync();
                ViewBag.Message = "Đổi mật khẩu thành công!";
                return RedirectToAction("Logout", "Home");
               
            }

            var giangVien = await _context.GiangViens.FirstOrDefaultAsync(u => u.MaGiangVien == userName);
            if (giangVien != null)
            {
                if (giangVien.MatKhau != model.CurrentPassword)
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác");
                    return View(model);
                }

                giangVien.MatKhau = model.NewPassword;
                await _context.SaveChangesAsync();
                ViewBag.Message = "Đổi mật khẩu thành công!";
                return RedirectToAction("Logout", "Home");
            }

            var sinhVien = await _context.SinhViens.FirstOrDefaultAsync(u => u.MaSinhVien == userName);
            if (sinhVien != null)
            {
                if (sinhVien.MatKhau != model.CurrentPassword)
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác");
                    return View(model);
                }

                sinhVien.MatKhau = model.NewPassword;
                await _context.SaveChangesAsync();
                ViewBag.Message = "Đổi mật khẩu thành công!";
                return RedirectToAction("Logout", "Home");
            }

            var nguoiQuanLy = await _context.NguoiQuanLyPhongMays.FirstOrDefaultAsync(u => u.MaNguoiQuanLy == userName);
            if (nguoiQuanLy != null)
            {
                if (nguoiQuanLy.MatKhau != model.CurrentPassword)
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác");
                    return View(model);
                }

                nguoiQuanLy.MatKhau = model.NewPassword;
                await _context.SaveChangesAsync();
                ViewBag.Message = "Đổi mật khẩu thành công!";
                return RedirectToAction("Logout", "Home");
            }

            ModelState.AddModelError("", "Không tìm thấy người dùng");
            return View(model);
        }

    }
}
