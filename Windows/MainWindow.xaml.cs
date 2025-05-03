using ExamShvets.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace ExamShvets.Windows
{
    public partial class MainWindow : Window
    {
        private readonly ExamShvetsContext _context = new();

        public MainWindow()
        {
            InitializeComponent();
            LoadPartners();
        }

        private void LoadPartners()
        {
            List<PartnerDTO> partnerDTOs = new();

            var partnerProducts = _context.PartnersImports
                .AsNoTracking()
                .Include(p => p.PartnerProductsImports)
                .Include(p => p.PartnerType)
                .ToList();

            foreach (var part in partnerProducts)
            {
                int totalSales = part.PartnerProductsImports.Select(p => p.Count).Sum();
                partnerDTOs.Add(new PartnerDTO
                {
                    Id = part.PartnerId,
                    Phone = part.PhonePartner,
                    Name = part.Title,
                    Position = part.Director,
                    Rating = part.Rating,
                    Type = part.PartnerType.Title,
                    Email = part.EmailPartner,
                    Address = part.AddressPartner,
                    Discount = CalculateDiscount(totalSales)
                });
            }

            PartnersListBox.ItemsSource = null; // Сброс перед установкой
            PartnersListBox.ItemsSource = partnerDTOs;
        }

        public static int CalculateDiscount(int sales)
        {
            return sales switch
            {
                < 10000 => 0,
                < 50000 => 5,
                < 300_000 => 10,
                _ => 15
            };
        }
        
        private void OpenAddForm(object sender, RoutedEventArgs e)
        {
            AddPartnerWindow partnerWindow = new AddPartnerWindow();
            bool? result = partnerWindow.ShowDialog();
            if(result == true)
            {
                LoadPartners();
            }
        }

      

        private void OpenUpdateForm(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(PartnersListBox.SelectedItem is PartnerDTO partnerDTO)
            {
                AddPartnerWindow addPartnerWindow = new AddPartnerWindow(partnerDTO);
                if (addPartnerWindow.ShowDialog() == true)
                {
                    LoadPartners();
                }

            }

        }
    }

   
}
