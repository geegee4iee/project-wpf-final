using CrystalDecisions.CrystalReports.Engine;
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
using Unilever.DTO.Entity;
using DevExpress.Xpf.Core;

namespace Unilever.Views.StockReport
{
    /// <summary>
    /// Interaction logic for StockReportView.xaml
    /// </summary>
    public partial class StockReportView : Window
    {
        public StockReportView()
        {
            InitializeComponent();
        }

        private void btnOpenReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = txtDate.DateTime;
            using (UnileverEntities ent = new UnileverEntities())
            {
                var lstStock = (from c in ent.Stocks
                               where c.Month == date.Month && c.Year == date.Year
                               select new
                               {
                                   DisId = c.DistributorId,
                                   DisName = c.Distributor.Name,
                                   ProId = c.ProId,
                                   ProName = c.Product.Name,
                                   c.Year,
                                   c.Month,
                                   Quantity = c.Quantity.Value,
                               }).ToList();

                if (lstStock.Count == 0)
                {
                    DXMessageBox.Show("Không có dữ liệu thống kê");
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load("./Views/CrystalReport/StockReport.rpt");
                report.SetDataSource(lstStock);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }

        private void btnOpenReport2_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = txtDate2.DateTime;
            using (UnileverEntities ent = new UnileverEntities())
            {
                var lstStock = (from c in ent.Stocks
                                where  c.Year == date.Year
                                select new
                                {
                                    DisId = c.DistributorId,
                                    DisName = c.Distributor.Name,
                                    ProId = c.ProId,
                                    ProName = c.Product.Name,
                                    c.Year,
                                    c.Month,
                                    Quantity = c.Quantity.Value,
                                }).ToList();

                if (lstStock.Count == 0)
                {
                    DXMessageBox.Show("Không có dữ liệu thống kê");
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load("./Views/CrystalReport/StockReport.rpt");
                report.SetDataSource(lstStock);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }
    }
}
