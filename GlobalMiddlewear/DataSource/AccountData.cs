using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMiddlewear.Models;
namespace GlobalMiddlewear.DataSource
{
    public class AccountData
    {
        public static List<Account> accounts = new()
        {
            new Account { SoTaiKhoan = "99999", TenTaiKhoan = "Phan Sy Tu", SoDu = 100000000 },
            new Account { SoTaiKhoan = "99989", TenTaiKhoan = "Anh La Tu", SoDu = 1500000 }
        };
    }
}