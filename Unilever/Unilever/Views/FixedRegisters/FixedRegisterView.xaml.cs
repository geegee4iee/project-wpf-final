using DevExpress.Xpf.Core;
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
using Unilever.DTO.Entity;

namespace Unilever.Views.FixedRegisters
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
            var product = cbxProducts.SelectedItem as Product;
            var distributor = cbxDistributors.SelectedItem as Unilever.DTO.Entity.Distributor;

            FixedRegister reg = new FixedRegister
            {
                DistributorId = distributor.Id,
                ProId = product.Id,
                Quantity = int.Parse(txtQuantity.Text)
            };

            if (!new FixedRegisterDAO().Add(reg) == true)
            {
                DXMessageBox.Show("Nhà phân phối này đã đăng ký sản phẩm này!");
            }

            RefreshGridFixedRegister();
        }

        private void RefreshGridFixedRegister()
        {
            grdFixedRegisters.ItemsSource = null;
            grdFixedRegisters.ItemsSource = new FixedRegisterDAO().GetAll();
        }

        private void grdFixedRegisters_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var lstProducts = new ProductDao().GetAll();
                var lstDistributors = new DistributorDAO().GetAll();
                var reg = grdFixedRegisters.SelectedItem as FixedRegister;

                cbxProducts.ItemsSource = lstProducts;
                cbxDistributors.ItemsSource = lstDistributors;
                cbxProducts.SelectedItem = lstProducts.Where(c => c.Id == reg.ProId).FirstOrDefault();
                cbxDistributors.SelectedItem = lstDistributors.Where(c => c.Id == reg.DistributorId).FirstOrDefault();

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

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshEditFixedRegister();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var product = cbxProducts.SelectedItem as Product;
            var distributor = cbxDistributors.SelectedItem as DTO.Entity.Distributor;
            int quantity = int.Parse(txtQuantity.Text);

            new FixedRegisterDAO().Update(distributor.Id, product.Id, quantity);
            RefreshEditFixedRegister();
            RefreshGridFixedRegister();
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
                new FixedRegisterDAO().Remove(reg.DistributorId, reg.ProId);
                RefreshGridFixedRegister();
            }
            else
            {

            }

        }

        private void grdFixedRegisters_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblFixedRegister.ShowEditor();
            RefreshEditFixedRegister();
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

        //private void grdFixedRegisters_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var item = grdFixedRegisters.SelectedItem as DTO.Entity.FixedRegister;

        //    if (item == null)
        //    {
        //        return;
        //    }
        //}
    }
}
