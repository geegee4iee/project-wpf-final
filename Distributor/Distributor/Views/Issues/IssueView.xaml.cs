using Distributor.DTO.Entity;
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
using Distributor.DAO;
using DevExpress.Xpf.Core;

namespace Distributor.Views.Issues
{
    /// <summary>
    /// Interaction logic for IssueView.xaml
    /// </summary>
    public partial class IssueView : Window
    {
        void RefreshGridIssue()
        {
            grdIssues.ItemsSource = new IssueDAO().GetAll();
        }

        void RefreshGridIssueDetails()
        {
            grdIssueDetails.ItemsSource = null;
            grdIssueDetails.ItemsSource = lstIssueDetails;
        }

        void RefreshGridIssueDetailViews()
        {
            grdIssueDetailsView.ItemsSource = null;
            var curIss = grdIssues.ItemsSource as Issue;
            if (curIss != null)
            {
                grdIssueDetailsView.ItemsSource = new IssueDetailDAO().GetAll(curIss.SellerId, curIss.DateOfIssue);
            }
        }

        public IssueView()
        {
            InitializeComponent();
        }

        private void btnIssAdd_Click(object sender, RoutedEventArgs e)
        {
            Issue iss = new Issue();
            Seller sel = cbxSellers.SelectedItem as Seller;

            if (sel == null)
            {
                DXMessageBox.Show("Chưa chọn nhân viên bán hàng");
                return;
            }

            if (txtIssDateOfIssue.Text == "")
            {
                iss.DateOfIssue = DateTime.Today;
            }
            else
            {
                iss.DateOfIssue = Convert.ToDateTime(txtIssDateOfIssue.Text);
            }

            iss.SellerId = sel.Id;
            iss.Total = Decimal.Parse(txtIssTotal.Text);
            iss.Debt = iss.Total;

            if (lstIssueDetails.Count == 0)
            {
                DXMessageBox.Show("Chưa có chi tiết sản phẩm muốn xuất");
                return;
            }

            if (!new IssueDAO().IsExist(iss))
            {
                new IssueDAO().Add(iss, lstIssueDetails);
            }
            else
            {
                DXMessageBox.Show("Nhân viên này đã được xuất hàng vào thời gian này");
                return;
            }
            
            RefreshGridIssue();
        }

        static List<IssueDetail> lstIssueDetails = new List<IssueDetail>(); 

        private void btnIssDeAdd_Click(object sender, RoutedEventArgs e)
        {
            IssueDetail issueDetail = new IssueDetail();
            Product proc = cbxProduct.SelectedItem as Product;
            issueDetail.ProId = proc.Id;
            issueDetail.Quantity = int.Parse(txtQuantity.Text);
            issueDetail.Remainder = issueDetail.Quantity.Value;
            issueDetail.Amount = issueDetail.Quantity.Value * proc.Price;
            var curIssueDetail = lstIssueDetails.Where(c => c.ProId == issueDetail.ProId).FirstOrDefault();

            if (curIssueDetail != null)
            {
                curIssueDetail.Quantity += issueDetail.Quantity;
                curIssueDetail.Remainder += issueDetail.Remainder;
                curIssueDetail.Amount += issueDetail.Amount;
            }
            else
            {
                lstIssueDetails.Add(issueDetail);
            }

            UpdateTextBoxTotal(issueDetail.Amount.Value);

            RefreshGridIssueDetails();
        }

        private void UpdateTextBoxTotal(decimal value)
        {
            decimal total = Decimal.Parse(txtIssTotal.Text);
            total += value;
            txtIssTotal.Text = total.ToString();
        }

        private void removeIssueDetail_Click(object sender, RoutedEventArgs e)
        {
            var curIssDetail = grdIssueDetails.SelectedItem as IssueDetail;
            UpdateTextBoxTotal(-curIssDetail.Amount.Value);
            lstIssueDetails.Remove(curIssDetail);

            RefreshGridIssueDetails();
        }

        private void grdIssues_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Issue iss = grdIssues.SelectedItem as Issue;
            if (iss == null)
            {
                return;
            }
            else
            {
                grdIssueDetailsView.ItemsSource = new IssueDetailDAO().GetAll(iss.SellerId, iss.DateOfIssue);
            }
        }

        private void removeIssue_Click(object sender, RoutedEventArgs e)
        {
            var iss = grdIssues.SelectedItem as Issue;
            if (iss != null)
            {
                new IssueDAO().Remove(iss);
                RefreshPayPanel();
                RefreshGridIssueDetailViews();
                RefreshGridIssue();
            }
        }

        private void grdIssueDetailsView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var curIssDetail = grdIssueDetailsView.SelectedItem as IssueDetail;
            if (curIssDetail == null)
            {
                return;
            }
            else
            {
                txtIssPayId.Text = curIssDetail.ProId.ToString();
                txtIssPayRemainder.Text = curIssDetail.Remainder.Value.ToString();
                btnPay.IsEnabled = true;
                btnReturnStock.IsEnabled = true;
            }
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (txtPay.Text == "")
            {
                DXMessageBox.Show("Bạn chưa nhập giá trị");
                return;
            }

            int value = Int32.Parse(txtPay.Text);
            var curIssDetail = grdIssueDetailsView.SelectedItem as IssueDetail;
            if (value <= 0 || value > curIssDetail.Remainder.Value)
            {
                DXMessageBox.Show("Giá trị không hợp lệ");
                return;
            }
            else
            {
                new IssueDetailDAO().UpdateRemainder(curIssDetail, value);
                RefreshGridIssue();
                RefreshGridIssueDetailViews();
                RefreshPayPanel();
            }
        }

        private void RefreshPayPanel()
        {
            txtPay.Text = "";
            txtIssPayId.Text = "";
            txtIssPayRemainder.Text = "";
            btnPay.IsEnabled = false;
            btnReturnStock.IsEnabled = false;
        }

        private void btnReturnStock_Click(object sender, RoutedEventArgs e)
        {
            var curIssDetail = grdIssueDetailsView.SelectedItem as IssueDetail;
            if (curIssDetail.Remainder == 0)
            {
                DXMessageBox.Show("Không còn hàng tồn");
                return;
            }
            else
            {
                new IssueDetailDAO().ReturnToStock(curIssDetail);
                RefreshGridIssue();
                RefreshGridIssueDetailViews();
                RefreshPayPanel();
            }
        }
    }
}
