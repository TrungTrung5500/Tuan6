using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using Tuan6.Models; 

namespace Tuan6.Controllers
{
    public class HomeController : Controller
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["shopConnection"].ConnectionString;

        private List<Loai> LayDanhSachLoai()
        {
            List<Loai> dsLoai = new List<Loai>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM tblLoai";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dsLoai.Add(new Loai
                    {
                        MaLoai = (int)reader["MaLoai"],
                        TenLoai = reader["TenLoai"].ToString()
                    });
                }
            }
            return dsLoai;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ViewBag.DanhSachLoai = LayDanhSachLoai();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}