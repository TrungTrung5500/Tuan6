using Tuan6.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

public class AccountController : Controller
{
    private string connectionString = ConfigurationManager.ConnectionStrings["shopConnection"].ConnectionString;

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(string soDienThoai, string matKhau)
    {
        KhachHang kh = null;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM tblKhachHang WHERE SoDienThoai = @SDT AND MatKhau = @MatKhau";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@SDT", soDienThoai);
            cmd.Parameters.AddWithValue("@MatKhau", matKhau);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                kh = new KhachHang
                {
                    MaKhachHang = (int)reader["MaKhachHang"],
                    TenKhachHang = reader["TenKhachHang"].ToString()
                };
            }
        }

        if (kh != null)
        {
            Session["User"] = kh; 
            return RedirectToAction("Index", "Home");
        }
        ViewBag.Error = "Số điện thoại hoặc mật khẩu không đúng.";
        return View();
    }

    public ActionResult Logout()
    {
        Session.Remove("User");
        return RedirectToAction("Index", "Home");
    }

    public ActionResult History()
    {
        if (Session["User"] == null)
        {
            return RedirectToAction("Login");
        }

        var user = Session["User"] as KhachHang;
        var danhSachDonHang = new List<DonHangViewModel>();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = @"SELECT h.MaHoaDon, h.NgayTao, s.TenSP, c.SoLuong, s.GiaBan
                             FROM tblHoaDon h
                             JOIN tblChiTiet c ON h.MaHoaDon = c.MaHD
                             JOIN tblSanPham s ON c.MaSP = s.MaSanPham
                             WHERE h.MaKH = @MaKH
                             ORDER BY h.NgayTao DESC";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaKH", user.MaKhachHang);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            DonHangViewModel currentDonHang = null;
            int lastMaHD = 0;

            while (reader.Read())
            {
                int maHD = (int)reader["MaHoaDon"];
                if (maHD != lastMaHD)
                {
                    currentDonHang = new DonHangViewModel
                    {
                        MaHoaDon = maHD,
                        NgayTao = (DateTime)reader["NgayTao"]
                    };
                    danhSachDonHang.Add(currentDonHang);
                    lastMaHD = maHD;
                }
                currentDonHang.ChiTiet.Add(new ChiTietDonHangViewModel
                {
                    TenSP = reader["TenSP"].ToString(),
                    SoLuong = (int)reader["SoLuong"],
                    GiaBan = (decimal)reader["GiaBan"]
                });
            }
        }
        return View(danhSachDonHang);
    }
}