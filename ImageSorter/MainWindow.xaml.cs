using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.VisualBasic.FileIO;
namespace ImageSorter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        String imgPath;
        Uri uri;
        BitmapImage bitmap;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void btnOpFileClick_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "*.jpg,*.jpeg,*.bmp,*.jif,*.ico,*.png,*.tif,*.wmf|*.jpg;*.jpeg;*.bmp;*.gif;*.ico;*.png;*.tif;*.wmf";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //set the image you selected as cover
                imgPath = openFileDialog1.FileName;
                uri = new Uri(imgPath, UriKind.Absolute);
                bitmap = new BitmapImage(uri);
                imgDisPic.Source = bitmap;
                txtOfDic.Text = "Image Url:" + imgPath;
                string strOfimgDisPath = System.IO.Path.GetDirectoryName(imgPath);
                txtOfDicPath.Text = "Image Path:" + strOfimgDisPath;
            }

        }

        private void btnSortStart_Click(object sender, RoutedEventArgs e)
        {
            // Verify imagepath is not null 
            if (imgPath != null)
            {
                string imgDi = System.IO.Path.GetDirectoryName(imgPath);
                DirectoryInfo imgF = new DirectoryInfo(imgDi);
                int strOfflagOfCped = 0;
                int strOfFlagOfHor = 0;
                int strOfFlagOfVer = 0;
                Boolean boolOfIsImage;
                foreach (FileInfo NextFile in imgF.GetFiles())
                {
                    boolOfIsImage = ImageAction.IsNotImage(NextFile.FullName);
                    if (boolOfIsImage == true)
                    {
                        //flag of all number
                        strOfflagOfCped++;
                        //accent this file's fullname
                        string filename = NextFile.FullName;
                        //now this file's directory
                        string strsourcepath = NextFile.DirectoryName;
                        //redefine uri for bitmap to use
                        uri = new Uri(filename);
                        //destnation Hor image dict 
                        string strHortargetPath = strsourcepath + "\\Hor";
                        //destnation Vertical image dict 
                        string strVertargetPath = strsourcepath + "\\Ver";
                        //destination of hor file
                        string strHorDestFile = System.IO.Path.Combine(strHortargetPath, NextFile.Name);
                        //destination of vertical file
                        string strVerDestFile = System.IO.Path.Combine(strVertargetPath, NextFile.Name);
                        //new a bitmap decoder to get the height and weivght of picture 
                        if (ImageAction.IsHorOrNot(uri))
                        //sort horizantal picture
                        {
                            //make progressbar can be seen
                            // pgb.Visibility = System.Windows.Visibility.Visible;
                            // verify directoty is no exist
                            if (System.IO.Directory.Exists(strHortargetPath))
                            {
                                //verify file is not exist
                                if (System.IO.File.Exists(strHorDestFile) == false)
                                {
                                    System.IO.File.Copy(filename, strHorDestFile);
                                    //set copied horizantal picture flag and view num
                                    strOfFlagOfHor++;
                                    txtOfNumOfHor.Text = "Horizontal Picture Number:" + strOfFlagOfHor.ToString();
                                }

                            }
                            else
                            {
                                //without directoty,with file,so here don't need to verify file exist
                                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(NextFile.DirectoryName);
                                System.IO.Directory.CreateDirectory(strHortargetPath);
                                System.IO.File.Copy(filename, strHorDestFile);
                                strOfFlagOfHor++;
                                txtOfNumOfHor.Text = "Horizontal Picture Number:" + strOfFlagOfHor.ToString();
                            }
                        }
                        else
                        //sort vertical picture
                        {
                            if (System.IO.Directory.Exists(strVertargetPath))
                            {
                                if (System.IO.File.Exists(strVerDestFile) == false)
                                {
                                    System.IO.File.Copy(filename, strVerDestFile);
                                    strOfFlagOfVer++;
                                    txtOfNumOfVer.Text = "Vertical Picture Number:" + strOfFlagOfVer.ToString();
                                }

                            }
                            else
                            {
                                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(NextFile.DirectoryName);
                                System.IO.Directory.CreateDirectory(strVertargetPath);
                                System.IO.File.Copy(filename, strVerDestFile);
                                strOfFlagOfVer++;
                                txtOfNumOfVer.Text = "Vertical Picture Number:" + strOfFlagOfVer.ToString();
                            }
                        }
                    }

                }
                //set all sorted picture num and view num
                txtOfNumOfCp.Text = "Already Sorted:" + strOfflagOfCped.ToString();

            }
            else
            {
                MessageBox.Show("Please Select Image Path!","Not Select Path");
            }
        }
    }
}
