using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unilever.DAO;

namespace Unilever.Views.Staff
{
    /// <summary>
    /// Interaction logic for AddStaffFrm.xaml
    /// </summary>
    public partial class AddStaffFrm : Window
    {
        public AddStaffFrm()
        {
            InitializeComponent();
        }

        private void btnAddMainStaff_Click(object sender, RoutedEventArgs e)
        {
            StaffDAO sd = new StaffDAO();
            DTO.Entity.Staff staff = new DTO.Entity.Staff
            {
                Name = txtFullName.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Password,
                Permission = cbxPermission.SelectedIndex
            };

            bool flag = sd.Add(staff);

            if (flag == true)
            {
                MessageBox.Show("Thêm Nhân viên thành công.");
            }
            else
            {
                MessageBox.Show("Thêm Nhân viên thất bại.");
            }
        }

        private void btnCancelAddStaff_Click(object sender, RoutedEventArgs e)
        {
            txtFullName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Password = string.Empty;
            cbxPermission.SelectedIndex = -1;
        }
    }
}
