using QLBH_MVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Seller.DTO.Entity;

namespace Seller.DAO
{
    class StaffDAO
    {
        public List<Staff> GetAll()
        {
            using (SellerEntities entity = new SellerEntities())
            {
                return entity.Staffs.ToList();
            }
        }

        public Staff GetByUsername(string username, string password)
        {
            string encPassword = MD5Encrypt.Encrypt(password);
            using (SellerEntities entity = new SellerEntities())
            {
                return entity.Staffs.Where(s => s.Username == username && s.Password == encPassword).FirstOrDefault();
            }
        }

        public bool Add(Staff staff)
        {
            using (SellerEntities entity = new SellerEntities())
            {
                staff.Password = MD5Encrypt.Encrypt(staff.Password);
                entity.Staffs.Add(staff);
                entity.SaveChanges();

                return true;
            }
        }

        public bool Remove(int id)
        {
            bool flag = true;

            using (SellerEntities entity = new SellerEntities())
            {
                var staff = entity.Staffs.Where(c => c.Id == id).FirstOrDefault();
                if (staff != null)
                {
                    if (staff.Permission != 0)
                    {
                        flag = false;
                    }
                    else
                    {
                        entity.Staffs.Remove(staff);
                        entity.SaveChanges();
                    }
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public bool IsExistAccount(String username)
        {
            using (SellerEntities entity = new SellerEntities())
            {
                if (entity.Staffs.Where(c => c.Username.Equals(username)).Any())
                {
                    return true;
                }
            }

            return false;
        }

        public bool Login(String username, String password)
        {
            using (SellerEntities entity = new SellerEntities())
            {
                String encryptedPwd = MD5Encrypt.Encrypt(password);
                if (entity.Staffs.Where(c => c.Username.Equals(username) && c.Password.Equals(encryptedPwd)).Any())
                {
                    return true;
                }
            }

            return false;
        }

        public bool UpdateInfo(Staff staff)
        {
            bool flag = true;

            using (SellerEntities entity = new SellerEntities())
            {
                var staffData = entity.Staffs.Where(c => c.Id == staff.Id).FirstOrDefault();
                if (staffData != null)
                {
                    staffData.Name = staff.Name;
                    staffData.Address = staff.Address;
                    staffData.Email = staff.Email;
                    staffData.Permission = staff.Permission;
                    entity.SaveChanges();
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public bool UpdatePassword(String userName, String oldPwd, String newPwd)
        {
            String encNewPwd = MD5Encrypt.Encrypt(newPwd);
            String encOldPwd = MD5Encrypt.Encrypt(oldPwd);
            bool flag = true;

            using (SellerEntities entity = new SellerEntities())
            {
                var acc = entity.Staffs.Where(c => c.Username == userName && c.Password == encOldPwd).FirstOrDefault();
                if (acc != null)
                {
                    acc.Password = encNewPwd;
                    entity.SaveChanges();
                }
                else
                {
                    flag = false;
                }
            }

            return flag;
        }
    }
}
