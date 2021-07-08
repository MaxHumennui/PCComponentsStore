using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShop
{
    public partial class Second : Form
    {
        public Second(string tag)
        {
            InitializeComponent();

            if(tag == "addCustom")
                contentPanel.Controls.Add(addCustom());
            if(tag == "addMaker")
                contentPanel.Controls.Add(addMaker());
            if(tag == "addAccess")
                contentPanel.Controls.Add(addAccess());
        }
        
        public Second(string[] tag)
        {
            InitializeComponent();

            if(tag[0] == "editAccess")
                contentPanel.Controls.Add(editAccess(tag[1]));
        }

        public Second(string[] tag, string[] counts)
        {
            InitializeComponent();

            if(tag[0] != "editAccess")
                contentPanel.Controls.Add(addOrder(tag, counts));
        }

        private Panel addCustom()
        {
            Panel content = new Panel()
            {
                Location = new Point(40, 100),
                Size = new Size(500, 500),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left)
            };

            Label firstnameLabel = new Label()
            {
                Text = "Ім'я",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 20),
                Size = new Size(100, 20)
            };
            Label lastnameLabel = new Label()
            {
                Text = "Прізвище",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 50),
                Size = new Size(100, 20)
            };
            Label surnameLabel = new Label()
            {
                Text = "По-батькові",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 80),
                Size = new Size(100, 20)
            };
            Label emailLabel = new Label()
            {
                Text = "E-mail",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 110),
                Size = new Size(100, 20)
            };
            Label phoneLabel = new Label()
            {
                Text = "Номер тел.",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 140),
                Size = new Size(100, 20)
            };

            TextBox firstnameTextBox = new TextBox()
            {
                Location = new Point(firstnameLabel.Location.X + 100, firstnameLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox lastnameTextBox = new TextBox()
            {
                Location = new Point(lastnameLabel.Location.X + 100, lastnameLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox surnameTextBox = new TextBox()
            {
                Location = new Point(surnameLabel.Location.X + 100, surnameLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox emailTextBox = new TextBox()
            {
                Location = new Point(emailLabel.Location.X + 100, emailLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox phoneTextBox = new TextBox()
            {
                Location = new Point(phoneLabel.Location.X + 100, phoneLabel.Location.Y),
                Size = new Size(150, 20)
            };

            Button complateButton = new Button()
            {
                Tag = content,
                Text = "Готово",
                Location = new Point(phoneLabel.Location.X + 125, 170),
                Size = new Size(100, 30)
            };
            
            complateButton.Click += new EventHandler(complateButton_Click);

            content.Controls.Add(firstnameLabel);
            content.Controls.Add(lastnameLabel);
            content.Controls.Add(surnameLabel);
            content.Controls.Add(emailLabel);
            content.Controls.Add(phoneLabel);
            content.Controls.Add(firstnameTextBox);
            content.Controls.Add(lastnameTextBox);
            content.Controls.Add(surnameTextBox);
            content.Controls.Add(emailTextBox);
            content.Controls.Add(phoneTextBox);
            content.Controls.Add(complateButton);

            return content;
        }

        public void complateButton_Click(object sender, EventArgs e)
        {
            Button complateButton = sender as Button;
            Panel getter = (Panel)complateButton.Tag;

            dataBase db = new dataBase();
            MySqlCommand addCustom = new MySqlCommand("INSERT `customers` (`firstname`, `lastname`, `surname`, `email`, `phone_number`) VALUES (@fname, @lname, @sname, @email, @phone)", db.getConnect());

            string[] boxes = new string[5];
            byte cnt = 0;

            foreach(Control control in getter.Controls)
            {
                if (control is TextBox)
                {
                    if (control.Text == "")
                    {
                        MessageBox.Show("Будь-ласка, заповніть всі поля");
                        return;
                    }

                    boxes[cnt] = control.Text;
                    cnt++;
                }
            }

            addCustom.Parameters.Add("@fname", MySqlDbType.VarChar).Value = boxes[0];
            addCustom.Parameters.Add("@lname", MySqlDbType.VarChar).Value = boxes[1];
            addCustom.Parameters.Add("@sname", MySqlDbType.VarChar).Value = boxes[2];
            addCustom.Parameters.Add("@email", MySqlDbType.VarChar).Value = boxes[3];
            addCustom.Parameters.Add("@phone", MySqlDbType.VarChar).Value = boxes[4];
            db.dbConnect();
            addCustom.ExecuteNonQuery();
            db.dbDisconnect();

            this.Close();
        }
        private Panel addMaker()
        {
            Panel content = new Panel()
            {
                Location = new Point(40, 100),
                Size = new Size(500, 500),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left)
            };

            Label nameLabel = new Label()
            {
                Text = "Назва ",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 20),
                Size = new Size(100, 20)
            };

            TextBox nameTextBox = new TextBox()
            {
                Location = new Point(nameLabel.Location.X + 100, nameLabel.Location.Y),
                Size = new Size(150, 20)
            };

            Button complateMakersButton = new Button()
            {
                Tag = content,
                Text = "Готово",
                Location = new Point(nameLabel.Location.X + 125, 170),
                Size = new Size(100, 30)
            };

            complateMakersButton.Click += new EventHandler(complateMakersButton_Click);

            content.Controls.Add(nameLabel);
            content.Controls.Add(nameTextBox);
            content.Controls.Add(complateMakersButton);

            return content;
        }

        public void complateMakersButton_Click(object sender, EventArgs e)
        {
            Button complateMakersButton = sender as Button;
            Panel getter = (Panel)complateMakersButton.Tag;

            string name = "";

            foreach (Control control in getter.Controls)
            {
                if (control is TextBox)
                {
                    if (control.Text == "")
                    {
                        MessageBox.Show("Будь-ласка, заповніть поле");
                        return;
                    }

                    name = control.Text;
                }
            }

            dataBase db = new dataBase();
            MySqlCommand addMaker = new MySqlCommand("INSERT `makers` (`name`) VALUES (@name)", db.getConnect());

            addMaker.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;

            db.dbConnect();
            addMaker.ExecuteNonQuery();
            db.dbDisconnect();

            this.Close();
        }

        public Panel addAccess()
        {
            Panel content = new Panel()
            {
                Location = new Point(40, 10),
                Size = new Size(500, 500),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left)
            };

            Label nameLabel = new Label()
            {
                Text = "Назва",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 20),
                Size = new Size(100, 20)
            };
            Label priceLabel = new Label()
            {
                Text = "Ціна",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 50),
                Size = new Size(100, 20)
            };
            Label descriptionLabel = new Label()
            {
                Text = "Характеристики/опис",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 80),
                Size = new Size(100, 40)
            };
            Label quantityLabel = new Label()
            {
                Text = "К-сть на складі",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 150),
                Size = new Size(100, 20)
            };
            Label typeLabel = new Label()
            {
                Text = "Тип",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 180),
                Size = new Size(100, 20)
            };
            Label imageLabel = new Label()
            {
                Text = "Зображення",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 210),
                Size = new Size(100, 20)
            };
            Label makerLabel = new Label()
            {
                Text = "Виробник",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 240),
                Size = new Size(100, 20)
            };
            Label deliveryDateLabel = new Label()
            {
                Text = "Дата поставки",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 270),
                Size = new Size(100, 20)
            };

            TextBox nameTextBox = new TextBox()
            {
                Location = new Point(nameLabel.Location.X + 100, nameLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox priceTextBox = new TextBox()
            {
                Location = new Point(priceLabel.Location.X + 100, priceLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox descriptionTextBox = new TextBox()
            {
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(descriptionLabel.Location.X + 100, descriptionLabel.Location.Y),
                Size = new Size(300, 60)
            };
            TextBox quantityTextBox = new TextBox()
            {
                Location = new Point(quantityLabel.Location.X + 100, quantityLabel.Location.Y),
                Size = new Size(150, 20)
            };
            ComboBox typeTextBox = new ComboBox()
            {
                Location = new Point(typeLabel.Location.X + 100, typeLabel.Location.Y),
                Size = new Size(150, 20)
            };
            Button imageButton = new Button()
            {
                Name = "image",
                Text = "Вибрати",
                Location = new Point(imageLabel.Location.X + 100, imageLabel.Location.Y),
                Size = new Size(100, 20)
            };
            ComboBox makerTextBox = new ComboBox()
            {
                Location = new Point(makerLabel.Location.X + 100, makerLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox deliveryDateTextBox = new TextBox()
            {
                Text = DateTime.Now.ToString("yyyy'-'MM'-'dd"),
                Location = new Point(deliveryDateLabel.Location.X + 100, deliveryDateLabel.Location.Y),
                Size = new Size(150, 20)
            };

            Button complateButton = new Button()
            {
                Tag = content,
                Text = "Готово",
                Location = new Point(deliveryDateLabel.Location.X + 125, 300),
                Size = new Size(100, 30)
            };

            complateButton.Click += new EventHandler(complateAccessButton_Click);
            priceTextBox.KeyPress += new KeyPressEventHandler(TextBoxDigitDot_KeyPress);
            quantityTextBox.KeyPress += new KeyPressEventHandler(TextBoxDigit_KeyPress);
            typeTextBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            makerTextBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            imageButton.Click += new EventHandler(imageButton_Click);

            DataTable types = new DataTable();
            dataBase db = new dataBase();
            MySqlCommand qweryTypesData = new MySqlCommand("SELECT * FROM `types`", db.getConnect());
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter.SelectCommand = qweryTypesData;
            adapter.Fill(types);

            foreach(DataRow row in types.Rows)
                typeTextBox.Items.Add(row["name"].ToString());

            if(typeTextBox.Items[0] != null)
                typeTextBox.Text = typeTextBox.Items[0].ToString();
            
            DataTable makers = new DataTable();
            MySqlCommand qweryMakersData = new MySqlCommand("SELECT * FROM `makers`", db.getConnect());

            adapter.SelectCommand = qweryMakersData;
            adapter.Fill(makers);

            foreach(DataRow row in makers.Rows)
                makerTextBox.Items.Add(row["name"].ToString());

            if(makerTextBox.Items[0] != null)
                makerTextBox.Text = makerTextBox.Items[0].ToString();

            content.Controls.Add(nameLabel);
            content.Controls.Add(priceLabel);
            content.Controls.Add(descriptionLabel);
            content.Controls.Add(quantityLabel);
            content.Controls.Add(typeLabel);
            content.Controls.Add(imageLabel);
            content.Controls.Add(makerLabel);
            content.Controls.Add(deliveryDateLabel);
            content.Controls.Add(nameTextBox);
            content.Controls.Add(priceTextBox);
            content.Controls.Add(descriptionTextBox);
            content.Controls.Add(quantityTextBox);
            content.Controls.Add(typeTextBox);
            content.Controls.Add(imageButton);
            content.Controls.Add(makerTextBox);
            content.Controls.Add(deliveryDateTextBox);
            content.Controls.Add(complateButton);

            return content;
        }

        public Panel editAccess(string tag)
        {
            dataBase db = new dataBase();
            DataTable getter = new DataTable();

            MySqlCommand qweryAccessData = new MySqlCommand("SELECT * FROM `accessories` WHERE `id_access` = @id", db.getConnect());
            qweryAccessData.Parameters.Add("@id", MySqlDbType.Int32).Value = int.Parse(tag);

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter.SelectCommand = qweryAccessData;
            adapter.Fill(getter);

            string name = "";
            string price = "";
            string description = "";
            string quantity = "";
            string type = "";
            string image = "";
            string maker = "";
            DateTime date = DateTime.Now;

            foreach (DataRow row in getter.Rows)
            {
                name = row["name"].ToString();
                price = row["price"].ToString();
                description = row["description"].ToString();
                quantity = row["quantity"].ToString();
                type = row["type"].ToString();
                image = row["image"].ToString();
                maker = row["maker"].ToString();
                date = DateTime.Parse(row["delivery_date"].ToString());
            }

            Panel content = new Panel()
            {
                Tag = tag,
                Location = new Point(40, 10),
                Size = new Size(500, 500),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left)
            };

            Label nameLabel = new Label()
            {
                Text = "Назва",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 20),
                Size = new Size(100, 20)
            };
            Label priceLabel = new Label()
            {
                Text = "Ціна",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 50),
                Size = new Size(100, 20)
            };
            Label descriptionLabel = new Label()
            {
                Text = "Характеристики/опис",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 80),
                Size = new Size(100, 40)
            };
            Label quantityLabel = new Label()
            {
                Text = "К-сть на складі",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 150),
                Size = new Size(100, 20)
            };
            Label typeLabel = new Label()
            {
                Text = "Тип",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 180),
                Size = new Size(100, 20)
            };
            Label imageLabel = new Label()
            {
                Text = "Зображення",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 210),
                Size = new Size(100, 20)
            };
            Label makerLabel = new Label()
            {
                Text = "Виробник",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 240),
                Size = new Size(100, 20)
            };
            Label deliveryDateLabel = new Label()
            {
                Text = "Дата поставки",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 270),
                Size = new Size(100, 20)
            };

            TextBox nameTextBox = new TextBox()
            {
                Text = name,
                Location = new Point(nameLabel.Location.X + 100, nameLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox priceTextBox = new TextBox()
            {
                Text = price,
                Location = new Point(priceLabel.Location.X + 100, priceLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox descriptionTextBox = new TextBox()
            {
                Text = description,
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(descriptionLabel.Location.X + 100, descriptionLabel.Location.Y),
                Size = new Size(300, 60)
            };
            TextBox quantityTextBox = new TextBox()
            {
                Text = quantity,
                Location = new Point(quantityLabel.Location.X + 100, quantityLabel.Location.Y),
                Size = new Size(150, 20)
            };
            ComboBox typeTextBox = new ComboBox()
            {
                Text = type,
                Location = new Point(typeLabel.Location.X + 100, typeLabel.Location.Y),
                Size = new Size(150, 20)
            };
            Button imageButton = new Button()
            {
                Text = image,
                Name = "image",
                Location = new Point(imageLabel.Location.X + 100, imageLabel.Location.Y),
                Size = new Size(100, 20)
            };
            ComboBox makerTextBox = new ComboBox()
            {
                Text = maker,
                Location = new Point(makerLabel.Location.X + 100, makerLabel.Location.Y),
                Size = new Size(150, 20)
            };
            TextBox deliveryDateTextBox = new TextBox()
            {
                Text = date.ToString("yyyy'-'MM'-'dd"),
                Location = new Point(deliveryDateLabel.Location.X + 100, deliveryDateLabel.Location.Y),
                Size = new Size(150, 20)
            };

            Button complateButton = new Button()
            {
                Tag = content,
                Text = "Готово",
                Location = new Point(deliveryDateLabel.Location.X + 125, 300),
                Size = new Size(100, 30)
            };

            complateButton.Click += new EventHandler(complateAccessEditButton_Click);
            priceTextBox.KeyPress += new KeyPressEventHandler(TextBoxDigitDot_KeyPress);
            quantityTextBox.KeyPress += new KeyPressEventHandler(TextBoxDigit_KeyPress);
            typeTextBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            makerTextBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            imageButton.Click += new EventHandler(imageButton_Click);

            DataTable types = new DataTable();
            MySqlCommand qweryTypesData = new MySqlCommand("SELECT * FROM `types`", db.getConnect()); 

            adapter.SelectCommand = qweryTypesData;
            adapter.Fill(types);

            foreach (DataRow row in types.Rows)
                typeTextBox.Items.Add(row["name"].ToString());

            if (typeTextBox.Items[0] != null)
                typeTextBox.Text = typeTextBox.Items[0].ToString();

            DataTable makers = new DataTable();
            MySqlCommand qweryMakersData = new MySqlCommand("SELECT * FROM `makers`", db.getConnect());

            adapter.SelectCommand = qweryMakersData;
            adapter.Fill(makers);

            foreach (DataRow row in makers.Rows)
                makerTextBox.Items.Add(row["name"].ToString());

            content.Controls.Add(nameLabel);
            content.Controls.Add(priceLabel);
            content.Controls.Add(descriptionLabel);
            content.Controls.Add(quantityLabel);
            content.Controls.Add(typeLabel);
            content.Controls.Add(imageLabel);
            content.Controls.Add(makerLabel);
            content.Controls.Add(deliveryDateLabel);
            content.Controls.Add(nameTextBox);
            content.Controls.Add(priceTextBox);
            content.Controls.Add(descriptionTextBox);
            content.Controls.Add(quantityTextBox);
            content.Controls.Add(typeTextBox);
            content.Controls.Add(imageButton);
            content.Controls.Add(makerTextBox);
            content.Controls.Add(deliveryDateTextBox);
            content.Controls.Add(complateButton);

            return content;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void TextBoxDigitDot_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if(!Char.IsDigit(key) && key != '.' && key != 08)
                e.Handled = true;
        }

        private void TextBoxDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if(!Char.IsDigit(key) && key != 08)
                e.Handled = true;
        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectImage = new OpenFileDialog();
            selectImage.Filter = "Всі зображення (*.JPG;*.PNG;*.JPEG;*.PNG)|*.JPG;*.PNG;*.JPEG;*.PNG";
            selectImage.Title = "Вибір файлу...";
            if(selectImage.ShowDialog() == DialogResult.OK)
            {
                string imagePath = selectImage.FileName;
                string source = @"resources\images\" + selectImage.SafeFileName;

                string[] pathTag = new string[2];

                pathTag[0] = imagePath;
                pathTag[1] = source;

                (sender as Button).Tag = pathTag;
                (sender as Button).Text = selectImage.SafeFileName;
            }
        }

        public void complateAccessButton_Click(object sender, EventArgs e)
        {
            Button complateMakersButton = sender as Button;
            Panel getter = (Panel)complateMakersButton.Tag;

            string[] data = new string[8];
            int cnt = 0;

            foreach(Control control in getter.Controls)
            {
                if(control is TextBox || control is ComboBox)
                {
                    if (control.Text == "")
                    {
                        MessageBox.Show("Будь-ласка, заповніть поле");
                        return;
                    }

                    data[cnt] = control.Text;
                    cnt++;
                }

                if(control is Button || control.Name == "image")
                {
                    if(control.Text == "Вибрати")
                    {
                        MessageBox.Show("Будь-ласка, виберіть зображення");
                        return;
                    }

                    if(control.Text == "Готово")
                        continue;

                    string[] pathTag = (string[])control.Tag;
                    try
                    {
                        File.Copy(pathTag[0], pathTag[1]);
                    }
                    catch
                    {
                        MessageBox.Show("Файл вже існує");
                    }

                    data[cnt] = pathTag[1];
                    cnt++;
                }
            }

            dataBase db = new dataBase();
            MySqlCommand addAccess = new MySqlCommand("INSERT `accessories` (`name`, `price`, `description`, `quantity`, `type`, `image`, `maker`, `delivery_date`) VALUES (@name, @price, @description, @quantity, @type, @image, @maker, @d_d)", db.getConnect());

            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

            addAccess.Parameters.Add("@name", MySqlDbType.VarChar).Value = data[0];
            addAccess.Parameters.Add("@price", MySqlDbType.Double).Value = double.Parse(data[1], formatter);
            addAccess.Parameters.Add("@description", MySqlDbType.VarChar).Value = data[2];
            addAccess.Parameters.Add("@quantity", MySqlDbType.Int32).Value = int.Parse(data[3]);
            addAccess.Parameters.Add("@type", MySqlDbType.VarChar).Value = data[4];
            addAccess.Parameters.Add("@image", MySqlDbType.VarChar).Value = data[5];
            addAccess.Parameters.Add("@maker", MySqlDbType.VarChar).Value = data[6];
            addAccess.Parameters.Add("@d_d", MySqlDbType.Date).Value = data[7];

            db.dbConnect();
            addAccess.ExecuteNonQuery();
            db.dbDisconnect();

            this.Close();
        }

        public void complateAccessEditButton_Click(object sender, EventArgs e)
        {
            Button complateMakersButton = sender as Button;
            Panel getter = (Panel)complateMakersButton.Tag;

            string[] data = new string[8];
            int cnt = 0;

            foreach(Control control in getter.Controls)
            {
                if(control is TextBox || control is ComboBox)
                {
                    if (control.Text == "")
                    {
                        MessageBox.Show("Будь-ласка, заповніть поле");
                        return;
                    }

                    data[cnt] = control.Text;
                    cnt++;
                }

                if(control is Button || control.Name == "image")
                {
                    if(control.Text == "Вибрати")
                    {
                        MessageBox.Show("Будь-ласка, виберіть зображення");
                        return;
                    }

                    if(control.Text == "Готово")
                        continue;

                    if(control.Tag != null)
                    {
                        string[] pathTag = (string[])control.Tag;

                        try
                        {
                            File.Copy(pathTag[0], pathTag[1]);
                        }
                        catch
                        {
                            MessageBox.Show("Файл вже існує");
                        }

                        data[cnt] = pathTag[1];
                    }
                    else
                    {
                        data[cnt] = (string)control.Text;
                    }

                    cnt++;
                }
            }

            dataBase db = new dataBase();
            MySqlCommand addAccess = new MySqlCommand("UPDATE `accessories` SET `name` = @name, `price` = @price, `description` = @description, `quantity` = @quantity, `type` = @type, `image` = @image, `maker` = @maker, `delivery_date` = @d_d WHERE `id_access` = @id", db.getConnect());

            IFormatProvider formatter = new NumberFormatInfo { NumberDecimalSeparator = "." };

            addAccess.Parameters.Add("@id", MySqlDbType.Int32).Value = int.Parse(getter.Tag.ToString());
            addAccess.Parameters.Add("@name", MySqlDbType.VarChar).Value = data[0];
            addAccess.Parameters.Add("@price", MySqlDbType.Double).Value = double.Parse(data[1], formatter);
            addAccess.Parameters.Add("@description", MySqlDbType.VarChar).Value = data[2];
            addAccess.Parameters.Add("@quantity", MySqlDbType.Int32).Value = int.Parse(data[3]);
            addAccess.Parameters.Add("@type", MySqlDbType.VarChar).Value = data[4];
            addAccess.Parameters.Add("@image", MySqlDbType.VarChar).Value = data[5];
            addAccess.Parameters.Add("@maker", MySqlDbType.VarChar).Value = data[6];
            addAccess.Parameters.Add("@d_d", MySqlDbType.Date).Value = data[7];

            db.dbConnect();
            addAccess.ExecuteNonQuery();
            db.dbDisconnect();

            this.Close();
        }

        private Panel addOrder(string[] tag, string[] counts)
        {
            string[] ids = tag;

            Panel content = new Panel()
            {
                Tag = ids,
                Location = new Point(40, 10),
                Size = new Size(500, 500),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left)
            };

            Label customerLabel = new Label()
            {
                Text = "Клієнт",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 20),
                Size = new Size(100, 20)
            };
            ComboBox customerComboBox = new ComboBox()
            {
                Name = "combo",
                Location = new Point(customerLabel.Location.X + 100, customerLabel.Location.Y),
                Size = new Size(200, 20)
            };

            Label emailLabel = new Label()
            {
                Text = "E-mail",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 50),
                Size = new Size(100, 20)
            };
            TextBox emailTextBox = new TextBox()
            {
                Name = "email",
                ReadOnly = true,
                Location = new Point(emailLabel.Location.X + 100, emailLabel.Location.Y),
                Size = new Size(200, 20)
            };

            Label phoneLabel = new Label()
            {
                Text = "Номер тел.",
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(40, 80),
                Size = new Size(100, 20)
            };
            TextBox phoneTextBox = new TextBox()
            {
                Name = "phone",
                ReadOnly = true,
                Location = new Point(phoneLabel.Location.X + 100, phoneLabel.Location.Y),
                Size = new Size(200, 60)
            };

            Button complateOrderButton = new Button()
            {
                Tag = content,
                Text = "Готово",
                Location = new Point(phoneLabel.Location.X + 125, 110),
                Size = new Size(100, 30)
            };

            DataTable costumers = new DataTable();
            dataBase db = new dataBase();
            MySqlCommand getCustom = new MySqlCommand("SELECT * FROM `customers`", db.getConnect());
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter.SelectCommand = getCustom;
            adapter.Fill(costumers);

            string[,] costumersData = new string[4, costumers.Rows.Count];
            byte cnt = 0;

            foreach(DataRow row in costumers.Rows)
            {
                costumersData[0, cnt] = row["id_custom"].ToString();
                costumersData[1, cnt] = row["firstname"].ToString() + " " + row["lastname"].ToString() + " " + row["surname"].ToString();
                costumersData[2, cnt] = row["email"].ToString();
                costumersData[3, cnt] = row["phone_number"].ToString();
                customerComboBox.Items.Add(costumersData[1, cnt]);
                cnt++;
            }

            customerComboBox.Tag = content;
            emailTextBox.Tag = costumersData;
            phoneTextBox.Tag = counts;

            if (costumersData[0, 0] != null)
            {
                customerComboBox.Text = costumersData[1, 0];
                emailTextBox.Text = costumersData[2, 0];
                phoneTextBox.Text = costumersData[3, 0];
            }
            else
            {
                MessageBox.Show("Клієнтів не знайдено!");
                return addCustom();
            }

            content.Controls.Add(customerLabel);
            content.Controls.Add(customerComboBox);
            content.Controls.Add(emailLabel);
            content.Controls.Add(emailTextBox);
            content.Controls.Add(phoneLabel);
            content.Controls.Add(phoneTextBox);
            content.Controls.Add(complateOrderButton);

            customerComboBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            customerComboBox.SelectedIndexChanged += new EventHandler(customerComboBox_SelectedIndexChanged);
            complateOrderButton.Click += new EventHandler(complateOrder_Click);

            return content;
        }

        private void customerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox customerComboBox = (ComboBox)sender;
            Panel data = (Panel)customerComboBox.Tag;
            int index = customerComboBox.SelectedIndex;
            TextBox emailBox = data.Controls.Find("email", true).FirstOrDefault() as TextBox;
            TextBox phoneBox = data.Controls.Find("phone", true).FirstOrDefault() as TextBox;
            string[,] arr = (string[,])emailBox.Tag;

            emailBox.Text = arr[2, index];
            phoneBox.Text = arr[3, index];
        }

        private void complateOrder_Click(object sender, EventArgs e)
        {
            Button complateOrder = (Button)sender;
            Panel data = (Panel)complateOrder.Tag;
            ComboBox selectCustom = data.Controls.Find("combo", true).FirstOrDefault() as ComboBox;
            TextBox emailBox = data.Controls.Find("email", true).FirstOrDefault() as TextBox;
            TextBox phoneBox = data.Controls.Find("phone", true).FirstOrDefault() as TextBox;
            int index = selectCustom.SelectedIndex;
            string[] ids = (string[])data.Tag;
            string[,] arr = (string[,])emailBox.Tag;
            string[] counts = (string[])phoneBox.Tag;

            DateTime date = DateTime.Now;

            Form1 form = new Form1();

            Panel contentPanel = form.Controls.Find("contentPanel", true).FirstOrDefault() as Panel;
            byte cont = 0;

            foreach(Control cnt in contentPanel.Controls)
            {
                MessageBox.Show(cnt.GetType().ToString());
                if (cnt is Panel)
                {
                    foreach(Control contrl in cnt.Controls)
                    {
                        if (contrl is NumericUpDown)
                        {
                            counts[cont] = cnt.Text;
                            MessageBox.Show(counts[cont]);
                            cont++;
                        }
                    }
                }
            }

            dataBase db = new dataBase();
            MySqlCommand addOrder = new MySqlCommand("INSERT `orders` (`id_custom`, `order_date`, `status`) VALUES (@id, @date, @stat)", db.getConnect());
            MySqlCommand addTotal = new MySqlCommand("INSERT `total` (`id_order`, `id_access`, `number`) VALUES (@idO, @idA, @cnt)", db.getConnect());
            MySqlCommand getIdOrder = new MySqlCommand("SELECT MAX(`id_order`) FROM `orders`", db.getConnect());

            addOrder.Parameters.Add("@id", MySqlDbType.Int32).Value = arr[0, index];
            addOrder.Parameters.Add("@date", MySqlDbType.Date).Value = date.ToString("yyyy'-'MM'-'dd");
            addOrder.Parameters.Add("@stat", MySqlDbType.VarChar).Value = "Оформлено";

            db.dbConnect();
            addOrder.ExecuteNonQuery();

            string idOrder = getIdOrder.ExecuteScalar().ToString();

            for (int i = 0; i < ids.Length - 1; i++)
            {
                addTotal.Parameters.Clear();
                addTotal.Parameters.Add("@idO", MySqlDbType.Int32).Value = int.Parse(idOrder);
                addTotal.Parameters.Add("@idA", MySqlDbType.Int32).Value = int.Parse(ids[i]);
                addTotal.Parameters.Add("@cnt", MySqlDbType.Int32).Value = int.Parse(counts[i]);
                addTotal.ExecuteNonQuery();
            }
            db.dbDisconnect();

            this.Close();
        }
    }
}
