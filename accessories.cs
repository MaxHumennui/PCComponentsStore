using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShop
{
    class accessories
    {
        static string[] ids = new string[1];

        public Panel addAccessComponent(string name, string description, string price, string quantity, string image, int PanelWidth, string id, int heigh)
        {
            Panel newComponent = new Panel
            {
                BackColor = Color.FromArgb(233, 233, 233),
                Location = new Point(0, heigh),
                Size = new Size(PanelWidth, 150),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left)
            };

            PictureBox picBox = new PictureBox
            {
                Location = new Point(0,25),
                Size = new Size(150, 100),
                ImageLocation = image,
                Anchor = AnchorStyles.Left,
                ErrorImage = Image.FromFile(@"resources\errorimage\Error100x100.gif")
            };

            TextBox nameAccess = new TextBox
            {
                BackColor = Color.White,
                Location = new Point(8, 0),
                Size = new Size(PanelWidth - 13, 20),
                ReadOnly = true,
                Text = name,
                Anchor = (AnchorStyles.Left | AnchorStyles.Right)
            };

            TextBox descriptionAccess = new TextBox
            {
                BackColor = Color.White,
                Location = new Point(155, 25),
                Multiline = true,
                WordWrap = true,
                ScrollBars = ScrollBars.Vertical,
                Size = new Size(PanelWidth - 405, 100),
                ReadOnly = true,
                Text = description,
                Anchor = (AnchorStyles.Left | AnchorStyles.Right)
            };

            TextBox priceAccess = new TextBox
            {
                BackColor = Color.White,
                Location = new Point(PanelWidth - 240, 25),
                Size = new Size(155, 20),
                ReadOnly = true,
                Text = price + " ₴",
                Anchor = AnchorStyles.Right
            };

            TextBox quantityAccess = new TextBox
            {
                BackColor = Color.White,
                Location = new Point(PanelWidth - 240, 50),
                Size = new Size(60, 20),
                ReadOnly = true,
                Text = quantity,
                Anchor = AnchorStyles.Right
            };

            Button addAccessButton = new Button
            {
                Tag = id,
                Location = new Point(newComponent.Width - 80, 24),
                Text = "У кошик",
                Size = new Size(75, 25),
                Anchor = AnchorStyles.Right
            };
            Button editAccessButton = new Button
            {
                Tag = id,
                Location = new Point(newComponent.Width - 80, 54),
                Text = "Редагувати",
                Size = new Size(75, 25),
                Anchor = AnchorStyles.Right
            };
            Button deleteAccessButton = new Button
            {
                Tag = id,
                Location = new Point(newComponent.Width - 80, 84),
                Text = "Видалити",
                Size = new Size(75, 25),
                Anchor = AnchorStyles.Right
            };

            addAccessButton.Click += new EventHandler(add_Click);
            editAccessButton.Click += new EventHandler(edit_Click);
            deleteAccessButton.Click += new EventHandler(delete_Click);

            newComponent.Controls.Add(picBox);
            newComponent.Controls.Add(nameAccess);
            newComponent.Controls.Add(descriptionAccess);
            newComponent.Controls.Add(priceAccess);
            newComponent.Controls.Add(quantityAccess);
            newComponent.Controls.Add(addAccessButton);
            newComponent.Controls.Add(editAccessButton);
            newComponent.Controls.Add(deleteAccessButton);

            return newComponent;
        }

        private void add_Click(object sender, EventArgs e)
        {
            Button addAccessButton = sender as Button;
            string data = (string)addAccessButton.Tag;

            byte check = 1;

            for(int i = 0; i < ids.Length - 1; i ++)
            {
                if (data == ids[i])
                    check = 0;
            }

            if(check == 1)
            {
                ids[ids.Length - 1] = data;
                string[] arr = new string[ids.Length + 1];
                for (int i = 0; i < ids.Length; i++)
                    arr[i] = ids[i];
                ids = arr;
            }
        }
        private void edit_Click(object sender, EventArgs e)
        {
            Button addAccessButton = sender as Button;
            string[] data = new string[2];
            data[0] = "editAccess";
            data[1] = (string)addAccessButton.Tag;

            Second secondForm = new Second(data);
            secondForm.Show();
        }
        private void delete_Click(object sender, EventArgs e)
        {
            Button addAccessButton = sender as Button;
            string data = (string)addAccessButton.Tag;

            dataBase db = new dataBase();
            MySqlCommand deleteAccess = new MySqlCommand("DELETE FROM `accessories` WHERE `id_access` = @id", db.getConnect());

            deleteAccess.Parameters.Add("@id", MySqlDbType.Int32).Value = data;

            db.dbConnect();
            deleteAccess.ExecuteNonQuery();
            db.dbDisconnect();
        }

        public static string[] getIDs()
        {
            return ids;
        }
    }
}
