using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuan6.Models
{
    public class SanPham
    {
        public int MaSanPham { get; set; }
        public string TenSP { get; set; }
        public decimal GiaBan { get; set; }
        public string GhiChu { get; set; }
        public string Hinh { get; set; }
    }
}