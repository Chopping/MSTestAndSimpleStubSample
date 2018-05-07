using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    // 这里封装的是数据访问层的代码
    public interface IUserDao// 待测对象的依赖对象
    {        
        // 模拟的对用户的增删改查
        int CreateUser(User u);
        int UpdateUser(User u);
        int DeleteUser(User u);

        int GetAllUserCount();
        User FindUser(string Username);
        HashSet<User> FindAllUser();
    }
}
