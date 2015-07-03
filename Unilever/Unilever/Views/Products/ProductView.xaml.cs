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

namespace Unilever.Views.Products
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        public ProductView()
        {
            InitializeComponent();
        }

        private void LayoutGroup_Loaded(object sender, RoutedEventArgs e)
        {
            btnProUpdate.IsEnabled = false;
            cbxCategory.ItemsSource = new CategoryDAO().GetAll();

        }

        private void btnProRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnProUpdate.IsEnabled = false;
            btnProAdd.IsEnabled = true;
            Product pro = new Product();
            InputChange(pro);
        }

        private void InputChange(Product pro)
        {
            txtId.Text = pro.Id.ToString();
            txtName.Text = pro.Name;
            txtPrice.Text = string.Format("{0:N0}", pro.Price);
            txtQuantity.Text = pro.Quantity.ToString();
            txtSale.Text = pro.Sale.ToString();
            FlowDocument myFlowDoc = new FlowDocument();
            myFlowDoc.Blocks.Add(new Paragraph(new Run(pro.Description)));
            txtDesc.Document = myFlowDoc;
            cbxCategory.ItemsSource = new CategoryDAO().GetAll();

            if(pro.Category == null)
            {
                cbxCategory.SelectedIndex = -1;
            }
            else
            {
                cbxCategory.SelectedText = pro.Category.Name;
            }
        }

        private void btnProAdd_Click(object sender, RoutedEventArgs e)
        {
            Product pro = null;

            string name = txtName.Text;
            if(string.IsNullOrEmpty(name))
            {
                DXMessageBox.Show("Vui lòng nhập tên sản phẩm!");
                return;
            }
            else
            {

            }

            decimal price = 0;
            try
            {
                price = Convert.ToDecimal(txtPrice.Text);
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Thêm sản phẩm không thành công!");
                return;
            }

            Int32 quantity = 0;
            try
            {
                quantity = Convert.ToInt32(txtQuantity.Text);
                if(quantity < 0)
                {
                    DXMessageBox.Show("Vui lòng không nhập số lượng âm!");
                    return;
                }
                else
                {
                   
                }
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Thêm sản phẩm không thành công!");
                return;
            }

            Int32 sale = 0;
            try
            {
                sale = Convert.ToInt32(txtSale.Text);
                if (sale < 0)
                {
                    DXMessageBox.Show("Vui lòng không nhập số lượng bán âm!");
                    return;
                }
                else
                {
                    
                }
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Thêm sản phẩm không thành công!");
                return;
            }

            string description = null;
            try
            {
                description = new TextRange(txtDesc.Document.ContentStart, txtDesc.Document.ContentEnd).Text;
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Thêm sản phẩm không thành công!");
                return;
            }

            int catId = (cbxCategory.SelectedItem as Category).Id;
            try
            {
                pro = new Product
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Sale = sale,
                    Description = description,
                    CatId = catId
                };

                if (new ProductDao().Add(pro))
                {
                    DXMessageBox.Show("Thêm sản phẩm thành công!");
                    grdViewPro.ItemsSource = new ProductDao().GetAll();
                }
                else
                {
                    DXMessageBox.Show("Thêm sản phẩm không thành công!");
                }
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Thêm sản phẩm không thành công!");
                return;
            }
           
        }

        private void grdViewPro_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdViewPro.SelectedItem as Product;
            if(item == null)
            {
                return;
            }
            else
            {

            }

            InputChange(new ProductDao().GetById(item.Id));

            btnProAdd.IsEnabled = false;
            btnProUpdate.IsEnabled = true;
        }

        private void grdViewPro_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            contextPro.ShowEditor();
        }

        private void removePro_Click(object sender, RoutedEventArgs e)
        {
            var item = grdViewPro.SelectedItem as Product;

            if (new ProductDao().Remove(item.Id))
            {
                DXMessageBox.Show("Xóa dữ liệu thành công! ");
            }
            else
            {
                DXMessageBox.Show("Xóa không thành công!");
            }

            grdViewPro.ItemsSource = new ProductDao().GetAll();
        }

        private void btnProUpdate_Click(object sender, RoutedEventArgs e)
        {
            Product pro = null;

            string name = txtName.Text;
            if (string.IsNullOrEmpty(name))
            {
                DXMessageBox.Show("Vui lòng nhập tên sản phẩm!");
                return;
            }
            else
            {

            }

            decimal price = 0;
            try
            {
                price = Convert.ToDecimal(txtPrice.Text);
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Cập nhật sản phẩm không thành công!");
                return;
            }

            Int32 quantity = 0;
            try
            {
                quantity = Convert.ToInt32(txtQuantity.Text);
                if (quantity < 0)
                {
                    DXMessageBox.Show("Vui lòng không nhập số lượng âm!");
                    return;
                }
                else
                {

                }
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Cập nhật sản phẩm không thành công!");
                return;
            }

            Int32 sale = 0;
            try
            {
                sale = Convert.ToInt32(txtSale.Text);
                if (sale < 0)
                {
                    DXMessageBox.Show("Vui lòng không nhập số lượng bán âm!");
                    return;
                }
                else
                {

                }
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Cập nhật sản phẩm không thành công!");
                return;
            }

            string description = null;
            try
            {
                description = new TextRange(txtDesc.Document.ContentStart, txtDesc.Document.ContentEnd).Text;
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Cập nhật sản phẩm không thành công!");
                return;
            }

            int catId = (cbxCategory.SelectedItem as Category).Id;
            try
            {
                pro = new Product
                {
                    Id = Convert.ToInt32(txtId.Text),
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Sale = sale,
                    Description = description,
                    CatId = catId
                };

                if (new ProductDao().UpdateInfo(pro))
                {
                    DXMessageBox.Show("Cập nhật sản phẩm thành công!");
                    grdViewPro.ItemsSource = new ProductDao().GetAll();
                }
                else
                {
                    DXMessageBox.Show("Cập nhật sản phẩm không thành công!");
                }
            }
            catch (System.Exception ex)
            {
                DXMessageBox.Show("Cập nhật sản phẩm không thành công!");
                return;
            }
        }

        private void removePro_Loaded(object sender, RoutedEventArgs e)
        {
            Product pro = null;
            try
            {
                pro = grdViewPro.SelectedItem as Product;
            }
            catch (System.Exception ex)
            {
                return;
            }

            if (pro == null)
            {
                removePro.IsEnabled = false;
            }
            else
            {
                removePro.IsEnabled = true;
            }
        }
    }
}
