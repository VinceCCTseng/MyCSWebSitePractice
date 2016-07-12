using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

//namespace MyWebSitePractice1
//{
public class sqlAccess
    {
        // Create variable for this class
        private SqlConnection conn;
        public const int UFIRSTNAME = 1, ULASTNAME = 2, UEMAIL = 3, UWEBSITE = 4, UDOB = 5, UMEMBERCARD = 6, ULOYALTYMEMBER = 7, UPHONE = 8, UMOBILE = 9, UFAX = 10;
        public const string MEMBERCARD = "membercard", AUTHORITY = "authority";
        public const string CUSTOMERID = "customerid", FRISTNAME = "firstname", LASTNAME = "lastname", EMAIL = "email", WEBSITE = "website", DOB = "dob", LOYALTYMEMBER = "loyaltymember", PHONE = "phone", MOBILE = "mobile", FAX = "fax";
        public const string P_ID = "product_Id", P_SERIALNUMBER = "product_SerialNumber", P_NAME = "product_Name", P_PRICE = "product_Price", P_IMAGELINK = "product_Imagelink", P_CATEGORY = "product_Category", P_COLOR = "product_color", P_COMMENT = "product_comment";

        // Constructor - to connect database once using salAccess class 
        public sqlAccess()
        {
        //conn = new SqlConnection(@"Data Source=VINCE-PC\SQLEXPRESS;Initial Catalog=customerdatabase;Integrated Security=True;MultipleActiveResultSets=True");  
        //localhost
        conn = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=customerdatabase;Integrated Security=True;MultipleActiveResultSets=True");
        conn.Open();
        }
        public sqlAccess(string webconfig)
        {
            conn = new SqlConnection(webconfig);
            conn.Open();
        }

    // Destructors
        public void sqldisconnect()
        {
            conn.Close();
        }

        // Part I - Sql connect to useraccount
        // I-1. Login checker (user, password)
        // I-2. IsUserExist (membercard) 
        // I-3. CreateUserAccount (username, userpassword, authority, membercard)
        // I-4. DeleteAccount (membercard)

    // I-1. Login checker - check the user and password and return the result
    public string LoginCheck(string user, string password)
        {
            var sqllogin = "SELECT * FROM useraccount WHERE username = @username and password = @password";
            using (var cmd = new SqlCommand(sqllogin, conn))
            {
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@password", password);
               
                SqlDataReader sqlreader = cmd.ExecuteReader();
                sqlreader.Read();
                
                if (sqlreader.HasRows  && sqlreader[AUTHORITY].ToString().CompareTo("True") == 0 ? true : false)
                    return "2." + sqlreader[MEMBERCARD].ToString();
                else if (sqlreader.HasRows)
                    return "1." + sqlreader[MEMBERCARD].ToString();
                else
                    return "0";                
            }

        }
        // I-2. IsUserExist (membercard) 
        public Boolean IsUserExist(string membercard)
        {
            var sqlusercheck = "SELECT * FROM useraccount WHERE membercard = @membercard";
            using (var cmd = new SqlCommand(sqlusercheck, conn))
            {
                cmd.Parameters.AddWithValue("@membercard", membercard);

                SqlDataReader sqlreader = cmd.ExecuteReader();
                sqlreader.Read();
                if (sqlreader.HasRows)
                    return true;
                else
                    return false;
            }
        }
        // I-3. CreateUserAccount (username, userpassword, authority, membercard)
        public Boolean CreateUserAccount(string username, string userpassword, Boolean authority, string membercard)
        {
            string authmode = (authority == true) ?"True":"False";
            var sqlinsertuser = "INSERT INTO useraccount VALUES (@_username, @_userpassword, @_authority, @_membercard)";
            try
            {
                using (var cmd = new SqlCommand(sqlinsertuser, conn))
                {
                    cmd.Parameters.AddWithValue("@_username", username);
                    cmd.Parameters.AddWithValue("@_userpassword", userpassword);
                    cmd.Parameters.AddWithValue("@_authority", authmode);
                    cmd.Parameters.AddWithValue("@_membercard", membercard);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("[Error] Cannot insert into SQL server! ==>"+ex);
            }
            
        }
        // I-4. DeleteAccount (membercard)
        public Boolean DeleteAccount(string membercard)
        {
            var sqlupdatecustomer = "DELETE FROM useraccount WHERE membercard = @_membercard;";
            try
            {
                using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                {
                    cmd.Parameters.AddWithValue("@_membercard", membercard);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }      
            catch
            {
                throw new ApplicationException("SQL connection failed on"+ this);
            }
        }

        // Part II - SQL connect to Customer
        // II-1a. RetrievalCustomerList - to get the customer list and return it without sort condition
        // II-1b. RetrievalCustomerList (sortItem, desc) - to get the customer list and return it with sort condition
        // II-2a. SaveRecord (FirstName, LastName, Email, Website, DOB, MemberCard, LoyaltyMember, Phone, Mobile, Fax) - to insert whole a new customer card 
        // II-2b. SaveRecord (customerid, updateitemid, itemdetail) - to update customer card 
        // II-3 DeleteRecord (customerid) - to delete function
        // II-4 SearchRecord (updateitem, itemdetail) - to search record by condition and returen the result

        // II-1a. RetrievalCustomerList - to get the customer list and return it without sort condition
        public ourCustomerList RetrievalCustomerList()
        {

            ourCustomerList customerlist = new ourCustomerList();

            var sqlgetcustomerlist = "SELECT * FROM CUSTOMER";
            SqlDataReader customerreader;
            using (var cmd = new SqlCommand(sqlgetcustomerlist, conn))
            {
                customerreader = cmd.ExecuteReader();
                if (customerreader.HasRows)
                {
                    while (customerreader.Read())
                    {
                        DateTime dt = DateTime.Parse(customerreader[DOB].ToString());
                        ourCustomer acustomer = new ourCustomer(int.Parse(customerreader[CUSTOMERID].ToString()));
                        acustomer.FirstName = customerreader[FRISTNAME].ToString();
                        acustomer.LastName = customerreader[LASTNAME].ToString();
                        acustomer.Email = customerreader[EMAIL].ToString();
                        acustomer.Website = customerreader[WEBSITE].ToString();
                        acustomer.DOB = dt.ToString("dd") + "-" + dt.ToString("MM") + "-" + dt.ToString("yyyy");
                        acustomer.LoyaltyMember = (customerreader[LOYALTYMEMBER].ToString().CompareTo("True") == 0) ? "true":"false";
                        acustomer.Phone = customerreader[PHONE].ToString();
                        acustomer.Mobile = customerreader[MOBILE].ToString();
                        acustomer.Fax = customerreader[FAX].ToString();
                        acustomer.MemberCard = customerreader[MEMBERCARD].ToString();
                           
                        customerlist.Addcustomer(acustomer);                     
                    }
                    return customerlist;
                }
                else
                    throw new ApplicationException("[Error] Cannot retrieve the data!");
            }

        }

        // II-1b. RetrievalCustomerList (sortItem, desc) - to get the customer list and return it with sort condition
        public ourCustomerList RetrievalCustomerList(string sortItem, Boolean desc)
        {//SELECT * FROM CUSTOMER ORDER BY firstname DESC;

            ourCustomerList customerlist = new ourCustomerList();
            string storMode = (desc == true) ? "DESC" : "ASC";
            var sqlgetcustomerlist = "SELECT * FROM CUSTOMER ORDER BY "+ sortItem +" "+ storMode;
            SqlDataReader customerreader;
            using (var cmd = new SqlCommand(sqlgetcustomerlist, conn))
            {
                customerreader = cmd.ExecuteReader();
                if (customerreader.HasRows)
                {
                    while (customerreader.Read())
                    {
                        DateTime dt = DateTime.Parse(customerreader[DOB].ToString());
                        ourCustomer acustomer = new ourCustomer(int.Parse(customerreader[CUSTOMERID].ToString()));
                        acustomer.FirstName = customerreader[FRISTNAME].ToString();
                        acustomer.LastName = customerreader[LASTNAME].ToString();
                        acustomer.Email = customerreader[EMAIL].ToString();
                        acustomer.Website = customerreader[WEBSITE].ToString();
                        acustomer.DOB = dt.ToString("dd") + "-" + dt.ToString("MM") + "-" + dt.ToString("yyyy");
                        acustomer.LoyaltyMember = (customerreader[LOYALTYMEMBER].ToString().CompareTo("True") == 0) ? "true" : "false";
                        acustomer.Phone = customerreader[PHONE].ToString();
                        acustomer.Mobile = customerreader[MOBILE].ToString();
                        acustomer.Fax = customerreader[FAX].ToString();
                        acustomer.MemberCard = customerreader[MEMBERCARD].ToString();

                        customerlist.Addcustomer(acustomer);
                    }
                    return customerlist;
                }
                else
                    throw new ApplicationException("[Error] Cannot retrieve the data!");
            }

        }

        // II-2a. SaveRecord(FirstName, LastName, Email, Website, DOB, MemberCard, LoyaltyMember, Phone, Mobile, Fax) - to insert whole a new customer card 
        public Boolean SaveRecord(string FirstName, string LastName, string Email, string Website, string DOB, string MemberCard, string LoyaltyMember, string Phone, string Mobile, string Fax)
        {
                var sqlinsertcustomer = "INSERT INTO customer VALUES (@_firstName, @_lastName, @_email, @_website, @_dob, @_membercard, @_loyaltymember, @_phone, @_mobile, @_fax)";
            try
            {
                using (var cmd = new SqlCommand(sqlinsertcustomer, conn))
                {
                    cmd.Parameters.AddWithValue("@_firstName", FirstName);
                    cmd.Parameters.AddWithValue("@_lastname", LastName);
                    cmd.Parameters.AddWithValue("@_email", Email);
                    cmd.Parameters.AddWithValue("@_website", Website);
                    cmd.Parameters.AddWithValue("@_dob", DOB);
                    cmd.Parameters.AddWithValue("@_membercard", MemberCard);
                    cmd.Parameters.AddWithValue("@_loyaltymember", ((LoyaltyMember.CompareTo("true") == 0) ? 1 : 0));
                    cmd.Parameters.AddWithValue("@_phone", Phone);
                    cmd.Parameters.AddWithValue("@_mobile", Mobile);
                    cmd.Parameters.AddWithValue("@_fax", Fax);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("[Error] Cannot retrieve the data!"+ex);                
            }

        }

        // II-2b. SaveRecord (customerid, updateitemid, itemdetail) - to update customer card 
        public Boolean SaveRecord(int customerid, int updateitemid, string itemdetail)
        {

            var sqlupdatecustomer = "";
            switch (updateitemid)
            {
                case UFIRSTNAME:
                    sqlupdatecustomer = "UPDATE customer SET firstname = @_FirstName WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_FirstName", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case ULASTNAME:
                    sqlupdatecustomer = "UPDATE customer SET lastname = @_LastName WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_LastName", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case UEMAIL:
                    sqlupdatecustomer = "UPDATE customer SET email = @_Email WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_Email", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case UWEBSITE:
                    sqlupdatecustomer = "UPDATE customer SET website = @_Website WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_Website", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case UDOB:
                //sqlupdatecustomer = "UPDATE customer SET dob = @_Dob WHERE customerid=@_Customerid;";
                sqlupdatecustomer = "UPDATE customer SET dob = CONVERT( DATETIME , @_Dob ,105 ) WHERE customerid=@_Customerid;";
                using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {                        
                        cmd.Parameters.AddWithValue("@_Dob", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case ULOYALTYMEMBER:
                    sqlupdatecustomer = "UPDATE customer SET loyaltymember = @_LoyaltyMember WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_LoyaltyMember", (itemdetail.CompareTo("true") == 0 ? 1 : 0));
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case UPHONE:
                    sqlupdatecustomer = "UPDATE customer SET phone = @_Phone WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_Phone", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case UMOBILE:
                    sqlupdatecustomer = "UPDATE customer SET mobile = @_mobile WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_mobile", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
                case UFAX:
                    sqlupdatecustomer = "UPDATE customer SET fax = @_Fax WHERE customerid=@_Customerid;";
                    using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
                    {
                        cmd.Parameters.AddWithValue("@_Fax", itemdetail);
                        cmd.Parameters.AddWithValue("@_Customerid", customerid);
                        cmd.ExecuteNonQuery();
                    }
                    break;
            }
              
            return true;
        }

        // II-3 DeleteRecord (customerid) - to delete function
        public Boolean DeleteRecord(int customerid)
        {            
            var sqlupdatecustomer = "DELETE FROM customer WHERE customerid = @_Customerid;";
            using (var cmd = new SqlCommand(sqlupdatecustomer, conn))
            {
                cmd.Parameters.AddWithValue("@_Customerid", customerid);
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        // II-4 SearchRecord (updateitem, itemdetail) - to search record by condition and returen the result
        public ourCustomerList SearchRecord(string updateitem, string itemdetail)
        {

            ourCustomerList searchresult = new ourCustomerList();
            SqlDataReader sqlsearchresult;
            var sqlsearchcustomer = "SELECT * FROM customer WHERE " + updateitem.ToLower() + " = @_Detail";
            try
            {
                using (var cmd = new SqlCommand(sqlsearchcustomer, conn))
                {
                    cmd.Parameters.AddWithValue("@_Detail", itemdetail);
                    sqlsearchresult = cmd.ExecuteReader();
                    if (sqlsearchresult.HasRows)
                    {
                        while (sqlsearchresult.Read())
                        {
                            DateTime dt = DateTime.Parse(sqlsearchresult[DOB].ToString());
                            
                            ourCustomer acustomer = new ourCustomer(int.Parse(sqlsearchresult[CUSTOMERID].ToString()));
                            acustomer.FirstName = sqlsearchresult[FRISTNAME].ToString();
                            acustomer.LastName = sqlsearchresult[LASTNAME].ToString();
                            acustomer.Email = sqlsearchresult[EMAIL].ToString();
                            acustomer.Website = sqlsearchresult[WEBSITE].ToString();
                            acustomer.DOB = dt.ToString("dd") + "-" + dt.ToString("MM") + "-" + dt.ToString("yyyy");
                            acustomer.LoyaltyMember = (sqlsearchresult[LOYALTYMEMBER].ToString().CompareTo("True") == 0) ? "true" : "false";
                            acustomer.Phone = sqlsearchresult[PHONE].ToString();
                            acustomer.Mobile = sqlsearchresult[MOBILE].ToString();
                            acustomer.Fax = sqlsearchresult[FAX].ToString();
                            acustomer.MemberCard = sqlsearchresult[MEMBERCARD].ToString();
                            //
                            searchresult.Addcustomer(acustomer);
                        }
                    }
                }
                if (searchresult != null)
                    return searchresult;
                else
                {
                    throw new ApplicationException("[Error] Cannot retrieve the data!");                    
                }
            }
            catch
            {
                throw new ApplicationException("[Error] Cannot retrieve the SQL server!");
            }


        }

        // Part III - Sql connect with product
        //III-1. Get list 
        public ourProductlist RetrievalProductList()
        {
            ourProductlist productList = new ourProductlist();

            var sqlgetcustomerlist = "SELECT * FROM product";
            SqlDataReader productReader;

            try
            {
                using (var cmd = new SqlCommand(sqlgetcustomerlist, conn))
                {
                    productReader = cmd.ExecuteReader();
                    if (productReader.HasRows)
                    {
                        while (productReader.Read())
                        {
                            ourProduct aProduct = new ourProduct(int.Parse(productReader[P_ID].ToString()));
                            aProduct.SerialNumber = productReader[P_SERIALNUMBER].ToString();
                            aProduct.Name = productReader[P_NAME].ToString();
                            aProduct.Price = double.Parse(productReader[P_PRICE].ToString());
                            aProduct.Imagelink = productReader[P_IMAGELINK].ToString();
                            aProduct.Category = productReader[P_CATEGORY].ToString();
                            aProduct.Color = productReader[P_COLOR].ToString();
                            aProduct.Comment = productReader[P_COMMENT].ToString();
                            productList.addProduct(aProduct);
                        }
                        return productList;
                    }
                    else
                    { 
                        throw new ApplicationException("[Error] Cannot retrieve the data!");
                    }
                }
            }
            catch
            {
                return productList;
                // TODO: SQL issue
            }
        }
        //III-2. Get product
        //III-3. Update
        //III-4. Insert 
        //III-5. Delete 
        //III-6. Search 

    }
//}