using DevExpress.LookAndFeel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;

namespace PS.Lib.WinForms.Report
{
    public class ReportDesignTool : IReportDesignTool
    {
        private XtraReport xtraReport = new XtraReport();

        public ReportDesignTool(XtraReport report)
        {
            xtraReport = report;
        }

        public void Dispose()
        {

        }

        public void ShowPreview()
        {
            xtraReport.ShowPreview();
        }

        public void ShowPreviewDialog()
        {
            xtraReport.ShowPreviewDialog();
        }

        public void ShowDesigner()
        {
            xtraReport.ShowDesigner();
        }

        public void ShowDesigner(UserLookAndFeel lookAndFeel)
        {
            xtraReport.ShowDesigner(lookAndFeel);
        }

        public void ShowDesigner(UserLookAndFeel lookAndFeel, DesignDockPanelType hiddenPanels)
        {
            xtraReport.ShowDesigner(lookAndFeel, hiddenPanels);
        }

        public void ShowDesignerDialog()
        {
            xtraReport.ShowDesignerDialog();
        }

        public void ShowDesignerDialog(UserLookAndFeel lookAndFeel)
        {
            xtraReport.ShowDesignerDialog(lookAndFeel);
        }

        public void ShowDesignerDialog(UserLookAndFeel lookAndFeel, DesignDockPanelType hiddenPanels)
        {
            xtraReport.ShowDesignerDialog(lookAndFeel, hiddenPanels);
        }

        public void ShowRibbonDesigner()
        {
            xtraReport.ShowRibbonDesigner();
        }

        public void ShowRibbonDesigner(UserLookAndFeel lookAndFeel)
        {
            xtraReport.ShowRibbonDesigner(lookAndFeel);
        }

        public void ShowRibbonDesigner(UserLookAndFeel lookAndFeel, DesignDockPanelType hiddenPanels)
        {
            xtraReport.ShowRibbonDesigner(lookAndFeel, hiddenPanels);
        }

        public void ShowRibbonDesignerDialog()
        {
            xtraReport.ShowRibbonDesignerDialog();
        }

        public void ShowRibbonDesignerDialog(UserLookAndFeel lookAndFeel)
        {
            xtraReport.ShowRibbonDesignerDialog(lookAndFeel);
        }

        public void ShowRibbonDesignerDialog(UserLookAndFeel lookAndFeel, DesignDockPanelType hiddenPanels)
        {
            xtraReport.ShowRibbonDesignerDialog(lookAndFeel, hiddenPanels);
        }
    }
}
