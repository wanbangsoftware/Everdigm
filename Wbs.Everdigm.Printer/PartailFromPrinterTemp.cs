using System;
using System.Configuration;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Printer
{
    public partial class FormPrinter
    {
        private TB_Satellite tempObj = new TB_Satellite()
        {
            CardNo = ConfigurationManager.AppSettings["TEST_LABEL_IMEI"],
            ManufactureDate = ConfigurationManager.AppSettings["TEST_LABEL_MFD"],
            Manufacturer = ConfigurationManager.AppSettings["TEST_LABEL_MF"],
            FWVersion = ConfigurationManager.AppSettings["TEST_LABEL_FW"],
            PcbNumber = ConfigurationManager.AppSettings["TEST_LABEL_PCB"],
            RatedVoltage = ConfigurationManager.AppSettings["TEST_LABEL_RV"]
        };

        private TB_Terminal tempTerminal = new TB_Terminal()
        {
            Number = "2016012301",
            Sim = "89007423",
            ProductionDate = DateTime.Now
        };
    }
}