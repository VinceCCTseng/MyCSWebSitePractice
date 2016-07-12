using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MyWebSitePractice1;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Globalization;

//This is my first POCO Class for Customer
//namespace MyWebSitePractice1
//{
public class ourCustomer : INotifyPropertyChanged
    {
        // There are several item to input and check

        private int _customerId;
        private string _firstName; // (25)
        private string _lastName;  // (25)
        private string _email;  // (50)
        private string _website; // (200)
        private string _dOB; // Date
        private string _memberCard; // (20) not NULL
        private string _loyaltyMember; // true and false
        private string _phone; //(10)
        private string _mobile; //(10)
        private string _fax; //(10)
        
        public event PropertyChangedEventHandler PropertyChanged;

        //constructure - only for new customer
        public ourCustomer()
        {
            _customerId = 65535;
            _firstName = "default";
            _lastName = "default";
            _email = "default@default.default";
            _website = "default.default.default";
            _dOB = "01-01-1901";
            _memberCard = "default";
            _loyaltyMember = "false";
            _phone = "0000000000";
            _mobile = "0000000000";
            _fax = "0000000000";
        }

        //constructure - for exists customers
        public ourCustomer(int customerid)
        {
            _customerId = customerid;
        }

        // CustomerID is auto increment
        public int CustomerId
        {
            get
            {
                return _customerId;
            }            
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 25)
                {
                    _firstName = value;
                    
                }
                else
                    throw new ApplicationException("The length of [FirstName] is invalid!");
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 25)
                    _lastName = value;
                else
                    throw new ApplicationException("The length of [LastName] is invalid!");

            }

        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 25)
                    _email = value;
                else
                    throw new ApplicationException("The length of [Email] is invalid!");

            }
        }

        public string DOB
        {
            get
            {
                return _dOB;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _dOB = value;
                else
                    throw new ApplicationException("The [DOB] is invalid!");

            }
        }

        public string MemberCard
        {
            get
            {
                return _memberCard;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 20)
                    _memberCard = value;
                else
                    throw new ApplicationException("The length of [MemberCard] is invalid!");

            }

        }

        public string LoyaltyMember
        {
            get
            {
                return _loyaltyMember;
            }
            set
            {
                _loyaltyMember = value;
            }
        }

        //private string phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 10)
                    _phone = value;
                else
                    throw new ApplicationException("The length of [Phone] is invalid!");

            }
        }
        //private string mobile;
        public string Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 10)
                    _mobile = value;
                else
                    throw new ApplicationException("The length of [Mobile] is invalid!");

            }
        }
        //fax;
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 10)
                    _fax = value;
                else
                    throw new ApplicationException("The length of [Fax] is invalid!");

            }
        }

        //website
        public string Website
        {
            get
            {
                return _website;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length <= 200)
                    _website = value;
                else
                    throw new ApplicationException("The length of [Website] is invalid!");

            }
        }

        // Save a new customer card into database
        public Boolean SaveCustomer()
        {
            Boolean successFlag = false;
            sqlAccess sqlconnfornewcustomer = new sqlAccess();
            successFlag = sqlconnfornewcustomer.SaveRecord(this._firstName, this._lastName, this._email, this._website, this._dOB, this._memberCard, this._loyaltyMember, this._phone, this._mobile, this._fax);
            return successFlag;
        }

        // Update customer card 
        public Boolean SaveCustomer(ourCustomer newinfo)
        {
            Boolean successFlag = false;
            // Divide two different entry, first one for insert new one; second for update.
            sqlAccess sqlconnforcustomerupdate = new sqlAccess();

            // 1. FirstName check
            if (this._firstName.CompareTo(newinfo._firstName) != 0)
            {
                this._firstName = newinfo._firstName;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.UFIRSTNAME, this._firstName);
            }

            // 2. LastName check
            if (this._lastName.CompareTo(newinfo._lastName) != 0)
            {
                this._lastName = newinfo._lastName;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.ULASTNAME, this._lastName);
            }

            // 3. Email check
            if (this._email.CompareTo(newinfo._email) != 0)
            {
                this._email = newinfo._email;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.UEMAIL, this._email);
            }

            // 4. Website check
            if (this._website.CompareTo(newinfo._website) != 0)
            {
                this._website = newinfo._website;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.UWEBSITE, this._website);
            }

            // 5.DOB check 
            if (this._dOB.CompareTo(newinfo._dOB) != 0)
            {
                this._dOB = newinfo._dOB;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.UDOB, this._dOB);
            }

            // 6. Loyaltymember check
            if (this._loyaltyMember.CompareTo(newinfo._loyaltyMember) != 0)
            {
                this._loyaltyMember = newinfo._loyaltyMember;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.ULOYALTYMEMBER, this._loyaltyMember);
            }

            // 7. phone check
            if (this._phone.CompareTo(newinfo._phone) != 0)
            {
                this._phone = newinfo._phone;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.UPHONE, this._phone);
            }

            // 8. moblie check
            if (this._mobile.CompareTo(newinfo._mobile) != 0)
            {
                this._mobile = newinfo._mobile;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.UMOBILE, this._mobile);
            }

            // 9. fax check
            if (this._fax.CompareTo(newinfo._fax) != 0)
            {
                this._fax = newinfo._fax;
                successFlag = sqlconnforcustomerupdate.SaveRecord(this._customerId, sqlAccess.UFAX, this._fax);
            }
            return successFlag;
        }


    }

    public class ourCustomerList:ourCustomer
    {
        private BindingList<ourCustomer> ourcustomerlist;
        public ourCustomerList()
        {
            ourcustomerlist = new BindingList<ourCustomer>();
        }
        // 1. add a new customer into list
        public Boolean Addcustomer(ourCustomer acustomer)
        {
            ourcustomerlist.Add(acustomer);
            return true;                        
        }

        // 2. get a customer from list through index
        public ourCustomer Getacustomer(int index)
        {
            return ourcustomerlist[index];
        }
        
        // 3. get counter
        public int Getlistcount()
        {
            return ourcustomerlist.Count();
        }

        public BindingList<ourCustomer> Getcustomerlist()
        {
            return ourcustomerlist;
        }

        //
        // ?. remove
        //public Boolean Addcustomer(ourCustomer acustomer)
        //{
        //    ourcustomerlist.Add(acustomer);
        //    return true;
        //}

    }

    public class customerTools
    { 
        public bool customerDataValidation(int itemid, string item)
        {
            bool checkstatus = false;
            switch(itemid)
            {
                case sqlAccess.UFIRSTNAME:
                case sqlAccess.ULASTNAME:
                    if (item.Length > 0 && item.Length <= 25)
                        checkstatus = true;
                    break;
                case sqlAccess.UEMAIL:
                    try
                    {
                        checkstatus = Regex.IsMatch(item,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    }
                    catch (RegexMatchTimeoutException)
                    {
                        checkstatus = false;
                    }
                    break;
                case sqlAccess.UWEBSITE:
                    if (item.Length > 3 && item.Length <= 200)
                    checkstatus = true;
                    break;
                case sqlAccess.UDOB:
                    DateTime d;
                    checkstatus = DateTime.TryParseExact(item, "dd-MM-yyyy", null, DateTimeStyles.None, out d);                       
                    break;
                case sqlAccess.UPHONE:
                case sqlAccess.UMOBILE:
                case sqlAccess.UFAX:
                    if (item.Length == 10)
                        checkstatus = true;
                    break;

            }
            return checkstatus;
        }
}  
//}


