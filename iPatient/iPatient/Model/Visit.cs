﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.Model
{
    public class Visit
    {
        public string Time { get; set; }
        public bool isAvailable { get; set; }
        public bool isUserVisit { get; set; }

        public Color Color
        {
            get
            {
                return isAvailable ? Color.FromHex("#11324D") : (isUserVisit ? Color.FromHex("#ff922b") : Color.FromHex("#adc9c1"));
            }
        }
    }
}
