using PANS.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace PANS.UI
{
    /// <summary>
    /// Interaction logic for FunctionGraphControl.xaml
    /// </summary>
    public partial class FunctionGraphControl : UserControl
    {
        private List<GraphPoint> _coordinates { get; set; }

        public FunctionGraphControl()
        {
            InitializeComponent();
            _coordinates = new List<GraphPoint>(5)
            {
                new GraphPoint(0, 0),
                new GraphPoint(0, 0),
                new GraphPoint(0, 0),
                new GraphPoint(0, 0),
                new GraphPoint(0, 0)
            };
        }

        private void RefreshGraph()
        {
            Plot.Plot.Clear();
            Plot.Plot.AddScatter(_coordinates.Select(x => x.X).ToArray(), _coordinates.Select(x => x.Y).ToArray());
            Plot.Refresh();
        }

        private void X1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetX(sender as TextBox, 0);
        }

   
        private void X2_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetX(sender as TextBox, 1);
        }
        private void X3_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetX(sender as TextBox, 2);
        }
        private void X4_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetX(sender as TextBox, 3);
        }
        private void X5_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetX(sender as TextBox, 4);
        }
        private void Y1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetY(sender as TextBox, 0);
        }
        private void Y2_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetY(sender as TextBox, 1);
        }
        private void Y3_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetY(sender as TextBox, 2);
        }
        private void Y4_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetY(sender as TextBox, 3);
        }
        private void Y5_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SetY(sender as TextBox, 4);
        }
        private void SetX(TextBox tbx, int index)
        {
            if (string.IsNullOrEmpty(tbx.Text))
            {
                return;
            }

            var isNumeric = double.TryParse(tbx.Text, out double n);

            if (isNumeric)
            {
                _coordinates[index].X = n;
            }
            else
            {
                tbx.Text = "Err";
            }

            RefreshGraph();
        }


        private void SetY(TextBox tbx, int index)
        {
            if (string.IsNullOrEmpty(tbx.Text))
            {
                return;
            }

            var isNumeric = double.TryParse(tbx.Text, out double n);

            if (isNumeric)
            {
                _coordinates[index].Y = n;
            }
            else
            {
                tbx.Text = "Err";
            }

            RefreshGraph();
        }
    }
}
