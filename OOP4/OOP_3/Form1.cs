using AutoMapper;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OOP_3
{
    public partial class Form1 : Form
    {
        public ApplicationDbContext context;
        public User user;
        public Mapper mapper;

        private string userName = "";
        private string error = "";

        string FileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var categorysList = context.Categorys.ToList();
            KategorijosPasirinkimoComboBox.Items.Add("Select one");
            foreach (var item in categorysList)
            {
                KategorijosPasirinkimoComboBox.Items.Add(item.CategoryName);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            context = new ApplicationDbContext();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UsersList>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ReverseMap();
            });

            mapper = new Mapper(config);

            LoadData();
            LoadPrekiuGrid();
        }
        private void Login()
        {

            if (loginTextBox.Text != "")
            {
                userName = loginTextBox.Text;
                MessageBox.Show("login");
            }
            else
            {
                throw new Exception("error,please enter userName"); //throw exception            
            }

        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            //string Username = loginTextBox.Text;
            //string UserPassword = Password.Text;
            //bool AdminButton = AdminRadioButton.Checked;

            //dataGridView1.Rows.Add("ABC", "25");
            //var a  = dataGridView1.SelectedRows;

            if (string.IsNullOrEmpty(loginTextBox.Text) || string.IsNullOrEmpty(Password.Text))
            {
                MessageBox.Show("Neteisingai ivesta informacija.");
            }

            var user = context.Users.AsNoTracking().Include(x => x.WishList).Where(a => a.Username.Equals(loginTextBox.Text) && a.Password.Equals(Password.Text))
                .SingleOrDefault();

            if (user != null)
            {
                this.user = user;

                if (user.UserType == UserType.Admin)
                {
                    MessageBox.Show("Prisijunget kaip administratorius");
                    ProfileVisibility();
                    AdministratorVisibility();
                }
                else if (user.UserType == UserType.Accountant)
                {
                    MessageBox.Show("Prisijunget kaip finansininkas");
                    ProfileVisibility();
                    FinansinikasVisibility();
                    FinansininkoGridLoad();
                }
                else if (user.UserType == UserType.User)
                {
                    MessageBox.Show("Prisijunget kaip vartotojas");
                    ProfileVisibility();
                    UserVisibility();
                }
            }
            else
            {
                MessageBox.Show("Si paskyra neegzistuoja");
            }

        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            RegistrationVisibility();
        }
        private void RegistrationVisibility()
        {
            LoginButton.Visible = false;
            FirstName.Visible = true;
            FirstNameInput.Visible = true;
            LastName.Visible = true;
            LastNameInput.Visible = true;
            Birthday.Visible = true;
            BirthdayPicker.Visible = true;
            SubmitButton.Visible = true;
            RegisterButton.Visible = true;
            BackButton.Visible = true;
            RegisterButton.Visible = false;
            radioButtonsPanel.Visible = true;
            VarotojuInformacija.Visible = false;
            PrekiuSarasoPanel.Visible = false;
            IsimintinuPrekiuSarasoButton.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            ApsipirkimuButtonFinansininko.Visible = false;
        }
        private void AdministratorVisibility()
        {
            AdminText.Visible = true;
            VarotojuInformacija.Visible = true;
            PrekesKategorijosButton.Visible = true;
            PridetiPrekeButton.Visible = true;
            PrekiuSarasoPanel.Visible = false;
            IsimintinuPrekiuSarasoButton.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            ApsipirkimuButtonFinansininko.Visible = false;
        }

        private void UserVisibility()
        {
            VartotojasForm.Visible = true;
            VarotojuInformacija.Visible = false;
            PrekiuSarasoPanel.Visible = true;
            IsimintinuPrekiuSarasoButton.Visible = true;
            PrekiuKrepselisPanel.Visible = false;
            PrekiuKrepselisButton.Visible = true;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            ApsipirkimuIstorijaButton.Visible = true;
            ApsipirkimuButtonFinansininko.Visible = false;
        }

        private void FinansinikasVisibility()
        {
            AdminText.Visible = false;
            VarotojuInformacija.Visible = true;
            PrekesKategorijosButton.Visible = true;
            PridetiPrekeButton.Visible = true;
            PrekiuSarasoPanel.Visible = false;
            IsimintinuPrekiuSarasoButton.Visible = false;
            AccountantText.Visible = true;
            VartotojasForm.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            PrekiuSarasasButton.Visible = false;
            PridetiPrekeButton.Visible = false;
            PrekesKategorijosButton.Visible = false;
            VarotojuInformacija.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            ApsipirkimuButtonFinansininko.Visible = true;
        }

        private void ProfileVisibility()
        {
            UsernameLabel.Visible = false;
            PasswordLabel.Visible = false;
            UserButton.Visible = false;
            AdminButton.Visible = false;
            AccountantButton.Visible = false;
            loginTextBox.Visible = false;
            Password.Visible = false;
            LoginButton.Visible = false;
            FirstName.Visible = false;
            FirstNameInput.Visible = false;
            LastName.Visible = false;
            LastNameInput.Visible = false;
            Birthday.Visible = false;
            BirthdayPicker.Visible = false;
            SubmitButton.Visible = false;
            RegisterButton.Visible = false;
            BackButton.Visible = true;
            RegisterButton.Visible = false;
            ProfilisButton.Visible = true;
            radioButtonsPanel.Visible = false;
            VarotojuInformacija.Visible = false;
            PrekiuSarasoPanel.Visible = false;
            WhiteListPanel.Visible = false;
            IsimintinuPrekiuSarasoButton.Visible = false;
            PrekiuSarasasButton.Visible = true;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            ApsipirkimuButtonFinansininko.Visible = false;
        }

        private void BackVisibility()
        {
            UsernameLabel.Visible = true;
            PasswordLabel.Visible = true;
            UserButton.Visible = true;
            AdminButton.Visible = true;
            AccountantButton.Visible = true;
            loginTextBox.Visible = true;
            Password.Visible = true;
            LoginButton.Visible = true;
            FirstName.Visible = false;
            FirstNameInput.Visible = false;
            LastName.Visible = false;
            LastNameInput.Visible = false;
            Birthday.Visible = false;
            BirthdayPicker.Visible = false;
            SubmitButton.Visible = false;
            RegisterButton.Visible = false;
            BackButton.Visible = false;
            RegisterButton.Visible = true;
            VartotojasForm.Visible = false;
            AdminText.Visible = false;
            ProfilisButton.Visible = false;
            radioButtonsPanel.Visible = false;
            ProfilioRedagavimoPanele.Visible = false;
            VarotojuInformacija.Visible = false;
            VartotojuSarasoPanele.Visible = false;
            PrekesKategorijosPanele.Visible = false;
            PridetiPrekeButton.Visible = false;
            PrekiuSarasoPanel.Visible = false;
            WhiteListPanel.Visible = false;
            IsimintinuPrekiuSarasoButton.Visible = false;
            AccountantText.Visible = false;
            PrekesKategorijosButton.Visible = false;
            PrekesIdejimoPanel.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            PrekiuKrepselisButton.Visible = false;
            PrekiuSarasasButton.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            ApsipirkimuIstorijaButton.Visible = false;
            ApsipirkimuButtonFinansininko.Visible = false;
            FinansinikasApsipirkimuIstorijaPanel.Visible = false;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            int age = DateTime.Today.Year - BirthdayPicker.Value.Year;

            if (string.IsNullOrWhiteSpace(loginTextBox.Text) || string.IsNullOrWhiteSpace(Password.Text) || string.IsNullOrWhiteSpace(FirstNameInput.Text) || string.IsNullOrWhiteSpace(LastNameInput.Text))
            {
                MessageBox.Show("Laukas neivestas!");
                return;
            }
            if (age <= 14)
            {
                MessageBox.Show(age.ToString());
                return;
            }

            var user = new User
            { //init object
                FirstName = FirstNameInput.Text,
                LastName = LastNameInput.Text,
                Birthddate = BirthdayPicker.Value,
                Password = Password.Text,
                Username = loginTextBox.Text
            };

            var a = radioButtonsPanel.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (a == null)// C# HELLO ???
            {
                MessageBox.Show("Nepasirinktas tipas");
                return;
            }

            switch (a.Name)
            {
                case "AdminButton":
                    user.UserType = UserType.Admin;
                    break;

                case "UserButton":
                    user.UserType = UserType.User;
                    break;

                case "AccountantButton":
                    user.UserType = UserType.Accountant;
                    break;

                default:
                    break;
            }

            AddUser(user);

            MessageBox.Show("Registracija sekminga");

            BackVisibility();
        }

        private void AddUser(User user)
        {
            var users = context.Users.AsNoTracking().FirstOrDefault(x => x.FirstName == user.FirstName && x.LastName == user.LastName);

            if (users != null)
            {
                MessageBox.Show("User allready exist in db");
                return;
            }

            context.Users.Attach(user);
            context.SaveChanges();
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            BackVisibility();
            this.user = null;
            loginTextBox.Text = "";
            Password.Text = "";
            //AdminCheckbox.Checked = false;
        }

        private void ProfilisButton_Click(object sender, EventArgs e)
        {
            ProfilioRedagavimoPanele.Visible = true;
            PrekiuSarasoPanel.Visible = false;
            PrekesIdejimoPanel.Visible = false;
            PrekesKategorijosPanele.Visible = false;
            VartotojuSarasoPanele.Visible = false;
            WhiteListPanel.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
        }

        private void PakeistiSlaptazodiMygtukas_Click(object sender, EventArgs e)
        {
            ProfilioRedagavimoPanele.Visible = false;
            var user = context.Users.SingleOrDefault(x => x.Id == this.user.Id);

            if (ProfilioPaneleDabartinisPw.Text != user.Password)
            {
                MessageBox.Show("Neteisingas slaptazodis");
                return;
            }

            user.Password = ProfilioPanelNaujasPw.Text;

            context.Users.Update(user);
            context.SaveChanges();

            MessageBox.Show("Slaptazodis pakeistas");
        }

        private void VarotojuInformacija_Click(object sender, EventArgs e)
        {
            VartotojuSarasoPanele.Visible = true;
            ProfilioRedagavimoPanele.Visible = true;
            PrekiuSarasoPanel.Visible = false;
            PrekesIdejimoPanel.Visible = false;
            PrekesKategorijosPanele.Visible = false;
            WhiteListPanel.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            LoadGrid();
        }
        private void LoadGrid()
        {
            var user = context.Users.ToList();

            List<User> list = context.Users.AsNoTracking().ToList();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UsersList>().ReverseMap();
            });

            var mapper = new Mapper(config);

            dataGridView2.DataSource = mapper.Map<List<UsersList>>(user);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select one row.");
                return;
            }

            var row = dataGridView2.SelectedRows[0];
            var userId = row.Cells["Id"].Value;

            var user = context.Users.Where(x => x.Id == (int)userId).SingleOrDefault();

            if (user == null)
            {
                MessageBox.Show("user doesn't exist");
                return;
            }

            context.Users.Remove(user);
            context.SaveChanges();

            LoadGrid();
            VartotojuSarasoPanele.Visible = false;
        }
        private void PrekesKategorijosButton_Click(object sender, EventArgs e)
        {
            ProfilioRedagavimoPanele.Visible = true;
            PrekiuSarasoPanel.Visible = false;
            PrekesIdejimoPanel.Visible = false;
            VartotojuSarasoPanele.Visible = false;
            WhiteListPanel.Visible = false;
            PrekesKategorijosPanele.Visible = true;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
        }

        private void PrekesKategorijosIvedimoButton_Click(object sender, EventArgs e)
        {
            //PrekesKategorijosInput ivedimo lauko pavadinimas
            if (string.IsNullOrEmpty(PrekesKategorijosInput.Text))
            {
                MessageBox.Show("Iveskite kategorijos pavadinima");
                return;
            }

            var category = new Category
            {
                CategoryName = PrekesKategorijosInput.Text,
            };

            context.Categorys.Attach(category);

            context.SaveChanges();

            MessageBox.Show("Informacija ivesta");

            PrekesKategorijosPanele.Visible = false;
        }
        private void NuotraukaPridetiButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void PridetiPrekeButton_Click(object sender, EventArgs e)
        {
            PrekesIdejimoPanel.Visible = true;
            ProfilioRedagavimoPanele.Visible = true;
            PrekiuSarasoPanel.Visible = false;
            PrekesKategorijosPanele.Visible = false;
            VartotojuSarasoPanele.Visible = false;
            WhiteListPanel.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
        }

        private void PridetiPrekeMenuButton_Click(object sender, EventArgs e)
        {
            PrekesIdejimoPanel.Visible = false;

            if (string.IsNullOrEmpty(PrekesPavadinimasIvedimoLaukas.Text) || string.IsNullOrEmpty(PrekesDescrIvedimoLaukas.Text) || string.IsNullOrEmpty(KainosIvedimoLaukas.Text) ||
               KategorijosPasirinkimoComboBox.SelectedItem == null || string.IsNullOrEmpty(FileName))
            {
                MessageBox.Show("Iveskite informacija i kiekviena lauka");
            }

            byte[] file;

            using (var stream = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }



            //using (stream = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            //{
            //    using (var reader = new BinaryReader(stream))
            //    {
            //        file = reader.ReadBytes((int)stream.Length);
            //    }
            //}

            var category = KategorijosPasirinkimoComboBox.SelectedItem.ToString();

            Product product = new Product
            {
                Name = PrekesPavadinimasIvedimoLaukas.Text,
                Description = PrekesDescrIvedimoLaukas.Text,
                Price = Convert.ToDouble(KainosIvedimoLaukas.Text),
                Image = file,
                CategoryId = context.Categorys.Where(x => x.CategoryName == KategorijosPasirinkimoComboBox.SelectedItem.ToString()).SingleOrDefault().Id
            };

            context.Products.Attach(product);

            context.SaveChanges();

            MessageBox.Show("Informacija ivesta");

            PrekesPavadinimasIvedimoLaukas.Text = string.Empty;
            PrekesDescrIvedimoLaukas.Text = string.Empty;
            KainosIvedimoLaukas.Text = string.Empty;
            KategorijosPasirinkimoComboBox.SelectedItem = null;

            PrekesIdejimoPanel.Visible = false;

        }

        private void LoadPrekiuGrid()
        {
            List<Product> sarasas = context.Products.Include(x => x.Category).ToList();

            var a = (from l in sarasas
                     select new
                     {
                         ID = l.Id,
                         Category = l.Category.CategoryName,
                         Name = l.Name,
                         Description = l.Description,
                         Price = l.Price
                     }).ToList();

            PrekiuEsamuSarasoPanel.DataSource = a;
        }
        private void LoadWhislistGrid()
        {
            List<WishList> sarasas = context.WishLists.Include(x => x.Product).ToList();

            var wishListProducts = (from p in context.Products
                                    join w in context.WishLists.Where(x => x.UserId == this.user.Id) on p.Id equals w.ProductId
                                    select p).ToList();

            PrekiuWishListGridview.DataSource = wishListProducts;

        }

        private void PrekiuKrepselisPanelGrid()
        {
            List<ProductCart> sarasas = context.ProductCarts.Include(x => x.Product).ToList();

            var ProductCart = (from p in context.Products
                                    join w in context.ProductCarts.Where(x => x.UserId == this.user.Id) on p.Id equals w.ProductId
                                    select p).ToList();

            PrekiuKrepselioGridView.DataSource = ProductCart;
        }

        private void PrekiuEsamuSarasoPanel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //private void LoadGrid()
            //{
            //    var user = context.Users.ToList();

            //    List<User> list = context.Users.AsNoTracking().ToList();

            // claas c   int Id

            // class b int id 

            //    dataGridView2.DataSource = mapper.Map<List<UsersList>>(user);
            //}
        }

        private void IdetiIIsimintinasPrekesButton_Click(object sender, EventArgs e)
        {
            if (PrekiuEsamuSarasoPanel.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select one row.");
                return;
            }

            var row = PrekiuEsamuSarasoPanel.SelectedRows[0];
            var produtctId = row.Cells["ID"].Value;

            var user = context.Users.Include(x => x.WishList).SingleOrDefault(x => x.Id == this.user.Id);

            var userblah = context.WishLists.Where(x => x.ProductId == (int)produtctId && x.UserId == user.Id).SingleOrDefault();

            if (userblah != null)
            {
                MessageBox.Show("Toks dalykas jau yra whisliste.");
                return;
            }

            user.WishList.Add(new WishList
            {
                ProductId = (int)produtctId,
                UserId = user.Id
            });

            context.Users.Update(user);
            context.SaveChanges();
            MessageBox.Show("Ideti i jusu whislista.");
        }

        private void IsimintinuPrekiuSarasoButton_Click(object sender, EventArgs e)
        {
            WhiteListPanel.Visible = true;
            LoadWhislistGrid();
        }

        private void IsintrintiIsWhitelistButton_Click(object sender, EventArgs e)
        {
            if (PrekiuWishListGridview.SelectedRows.Count != 1)
            {

                MessageBox.Show("Please select one row.");
                return;
            }
            //PrekiuWishListGridview
            var row = PrekiuWishListGridview.SelectedRows[0];
            var productId = (int)row.Cells["Id"].Value;

            var ProductInfo = context.WishLists.Where(x => x.ProductId == productId && x.UserId == user.Id).SingleOrDefault();

            if (ProductInfo == null)
            {
                MessageBox.Show("user doesn't exist");
                return;
            }
            context.WishLists.Remove(ProductInfo);

            context.SaveChanges();

            LoadWhislistGrid();
        }

        private void PrekiuSarasasButton_Click(object sender, EventArgs e)
        {
            
            PrekiuSarasoPanel.Visible = true;
            PrekiuKrepselisPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
        }

        private void PrekiuKrepselisButton_Click(object sender, EventArgs e)
        {
            PrekiuKrepselisPanel.Visible = true;
            PrekesIdejimoPanel.Visible = false;
            ProfilioRedagavimoPanele.Visible = false;
            PrekiuSarasoPanel.Visible = false;
            PrekesKategorijosPanele.Visible = false;
            VartotojuSarasoPanele.Visible = false;
            WhiteListPanel.Visible = false;
            ApsipirkimuIstorijaUserPanel.Visible = false;
            PrekiuKrepselisPanelGrid();
            UpdatePriceLabel();

            if (PrekiuEsamuSarasoPanel.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select one row.");
                return;
            }

            var row = PrekiuEsamuSarasoPanel.SelectedRows[0];
            var produtctId = row.Cells["ID"].Value;

            var user = context.Users.Include(x => x.WishList).SingleOrDefault(x => x.Id == this.user.Id);

            var userblah = context.WishLists.Where(x => x.ProductId == (int)produtctId && x.UserId == user.Id).SingleOrDefault();

            if (userblah != null)
            {
                MessageBox.Show("Toks dalykas jau yra whisliste.");
                return;
            }

            user.WishList.Add(new WishList
            {
                ProductId = (int)produtctId,
                UserId = user.Id
            });

            context.Users.Update(user);
            context.SaveChanges();
            MessageBox.Show("Ideti i jusu whislista.");


        }

        private void ApsipirkimuIstorijaLoad()
        {
           List<PurchaseHistory> sarasas = context.PurchaseHistorys.Where(x => x.UserId == this.user.Id).Include(x => x.Product).ToList(); // su prisijungtu acc

            var a = (from z in sarasas
                     select new
                     {
                         Name = z.Product.Name,
                         Description = z.Product.Description,
                         Price = z.Product.Price,
                         Date = z.Date,
                         PurchaseId = z.PurchaseId
                     }).OrderBy(x => x.PurchaseId).ToList();

            ApsipirkimuIstorijaDataGridPanel.DataSource = a;
        }

        private void ApsipirkimuIstorijaButton_Click(object sender, EventArgs e)
        {
            ApsipirkimuIstorijaUserPanel.Visible = true;
            PrekesIdejimoPanel.Visible = false;
            PrekiuSarasoPanel.Visible = false;

            //ApsipirkimuIstorijaUserPanel
            ApsipirkimuIstorijaLoad();


        }

        private void FinansininkoGridLoad()
        {
            List<PurchaseHistory> sarasas = context.PurchaseHistorys.Include(x => x.Product).ToList(); // su visais
                                                                                                                                            
            var a = (from z in sarasas
                     select new
                     {
                         Name = z.Product.Name,
                         Description = z.Product.Description,
                         Price = z.Product.Price,
                         Date = z.Date,
                         PurchaseId = z.PurchaseId
                     }).OrderBy(x => x.PurchaseId).ToList();

            ApsipirkimuIstorijaDataGridPanel.DataSource = a;
        }

        private void ApsipirkimuButtonFinansininko_Click(object sender, EventArgs e)
        {
            FinansinikasApsipirkimuIstorijaPanel.Visible = true;

            List<PurchaseHistory> sarasas = context.PurchaseHistorys.Include(x => x.Product).ToList();

            var a = (from z in sarasas
                     select new
                     {
                         Name = z.Product.Name,
                         Description = z.Product.Description,
                         Price = z.Product.Price,
                         Date = z.Date,
                         PurchaseId = z.PurchaseId
                     }).OrderBy(x => x.PurchaseId).ToList();

            FinansininkoGrid.DataSource = a;

        }

        private void PrekiuKrepselioInsertButton_Click(object sender, EventArgs e)
        {
            if (PrekiuEsamuSarasoPanel.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select one row.");
                return;
            }

            var row = PrekiuEsamuSarasoPanel.SelectedRows[0];
            var produtctId = row.Cells["ID"].Value;

            var user = context.Users.Include(x => x.ProductCart).SingleOrDefault(x => x.Id == this.user.Id);

            if (user == null)
            {
                MessageBox.Show("tokio userio nera musu database");
                return;
            }

            user.ProductCart.Add(new ProductCart
            {
                ProductId = (int)produtctId,
                UserId = user.Id
            });

            context.Users.Update(user);
            context.SaveChanges();
            MessageBox.Show("Ideti i jusu whislista.");
            UpdatePriceLabel();
        }

        private void UpdatePriceLabel()
        {
            double total = PrekiuKrepselioGridView.Rows.Cast<DataGridViewRow>().Sum(t => (double)t.Cells["Price"].Value);

            var wishListProducts = (from p in context.Products
                                    join w in context.WishLists.Where(x => x.UserId == this.user.Id) on p.Id equals w.ProductId
                                    select p).ToList();

            var Birthday = context.Users.Where(x => x.Id == this.user.Id).Select(x => x.Birthddate).SingleOrDefault();

            DateTime today = DateTime.Today;
            DateTime next = Birthday.AddYears(today.Year - Birthday.Year);

            if (next < today)
                next = next.AddYears(1);

            var diena = (next - today).Days;
            if (diena <= 7 || diena <= 359)
            {
               TotalPriceLabel.Text = (total - (total * 0.1)).ToString("0.0");
               return;
            }
            // var asd = DateTime.Now
            TotalPriceLabel.Text = total.ToString("0.0");
        }

        private void PirktiButton_Click(object sender, EventArgs e)
        {
            //PrekiuKrepselioGridView
            var ProductCartInfo = this.context.ProductCarts.Where(x => x.UserId == this.user.Id).ToList();

            if (ProductCartInfo == null)
            {
                MessageBox.Show("Kartas yra tuscias");
                return;
            }

            var superRandom = Guid.NewGuid();

            var informacija = ProductCartInfo.Select(x => 
                new PurchaseHistory
                {
                    ProductId = x.ProductId,
                    PurchaseId = superRandom,
                    Date = DateTime.Now,
                    UserId = x.UserId,
                }
            );

            context.ProductCarts.RemoveRange(context.ProductCarts.Where(x => x.UserId == this.user.Id));

            context.PurchaseHistorys.AddRange(informacija);
            context.SaveChanges();
            MessageBox.Show("Nusipirkot.");
           // ApsipirkimuIstorijaLoad();
        }


        private void EksportuotiNaudojantJson_Click(object sender, EventArgs e)
        {
            List<PurchaseHistory> sarasas = context.PurchaseHistorys.Include(x => x.Product).ToList();
            var b = (from x in sarasas
                     group x by x.PurchaseId into v
                     select new { v }).ToDictionary(g => g.v.Key, g => g.v.Select(x => new
                     {
                         ProductName = x.Product.Name,
                         ProductPrice = x.Product.Price,
                         ProductCategory = x.Product.Category.CategoryName,
                         ProductDescription = x.Product.Description
                     }));


            string json = JsonConvert.SerializeObject(b, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

            File.WriteAllText(@"D:\JsonFile.txt", json);
            MessageBox.Show("Json irarsytas");
        }
    }
}
