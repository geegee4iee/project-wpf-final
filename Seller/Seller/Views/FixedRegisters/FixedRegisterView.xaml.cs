using DevExpress.Xpf.Core;
using Seller.DAO;
using Seller.DTO.Entity;
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

namespace Seller.Views.FixedRegisters
{
    /// <summary>
    /// Interaction logic for FixedRegisterView.xaml
    /// </summary>
    public partial class FixedRegisterView : Window
    {
        public FixedRegisterView()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Product product = null;
            Seller.DTO.Entity.Customer distributor = null;
            FixedRegister reg = null;
            try
            {
                product = cbxProducts.SelectedItem as Product;
                distributor = cbxDistributors.SelectedItem as Seller.DTO.Entity.Customer;

                reg = new FixedRegister
                {
                    CusId = distributor.Id,
                    ProId = product.Id,
                    Quantity = int.Parse(txtQuantity.Text)
                };
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Đăng ký không thành công!");
                return;
            }

            if (!new FixedRegisterDAO().Add(reg) == true)
            {
                DXMessageBox.Show("Khách hàng này đã đăng ký sản phẩm này!");
            }
            else
            {

            }

            RefreshGridFixedRegister();
        }

        private void RefreshGridFixedRegister()
        {
            grdFixedRegisters.ItemsSource = null;
            grdFixedRegisters.ItemsSource = new FixedRegisterDAO().GetAll();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = cbxProducts.SelectedItem as Product;
                var distributor = cbxDistributors.SelectedItem as Seller.DTO.Entity.Customer;
                int quantity = int.Parse(txtQuantity.Text);

                new FixedRegisterDAO().Update(distributor.Id, product.Id, quantity);

                RefreshEditFixedRegister();
                RefreshGridFixedRegister();
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Cập nhật đăng ký cố định không thành công!");
            }     
        }

        private void RefreshEditFixedRegister()
        {
            cbxProducts.SelectedIndex = 0;
            cbxDistributors.SelectedIndex = 0;
            txtQuantity.Text = "1";
            btnUpdate.IsEnabled = false;
            btnRegister.IsEnabled = true;
            cbxDistributors.IsEnabled = true;
            cbxProducts.IsEnabled = true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshEditFixedRegister();
        }

        private void grdFixedRegisters_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblFixedRegister.ShowEditor();
            RefreshEditFixedRegister();
        }

        private void grdFixedRegisters_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var lstProducts = new ProductDao().GetAll();
                var lstDistributors = new CustomerDAO().GetAll();
                var reg = grdFixedRegisters.SelectedItem as FixedRegister;

                cbxProducts.ItemsSource = lstProducts;
                cbxDistributors.ItemsSource = lstDistributors;
                cbxProducts.SelectedItem = lstProducts.Where(c => c.Id == reg.ProId).FirstOrDefault();
                cbxDistributors.SelectedItem = lstDistributors.Where(c => c.Id == reg.CusId).FirstOrDefault();

                txtQuantity.Text = reg.Quantity.ToString();
                btnUpdate.IsEnabled = true;
                btnRegister.IsEnabled = false;
                cbxDistributors.IsEnabled = false;
                cbxProducts.IsEnabled = false;
            }
            catch (System.Exception ex)
            {

            }
        }

        private void removeFixedRegister_Loaded(object sender, RoutedEventArgs e)
        {
            FixedRegister reg = null;
            try
            {
                reg = grdFixedRegisters.SelectedItem as FixedRegister;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (reg == null)
            {
                removeFixedRegister.IsEnabled = false;
            }
            else
            {
                removeFixedRegister.IsEnabled = true;
            }       
        }

        private void removeFixedRegister_Click(object sender, RoutedEventArgs e)
        {
            FixedRegister reg = null;
            try
            {
                reg = grdFixedRegisters.SelectedItem as FixedRegister;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (reg == null)
            {
                return;
            }

            MessageBoxResult result = DXMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Đăng ký cố định", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                new FixedRegisterDAO().Remove(reg.CusId, reg.ProId);
                RefreshGridFixedRegister();
            }
            else
            {

            }
        }
    }
}
