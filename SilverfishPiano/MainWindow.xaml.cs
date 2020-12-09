using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using NAudio.Wave;

namespace SilverfishPiano
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        DispatcherTimer[] timers = new DispatcherTimer[8];
        Timer[] timers2 = new Timer[8];
        private void SoundButton_Click(object sender, EventArgs e)
        {
            Stream stream = new MemoryStream();
            var button = sender as Button;
            switch (button.Name)
            {
                case "One":
                    Properties.Resources.SilverfishDeath1.CopyTo(stream);
                    break;
                case "Two":
                    Properties.Resources.SilverfishHurt1.CopyTo(stream);
                    break;
                case "Three":
                    Properties.Resources.SilverfishHurt2.CopyTo(stream);
                    break;
                case "Four":
                    Properties.Resources.SilverfishHurt3.CopyTo(stream);
                    break;
                case "Five":
                    Properties.Resources.SilverfishIdle1.CopyTo(stream);
                    break;
                case "Six":
                    Properties.Resources.SilverfishIdle2.CopyTo(stream);
                    break;
                case "Seven":
                    Properties.Resources.SilverfishIdle3.CopyTo(stream);
                    break;
                case "Eight":
                    Properties.Resources.SilverfishIdle4.CopyTo(stream);
                    break;

            }
            PlaySound(stream);


        }
        void PlaySound(Stream stream)
        {
            stream.Position = 0;
            WaveFileReader reader = new WaveFileReader(stream);
            LoopStream wavSong = new LoopStream(reader);
            wavSong.EnableLooping = false;
            WaveOut player = new WaveOut();
            player.Init(wavSong);
            player.Play();
        }
        //void StartPlayAudio(string name, bool loop)
        //{
        //    if(!AudioPlayers.ContainsKey(name))
        //        AudioPlayers.Add(name, new WaveOut());
        //    LoopStream wavSong = new LoopStream(reader);
        //    wavSong.EnableLooping = loop;
        //    AudioPlayers[name] = new WaveOut();
        //    AudioPlayers[name].Init(wavSong);
        //    AudioPlayers[name].Play();  
        //}
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.NumPad1:
                    SoundButton_Click(One, null);
                    break;
                case Key.A:
                    goto case Key.NumPad1;

                case Key.NumPad2:
                    SoundButton_Click(Two, null);
                    break;
                case Key.S:
                    goto case Key.NumPad2;

                case Key.NumPad3:
                    SoundButton_Click(Three, null);
                    break;
                case Key.D:
                    goto case Key.NumPad3;

                case Key.NumPad4:
                    SoundButton_Click(Four, null);
                    break;
                case Key.F:
                    goto case Key.NumPad4;

                case Key.NumPad5:
                    SoundButton_Click(Five, null);
                    break;
                case Key.G:
                    goto case Key.NumPad5;

                case Key.NumPad6:
                    SoundButton_Click(Six, null);
                    break;
                case Key.H:
                    goto case Key.NumPad6;

                case Key.NumPad7:
                    SoundButton_Click(Seven, null);
                    break;
                case Key.J:
                    goto case Key.NumPad7;

                case Key.NumPad8:
                    SoundButton_Click(Eight, null);
                    break;
                case Key.K:
                    goto case Key.NumPad8;


                default:
                    return;

            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            void ConfigTimer(int Id, Button but)
            {
                if((int)e.NewValue == 0)
                {
                    timers[Id].Stop();
                    return;
                }
                if(timers[Id] == null)
                    {
                    timers[Id] = new DispatcherTimer();
                    timers[Id].Tick += (o, e) => SoundButton_Click(but, null);
                }
                timers[Id].Interval = new TimeSpan(0, 0, 0, 0, (int)e.NewValue);
                timers[Id].Start();
            }
            switch (slider.Name)
            {
                case "OneSlider":
                    ConfigTimer(0, One);
                    break;
                case "TwoSlider":
                    ConfigTimer(1, Two);
                    break;
                case "ThreeSlider":
                    ConfigTimer(2, Three);
                    break;
                case "FourSlider":
                    ConfigTimer(3, Four);
                    break;
                case "FiveSlider":
                    ConfigTimer(4, Five);
                    break;
                case "SixSlider":
                    ConfigTimer(5, Six);
                    break;
                case "SevenSlider":
                    ConfigTimer(6, Seven);
                    break;
                case "EightSlider":
                    ConfigTimer(7, Eight);
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Жми кливиши с Num0 до Num 8 или c A до K  чтобы извлечь звук. "+
                            "Двигай ползунки чтобы задать бит. Чтобы отключить бит двигай ползунок в начальное положение", "Управление", MessageBoxButton.OK);
        }
    }
}
