using Repository.Entities;
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
using System.Windows.Shapes;

namespace PE_PRN211_SP23_TrialTest_PhamVanTuanHieu
{
    /// <summary>
    /// Interaction logic for StudentDetail.xaml
    /// </summary>
    public partial class StudentDetail : Window
    {
        public Student _selected = null;
        public StudentDetail()
        {
            InitializeComponent();
        }
    }
}
