using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Urrutia_Project1.Models
{
    public class ProductDetailsModel
    {
        #region ProductDetails Variables
        public int pId { get; set; }
        public string pName { get; set; }
        public string pType { get; set; }
        public float pPrice { get; set; }
        public int pQty { get; set; }
        public bool pIsInStock { get; set; }

        SqlConnection con = new SqlConnection("server= DESKTOP-2LDPKO5\\NICHOLASINSTANCE; database= Project1; integrated security=true");
        #endregion

        #region Add Product
        public string AddProduct(ProductDetailsModel newProduct)
        {
            SqlCommand cmd_addProduct = new SqlCommand("insert into ProductDetails values (@pName, @pType, @pPrice, @pQty, @pIsInStock)", con);

            cmd_addProduct.Parameters.AddWithValue("@pName", newProduct.pName);
            cmd_addProduct.Parameters.AddWithValue("@pType", newProduct.pType);
            cmd_addProduct.Parameters.AddWithValue("@pPrice", newProduct.pPrice);
            cmd_addProduct.Parameters.AddWithValue("@pQty", newProduct.pQty);
            cmd_addProduct.Parameters.AddWithValue("@pIsInStock", newProduct.pIsInStock);

            try
            {
                con.Open();
                cmd_addProduct.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Product " + pName + "Added Successfully";
        }
        #endregion

        #region Read Product
        public List<ProductDetailsModel> GetProductList()
        {
            SqlCommand cmd_getProducts = new SqlCommand("select * from ProductDetails", con);
            List<ProductDetailsModel> pList = new List<ProductDetailsModel>();
            SqlDataReader readAllProducts = null;
            try
            {
                con.Open();
                readAllProducts = cmd_getProducts.ExecuteReader();

                while (readAllProducts.Read())
                {
                    pList.Add(new ProductDetailsModel()
                    {
                        pId = Convert.ToInt32(readAllProducts[0]),
                        pName = readAllProducts[1].ToString(),
                        pType = readAllProducts[2].ToString(),
                        pPrice = (float)Convert.ToDouble(readAllProducts[3]),
                        pQty = Convert.ToInt32(readAllProducts[4]),
                        pIsInStock = Convert.ToBoolean(readAllProducts[5])
                    });
                }

            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readAllProducts.Close();
                con.Close();
            }
            return pList;
        }
        #endregion

        #region Read Product by ID
        public ProductDetailsModel GetProductDetails(int pId)
        {

            SqlCommand cmd_searchById = new SqlCommand("select * from ProductDetails where pId=@pId", con);
            cmd_searchById.Parameters.AddWithValue("@pId", pId);
            SqlDataReader readProduct = null;
            ProductDetailsModel pdm = new ProductDetailsModel();
            try
            {
                con.Open();
                readProduct = cmd_searchById.ExecuteReader();

                if (readProduct.Read())
                {
                    pdm.pId = Convert.ToInt32(readProduct[0]);
                    pdm.pName = readProduct[1].ToString();
                    pdm.pType = readProduct[2].ToString();
                    pdm.pPrice = (float)Convert.ToDouble(readProduct[3]);
                    pdm.pQty = Convert.ToInt32(readProduct[4]);
                    pdm.pIsInStock = Convert.ToBoolean(readProduct[5]);
                }
                else
                {
                    throw new Exception("Product Not Found");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readProduct.Close();
                con.Close();
            }
            return pdm;
        }
        #endregion

        #region Update Product
        public string UpdateProduct(ProductDetailsModel update)
        {

            SqlCommand cmd_updateProduct = new SqlCommand("update ProductDetails set pName=@pName,pType=@pType, pPrice=@pPrice, pQty= @qty, pIsInStock=@pIsInStock where pId =@pId", con);
            cmd_updateProduct.Parameters.AddWithValue("@pName", update.pName);
            cmd_updateProduct.Parameters.AddWithValue("@pType", update.pType);
            cmd_updateProduct.Parameters.AddWithValue("@pPrice", update.pPrice);
            cmd_updateProduct.Parameters.AddWithValue("@qty", update.pQty);
            cmd_updateProduct.Parameters.AddWithValue("@pIsInStock", update.pIsInStock);
            cmd_updateProduct.Parameters.AddWithValue("@pId", update.pId); //Still unsure why this comes at the end by convention...

            try
            {
                con.Open();
                cmd_updateProduct.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Updated Product Successfully";
        }
        #endregion

        #region Delete Product
        public string DeleteProduct(int pId)
        {
            SqlCommand cmd_deleteProduct = new SqlCommand("delete from ProductDetails where pId = @pId", con);
            cmd_deleteProduct.Parameters.AddWithValue("@pId", pId);
            try
            {
                con.Open();
                cmd_deleteProduct.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Product Deleted Successfully";
        }
        #endregion
    }
}
