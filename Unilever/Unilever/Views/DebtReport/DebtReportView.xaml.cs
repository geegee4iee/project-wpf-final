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
using Unilever.DAO;

namespace Unilever.Views.DebtReport
{
    /// <summary>
    /// Interaction logic for DebtReportView.xaml
    /// </summary>
    public partial class DebtReportView : Window
    {
        public DebtReportView()
        {
            InitializeComponent();
        }

        private void btnOpenReport_Click(object sender, RoutedEventArgs e)
        {
            new DebtDAO().AutoAdd();
            new DebtDAO().AutoUpdate();
            DateTime date = txtDate.DateTime;

            using (UnileverEntities ent = new UnileverEntities())
            {
                var lstDebt = (from c in ent.Debts
                                where c.Year == date.Year
                                select new
                                {
                                    DisId = c.DistributorId,
                                    DisName = c.Distributor.Name,
                                    c.Year,
                                    Month1 = c.Month1.Value == null?0:c.Month1.Value,
                                    Month2 = c.Month2.Value == null ? 0 : c.Month2.Value,
                                    Month3 = c.Month3.Value == null ? 0 : c.Month3.Value,
                                    Month4 = c.Month4.Value == null ? 0 : c.Month4.Value,
                                    Month5 = c.Month5.Value == null ? 0 : c.Month5.Value,
                                    Month6 = c.Month6.Value == null ? 0 : c.Month6.Value,
                                    Month7 = c.Month7.Value == null ? 0 : c.Month7.Value,
                                    Month8 = c.Month8.Value == null ? 0 : c.Month8.Value,
                                    Month9 = c.Month9.Value == null ? 0 : c.Month9.Value,
                                    Month10 = c.Month10.Value == null ? 0 : c.Month10.Value,
                                    Month11 = c.Month11.Value == null ? 0 : c.Month11.Value,
                                    Month12 = c.Month12.Value == null ? 0 : c.Month12.Value
                                }).ToList();

                if (lstDebt.Count == 0)
                {
                    DXMessageBox.Show("Chưa có dữ liệu thống kê");
                    return;
                }

                ReportDocument report = new ReportDocument();
                report.Load("./Views/CrystalReport/DebtReport.rpt");
                report.SetDataSource(lstDebt);
                crystalReportsViewer1.ViewerCore.ReportSource = report;
            }
        }
    }
}
