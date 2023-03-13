using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MF900
{
    public class Login
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)] //主键设置
        public string User { get; set; }
        public string Password { get; set; }

        public int ID { get; set; }
    }
}
