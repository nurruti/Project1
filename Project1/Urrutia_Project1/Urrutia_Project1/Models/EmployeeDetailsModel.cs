using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Urrutia_Project1.Models
{
    public class EmployeeDetailsModel
    {
        #region EmployeeDetails Variables
        public int empId { get; set; }
        public string empFirstName { get; set; }
        public string empLastName { get; set; }
        public string empRole { get; set; }
        public int empAge { get; set; }
        public DateTime empHireDate { get; set; }
        public int empSalary { get; set; }
        public long empPhone { get; set; }
        public string cUsername { get; set; }
        public string cPassword { get; set; }

        SqlConnection con = new SqlConnection("server= DESKTOP-2LDPKO5\\NICHOLASINSTANCE; database= Project1; integrated security=true");
        #endregion

        #region Add Employee
        public string AddEmployee(EmployeeDetailsModel newEmployee)
        {
            SqlCommand cmd_addEmployee = new SqlCommand("insert into EmployeeDetails values (@empFirstName, @empLastName, @empRole, @empAge " +
                "@empHireDate, @empSalary, @empPhone, @cUsername, @cPassword)", con);

            //Debating if I want this here...
            empHireDate = DateTime.Now;

            cmd_addEmployee.Parameters.AddWithValue("@empFirstName", newEmployee.empFirstName);
            cmd_addEmployee.Parameters.AddWithValue("@empLastName", newEmployee.empLastName);
            cmd_addEmployee.Parameters.AddWithValue("@empRole", newEmployee.empRole);
            cmd_addEmployee.Parameters.AddWithValue("@empAge", newEmployee.empAge);
            cmd_addEmployee.Parameters.AddWithValue("@empHireDate", newEmployee.empHireDate);
            cmd_addEmployee.Parameters.AddWithValue("@empSalary", newEmployee.empSalary);
            cmd_addEmployee.Parameters.AddWithValue("@empPhone", newEmployee.empPhone);
            cmd_addEmployee.Parameters.AddWithValue("@cUsername", newEmployee.cUsername);
            cmd_addEmployee.Parameters.AddWithValue("@cPassword", newEmployee.cPassword);

            try
            {
                con.Open();
                cmd_addEmployee.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Employee" + empFirstName + " " + empLastName + "Added Successfully";
        }
        #endregion

        #region Read Employee
        public List<EmployeeDetailsModel> GetEmployeeList()
        {
            SqlCommand cmd_getEmployees = new SqlCommand("select * from EmployeeDetails", con);
            List<EmployeeDetailsModel> empList = new List<EmployeeDetailsModel>();
            SqlDataReader readAllEmployees = null;
            try
            {
                con.Open();
                readAllEmployees = cmd_getEmployees.ExecuteReader();

                while (readAllEmployees.Read())
                {
                    empList.Add(new EmployeeDetailsModel()
                    {
                        empId = Convert.ToInt32(readAllEmployees[0]),
                        empFirstName = readAllEmployees[1].ToString(),
                        empLastName = readAllEmployees[2].ToString(),
                        empRole = readAllEmployees[3].ToString(),
                        empAge = Convert.ToInt32(readAllEmployees[4]),
                        empHireDate = Convert.ToDateTime(readAllEmployees[5]),
                        empSalary = Convert.ToInt32(readAllEmployees[6]),
                        empPhone = Convert.ToInt64(readAllEmployees[7]),
                        cUsername = readAllEmployees[8].ToString(),
                        cPassword = readAllEmployees[9].ToString()
                    });
                }

            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readAllEmployees.Close();
                con.Close();
            }
            return empList;
        }
        #endregion

        #region Read Employee by ID
        public EmployeeDetailsModel GetEmployeeDetails(int empId)
        {

            SqlCommand cmd_searchById = new SqlCommand("select * from EmployeeDetails where empId=@empId", con);
            cmd_searchById.Parameters.AddWithValue("@empId", empId);
            SqlDataReader readEmployee = null;
            EmployeeDetailsModel empdm = new EmployeeDetailsModel();
            try
            {
                con.Open();
                readEmployee = cmd_searchById.ExecuteReader();

                if (readEmployee.Read())
                {
                    empdm.empId = Convert.ToInt32(readEmployee[0]);
                    empdm.empFirstName = readEmployee[1].ToString();
                    empdm.empLastName = readEmployee[2].ToString();
                    empdm.empRole = readEmployee[3].ToString();
                    empdm.empAge = Convert.ToInt32(readEmployee[4]);
                    empdm.empHireDate = Convert.ToDateTime(readEmployee[5]);
                    empdm.empSalary = Convert.ToInt32(readEmployee[6]);
                    empdm.empPhone = Convert.ToInt64(readEmployee[7]);
                    empdm.cUsername = readEmployee[8].ToString();
                    empdm.cPassword = readEmployee[9].ToString();
                }
                else
                {
                    throw new Exception("Employee Not Found");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                readEmployee.Close();
                con.Close();
            }
            return empdm;
        }
        #endregion

        #region Update Employee
        public string UpdateEmployee(EmployeeDetailsModel update)
        {
            /*empHireDate=@empHireDate,*/
            SqlCommand cmd_updateEmployee = new SqlCommand("update EmployeeDetails set empFirstName=@empFirstName,empLastName=@empLastName, empRole=@empRole, " +
                "empAge=@empAge, empSalary=@empSalary, empPhone= @empPhone, cUsername=@cUsername, cPassword=@cPassword where empId =@empId", con);
            cmd_updateEmployee.Parameters.AddWithValue("@empFirstName", update.empFirstName);
            cmd_updateEmployee.Parameters.AddWithValue("@empLastName", update.empLastName);
            cmd_updateEmployee.Parameters.AddWithValue("@empRole", update.empRole);
            cmd_updateEmployee.Parameters.AddWithValue("@empAge", update.empAge);
            //cmd_updateEmployee.Parameters.AddWithValue("@empHireDate", update.empHireDate);
            cmd_updateEmployee.Parameters.AddWithValue("@empSalary", update.empSalary);
            cmd_updateEmployee.Parameters.AddWithValue("@empPhone", update.empPhone);
            cmd_updateEmployee.Parameters.AddWithValue("@cUsername", update.cUsername);
            cmd_updateEmployee.Parameters.AddWithValue("@cPassword", update.cPassword);
            cmd_updateEmployee.Parameters.AddWithValue("@empId", update.empId);

            try
            {
                con.Open();
                cmd_updateEmployee.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Updated Employee Successfully";
        }
        #endregion

        #region Delete Employee
        public string DeleteEmployee(int empId)
        {
            SqlCommand cmd_deleteEmployee = new SqlCommand("delete from EmployeeDetails where empId = @empId", con);
            cmd_deleteEmployee.Parameters.AddWithValue("@empId", empId);
            try
            {
                con.Open();
                cmd_deleteEmployee.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return "Employee Deleted Successfully";
        }
        #endregion
    }
}