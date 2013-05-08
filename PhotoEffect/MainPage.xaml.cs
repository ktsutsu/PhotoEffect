using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace PhotoEffect
{
    public partial class MainPage : PhoneApplicationPage
    {
        PhotoChooserTask task = new PhotoChooserTask();

        // コンストラクター
        public MainPage()
        {
            InitializeComponent();
            task.Completed += new EventHandler<PhotoResult>(task_Completed);
            task.ShowCamera = true;
        }

        void task_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK) {
                return;
            }

            // 選択した画像をimageコントロールに表示する
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(e.ChosenPhoto);
            imgPhoto.Source = bmp;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            task.PixelWidth = 440;
            task.PixelHeight = 440;
            task.Show();
        }
    }
}