using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;

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

            // 白枠を追加する
            frame.BorderBrush = new SolidColorBrush(Colors.White);
            frame.BorderThickness = new Thickness(15);

            // 傾ける
            RotateTransform rotate = new RotateTransform();
            rotate.Angle = 5;
            imgView.RenderTransformOrigin = new Point(0.5, 0.5);
            imgView.RenderTransform = rotate;

            // 画像の重ね合わせ
            imgPicture.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            task.PixelWidth = 440;
            task.PixelHeight = 440;
            task.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Grid から Jpeg 保存用ストリームを作成
            WriteableBitmap wp = new WriteableBitmap(imgGrid, null);
            MemoryStream stream = new MemoryStream();
            wp.SaveJpeg(stream, wp.PixelWidth, wp.PixelHeight, 0, 100);

            // PictureHub に保存する
            using (MediaLibrary lib = new MediaLibrary()) {
                lib.SavePicture("PhotoEffect-" + DateTime.Now.ToString("yyyyMMddhhmmss"), stream.ToArray());
                MessageBox.Show("保存しました");
            }
        }

        private void mnuVersion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PhotoEffect Version 1.0");
        }
    }
}