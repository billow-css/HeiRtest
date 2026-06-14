using System;
using System.Collections.Generic;
using System.Data;
using HeiResturant.Models;

namespace HeiResturant.DAL
{
    public static class FoodDAL
    {
        public static List<FoodItem> GetAvailableFoods()
        {
            const string sql = @"
                SELECT f.FoodId, f.RestaurantId, f.CategoryId, c.Name AS CategoryName,
                       f.Name, f.Price, f.Stock, f.IsAvailable
                FROM Foods f
                INNER JOIN FoodCategories c ON f.CategoryId = c.CategoryId
                WHERE f.IsAvailable = 1 AND f.Stock > 0
                ORDER BY c.CategoryId, f.FoodId";

            var list = new List<FoodItem>();
            var dt = DbHelper.ExecuteQuery(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(MapRow(row));
            return list;
        }

        public static DataTable GetAllFoods()
        {
            return DbHelper.ExecuteQuery(@"
                SELECT f.FoodId, c.Name AS CategoryName,
                       f.Name, f.Price, f.Stock, f.IsAvailable
                FROM Foods f
                INNER JOIN FoodCategories c ON f.CategoryId = c.CategoryId
                ORDER BY f.FoodId");
        }

        public static DataTable GetCategories()
        {
            return DbHelper.ExecuteQuery("SELECT CategoryId, Name FROM FoodCategories ORDER BY CategoryId");
        }

        public static DataTable GetRestaurants()
        {
            return DbHelper.ExecuteQuery("SELECT RestaurantId, Name, Location FROM Restaurants ORDER BY RestaurantId");
        }

        public static int GetDefaultRestaurantId()
        {
            var result = DbHelper.ExecuteScalar("SELECT TOP 1 RestaurantId FROM Restaurants ORDER BY RestaurantId");
            return result == null ? 0 : Convert.ToInt32(result);
        }

        public static int AddFood(int categoryId, string name, decimal price, int stock)
        {
            var restaurantId = GetDefaultRestaurantId();
            return DbHelper.ExecuteNonQuery(@"
                INSERT INTO Foods (RestaurantId, CategoryId, Name, Price, Stock)
                VALUES (@rid, @cid, @name, @price, @stock)",
                DbHelper.Param("@rid", restaurantId),
                DbHelper.Param("@cid", categoryId),
                DbHelper.Param("@name", name),
                DbHelper.Param("@price", price),
                DbHelper.Param("@stock", stock));
        }

        public static int UpdateFood(int foodId, string name, decimal price, int stock, bool isAvailable)
        {
            return DbHelper.ExecuteNonQuery(@"
                UPDATE Foods SET Name=@name, Price=@price, Stock=@stock, IsAvailable=@avail
                WHERE FoodId=@id",
                DbHelper.Param("@name", name),
                DbHelper.Param("@price", price),
                DbHelper.Param("@stock", stock),
                DbHelper.Param("@avail", isAvailable),
                DbHelper.Param("@id", foodId));
        }

        public static int DeleteFood(int foodId)
        {
            return DbHelper.ExecuteNonQuery("DELETE FROM Foods WHERE FoodId=@id",
                DbHelper.Param("@id", foodId));
        }

        private static FoodItem MapRow(DataRow row)
        {
            return new FoodItem
            {
                FoodId = Convert.ToInt32(row["FoodId"]),
                RestaurantId = Convert.ToInt32(row["RestaurantId"]),
                CategoryId = Convert.ToInt32(row["CategoryId"]),
                CategoryName = row["CategoryName"].ToString(),
                Name = row["Name"].ToString(),
                Price = Convert.ToDecimal(row["Price"]),
                Stock = Convert.ToInt32(row["Stock"]),
                IsAvailable = Convert.ToBoolean(row["IsAvailable"])
            };
        }
    }
}
