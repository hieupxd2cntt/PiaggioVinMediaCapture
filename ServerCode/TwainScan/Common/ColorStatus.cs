using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TwainScan
{
    public class ListColor
    {
        private List<ValueColor> _Colors { get; set; }
        public List<ValueColor> Colors { get { return _Colors; } }
        public ListColor(int type)
        {
            _Colors = new List<ValueColor>();
            switch(type){
                case 1://Load StatusUser
                    LoadColorStatusUser();
                    break;
                case 2://Load Status Báo cáo
                    LoadColorStatusReport();
                    break;
            }
            
        }
        public void LoadColorStatusUser()
        {
            _Colors.Add(new ValueColor { Value = 1, color = Color.Black });
            _Colors.Add(new ValueColor { Value = 2, color = Color.Red });
        }
        public void LoadColorStatusReport()
        {
            _Colors.Add(new ValueColor { Value = 1, color = Color.Black });
            _Colors.Add(new ValueColor { Value = 2, color = Color.Green });
            _Colors.Add(new ValueColor { Value = 3, color = Color.Red });
            _Colors.Add(new ValueColor { Value = 5, color = Color.Blue });
        }
    }
    public class ValueColor
    {
        public int Value { get; set; }
        public Color color { get; set; }
    }
}
