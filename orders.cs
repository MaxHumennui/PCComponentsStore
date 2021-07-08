using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShop
{
    class orders
    {
        public Panel addOrderComponent(int panelWidth, string headingText, string state, string[] image, string[] name, string[] count, int countAccess, string id, int height)
        {
            Panel newComponentOrder = new Panel
            {
                Tag = id,
                Size = new Size(panelWidth, 225),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left),
                Location = new Point(0, height),
                AutoScroll = true
            };

            TextBox heading = new TextBox()
            {
                ReadOnly = true,
                Size = new Size(panelWidth, 20),
                Location = new Point(0, 0),
                Text = headingText,
                Anchor = (AnchorStyles.Right | AnchorStyles.Left)
            };

            Button editState = new Button()
            {
                Tag = newComponentOrder,
                Size = new Size(100, 20),
                Location = new Point(130, 205),
                Text = "Змінити"
            };

            ComboBox viewState = new ComboBox()
            {
                Size = new Size(120, 20),
                Location = new Point(0, 204),
                Text = state
            };

            viewState.KeyPress += new KeyPressEventHandler(viewState_KeyPress);
            editState.Click += new EventHandler(editState_Click);

            int width = 0;

            for (int i = 0; i < countAccess; i++)
            {
                newComponentOrder.Controls.Add(addAccessData(image[i], name[i], count[i], width));
                width += 120;
            }

            viewState.Items.Add("Оформлено");
            viewState.Items.Add("Очікуються товари");
            viewState.Items.Add("Завершено");

            newComponentOrder.Controls.Add(heading);
            newComponentOrder.Controls.Add(editState);
            newComponentOrder.Controls.Add(viewState);

            return newComponentOrder;
        }

        private void viewState_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public Panel addAccessData(string image, string name, string count, int width)
        {
            Panel accessData = new Panel()
            {
                Size = new Size(110, 160),
                Anchor = (AnchorStyles.Left),
                Location = new Point(width, 30)
            };

            PictureBox accessImage = new PictureBox()
            {
                ImageLocation = image,
                Size = new Size(110, 100),
                Location = new Point(0, 0),
                ErrorImage = Image.FromFile(@"resources\errorimage\Error100x100.gif")
            };

            TextBox nameAccess = new TextBox()
            {
                ReadOnly = true,
                Text = name,
                Size = new Size(110, 20),
                Location = new Point(0, accessImage.Height + 5)
            };

            TextBox countText = new TextBox()
            {
                ReadOnly = true,
                Text = count,
                Size = new Size(110, 20),
                Location = new Point(0, accessImage.Height + nameAccess.Height + 10)
            };

            accessData.Controls.Add(accessImage);
            accessData.Controls.Add(nameAccess);
            accessData.Controls.Add(countText);

            return accessData;
        }

        private void editState_Click(object sender, EventArgs e)
        {
            Button editState = sender as Button;
            Panel getter = (Panel)editState.Tag;

            string id = getter.Tag.ToString();
            string state = "";

            foreach(Control cont in getter.Controls)
            {
                if(cont is ComboBox)
                    state = cont.Text;
            }

            dataBase db = new dataBase();
            MySqlCommand editOrder = new MySqlCommand("UPDATE `orders` SET `status` = @status WHERE `id_order` = @id", db.getConnect());

            editOrder.Parameters.Add("@status", MySqlDbType.VarChar).Value = state;
            editOrder.Parameters.Add("@id", MySqlDbType.Int32).Value = int.Parse(id);

            db.dbConnect();
            editOrder.ExecuteNonQuery();
            db.dbDisconnect();
        }
    }
}
