using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Urrutia_Project1.Models
{
    public class CustomerDetailsModel
    {
        #region CustomerDetails Variables
        public int cId { get; set; }
        public string cFirstName { get; set; }
        public string cLastName { get; set; }
        public string cEmail { get; set; }
        public long cPhoneNo { get; set; }
        public string cUsername { get; set; }
        public string cPassword { get; set; }

        SqlConnection con = new SqlConnection("server= DESKTOP-2LDPKO5\\NICHOLASINSTANCE; database= Project1; integrated security=true");
        #endregion

        #region Add Customer
        public string AddCustomer(CustomerDetailsModel newCustomer)
        {
            SqlCommand cmd_addCustomer = new SqlCommand("insert into CustomerDetails values (@cFirstName, @cLastName, @cEmail, @cPhoneNo, @cUsername, @cPassword)", con);

            cmd_addCustomer.Parameters.AddWithValue("@cFirstName", newCustomer.cFirstName);
            cmd_addCustomer.Parameters.AddWithValue("@cLastName", newCustomer.cLastName);
            cmd_addCustomer.Parameters.AddWithValue("@cEmail", newCustomer.cEmail);
            cmd_addCustomer.Parameters.AddWithValue("@cPhoneNo", newCustomer.cPhoneNo);
            cmd_addCustomer.Parameters.AddWithValue("@cUsername", newCustomer.cUsername);
            cmd_addCustomer.Parameters.AddWithValue("@cPassword", newCustomer.cPassword);

            try
            {
                con.Open();
                cmd_addCustomer.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Customer" + cFirstName + " " + cLastName + "Added Successfully";
        }
        #endregion

        #region Read Customer
        public List<CustomerDetailsModel> GetCustomerList()
        {
            SqlCommand cmd_getCustomers = new SqlCommand("select * from CustomerDetails", con);
            List<CustomerDetailsModel> cList = new List<CustomerDetailsModel>();
            SqlDataReader readAllCustomers = null;
            try
            {
                con.Open();
                readAllCustomers = cmd_getCustomers.ExecuteReader();

                while (readAllCustomers.Read())
                {
                    cList.Add(new CustomerDetailsModel()
                    {
                        cId = Convert.ToInt32(readAllCustomers[0]),
                        cFirstName = readAllCustomers[1].ToString(),
                        cLastName = readAllCustomers[2].ToString(),
                        cEmail = readAllCustomers[3].ToString(),
                        cPhoneNo = Convert.ToInt64(readAllCustomers[4]),
                        cUsername = readAllCustomers[5].ToString(),
                        cPassword = readAllCustomers[6].ToString()
                    });
                }

            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readAllCustomers.Close();
                con.Close();
            }
            return cList;
        }
        #endregion

        #region Read Customer by ID
        public CustomerDetailsModel GetCustomerDetails(int cId)
        {

            SqlCommand cmd_searchById = new SqlCommand("select * from CustomerDetails where cId=@cId", con);
            cmd_searchById.Parameters.AddWithValue("@cId", cId);
            SqlDataReader readCustomer = null;
            CustomerDetailsModel cdm = new CustomerDetailsModel();
            try
            {
                con.Open();
                readCustomer = cmd_searchById.ExecuteReader();

                if (readCustomer.Read())
                {
                    cdm.cId = Convert.ToInt32(readCustomer[0]);
                    cdm.cFirstName = readCustomer[1].ToString();
                    cdm.cLastName = readCustomer[2].ToString();
                    cdm.cEmail = readCustomer[3].ToString();
                    cdm.cPhoneNo = Convert.ToInt64(readCustomer[4]);
                    cdm.cUsername = readCustomer[5].ToString();
                    cdm.cPassword = readCustomer[6].ToString();
                }
                else
                {
                    throw new Exception("Customer Not Found. Please enter a valid Customer ID");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readCustomer.Close();
                con.Close();
            }
            return cdm;
        }
        #endregion

        #region Update Customer
        public string UpdateCustomer(CustomerDetailsModel update)
        {

            SqlCommand cmd_updateCustomer = new SqlCommand("update CustomerDetails set cFirstName=@cFirstName,cLastName=@cLastName, cEmail=@cEmail, cPhoneNo= @cPhoneNo, cUsername=@cUsername, cPassword=@cPassword where cId =@cId", con);
            cmd_updateCustomer.Parameters.AddWithValue("@cFirstName", update.cFirstName);
            cmd_updateCustomer.Parameters.AddWithValue("@cLastName", update.cLastName);
            cmd_updateCustomer.Parameters.AddWithValue("@cEmail", update.cEmail);
            cmd_updateCustomer.Parameters.AddWithValue("@cPhoneNo", update.cPhoneNo);
            cmd_updateCustomer.Parameters.AddWithValue("@cUsername", update.cUsername);
            cmd_updateCustomer.Parameters.AddWithValue("@cPassword", update.cPassword);
            cmd_updateCustomer.Parameters.AddWithValue("@cId", update.cId);

            try
            {
                con.Open();
                cmd_updateCustomer.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Updated Customer Successfully";
        }
        #endregion

        #region Delete Customer
        public string DeleteCustomer(int cId)
        {
            SqlCommand cmd_deleteCustomer = new SqlCommand("delete from CustomerDetails where cId = @cId", con);
            cmd_deleteCustomer.Parameters.AddWithValue("@cId", cId);
            try
            {
                con.Open();
                cmd_deleteCustomer.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Customer Deleted Successfully";
        }
        #endregion
    }
}
