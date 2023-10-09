using Data.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities.Setup
{
    public class AboutUs : BaseEntity
    {
        public string MainTitle { get; set; }
        public string FirstMainSection { get; set; }
        public string SecondMainSection { get; set; }
        public string ThirdMainSection { get; set; }

        public string SecondTitle { get; set; }
        public string SecondTitleSection { get; set; }

        public string ThirdTitle { get; set; }
        public string ThirdTitleSection { get; set; }
    }
}
