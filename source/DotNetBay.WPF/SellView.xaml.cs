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
using DotNetBay.Data.Entity;
using Microsoft.Win32;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window
    {
        public string ImageFileName { get; set; }

        public SellView()
        {
            InitializeComponent();
            ImageFileName = "...";
            DataContext = this;
        }

        private void AddImageButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImageFileName = openFileDialog.FileName;
            }
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void SellClick(object sender, RoutedEventArgs e) { 
            if (TitleBox.Text == "")
            {
                Console.WriteLine("No Text");
                return;
            }

            if (DescriptionBox.Text == "")
            {
                Console.WriteLine("No Text");
                return;
            }


            double price = Double.Parse(PriceBox.Text);
            if (price <= 0)
            {
                Console.WriteLine("Price must be greater than 0");
                return;
            }


            DateTime start;
            if (DateTime.TryParse(StartDate.Text, out start))
            {
                if (start>=DateTime.Now)
                {
                    Console.WriteLine("Can't pick a date in the past");
                    return;
                }
            }
            

            DateTime end;
            if (DateTime.TryParse(EndDate.Text, out end))
            {
                if (end<DateTime.Now)
                {
                    Console.WriteLine("Can't pick a date in the past");
                    return;
                }

                if (end <= start)
                {
                    Console.WriteLine("Can't pick a date earlier than the StartDate");
                    return;
                }
            }

            //Prüfe Bild 

            var auction = new Auction
            {
                Title = TitleBox.Text,
                Description = DescriptionBox.Text,
                StartPrice = price,
                StartDateTimeUtc = start,
                EndDateTimeUtc = end,
                Image = null
            };

            (Application.Current as App).AuctionService.Save(auction);

            this.Close();
}


        }
    }

