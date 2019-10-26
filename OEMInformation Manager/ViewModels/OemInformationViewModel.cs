using System;
using OEMInformation_Manager.Helpers;
using OEMInformation_Manager.Models;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using OEMInformation_Manager.Properties;

namespace OEMInformation_Manager.ViewModels
{
    public class OemInformationViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        private readonly OemInformationData _oemInformationData;

        #region Properties
        private string _logo;
        public string Logo
        {
            get => _logo;
            set
            {
                _logo = value;

                OnPropertyChanged(nameof(Logo));
            }
        }

        private string _manufacturer;
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                _manufacturer = value;

                OnPropertyChanged(nameof(Manufacturer));
            }
        }

        private string _model;
        public string Model
        {
            get => _model;
            set
            {
                _model = value;

                OnPropertyChanged(nameof(Model));
            }
        }

        private string _supportHours;
        public string SupportHours
        {
            get => _supportHours;
            set
            {
                _supportHours = value;

                OnPropertyChanged(nameof(SupportHours));
            }
        }

        private string _supportPhone;
        public string SupportPhone
        {
            get => _supportPhone;
            set
            {
                _supportPhone = value;

                OnPropertyChanged(nameof(SupportPhone));
            }
        }

        private string _supportUrl;
        public string SupportUrl
        {
            get => _supportUrl;
            set
            {
                _supportUrl = value;

                OnPropertyChanged(nameof(SupportUrl));
            }
        }
        #endregion

        public OemInformationViewModel()
        {
            _oemInformationData = OemInformationManager.ReadRegistry();

            #region Set data from Registry
            Logo = _oemInformationData.Logo;
            Manufacturer = _oemInformationData.Manufacturer;
            Model = _oemInformationData.Model;
            SupportHours = _oemInformationData.SupportHours;
            SupportPhone = _oemInformationData.SupportPhone;
            SupportUrl = _oemInformationData.SupportUrl;
            #endregion
        }

        #region Commands
        private ICommand _applyOemInformationCommand;
        public ICommand ApplyOemInformationCommand => _applyOemInformationCommand ?? (_applyOemInformationCommand = new Command(ApplyOemInformation));

        private void ApplyOemInformation()
        {
            Logo = OemLogoHelper.Copy(Logo);

            _oemInformationData.SetOemInformationData(_logo, _manufacturer, _model, _supportHours, _supportPhone, _supportUrl);

            OemInformationManager.WriteRegistry(_oemInformationData);

            //Process.Start("control", "/name Microsoft.System");
            MessageBox.Show("OEM Information has been changed!", "Congratulations");
        }

        private ICommand _selectLogoCommand;
        public ICommand SelectLogoCommand => _selectLogoCommand ?? (_selectLogoCommand = new Command(SelectLogo));

        private void SelectLogo()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Title = "Select OEM Logo",
                Filter = "bmp files (*.bmp)| *.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                Logo = openFileDialog.FileName;
            }
        }

        private ICommand _loadDefaultOemDataCommand;
        public ICommand LoadDefaultOemDataCommand => _loadDefaultOemDataCommand ?? (_loadDefaultOemDataCommand = new Command(LoadDefaultOemData));

        private void LoadDefaultOemData()
        {
            try
            {
                #region Set data from Config file
                Logo = OemLogoHelper.GetDefaultLogoName();
                Manufacturer = ConfigurationManager.AppSettings["Manufacturer"];
                Model = ConfigurationManager.AppSettings["Model"];
                SupportHours = ConfigurationManager.AppSettings["SupportHours"];
                SupportPhone = ConfigurationManager.AppSettings["SupportPhone"];
                SupportUrl = ConfigurationManager.AppSettings["SupportUrl"];
                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        #endregion
    }
}
