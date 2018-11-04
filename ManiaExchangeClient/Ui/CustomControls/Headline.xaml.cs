using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManiaExchangeClient.Ui.CustomControls
{
    /// <summary>
    /// Interaction logic for Headline.xaml
    /// </summary>
    public partial class Headline : UserControl
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Headline"/>
        /// </summary>
        public Headline()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The background color property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(
            nameof(BackgroundColor), typeof(SolidColorBrush), typeof(Headline), new PropertyMetadata(default(SolidColorBrush)));

        /// <summary>
        /// Gets or sets the background color
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get => (SolidColorBrush)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// The header text property
        /// </summary>
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register(
            nameof(HeaderText), typeof(string), typeof(Headline), new PropertyMetadata("Header text"));

        /// <summary>
        /// Gets or sets the header text
        /// </summary>
        public string HeaderText
        {
            get => (string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly DependencyProperty HeaderTextFontSizeProperty = DependencyProperty.Register(
            nameof(HeaderTextFontSize), typeof(int), typeof(Headline), new PropertyMetadata(14));

        /// <summary>
        /// Gets or sets the font size
        /// </summary>
        public int HeaderTextFontSize
        {
            get => (int)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// The border color property
        /// </summary>
        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register(
            nameof(BorderColor), typeof(SolidColorBrush), typeof(Headline), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

        /// <summary>
        /// Gets or sets the border color
        /// </summary>
        public SolidColorBrush BorderColor
        {
            get => (SolidColorBrush)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
    }
}
