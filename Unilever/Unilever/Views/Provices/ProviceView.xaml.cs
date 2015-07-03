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

namespace Unilever.Views.Provices
{
    /// <summary>
    /// Interaction logic for ProviceView.xaml
    /// </summary>
    public partial class ProviceView : Window
    {
        public ProviceView()
        {
            InitializeComponent();

            txtId.Text = "Mặc định";
            btnUpdate.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Province item = new DTO.Entity.Province();
            if (string.IsNullOrEmpty(txtName.Text))
            {
                DXMessageBox.Show("Vui lòng nhập tên tỉnh!");
                return;
            }
            else
            {
                item.ProvinceName = txtName.Text;
            }

            if(new ProvinceDAO().Add(item))
            {
                DXMessageBox.Show("Thêm tỉnh thành thành công!");
            }
            else
            {
                DXMessageBox.Show("Thêm tỉnh thành không thành công!");
            }

            grdProvince.ItemsSource = new ProvinceDAO().GetAll();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Province item = new DTO.Entity.Province();
            item.Id = int.Parse(txtId.Text);

            if (string.IsNullOrEmpty(txtName.Text))
            {
                DXMessageBox.Show("Vui lòng nhập tên tỉnh!");
                return;
            }
            else
            {
                item.ProvinceName = txtName.Text;
            }

            if(new ProvinceDAO().Update(item))
            {
                DXMessageBox.Show("Cập nhật tỉnh thành thành công!");
            }
            else
            {
                DXMessageBox.Show("Cập nhật tỉnh thành không thành công!");
            }

            grdProvince.ItemsSource = new ProvinceDAO().GetAll();
        }

        private void ChangeInput(DTO.Entity.Province item)
        {
            txtId.Text = item.Id.ToString();
            txtName.Text = item.ProvinceName;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Province item = new DTO.Entity.Province();
            ChangeInput(item);

            txtId.Text = "Mặc định";
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
        }

        private void grdProvince_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdProvince.SelectedItem as DTO.Entity.Province;

            if(item == null)
            {
                return;
            }

            btnAdd.IsEnabled = false;
            btnUpdate.IsEnabled = true;

            ChangeInput(item);
        }

        private void grdProvince_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextProvince.ShowEditor();
        }

        private void removeProvince_Click(object sender, RoutedEventArgs e)
        {
            var item = grdProvince.SelectedItem as DTO.Entity.Province;

            if (new ProvinceDAO().Remove(item.Id))
            {
                DXMessageBox.Show("Xoá tỉnh thành thành công!");
            }
            else
            {
                DXMessageBox.Show("Xoá tỉnh thành không thành công!");
            }

            grdProvince.ItemsSource = new ProvinceDAO().GetAll();
        }

        private void removeProvince_Loaded(object sender, RoutedEventArgs e)
        {
            Province prov = null;
            try
            {
                prov = grdProvince.SelectedItem as Province;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (prov == null)
            {
                removeProvince.IsEnabled = false;
            }
            else
            {
                removeProvince.IsEnabled = true;
            }
        }


    }
}
