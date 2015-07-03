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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Seller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FullName { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CloseAllTab();
            pnInfo.Visibility = Visibility.Visible;
        }

        private void CloseAllTab()
        {
            pnInfo.Visibility = Visibility.Collapsed;
            pnCategory.Visibility = Visibility.Collapsed;
            pnInterestOfYear.Visibility = Visibility.Collapsed;
            pnOrder.Visibility = Visibility.Collapsed;
            pnProduct.Visibility = Visibility.Collapsed;
            pnCustomer.Visibility = Visibility.Collapsed;
            pFixedRegister.Visibility = Visibility.Collapsed;

            pnImportExcel.Visibility = Visibility.Collapsed;
            pnExportExcel.Visibility = Visibility.Collapsed;

            pnSalesReport.Visibility = Visibility.Collapsed;
            pnDebtReport.Visibility = Visibility.Collapsed;
        }

        private void btnInfo_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnInfo.Visibility = Visibility.Visible;
        }

        private void btnExit_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnFixedRegister_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pFixedRegister.Visibility = Visibility.Visible;
        }

        private void btnCustomer_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnCustomer.Visibility = Visibility.Visible;
        }

        private void btnProduct_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

            CloseAllTab();
            pnProduct.Visibility = Visibility.Visible;
        }

        private void btnCategory_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnCategory.Visibility = Visibility.Visible;
        }

        private void btnOrder_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnOrder.Visibility = Visibility.Visible;
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnOrder.Visibility = Visibility.Visible;
        }

        private void btnSalesReport_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnSalesReport.Visibility = Visibility.Visible;
        }

        private void btnDebtReport_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnDebtReport.Visibility = Visibility.Visible;
        }

        private void btnInterest_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnInterestOfYear.Visibility = Visibility.Visible;
        }

        private void btnImportExcel_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnImportExcel.Visibility = Visibility.Visible;
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnExportExcel.Visibility = Visibility.Visible;
        }
    }
}
