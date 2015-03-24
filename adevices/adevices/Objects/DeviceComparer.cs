using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using adbdevices;
namespace adevices
{
    public class DeviceComparer : IComparer
    {
        Comparer _comparer = new Comparer(System.Globalization.CultureInfo.CurrentCulture);

        public int Compare(object x, object y)
        {
            // Convert string comparisons to int
            return _comparer.Compare(((AdbDevice)x).SerialNumber, ((AdbDevice)y).SerialNumber);
        }
    }
}
