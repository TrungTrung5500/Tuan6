using Tuan6.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

public class ProductController : Controller
{
    private string connectionString = ConfigurationManager.ConnectionStrings["shopConnection"].ConnectionString;

    public ActionResult Index(int id)
    {
        List<SanPham> dsSanPham = new List<SanPham>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM tblSanPham WHERE MaLoai = @MaLoai";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaLoai", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dsSanPham.Add(new SanPham
                {
                    MaSanPham = (int)reader["MaSanPham"],
                    TenSP = reader["TenSP"].ToString(),
                    GiaBan = (decimal)reader["GiaBan"],
                    Hinh = reader["Hinh"].ToString()
                });
            }
        }
        return View("ProductList", dsSanPham); 
    }

    public ActionResult Details(int id)
    {
        SanPham sanPham = null;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM tblSanPham WHERE MaSanPham = @MaSP";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@MaSP", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                sanPham = new SanPham
                {
                    MaSanPham = (int)reader["MaSanPham"],
                    TenSP = reader["TenSP"].ToString(),
                    GiaBan = (decimal)reader["GiaBan"],
                    GhiChu = reader["GhiChu"].ToString(),
                    Hinh = reader["Hinh"].ToString()
                };
            }
        }
        return View(sanPham);
    }

    public ActionResult Search(string keyword)
    {
        List<SanPham> dsSanPham = new List<SanPham>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM tblSanPham WHERE TenSP LIKE @Keyword";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dsSanPham.Add(new SanPham
                {
                    MaSanPham = (int)reader["MaSanPham"],
                    TenSP = reader["TenSP"].ToString(),
                    GiaBan = (decimal)reader["GiaBan"],
                    Hinh = reader["Hinh"].ToString()
                });
            }
        }
        ViewBag.Keyword = keyword; 
        return View("ProductList", dsSanPham); 
    }
}