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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unilever.DAO;
using Unilever.DTO.Entity;

namespace Unilever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDelete_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            pnDanhSach.Closed = false;
            pnAddStaff.Closed = true;
            StaffDAO sd = new StaffDAO();
            gridListStaff.ItemsSource = sd.GetAll().Select(s => new { s.Name, s.Username, s.Email, s.Address }).ToList();
        }

        private void btnViewDist_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            pnAddStaff.Closed = true;
            pnDanhSach.Closed = false;
            DistributorDAO dd = new DistributorDAO();
            gridListStaff.ItemsSource = dd.GetAll().Select(d => new { d.Name, d.Address, d.Email, d.Phone, d.TimeLimit });
        }

        private void btnAddStaff_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            pnDanhSach.Closed = true;
            pnUpdateStaff.Closed = true;
            pnAddStaff.Closed = false;
        }

        private void btnAddMainStaff_Click(object sender, RoutedEventArgs e)
        {
            StaffDAO sd = new StaffDAO();
            Staff staff = new Staff
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
            txtEmail.Text= string.Empty;
            txtUsername.Text= string.Empty;
            txtPassword.Password= string.Empty;
            cbxPermission.SelectedIndex = -1;
        }
    }
}
