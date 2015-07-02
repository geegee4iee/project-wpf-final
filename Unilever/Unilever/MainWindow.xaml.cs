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
using Unilever.DAO;
using Unilever.DTO.Entity;

namespace Unilever
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int PermissionFlag { get; set; }
        public string FullName { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CloseAllTab();
            if(PermissionFlag == 1)
            {
                CloseByPermission();
            }
            else
            {
                OpenByPermission();
            }
        }

        private void OpenByPermission()
        {
            GroupHoaDon.IsVisible = true;
            GroupTonKho.IsVisible = true;
            btnDistibutor.IsVisible = true;
            rbpBaoCao.IsVisible = true;
            rbpKhac.IsVisible = true;
            btnStaff.IsVisible = false;
        }

        private void CloseByPermission()
        {
            GroupHoaDon.IsVisible = false;
            GroupTonKho.IsVisible = false;
            btnDistibutor.IsVisible = false;
            rbpBaoCao.IsVisible = false;
            rbpKhac.IsVisible = false;
            btnStaff.IsVisible = true;
        }

        private void CloseAllTab()
        {
            pnStaff.Visibility = Visibility.Collapsed;
            pnCategory.Visibility = Visibility.Collapsed;
            pnImportExcel.Visibility = Visibility.Collapsed;
            pnInterestOfYear.Visibility = Visibility.Collapsed;
            pnOrder.Visibility = Visibility.Collapsed;
            pnProduct.Visibility = Visibility.Collapsed;
            pnProvice.Visibility = Visibility.Collapsed;
            pnSalesReport.Visibility = Visibility.Collapsed;
            pnStock.Visibility = Visibility.Collapsed;
            pnTest.Visibility = Visibility.Collapsed;
            pFixedRegister.Visibility = Visibility.Collapsed;
            pnStockReport.Visibility = Visibility.Collapsed;
            pnDebtReport.Visibility = Visibility.Collapsed;
        }


        private void btnStaff_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnStaff.Visibility = Visibility.Visible;
        }

        private void btnDistibutor_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnTest.Visibility = Visibility.Visible;
        }

        private void btnFixedRegister_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pFixedRegister.Visibility = Visibility.Visible;
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

        private void btnProvice_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            CloseAllTab();
            pnProvice.Visibility = Visibility.Visible;
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

        private void btnExit_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            
            login log = new login();
            log.Show();
            this.Close();
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



    }
}
