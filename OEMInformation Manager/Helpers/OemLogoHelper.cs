using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace OEMInformation_Manager.Helpers
{
    public static class OemLogoHelper
    {
        private const string CopiedOemLogoName = "OemLogo.bmp";
        private const string ResourcesFolderName = "Resources";

        private static readonly string SpecialFolderName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        /// <summary>Copy OEM logo to <see cref="SpecialFolderName"/> folder</summary>
        /// <param name="currentLogoName"></param>
        /// <returns>output OEM logo path</returns>
        public static string Copy(string currentLogoName)
        {
            var outputFileName = GetOutputFileName();

            if (!outputFileName.PathEquals(currentLogoName))
            {
                try
                {
                    File.Copy(currentLogoName, outputFileName, true);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

            return outputFileName;
        }

        public static string GetDefaultLogoName()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), ResourcesFolderName, ConfigurationManager.AppSettings["Logo"]);
        }

        private static string GetOutputFileName()
        {
            return Path.Combine(SpecialFolderName, CopiedOemLogoName);
        }
    }
}
