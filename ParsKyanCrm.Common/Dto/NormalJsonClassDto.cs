using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsKyanCrm.Common.Dto
{
    public class NormalJsonClassDto
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
        public string Group { get; set; }
        public string LabelGroup { get; set; }

        public string Link { get; set; }

        public string Icon { get; set; }

        public string Order { get; set; }

        public string Group_Item { get; set; }
    }
}
