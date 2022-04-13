using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Urrutia_Project1.Models
{
    public class OrderDetailsModel
    {
        #region OrderDetails Variables
        public int oId { get; set; }
        //How does this ForeignKey thing work?!
        //public cId cId { get; set; }
        //[ForeignKey("cId")]
        public int Customer_ID { get; set; }
        public DateTime DateOrdered { get; set; }
        public int TotalCost { get; set; }
        public int EmployeeServicingID { get; set; }

        SqlConnection con = new SqlConnection("server= DESKTOP-2LDPKO5\\NICHOLASINSTANCE; database= Project1; integrated security=true");
        #endregion

        #region Add Order
        public string AddOrder(OrderDetailsModel newOrder)
        {
            SqlCommand cmd_addOrder = new SqlCommand("insert into OrderDetails values (@Customer_ID, @DateOrdered, @TotalCost, @EmployeeServicingID)", con);

            cmd_addOrder.Parameters.AddWithValue("@Customer_ID", newOrder.Customer_ID);
            cmd_addOrder.Parameters.AddWithValue("@DateOrdered", newOrder.DateOrdered);
            cmd_addOrder.Parameters.AddWithValue("@TotalCost", newOrder.TotalCost);
            cmd_addOrder.Parameters.AddWithValue("@EmployeeServicingID", newOrder.EmployeeServicingID);


            try
            {
                con.Open();
                cmd_addOrder.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Order Added Successfully";
        }
        #endregion

        #region Read Order
        public List<OrderDetailsModel> GetOrderList()
        {
            SqlCommand cmd_getOrders = new SqlCommand("select * from OrderDetails", con);
            List<OrderDetailsModel> cList = new List<OrderDetailsModel>();
            SqlDataReader readAllOrders = null;
            try
            {
                con.Open();
                readAllOrders = cmd_getOrders.ExecuteReader();

                while (readAllOrders.Read())
                {
                    cList.Add(new OrderDetailsModel()
                    {
                        oId = Convert.ToInt32(readAllOrders[0]),
                        Customer_ID = Convert.ToInt32(readAllOrders[1]),
                        DateOrdered = Convert.ToDateTime(readAllOrders[2]),
                        TotalCost = Convert.ToInt32(readAllOrders[3]),
                        EmployeeServicingID = Convert.ToInt32(readAllOrders[4])
                    });
                }

            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readAllOrders.Close();
                con.Close();
            }
            return cList;
        }
        #endregion

        #region Read Order by ID
        public OrderDetailsModel GetOrderDetails(int oId)
        {

            SqlCommand cmd_searchById = new SqlCommand("select * from OrderDetails where oId=@oId", con);
            cmd_searchById.Parameters.AddWithValue("@oId", oId);
            SqlDataReader readOrder = null;
            OrderDetailsModel odm = new OrderDetailsModel();
            try
            {
                con.Open();
                readOrder = cmd_searchById.ExecuteReader();

                if (readOrder.Read())
                {
                    odm.oId = Convert.ToInt32(readOrder[0]);
                    odm.Customer_ID = Convert.ToInt32(readOrder[1]);
                    odm.DateOrdered = Convert.ToDateTime(readOrder[2]);
                    odm.TotalCost = Convert.ToInt32(readOrder[3]);
                    odm.EmployeeServicingID = Convert.ToInt32(readOrder[4]);
                }
                else
                {
                    throw new Exception("Order Not Found. Please enter a valid Order ID");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readOrder.Close();
                con.Close();
            }
            return odm;
        }
        #endregion

        #region Update Order
        public string UpdateOrder(OrderDetailsModel update)
        {

            SqlCommand cmd_updateOrder = new SqlCommand("update OrderDetails set Customer_ID=@Customer_ID,DateOrdered=@DateOrdered, TotalCost=@TotalCost, EmployeeServicingID= @EmployeeServicingID where oId =@oId", con);
            cmd_updateOrder.Parameters.AddWithValue("@Customer_ID", update.Customer_ID);
            cmd_updateOrder.Parameters.AddWithValue("@DateOrdered", update.DateOrdered);
            cmd_updateOrder.Parameters.AddWithValue("@TotalCost", update.TotalCost);
            cmd_updateOrder.Parameters.AddWithValue("@EmployeeServicingID", update.EmployeeServicingID);
            cmd_updateOrder.Parameters.AddWithValue("@oId", update.oId);

            try
            {
                con.Open();
                cmd_updateOrder.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Updated Order Successfully";
        }
        #endregion

        #region Delete Order
        public string DeleteOrder(int oId)
        {
            SqlCommand cmd_deleteOrder = new SqlCommand("delete from OrderDetails where oId = @oId", con);
            cmd_deleteOrder.Parameters.AddWithValue("@oId", oId);
            try
            {
                con.Open();
                cmd_deleteOrder.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Order Deleted Successfully";
        }
        #endregion
    }
}
