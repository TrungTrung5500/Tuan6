using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tuan6.Models
{
    public class ChiTietDonHangViewModel
    {
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
    }

    public class DonHangViewModel
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayTao { get; set; }
        public List<ChiTietDonHangViewModel> ChiTiet { get; set; }

        public DonHangViewModel()
        {
            ChiTiet = new List<ChiTietDonHangViewModel>();
        }
    }
}