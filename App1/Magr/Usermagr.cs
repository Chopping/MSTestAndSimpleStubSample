using App1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Magr
{
    // 这里封装的是业务逻辑层的代码
    public class UserMagr
    {
        // 仿单例模式
        private IUserDao uDao;
        public IUserDao UDao { get => uDao; set => uDao = value; }

        // 可用方法
        public bool CheckUser(User u)
        {
            User res = uDao.FindUser(u.Username);
            return res.Username == u.Password ? true : false;
        }

        public bool AddOneUser(User u)
        {
            int res = uDao.CreateUser(u);
            return res == 1 ? true : false;
        }

        // 主要测试方法
        public int CreateUser(User u)
        {
            return uDao.CreateUser(u);
        }
    }
}
