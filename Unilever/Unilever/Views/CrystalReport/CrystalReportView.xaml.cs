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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using Unilever.DTO.Entity;

namespace Unilever.Views.CrystalReport
{
    /// <summary>
    /// Interaction logic for CrystalReportView.xaml
    /// </summary>
    public partial class CrystalReportView : Window
    {
        public CrystalReportView()
        {
            InitializeComponent();
           

           
        }

        private void crystalReportsViewer1_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDocument report = new ReportDocument();
            report.Load(@"D:\MyGitHub\project-wpf-final\Unilever\Unilever\Views\CrystalReport\SaleReport.rpt");

            using (UnileverEntities ent = new UnileverEntities())
            {
                var lstSale = (from c in ent.Sales
                               select new
                               {
                                   DisId = c.DistributorId,
                                   DisName = c.Distributor.Name,
                                   ProId = c.ProId,
                                   ProName = c.Product.Name,
                                   c.Year,
                                   c.Month,
                                   Quantity = c.Quantity.Value,
                                   Sales = c.Sales.Value
                               }).ToList();

                report.SetDataSource(lstSale);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnOpenReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = txtDate.DateTime;
           
            ReportDocument report = new ReportDocument();
            report.Load("./Views/CrystalReport/SaleReport.rpt");

            using (UnileverEntities ent = new UnileverEntities())
            {
                var lstSale = (from c in ent.Sales
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
                                   Sales = c.Sales.Value
                               }).ToList();

                report.SetDataSource(lstSale);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }
    }
}
