using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DotNetBay.Core;
using DotNetBay.Data.Entity;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();
        private IAuctionService auctionService;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<Auction> Auctions
        {
            get
            {
                this.auctionService = new AuctionService(App.MainRepository, new SimpleMemberService(App.MainRepository));
                this.auctions = new ObservableCollection<Auction>(this.auctionService.GetAll());
                return auctions;
            }
        }

        private void AuctionsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SellButtonClick(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog(); // Blocking
        }
    }
}
