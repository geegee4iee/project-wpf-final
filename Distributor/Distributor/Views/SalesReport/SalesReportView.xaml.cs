using CrystalDecisions.CrystalReports.Engine;
using DevExpress.Xpf.Core;
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

namespace Distributor.Views.SalesReport
{
    /// <summary>
    /// Interaction logic for SalesReportView.xaml
    /// </summary>
    public partial class SalesReportView : Window
    {
        public SalesReportView()
        {
            InitializeComponent();
            new SaleDAO().AutoAdd();
            new StockDAO().AutoAdd();
        }

        private void btnOpenReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = txtDate.DateTime;
            using (DistributorEntities ent = new DistributorEntities())
            {
                var lstSale = (from c in ent.Sales
                               where c.Month == date.Month && c.Year == date.Year
                               select new
                               {
                                   ProId = c.ProId,
                                   ProName = c.Product.Name,
                                   c.Year,
                                   c.Month,
                                   Quantity = c.Quantity.Value,
                                   Sales = c.Sales.Value
                               }).ToList();

                if (lstSale.Count == 0)
                {
                    DXMessageBox.Show("Không có dữ liệu thống kê");
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load("./Views/CrystalReport/SaleReport.rpt");
                report.SetDataSource(lstSale);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }

        private void btnOpenReport2_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = txtDate2.DateTime;
            using (DistributorEntities ent = new DistributorEntities())
            {
                var lstSale = (from c in ent.Sales
                               where c.Year == date.Year
                               select new
                               {
                                   ProId = c.ProId,
                                   ProName = c.Product.Name,
                                   c.Year,
                                   c.Month,
                                   Quantity = c.Quantity.Value,
                                   Sales = c.Sales.Value
                               }).ToList();

                if (lstSale.Count == 0)
                {
                    DXMessageBox.Show("Không có dữ liệu thống kê");
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load("./Views/CrystalReport/SaleReport.rpt");
                report.SetDataSource(lstSale);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }

        private void btnOpenReportBySeller_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = txtDate.DateTime;
            using (DistributorEntities ent = new DistributorEntities())
            {
                var lstSale = (from c in ent.SellerSales
                               where c.Month == date.Month && c.Year == date.Year
                               select new
                               {
                                   SellerId = c.SellerId,
                                   SellerName = c.Seller.Name,
                                   ProId = c.ProId,
                                   ProName = c.Product.Name,
                                   c.Year,
                                   c.Month,
                                   Quantity = c.Quantity.Value,
                                   Sales = c.Sales.Value
                               }).ToList();

                if (lstSale.Count == 0)
                {
                    DXMessageBox.Show("Không có dữ liệu thống kê");
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load("./Views/CrystalReport/SellerSaleReport.rpt");
                report.SetDataSource(lstSale);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }

        private void btnOpenReport2BySeller_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = txtDate2.DateTime;
            using (DistributorEntities ent = new DistributorEntities())
            {
                var lstSale = (from c in ent.SellerSales
                               where c.Year == date.Year
                               select new
                               {
                                   SellerId = c.SellerId,
                                   SellerName = c.Seller.Name,
                                   ProId = c.ProId,
                                   ProName = c.Product.Name,
                                   c.Year,
                                   c.Month,
                                   Quantity = c.Quantity.Value,
                                   Sales = c.Sales.Value
                               }).ToList();

                if (lstSale.Count == 0)
                {
                    DXMessageBox.Show("Không có dữ liệu thống kê");
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load("./Views/CrystalReport/SellerSaleReport.rpt");
                report.SetDataSource(lstSale);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }
    }
}
