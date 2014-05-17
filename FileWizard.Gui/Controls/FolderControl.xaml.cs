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
using Forms = System.Windows.Forms;

namespace FileWizard.Gui.Controls
{
    /// <summary>
    /// Interaction logic for FolderControl.xaml
    /// </summary>
    public partial class FolderControl : UserControl
    {
        public FolderControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty FolderPathProperty = DependencyProperty.Register(
            "FolderPath", typeof(string), typeof(FolderControl), 
            new FrameworkPropertyMetadata(){BindsTwoWayByDefault = true});

        public string FolderPath
        {
            get { return (string)GetValue(FolderPathProperty); }
            set { SetValue(FolderPathProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Had to use Windows.Forms folder browser dialog because no external libraries are allowed
            //and WPF doesn not have built in Folder Dialog.
            using (var openFolderDialog = new Forms.FolderBrowserDialog())
            {
                var result = openFolderDialog.ShowDialog();
                if (result == Forms.DialogResult.Cancel)
                    return;

                FolderPath = openFolderDialog.SelectedPath;
            }
        }
    }
}
