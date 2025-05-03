using ExamShvets.Models;

using Microsoft.IdentityModel.Tokens;

using System.Windows;

namespace ExamShvets.Windows
{
    public partial class AddPartnerWindow : Window
    {
        private readonly ExamShvetsContext _context = new();
        private PartnerDTO _partnerDTO;

        public AddPartnerWindow()
        {
            InitializeComponent();
            LoadTypes();
            Title = "Добавление партнера";
        }
        public AddPartnerWindow(PartnerDTO partnerDTO)
        {
            InitializeComponent();
            LoadTypes();
            FillFields(partnerDTO);
            add.Content = "Изменить";
            this.Title = "Изменение партнера";
            _partnerDTO = partnerDTO;
        }

        private void FillFields(PartnerDTO partnerDTO)
        {
            name.Text = partnerDTO.Name;
            rating.Text = partnerDTO.Rating.ToString();
            adress.Text = partnerDTO.Address;
            directorName.Text = partnerDTO.Position;
            phoneNumber.Text = partnerDTO.Phone;
            email.Text = partnerDTO.Email;
            var allTypes = type.ItemsSource as List<PartnerTypeImport>;
            type.SelectedItem = allTypes.Where(t => t.Title == partnerDTO.Type).FirstOrDefault();

        }

        private void LoadTypes()
        {
            // Загружаем список типов партнёров
            type.ItemsSource = _context.PartnerTypeImports.ToList();
            type.SelectedIndex = 0;
        }

        private void AddOrEditPartner(object sender, RoutedEventArgs e)
        {
            // Простейшая валидация: убедимся, что выбрали тип и ввели обязательные поля
            if (type.SelectedItem is not PartnerTypeImport selectedType)
            {
                MessageBox.Show("Пожалуйста, выберите тип партнёра.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateString("Наименование организации", name.Text) ||
                !ValidateRating(rating.Text) ||
                !ValidateString("Адрес",adress.Text) ||
                !ValidateName(directorName.Text) ||
                !ValidatePhone(phoneNumber.Text) ||
                !ValidateEmail(email.Text))
            {
               
                return;
            }

            try
            {
                // Создаём новую сущность партнёра
                var newPartner = new PartnersImport
                {

                    Title = name.Text.Trim(),
                    PartnerTypeId = selectedType.PartnerTypeId,
                    Rating = int.Parse(rating.Text),
                    AddressPartner = adress.Text.Trim(),
                    Director = directorName.Text.Trim(),
                    PhonePartner = phoneNumber.Text.Trim(),
                    EmailPartner = string.IsNullOrWhiteSpace(email.Text) ? null : email.Text.Trim()
                };
                if (_partnerDTO != null)
                {

               
                    var partner = _context.PartnersImports.Where(p => p.PartnerId == _partnerDTO.Id).FirstOrDefault();
                    if(partner != null)
                    {
                        partner.PartnerId = _partnerDTO.Id;
                        partner.AddressPartner = adress.Text.Trim();
                        partner.Director = directorName.Text;
                        partner.PhonePartner = phoneNumber.Text;
                        partner.Title = name.Text.Trim();
                        partner.PartnerTypeId = selectedType.PartnerTypeId;
                        partner.EmailPartner = email.Text.Trim();
                        _context.PartnersImports.Update(partner);
                        _context.SaveChanges();

                        MessageBox.Show("Партнёр успешно обновлен.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    newPartner.PartnerId = _context.PartnersImports.Count() + 2;
                    _context.PartnersImports.Add(newPartner);
                    _context.SaveChanges();
                    MessageBox.Show("Партнёр успешно обновлен.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                // Закрываем окно
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

          
        }
        private bool ValidateEmail(string email)
        {
            if (email.IsNullOrEmpty())
            {
                MessageBox.Show("Email пуст","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
            if (!email.Contains("@"))
            {
                MessageBox.Show("Email должен содержать @", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool ValidateString(string fieldName, string text)
        {
            if (text.IsNullOrEmpty())
            {
                MessageBox.Show($"Поле {fieldName} пустое", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private bool ValidateName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                MessageBox.Show("Имя пустое", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            bool nameContainsNumber = name.Where(p=>char.IsNumber(p)).Any();
            if (nameContainsNumber)
            {
                MessageBox.Show("Имя не должно содержать цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }

        private bool ValidateRating(string rating)
        {
            if(!int.TryParse(rating,out int  ratingNumber))
            {
                MessageBox.Show("Рейтинг не является числом", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(ratingNumber < 0)
            {
                MessageBox.Show("Рейтинг не может быть меньше нуля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool ValidatePhone(string phone)
        {
            if(phone.IsNullOrEmpty())
            {
                MessageBox.Show("Поле номер телефона пустое", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (phone.Where (p => char.IsLetter(p)).Any())
            {
                MessageBox.Show("Поле номер телефона не должны содержать буквы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!phone.StartsWith("+7"))
            {
                MessageBox.Show("Номер телефона должен начинаться с +7", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


    }
}
