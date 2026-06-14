using System;
using System.Data;
using HeiResturant.Models;

namespace HeiResturant.DAL
{
    public static class UserDAL
    {
        /// <summary>工作人员登录（测试阶段免密码）</summary>
        public static UserInfo LoginStaff(string username, string roleType)
        {
            const string sql = @"
                SELECT UserId, Username, RoleType
                FROM Users
                WHERE Username = @u AND RoleType = @r AND IsActive = 1";

            var dt = DbHelper.ExecuteQuery(sql,
                DbHelper.Param("@u", username),
                DbHelper.Param("@r", roleType));

            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new UserInfo
            {
                UserId = Convert.ToInt32(row["UserId"]),
                Username = row["Username"].ToString(),
                DisplayName = row["Username"].ToString(),
                RoleType = row["RoleType"].ToString()
            };
        }

        public static DataTable GetAllUsers()
        {
            return DbHelper.ExecuteQuery(@"
                SELECT UserId, Username, RoleType, IsActive, CreatedAt
                FROM Users ORDER BY UserId");
        }

        public static int AddUser(string username, string roleType)
        {
            return DbHelper.ExecuteNonQuery(@"
                INSERT INTO Users (Username, Password, RoleType) VALUES (@u, N'', @r)",
                DbHelper.Param("@u", username),
                DbHelper.Param("@r", roleType));
        }

        public static int DeleteUser(int userId)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM Users WHERE UserId = @id",
                DbHelper.Param("@id", userId));
        }

        public static void UpdateUser(int userId, string username, string roleType, bool isActive)
        {
            DbHelper.ExecuteNonQuery(@"
                UPDATE Users SET Username=@u, RoleType=@r, IsActive=@a WHERE UserId=@id",
                DbHelper.Param("@u", username),
                DbHelper.Param("@r", roleType),
                DbHelper.Param("@a", isActive),
                DbHelper.Param("@id", userId));
        }
    }
}
