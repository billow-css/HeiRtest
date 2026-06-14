using System;
using System.Data;
using HeiResturant.Models;

namespace HeiResturant.DAL
{
    public static class StudentDAL
    {
        /// <summary>学生刷学号/卡号直接登录（学号即卡号）</summary>
        public static UserInfo LoginByCardOrStudentNo(string input)
        {
            const string sql = @"
                SELECT s.StudentId, s.StudentNo, s.Name, s.ClassName,
                       c.CardId, c.CardNo, c.Balance, c.Status
                FROM Students s
                INNER JOIN Cards c ON c.StudentId = s.StudentId
                WHERE s.StudentNo = @input OR c.CardNo = @input";

            var dt = DbHelper.ExecuteQuery(sql, DbHelper.Param("@input", input));
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            if (row["Status"].ToString() != "Active")
                return null;

            return new UserInfo
            {
                StudentId = Convert.ToInt32(row["StudentId"]),
                CardId = Convert.ToInt32(row["CardId"]),
                Username = row["StudentNo"].ToString(),
                DisplayName = row["Name"].ToString(),
                RoleType = "Student"
            };
        }

        public static DataTable GetAllStudents()
        {
            return DbHelper.ExecuteQuery(@"
                SELECT s.StudentId, s.StudentNo, s.Name, s.ClassName, s.Phone,
                       c.CardId, c.CardNo, c.Balance, c.Status
                FROM Students s
                INNER JOIN Cards c ON c.StudentId = s.StudentId
                ORDER BY s.StudentId");
        }

        /// <summary>添加学生并自动开卡（卡号=学号，一人一卡）</summary>
        public static int AddStudent(string studentNo, string name, string className,
            string phone, decimal initialBalance)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        int studentId;
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = @"
                                INSERT INTO Students (StudentNo, Name, ClassName, Phone)
                                OUTPUT INSERTED.StudentId
                                VALUES (@no, @name, @cls, @phone)";
                            cmd.Parameters.Add(DbHelper.Param("@no", studentNo));
                            cmd.Parameters.Add(DbHelper.Param("@name", name));
                            cmd.Parameters.Add(DbHelper.Param("@cls", className));
                            cmd.Parameters.Add(DbHelper.Param("@phone", phone));
                            studentId = (int)cmd.ExecuteScalar();
                        }

                        int cardId;
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = @"
                                INSERT INTO Cards (StudentId, CardNo, Balance)
                                OUTPUT INSERTED.CardId
                                VALUES (@sid, @cno, @bal)";
                            cmd.Parameters.Add(DbHelper.Param("@sid", studentId));
                            cmd.Parameters.Add(DbHelper.Param("@cno", studentNo));
                            cmd.Parameters.Add(DbHelper.Param("@bal", initialBalance));
                            cardId = (int)cmd.ExecuteScalar();
                        }

                        if (initialBalance > 0)
                        {
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = trans;
                                cmd.CommandText = @"
                                    INSERT INTO Transactions (CardId, TransType, Amount, BalanceAfter, Remark)
                                    VALUES (@cid, N'Recharge', @amt, @bal, N'开卡充值')";
                                cmd.Parameters.Add(DbHelper.Param("@cid", cardId));
                                cmd.Parameters.Add(DbHelper.Param("@amt", initialBalance));
                                cmd.Parameters.Add(DbHelper.Param("@bal", initialBalance));
                                cmd.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        return studentId;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public static void UpdateStudent(int studentId, string name, string className, string phone)
        {
            DbHelper.ExecuteNonQuery(@"
                UPDATE Students SET Name=@name, ClassName=@cls, Phone=@phone WHERE StudentId=@id",
                DbHelper.Param("@name", name),
                DbHelper.Param("@cls", className),
                DbHelper.Param("@phone", phone),
                DbHelper.Param("@id", studentId));
        }
    }
}
