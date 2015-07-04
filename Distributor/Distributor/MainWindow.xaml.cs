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

namespace Distributor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CloseAllTab();
        }

        private void CloseAllTab()
        {
            pnSeller.Visibility = Visibility.Collapsed;
            pnInfo.Visibility = Visibility.Collapsed;

            pnProduct.Visibility = Visibility.Collapsed;
            pnCategory.Visibility = Visibility.Collapsed;
            pnStock.Visibility = Visibility.Collapsed;
            pnIssue.Visibility = Visibility.Collapsed;

            pnImportExcel.Visibility = Visibility.Collapsed;
            pnExportExcel.Visibility = Visibility.Collapsed;

            pnSalesReport.Visibility = Visibility.Collapsed;
            pnStockReport.Visibility = Visibility.Collapsed;
            pnDebtReport.Visibility = Visibility.Collapsed;
        }

        private void btnExit_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void btnInfo_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnInfo.Visibility = Visibility.Visible;
        }

        private void btnSeller_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnSeller.Visibility = Visibility.Visible;
        }

        private void btnStock_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnStock.Visibility = Visibility.Visible;
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

        private void btnIssue_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnIssue.Visibility = Visibility.Visible;
        }

        private void btnIssueDetail_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnIssue.Visibility = Visibility.Visible;
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

        private void btnStockReport_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnStockReport.Visibility = Visibility.Visible;
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
