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
            btnProUpdate.Visibility = Visibility.Hidden;
            cbxCategory.ItemsSource = new CategoryDAO().GetAll();

        }

        private void btnProRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnProUpdate.Visibility = Visibility.Hidden;
            btnProAdd.Visibility = Visibility.Visible;
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
            //cbxCategory.DisplayMember = pro.Category.Name;
        }

        private void btnProAdd_Click(object sender, RoutedEventArgs e)
        {
            Product pro = new Product
            {
                Name = txtName.Text,
                Price = Convert.ToDecimal(txtPrice.Text),
                Quantity = Convert.ToInt32(txtQuantity.Text),
                Sale = Convert.ToInt32(txtSale.Text),
                Description = new TextRange(txtDesc.Document.ContentStart, txtDesc.Document.ContentEnd).Text,
                CatId = (cbxCategory.SelectedItem as Category).Id
            };

            if (new ProductDao().Add(pro))
            {
                MessageBox.Show("Thêm sản phẩm thành công!");
                grdViewPro.ItemsSource = new ProductDao().GetAll();
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm không thành công!!!");
            }
        }

        private void grdViewPro_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grdViewPro.SelectedItem as Product;
            InputChange(new ProductDao().GetById(item.Id));
            btnProAdd.Visibility = Visibility.Hidden;
            btnProUpdate.Visibility = Visibility.Visible;
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
                MessageBox.Show("Xóa dữ liệu thành công! ");
            }
            else
            {
                MessageBox.Show("Xóa không thành công.");
            }
            grdViewPro.ItemsSource = new ProductDao().GetAll();
        }

        private void btnProUpdate_Click(object sender, RoutedEventArgs e)
        {
            Product pro = null;
            try
            {
                pro = new Product
                           {
                               Id = Convert.ToInt32(txtId.Text),
                               Name = txtName.Text,
                               Price = Convert.ToDecimal(txtPrice.Text),
                               Quantity = Convert.ToInt32(txtQuantity.Text),
                               Sale = Convert.ToInt32(txtSale.Text),
                               Description = new TextRange(txtDesc.Document.ContentStart, txtDesc.Document.ContentEnd).Text,
                               CatId = (cbxCategory.SelectedItem as Category).Id
                           };
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ, vui lòng kiểm tra lại!!!");
                return;
            }
            if (new ProductDao().UpdateInfo(pro))
            {
                MessageBox.Show("Thêm sản phẩm thành công!");
                grdViewPro.ItemsSource = new ProductDao().GetAll();
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm không thành công!!!");
            }
        }
    }
}
