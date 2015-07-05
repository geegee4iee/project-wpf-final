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

namespace Seller.Views.Orders
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        public OrderView()
        {
            InitializeComponent();
            grdOrderDetails.ItemsSource = lstOrderDetails; // ben tui chay loi o day 

            new DebtDAO().AutoAdd();
            new DebtDAO().AutoUpdate();
        }

        static List<Seller.DTO.Entity.OrderDetail> lstOrderDetails = new List<DTO.Entity.OrderDetail>();

        private void btnOrdDeAdd_Click(object sender, RoutedEventArgs e)
        {
            var proc = cbxProduct.SelectedItemValue as DTO.Entity.Product;
            if (proc == null)
            {
                DXMessageBox.Show("Chưa chọn sản phẩm");
                return;
            }

            int quantity = proc.Quantity.Value;
            if (quantity < int.Parse(txtQuantity.Text))
            {
                MessageBox.Show("Vượt quá số lượng hiện có");
                return;
            }

            var orderDetails = new Seller.DTO.Entity.OrderDetail();
            orderDetails.Product = proc;
            orderDetails.ProId = proc.Id;
            orderDetails.Quantity = int.Parse(txtQuantity.Text);
            UpdatePriceOrderDetail(orderDetails, proc.Price.Value);

            if (lstOrderDetails.Where(c => c.Product.Name.Equals(proc.Name)).Any())
            {
                var orderDtemp = lstOrderDetails.Where(c => c.Product.Name.Equals(proc.Name)).FirstOrDefault();
                orderDtemp.Quantity += int.Parse(txtQuantity.Text);
                UpdatePriceOrderDetail(orderDtemp, proc.Price.Value);
            }
            else
            {
                lstOrderDetails.Add(orderDetails);
            }

            RefreshGridOrderDetails();
        }

        private static void UpdatePriceOrderDetail(DTO.Entity.OrderDetail orderDetails, decimal price)
        {
            orderDetails.Price = price;
            orderDetails.Amount = price * orderDetails.Quantity;
        }

        private void grdOrderDetails_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            tblOrderDetail.ShowEditor();
        }

        private void removeOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            DTO.Entity.OrderDetail ord = grdOrderDetails.SelectedItem as DTO.Entity.OrderDetail;
            if (ord == null)
            {
                return;
            }

            lstOrderDetails.Remove(ord);

            RefreshGridOrderDetails();
        }

        private void RefreshGridOrderDetails()
        {
            grdOrderDetails.ItemsSource = null;
            grdOrderDetails.ItemsSource = lstOrderDetails;
            txtOrdTotal_Update();
        }

        private void RefreshGridOrders()
        {

            grdOrders.ItemsSource = new OrderDAO().GetAll();
        }

        private void txtOrdTotal_Update()
        {
            decimal total = 0;
            foreach (var ordDel in lstOrderDetails)
            {
                total += ordDel.Amount.Value;
            }

            txtOrdTotal.Text = total.ToString();
        }

        private void btnOrdAdd_Click(object sender, RoutedEventArgs e)
        {
            var customer = cbxCustomer.SelectedItem as Customer;
            if (customer == null)
            {
                DXMessageBox.Show("Chưa chọn khách hàng");
                return;
            }

            if (lstOrderDetails.Count == 0)
            {
                DXMessageBox.Show("Chưa có sản phẩm");
                return;
            }

            Order ord = new Order();
            ord.IsFixed = 0;

            if (txtOrdDateOfIssue.Text.Equals(""))
            {
                ord.DateOfIssue = DateTime.Now;
            }
            else
            {
                ord.DateOfIssue = Convert.ToDateTime(txtOrdDateOfIssue.Text);
            }

            ord.Total = decimal.Parse(txtOrdTotal.Text);
            ord.Payment = decimal.Parse(txtPayment.Text);
            ord.Remainder = ord.Total - ord.Payment;
            ord.CusId = customer.Id;

            if (customer.AllowDebt == 0 && ord.Remainder != 0)
            {
                DXMessageBox.Show("Đây là khách hàng vãng lai, không được phép nợ");
                return;
            }

            new OrderDAO().Add(ord, lstOrderDetails);

            lstOrderDetails = new List<OrderDetail>();
            cbxProduct.SelectedIndex = 0;
            txtPayment.Text = "";
            txtOrdDateOfIssue.Text = "";

            RefreshGridOrderDetails();
            RefreshGridOrders();
        }

        private void grdOrders_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            tblOrder.ShowEditor();
        }

        private void removeOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = DXMessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng?", "Xóa dữ liệu", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                Order ord = grdOrders.SelectedItem as Order;
                if (ord == null)
                {
                    return;
                }

                new OrderDAO().Remove(ord.Id);
                RefreshGridOrders();
            }
            else
            {
            }
        }

        private void grdOrders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ord = grdOrders.SelectedItem as Order;
            if (ord == null)
            {
                return;
            }

            grdOrderDetailsView.ItemsSource = new OrderDetailDAO().GetAll(ord.Id);
            txtOrdPayId.Text = ord.Id.ToString();
            txtOrdPayRemainder.Text = new OrderDAO().GetCurrentRemainder(ord.Id).ToString();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            decimal pay = decimal.Parse(txtPay.Text);
            decimal remain = decimal.Parse(txtOrdPayRemainder.Text);

            if (pay > remain)
            {
                DXMessageBox.Show("Vượt quá số tiền còn lại");
            }
            else
            {
                PaymentDetail payment = new PaymentDetail
                {
                    OrderId = int.Parse(txtOrdPayId.Text),
                    Paid = pay,
                    Remainder = remain - pay,
                    PayDate = DateTime.Now
                };

                new PaymentDetailDAO().Add(payment);
                RefreshGridOrders();
                txtOrdPayId.Text = "";
                txtOrdPayRemainder.Text = "";
                txtPay.Text = "";
            }
        }

        private void cbxCustomer_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            var customer = cbxCustomer.SelectedItem as Customer;
            if (customer != null)
            {
                lbAllow.Content = customer.AllowDebt.Value == 1 ? "Được phép nợ" : "Không được phép nợ";
            }
        }
    }
}
