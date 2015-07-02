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

namespace Unilever.Views.Categories
{
    /// <summary>
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : Window
    {
        public CategoryView()
        {
            InitializeComponent();

            txtId.Text = "Mặc định";
            btnUpdate.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.Category item = new DTO.Entity.Category();

            if (string.IsNullOrEmpty(txtName.Text))
            {
                DXMessageBox.Show("Vui lòng nhập tên danh mục!");
                return;
            }
            else
            {
                item.Name = txtName.Text;
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

            txtId.Text = "Mặc định";
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
        }

        private void grdCat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdCat.SelectedItem as DTO.Entity.Category;

            if(item == null)
            {
                return;
            }

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
    }
}
