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
    /// Interaction logic for StaffView.xaml
    /// </summary>
    public partial class StaffView : Window
    {
        public StaffView()
        {
            InitializeComponent();
        }

        private void removeStaff_Click(object sender, RoutedEventArgs e)
        {
            var item = grdStaff.SelectedItem as Unilever.DTO.Entity.Staff;

            if (new StaffDAO().Remove(item.Id))
            {
                MessageBox.Show("Xóa dữ liệu thành công! ");
            }
            else
            {
                MessageBox.Show("Xóa không thành công.");
            }
            grdStaff.ItemsSource = new StaffDAO().GetAll();

        }

        private void grdStaff_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextStaff.ShowEditor();
        }

        private void contextStaff_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            grdStaff.ItemsSource = new StaffDAO().GetAll();
        }

        private void grdStaff_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdStaff.SelectedItem as Unilever.DTO.Entity.Staff;
            ChangeInput(item);
            btnUpdateMainStaff.Visibility = Visibility.Visible;
            btnUpdateUser.Visibility = Visibility.Visible;
        }

        private void LayoutGroup_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btnAddStaff_Click(object sender, RoutedEventArgs e)
        {
            AddStaffFrm frm = new AddStaffFrm();
            frm.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grdStaff.ItemsSource = new StaffDAO().GetAll();
        }

        private void btnUpdateMainStaff_Click(object sender, RoutedEventArgs e)
        {
            StaffDAO sd = new StaffDAO();
            DTO.Entity.Staff staff = new DTO.Entity.Staff
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtFullName.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                Permission = cbxPermission.SelectedIndex
            };
            if (new StaffDAO().UpdateInfo(staff))
            {
                grdStaff.ItemsSource = new StaffDAO().GetAll();
                MessageBox.Show("Cập nhật thông tin cá nhân thành công!!!");
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin cá nhân không thành công!!!");
            }

        }

        private void btnStaffRefresh_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Staff staff = new DTO.Entity.Staff();
            ChangeInput(staff);
            btnUpdateMainStaff.Visibility = Visibility.Hidden;
            btnUpdateUser.Visibility = Visibility.Hidden;
            btnStaffAdd.Visibility = Visibility.Visible;
        }

        private void ChangeInput(DTO.Entity.Staff staff)
        {
            txtId.Text = staff.Id.ToString();
            txtFullName.Text = staff.Name;
            txtEmail.Text = staff.Email;
            txtAddress.Text = staff.Address;

            if (staff.Permission.HasValue == false)
                cbxPermission.SelectedIndex = -1;
            else
                cbxPermission.SelectedIndex = staff.Permission.Value;

            txtUsername.Text = staff.Username;
        }


    }
}
