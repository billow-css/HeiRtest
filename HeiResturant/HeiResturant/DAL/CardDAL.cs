using System;
using System.Data;

namespace HeiResturant.DAL
{
    public static class CardDAL
    {
        public static DataTable GetCardByCardNo(string cardNo)
        {
            return DbHelper.ExecuteQuery(@"
                SELECT c.CardId, c.CardNo, c.Balance, c.Status,
                       s.StudentId, s.StudentNo, s.Name AS StudentName, s.ClassName
                FROM Cards c
                INNER JOIN Students s ON c.StudentId = s.StudentId
                WHERE c.CardNo = @no OR s.StudentNo = @no",
                DbHelper.Param("@no", cardNo));
        }

        public static DataTable GetCardByStudentId(int studentId)
        {
            return DbHelper.ExecuteQuery(@"
                SELECT c.CardId, c.CardNo, c.Balance, c.Status,
                       s.StudentNo, s.Name AS StudentName, s.ClassName
                FROM Cards c
                INNER JOIN Students s ON c.StudentId = s.StudentId
                WHERE s.StudentId = @sid",
                DbHelper.Param("@sid", studentId));
        }

        public static int Recharge(int cardId, decimal amount)
        {
            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        decimal balance;
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "SELECT Balance FROM Cards WHERE CardId=@id";
                            cmd.Parameters.Add(DbHelper.Param("@id", cardId));
                            balance = Convert.ToDecimal(cmd.ExecuteScalar());
                        }

                        var newBalance = balance + amount;
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = "UPDATE Cards SET Balance=@b WHERE CardId=@id";
                            cmd.Parameters.Add(DbHelper.Param("@b", newBalance));
                            cmd.Parameters.Add(DbHelper.Param("@id", cardId));
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = trans;
                            cmd.CommandText = @"
                                INSERT INTO Transactions (CardId, TransType, Amount, BalanceAfter, Remark)
                                VALUES (@cid, N'Recharge', @amt, @bal, N'充值')";
                            cmd.Parameters.Add(DbHelper.Param("@cid", cardId));
                            cmd.Parameters.Add(DbHelper.Param("@amt", amount));
                            cmd.Parameters.Add(DbHelper.Param("@bal", newBalance));
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return 1;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public static DataTable GetTransactions(int cardId)
        {
            return DbHelper.ExecuteQuery(@"
                SELECT TransId, TransType, Amount, BalanceAfter, TransTime, Remark
                FROM Transactions WHERE CardId = @cid ORDER BY TransTime DESC",
                DbHelper.Param("@cid", cardId));
        }

        public static void UpdateCard(int cardId, decimal balance, string status)
        {
            DbHelper.ExecuteNonQuery(@"
                UPDATE Cards SET Balance=@b, Status=@s WHERE CardId=@id",
                DbHelper.Param("@b", balance),
                DbHelper.Param("@s", status),
                DbHelper.Param("@id", cardId));
        }
    }
}
