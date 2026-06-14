using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HeiResturant.Models;

namespace HeiResturant.DAL
{
    public static class OrderDAL
    {
        public static string Checkout(int cardId, int cashierId, int restaurantId,
            List<CartItem> items, out int orderId)
        {
            orderId = 0;
            if (items == null || items.Count == 0)
                return "购物车为空";

            decimal total = 0;
            foreach (var item in items)
                total += item.SubTotal;

            using (var conn = DbHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        decimal balance = GetBalance(conn, trans, cardId);
                        if (balance < total)
                        {
                            trans.Rollback();
                            return $"余额不足，当前余额 {balance:F2} 元，需支付 {total:F2} 元";
                        }

                        foreach (var item in items)
                        {
                            int stock = GetStock(conn, trans, item.FoodId);
                            if (stock < item.Quantity)
                            {
                                trans.Rollback();
                                return $"{item.FoodName} 库存不足（剩余 {stock}）";
                            }
                        }

                        orderId = InsertOrder(conn, trans, cardId, cashierId, restaurantId, total);

                        foreach (var item in items)
                        {
                            InsertDetail(conn, trans, orderId, item);
                            DeductStock(conn, trans, item.FoodId, item.Quantity);
                        }

                        var newBalance = balance - total;
                        UpdateBalance(conn, trans, cardId, newBalance);
                        InsertTransaction(conn, trans, cardId, total, newBalance, orderId);

                        trans.Commit();
                        return null;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        return "结账失败：" + ex.Message;
                    }
                }
            }
        }

        public static DataTable GetTodayOrders()
        {
            return DbHelper.ExecuteQuery(@"
                SELECT o.OrderId, c.CardNo, s.Name AS StudentName,
                       u.Username AS Cashier, o.TotalAmount, o.OrderTime
                FROM Orders o
                INNER JOIN Cards c ON o.CardId = c.CardId
                INNER JOIN Students s ON c.StudentId = s.StudentId
                INNER JOIN Users u ON o.CashierId = u.UserId
                WHERE CAST(o.OrderTime AS DATE) = CAST(GETDATE() AS DATE)
                ORDER BY o.OrderTime DESC");
        }

        public static DataTable GetOrderDetails(int orderId)
        {
            return DbHelper.ExecuteQuery(@"
                SELECT d.DetailId, f.Name AS FoodName, d.Quantity, d.UnitPrice, d.SubTotal
                FROM OrderDetails d
                INNER JOIN Foods f ON d.FoodId = f.FoodId
                WHERE d.OrderId = @oid",
                DbHelper.Param("@oid", orderId));
        }

        public static DataTable GetSalesSummary()
        {
            return DbHelper.ExecuteQuery(@"
                SELECT CAST(OrderTime AS DATE) AS OrderDate,
                       COUNT(*) AS OrderCount,
                       SUM(TotalAmount) AS TotalSales
                FROM Orders
                GROUP BY CAST(OrderTime AS DATE)
                ORDER BY OrderDate DESC");
        }

        public static DataTable GetLowStockFoods(int threshold)
        {
            return DbHelper.ExecuteQuery(@"
                SELECT f.FoodId, f.Name, f.Stock, f.Price, f.IsAvailable, c.Name AS CategoryName
                FROM Foods f
                INNER JOIN FoodCategories c ON f.CategoryId = c.CategoryId
                WHERE f.Stock <= @t AND f.IsAvailable = 1
                ORDER BY f.Stock",
                DbHelper.Param("@t", threshold));
        }

        private static decimal GetBalance(SqlConnection conn, SqlTransaction trans, int cardId)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.CommandText = "SELECT Balance FROM Cards WHERE CardId=@id AND Status=N'Active'";
                cmd.Parameters.Add(DbHelper.Param("@id", cardId));
                var result = cmd.ExecuteScalar();
                if (result == null) throw new InvalidOperationException("校园卡不存在或已冻结");
                return Convert.ToDecimal(result);
            }
        }

        private static int GetStock(SqlConnection conn, SqlTransaction trans, int foodId)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.CommandText = "SELECT Stock FROM Foods WHERE FoodId=@id";
                cmd.Parameters.Add(DbHelper.Param("@id", foodId));
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static int InsertOrder(SqlConnection conn, SqlTransaction trans,
            int cardId, int cashierId, int restaurantId, decimal total)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.CommandText = @"
                    INSERT INTO Orders (CardId, CashierId, RestaurantId, TotalAmount)
                    OUTPUT INSERTED.OrderId
                    VALUES (@cid, @uid, @rid, @total)";
                cmd.Parameters.Add(DbHelper.Param("@cid", cardId));
                cmd.Parameters.Add(DbHelper.Param("@uid", cashierId));
                cmd.Parameters.Add(DbHelper.Param("@rid", restaurantId));
                cmd.Parameters.Add(DbHelper.Param("@total", total));
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private static void InsertDetail(SqlConnection conn, SqlTransaction trans, int orderId, CartItem item)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.CommandText = @"
                    INSERT INTO OrderDetails (OrderId, FoodId, Quantity, UnitPrice, SubTotal)
                    VALUES (@oid, @fid, @qty, @price, @sub)";
                cmd.Parameters.Add(DbHelper.Param("@oid", orderId));
                cmd.Parameters.Add(DbHelper.Param("@fid", item.FoodId));
                cmd.Parameters.Add(DbHelper.Param("@qty", item.Quantity));
                cmd.Parameters.Add(DbHelper.Param("@price", item.UnitPrice));
                cmd.Parameters.Add(DbHelper.Param("@sub", item.SubTotal));
                cmd.ExecuteNonQuery();
            }
        }

        private static void DeductStock(SqlConnection conn, SqlTransaction trans, int foodId, int qty)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.CommandText = "UPDATE Foods SET Stock = Stock - @qty WHERE FoodId=@id";
                cmd.Parameters.Add(DbHelper.Param("@qty", qty));
                cmd.Parameters.Add(DbHelper.Param("@id", foodId));
                cmd.ExecuteNonQuery();
            }
        }

        private static void UpdateBalance(SqlConnection conn, SqlTransaction trans, int cardId, decimal balance)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.CommandText = "UPDATE Cards SET Balance=@b WHERE CardId=@id";
                cmd.Parameters.Add(DbHelper.Param("@b", balance));
                cmd.Parameters.Add(DbHelper.Param("@id", cardId));
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertTransaction(SqlConnection conn, SqlTransaction trans,
            int cardId, decimal amount, decimal balanceAfter, int orderId)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = trans;
                cmd.CommandText = @"
                    INSERT INTO Transactions (CardId, TransType, Amount, BalanceAfter, Remark)
                    VALUES (@cid, N'Consume', @amt, @bal, @remark)";
                cmd.Parameters.Add(DbHelper.Param("@cid", cardId));
                cmd.Parameters.Add(DbHelper.Param("@amt", amount));
                cmd.Parameters.Add(DbHelper.Param("@bal", balanceAfter));
                cmd.Parameters.Add(DbHelper.Param("@remark", $"订单 #{orderId}"));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
