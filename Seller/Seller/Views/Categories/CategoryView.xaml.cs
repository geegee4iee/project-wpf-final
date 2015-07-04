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

namespace Seller.Views.Categories
{
    /// <summary>
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : Window
    {
        public CategoryView()
        {
            InitializeComponent();

            btnUpdate.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Category item = new DTO.Entity.Category();

            try
            {
                item.Id = int.Parse(txtId.Text);
                item.Name = txtName.Text;
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Thêm loại sản phẩm không thành công!");
                return;
            }

            if (item.Id < 0)
            {
                DXMessageBox.Show("Vui lòng nhập mã loại sản phẩm là số dương!");
                return;
            }
            else
            {

            }

            if(string.IsNullOrEmpty(item.Name))
            {
                DXMessageBox.Show("Vui lòng nhập tên loại sản phẩm!");
                return;
            }
            else
            {

            }


            if (new CategoryDAO().Add(item))
            {
                DXMessageBox.Show("Thêm danh mục thành công!");
            }
            else
            {
                DXMessageBox.Show("Thêm danh mục không thành công!");
            }

            grdCat.ItemsSource = new CategoryDAO().GetAll();
        }

        private void ChangeInput(DTO.Entity.Category item)
        {
            txtId.Text = item.Id.ToString();
            txtName.Text = item.Name;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Category item = new DTO.Entity.Category();
            item.Id = int.Parse(txtId.Text);

            if (string.IsNullOrEmpty(txtName.Text))
            {
                DXMessageBox.Show("Vui lòng nhập tên danh mục!");
                return;
            }
            else
            {
                item.Name = txtName.Text;
            }

            if (new CategoryDAO().Update(item))
            {
                DXMessageBox.Show("Cập nhật danh mục thành công!");
            }
            else
            {
                DXMessageBox.Show("Cập nhật danh mục không thành công!");
            }
            grdCat.ItemsSource = new CategoryDAO().GetAll();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Category item = new DTO.Entity.Category();
            ChangeInput(item);

            txtId.IsEnabled = true;

            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
        }

        private void grdCat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdCat.SelectedItem as DTO.Entity.Category;

            if (item == null)
            {
                return;
            }

            txtId.IsEnabled = false;

            btnAdd.IsEnabled = false;
            btnUpdate.IsEnabled = true;

            ChangeInput(item);
        }

        private void grdCat_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextCategory.ShowEditor();
        }

        private void removeCategory_Click(object sender, RoutedEventArgs e)
        {
            var item = grdCat.SelectedItem as DTO.Entity.Category;

            if (new CategoryDAO().Remove(item.Id))
            {
                DXMessageBox.Show("Xoá danh mục thành công!");
            }
            else
            {
                DXMessageBox.Show("Xoá danh mục không thành công!");
            }

            grdCat.ItemsSource = new CategoryDAO().GetAll();
        }

        private void removeCategory_Loaded(object sender, RoutedEventArgs e)
        {
            Category cat = null;
            try
            {
                cat = grdCat.SelectedItem as Category;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (cat == null)
            {
                removeCategory.IsEnabled = false;
            }
            else
            {
                removeCategory.IsEnabled = true;
            }
        }
    }
}
