using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEMInformation_Manager.Models
{
    public class OemInformationData
    {
        public string Logo { get; private set; }

        public string Manufacturer { get; private set; }

        public string Model { get; private set; }

        public string SupportHours { get; private set; }

        public string SupportPhone { get; private set; }

        public string SupportUrl { get; private set; }

        public OemInformationData(string logo, string manufacturer, string model, string supportHours, string supportPhone, string supportUrl)
        {
            SetOemInformationData(logo, manufacturer, model, supportHours, supportPhone, supportUrl);
        }

        public void SetOemInformationData(string logo, string manufacturer, string model, string supportHours, string supportPhone, string supportUrl)
        {
            Logo = logo;
            Manufacturer = manufacturer;
            Model = model;
            SupportHours = supportHours;
            SupportPhone = supportPhone;
            SupportUrl = supportUrl;
        }

    }
}
