using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEMInformation_Manager.Models
{
    public static class OemInformationManager
    {
        #region Consts
        private const string SubKeyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation";

        private const string LogoValueName = "Logo";
        private const string ManufacturerValueName = "Manufacturer";
        private const string ModelValueName = "Model";
        private const string SupportHoursValueName = "SupportHours";
        private const string SupportPhoneValueName = "SupportPhone";
        private const string SupportUrlValueName = "SupportURL";
        #endregion

        public static OemInformationData ReadRegistry()
        {
            using (var registryKey = GetKey())
            {
                return new OemInformationData
                (
                    GetValue(registryKey, LogoValueName),
                    GetValue(registryKey, ManufacturerValueName),
                    GetValue(registryKey, ModelValueName),
                    GetValue(registryKey, SupportHoursValueName),
                    GetValue(registryKey, SupportPhoneValueName),
                    GetValue(registryKey, SupportUrlValueName)
                );
            }
        }

        public static void WriteRegistry(OemInformationData oemInformationData)
        {
            using (var registryKey = GetKey())
            {
                registryKey.SetValue(LogoValueName, oemInformationData.Logo, RegistryValueKind.String);
                registryKey.SetValue(ManufacturerValueName, oemInformationData.Manufacturer, RegistryValueKind.String);
                registryKey.SetValue(ModelValueName, oemInformationData.Model, RegistryValueKind.String);
                registryKey.SetValue(SupportHoursValueName, oemInformationData.SupportHours, RegistryValueKind.String);
                registryKey.SetValue(SupportPhoneValueName, oemInformationData.SupportPhone, RegistryValueKind.String);
                registryKey.SetValue(SupportUrlValueName, oemInformationData.SupportUrl, RegistryValueKind.String);
            }
        }

        private static RegistryKey GetKey()
        {
            using (var localKey = (Environment.Is64BitOperatingSystem)
                ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                : RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                return localKey.OpenSubKey(SubKeyName, true);
            }
        }

        private static string GetValue(RegistryKey key, string keyName) => (string)key.GetValue(keyName, string.Empty);
    }
}
