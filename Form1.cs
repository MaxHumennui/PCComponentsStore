using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int[] counts = new int[1];

        private void cusomers_Click(object sender, EventArgs e)
        { 
            DataGridView table = new DataGridView()
            {
                Width = contentPanel.Width,
                Height = contentPanel.Height,
                ColumnCount = 6,
                AutoGenerateColumns = false,
                ColumnHeadersVisible = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(0, 50),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom),
                AutoScrollOffset = new Point()
            };

            Button addCustom = new Button()
            {
                Tag = "addCustom",
                Text = "Додати",
                Location = new Point(contentPanel.Width - 240, 10),
                TabIndex = 1,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            addCustom.Click += new EventHandler(addCustom_Click);

            table.Columns[0].Name = "ID";
            table.Columns[1].Name = "Ім'я";
            table.Columns[2].Name = "Прізвище";
            table.Columns[3].Name = "По-батькові";
            table.Columns[4].Name = "e-mail";
            table.Columns[5].Name = "Номер тел.";

            DataTable getter = new DataTable();

            dataBase db = new dataBase();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand qwery = new MySqlCommand("SELECT * FROM `customers`", db.getConnect());

            adapter.SelectCommand = qwery;
            adapter.Fill(getter);

            table.DataSource = getter;
            table.Columns["ID"].DataPropertyName = "id_custom";
            table.Columns["Ім'я"].DataPropertyName = "firstname";
            table.Columns["Прізвище"].DataPropertyName = "lastname";
            table.Columns["По-батькові"].DataPropertyName = "surname";
            table.Columns["e-mail"].DataPropertyName = "email";
            table.Columns["Номер тел."].DataPropertyName = "phone_number";
            table.Columns[0].ReadOnly = true;

            Button editCustom = new Button()
            {
                Tag = getter,
                Text = "Редагувати",
                Location = new Point(contentPanel.Width - 160, 10),
                TabIndex = 2,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            editCustom.Click += new EventHandler(editCustom_Click);

            Button deleteCustom = new Button()
            {
                Tag = table,
                Text = "Видалити",
                Location = new Point(contentPanel.Width - 80, 10),
                TabIndex = 3,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            deleteCustom.Click += new EventHandler(deleteCustom_Click);

            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(addCustom);
            contentPanel.Controls.Add(editCustom);
            contentPanel.Controls.Add(deleteCustom);
            contentPanel.Controls.Add(table);
        }

        private void makers_Click(object sender, EventArgs e)
        {
            Button addMaker = new Button()
            {
                Tag = "addMaker",
                Text = "Додати",
                Location = new Point(contentPanel.Width - 240, 10),
                TabIndex = 1,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            addMaker.Click += new EventHandler(addMaker_Click);

            DataGridView table = new DataGridView()
            {
                Width = contentPanel.Width,
                Height = contentPanel.Height,
                ColumnCount = 2,
                AutoGenerateColumns = false,
                ColumnHeadersVisible = true,
                Location = new Point(0, 50),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom)
            };

            table.Columns[0].Name = "ID";
            table.Columns[1].Name = "Назва";

            DataTable getter = new DataTable();

            dataBase db = new dataBase();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand qwery = new MySqlCommand("SELECT * FROM `makers`", db.getConnect());

            adapter.SelectCommand = qwery;
            adapter.Fill(getter);

            table.DataSource = getter;
            table.Columns["ID"].DataPropertyName = "id_maker";
            table.Columns["Назва"].DataPropertyName = "name";

            Button editMaker = new Button()
            {
                Tag = getter,
                Text = "Редагувати",
                Location = new Point(contentPanel.Width - 160, 10),
                TabIndex = 2,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            editMaker.Click += new EventHandler(editMaker_Click);

            Button deleteMaker = new Button()
            {
                Tag = table,
                Text = "Видалити",
                Location = new Point(contentPanel.Width - 80, 10),
                TabIndex = 3,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            deleteMaker.Click += new EventHandler(deleteMaker_Click);

            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(addMaker);
            contentPanel.Controls.Add(editMaker);
            contentPanel.Controls.Add(deleteMaker);
            contentPanel.Controls.Add(table);
        }

        private void orders_Click(object sender, EventArgs e)
        {
            contentPanel.Controls.Clear();

            DataTable getter = new DataTable();
            DataTable getterAll = new DataTable();
            DataTable getterAccess = new DataTable();
            DataTable getterAccessID = new DataTable();

            dataBase db = new dataBase();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand qweryID = new MySqlCommand("SELECT * FROM `orders`", db.getConnect());
            MySqlCommand qweryCustom = new MySqlCommand("SELECT `firstname`, `lastname` FROM `customers` WHERE `id_custom` = @cID", db.getConnect());
            MySqlCommand qweryAccessID = new MySqlCommand("SELECT `id_access`, `number` FROM `total` WHERE `id_order` = @oID", db.getConnect());
            MySqlCommand qweryAccessCount = new MySqlCommand("SELECT COUNT(id_access) FROM `total` WHERE `id_order` = @oID", db.getConnect());
            MySqlCommand qweryAccess = new MySqlCommand("SELECT `name`, `image` FROM `accessories` WHERE `id_access` = @aID", db.getConnect());

            adapter.SelectCommand = qweryID;
            adapter.Fill(getter);

            string[] stringArrCustomID = new string[getter.Rows.Count + 1];
            string[] stringArrCustomName = new string[getter.Rows.Count + 1];
            string[] stringArrID = new string[getter.Rows.Count + 1];
            DateTime[] stringArrDate = new DateTime[getter.Rows.Count + 1];
            string[] stringArrStatus = new string[getter.Rows.Count + 1];

            int calc = 0;
            int iterateCount = 0;

            foreach(DataRow rows in getter.Rows)
            {
                stringArrID[calc] = rows["id_order"].ToString();
                stringArrCustomID[calc] = rows["id_custom"].ToString();
                stringArrDate[calc] = DateTime.Parse(rows["order_date"].ToString());
                stringArrStatus[calc] = rows["status"].ToString();
                calc++;
                iterateCount++;
            }

            calc = 0;
            int calc2 = 0;

            foreach (DataRow rows in getter.Rows)
            {
                qweryCustom.Parameters.Clear();
                qweryCustom.Parameters.Add("@cID", MySqlDbType.Int32).Value = int.Parse(stringArrCustomID[calc]);
                adapter.SelectCommand = qweryCustom;
                adapter.Fill(getterAll);
                foreach(DataRow rowsCustomers in getterAll.Rows)
                {
                    stringArrCustomName[calc2] = rowsCustomers["firstname"].ToString() + " " + rowsCustomers["lastname"].ToString();
                }
                calc++;
            }

            int countAccess;
            orders tableGen = new orders();

            int height = 0;

            db.dbConnect();
            for(int i = 0; i < iterateCount; i++)
            {
                qweryAccessCount.Parameters.Clear();
                qweryAccess.Parameters.Clear();
                qweryAccessCount.Parameters.Add("@oID", MySqlDbType.Int32).Value = int.Parse(stringArrID[i]);
                qweryAccess.Parameters.Add("@aID", MySqlDbType.Int32).Value = int.Parse(stringArrID[i]);
                
                countAccess = int.Parse(qweryAccessCount.ExecuteScalar().ToString());
                adapter.SelectCommand = qweryAccess;
                adapter.Fill(getterAccess);

                string[] images = new string[countAccess + 1];
                string[] names = new string[countAccess + 1];
                string[] accessID = new string[countAccess + 1];
                string[] counts = new string[countAccess + 1];

                qweryAccessID.Parameters.Clear();
                qweryAccessID.Parameters.Add("@oID", MySqlDbType.Int32).Value = int.Parse(stringArrID[i]);
                adapter.SelectCommand = qweryAccessID;
                getterAccessID.Clear();
                adapter.Fill(getterAccessID);

                int cnt = 0;

                foreach(DataRow rows in getterAccessID.Rows)
                {
                    accessID[cnt] = rows["id_access"].ToString();
                    counts[cnt] = rows["number"].ToString();
                    cnt++;
                }

                cnt = 0;

                for(int j = 0; j < countAccess; j++)
                {
                    qweryAccess.Parameters.Clear();
                    qweryAccess.Parameters.Add("@aID", MySqlDbType.Int32).Value = int.Parse(accessID[j]);
                    adapter.SelectCommand = qweryAccess;
                    getterAccess.Clear();
                    adapter.Fill(getterAccess);

                    cnt = 0;

                    foreach(DataRow rows in getterAccess.Rows)
                    {
                        images[cnt] = rows["image"].ToString();
                        names[cnt] = rows["name"].ToString();
                        cnt++;
                    }
                }
           
                string heading = "#" + stringArrID[i] + "   " + stringArrCustomName[i] + "   " + stringArrDate[i].ToString("yyyy'-'MM'-'dd");

                contentPanel.Controls.Add(tableGen.addOrderComponent(contentPanel.Width, heading, stringArrStatus[i], images, names, counts, countAccess, stringArrID[i], height));
                height += 235;
            }

            db.dbDisconnect();
        }

        private void access_Click(object sender, EventArgs e)
        {
            dataBase db = new dataBase();

            DataTable getter = new DataTable();

            MySqlCommand qweryAccessData = new MySqlCommand("SELECT * FROM `accessories`", db.getConnect());

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter.SelectCommand = qweryAccessData;
            adapter.Fill(getter);

            string[] id = new string[getter.Rows.Count];
            string[] names = new string[getter.Rows.Count];
            string[] prices = new string[getter.Rows.Count];
            string[] description = new string[getter.Rows.Count];
            string[] quantity = new string[getter.Rows.Count];
            string[] types = new string[getter.Rows.Count];
            string[] images = new string[getter.Rows.Count];
            string[] makers = new string[getter.Rows.Count];
            string[] dates = new string[getter.Rows.Count];

            int cnt = 0;

            foreach (DataRow row in getter.Rows)
            {
                id[cnt] = row["id_access"].ToString();
                names[cnt] = row["name"].ToString();
                prices[cnt] = row["price"].ToString();
                description[cnt] = row["description"].ToString();
                quantity[cnt] = row["quantity"].ToString();
                types[cnt] = row["type"].ToString();
                images[cnt] = row["image"].ToString();
                makers[cnt] = row["maker"].ToString();
                dates[cnt] = row["delivery_date"].ToString();
                cnt++;
            }

            accessories accessGen = new accessories();

            Button addAccess = new Button()
            {
                Tag = "addAccess",
                Text = "Додати",
                Location = new Point(contentPanel.Width - 80, 10),
                TabIndex = 1,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            addAccess.Click += new EventHandler(addAccess_Click);

            contentPanel.Controls.Clear();
            contentPanel.Controls.Add(addAccess);

            int heigh = 40;

            for (int i = 0; i < getter.Rows.Count; i++)
            {
                contentPanel.Controls.Add(accessGen.addAccessComponent(names[i], description[i], prices[i], quantity[i], images[i], contentPanel.Width, id[i], heigh));
                heigh += 155;
            }
        }

        private void bin_Click(object sender, EventArgs e)
        {
            dataBase db = new dataBase();
            DataTable getter = new DataTable();
            basket bin = new basket();

            MySqlCommand qweryAccess = new MySqlCommand("SELECT `name`, `image` FROM `accessories` WHERE `id_access` = @aID", db.getConnect());

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            string[] IDs = accessories.getIDs();
            string[] names = new string[IDs.Length - 1];
            string[] images = new string[IDs.Length - 1];
            string counts = "1";
            int cnt = 0;
            for (int i = 0; i < IDs.Length - 1; i++)
            {
                qweryAccess.Parameters.Clear();
                qweryAccess.Parameters.Add("@aID", MySqlDbType.Int32).Value = int.Parse(IDs[i]);
                adapter.SelectCommand = qweryAccess;
                adapter.Fill(getter);
                cnt = 0;

                foreach (DataRow row in getter.Rows)
                {
                    names[cnt] = row["name"].ToString();
                    images[cnt] = row["image"].ToString();
                    cnt++;
                }
            }

            Button complete = new Button()
            {
                Tag = IDs,
                Text = "Готово",
                Location = new Point(contentPanel.Width - 80, 10),
                Anchor = (AnchorStyles.Top | AnchorStyles.Right)
            };

            complete.Click += new EventHandler(binComplate_Click);

            contentPanel.Controls.Clear();
            if(IDs[0] != null)
            {
                contentPanel.Controls.Add(complete);
                int width = 0;
                for (int i = 0; i < IDs.Length - 1; i++)
                {
                    contentPanel.Controls.Add(bin.addComponent(contentPanel.Width, images[i], names[i], counts, width));
                    width += 115;
                }
            }
        }

        private void binComplate_Click(object sender, EventArgs e)
        {
            Button complate = sender as Button;
            string[] ids = (string[])complate.Tag;
            string[] counts = new string[ids.Length];
            byte cnt = 0;
            foreach(Control i in contentPanel.Controls)
            {
                if(i is Panel)
                {
                    foreach(Control j in i.Controls)
                    {
                        if(j is NumericUpDown)
                        {
                            counts[cnt] = j.Text;
                            cnt++;
                        }
                    }
                }
            }

            Second secondForm = new Second(ids, counts);
            secondForm.Show();

        }

        private void addCustom_Click(object sender, EventArgs e)
        {
            Button addCustom = sender as Button;
            string data = (string)addCustom.Tag;
            Second secondForm = new Second(data);
            secondForm.Show();
        }

        private void editCustom_Click(object sender, EventArgs e)
        {
                dataBase db = new dataBase();
                MySqlCommand editCustomQwery = new MySqlCommand("UPDATE `customers` SET `firstname` = @firstname, `lastname` = @lastname, `surname` = @surname, `email` = @email, `phone_number` = @phone_number WHERE `id_custom` = @id_custom", db.getConnect());
                Button editCustom = sender as Button;
                DataTable data = (DataTable)editCustom.Tag;

                foreach (DataRow row in data.Rows)
                {
                    editCustomQwery.Parameters.Clear();
                    editCustomQwery.Parameters.Add("@id_custom", MySqlDbType.Int32).Value = int.Parse(row["id_custom"].ToString());
                    editCustomQwery.Parameters.Add("@firstname", MySqlDbType.VarChar).Value = row["firstname"].ToString();
                    editCustomQwery.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = row["lastname"].ToString();
                    editCustomQwery.Parameters.Add("@surname", MySqlDbType.VarChar).Value = row["surname"].ToString();
                    editCustomQwery.Parameters.Add("@email", MySqlDbType.VarChar).Value = row["email"].ToString();
                    editCustomQwery.Parameters.Add("@phone_number", MySqlDbType.VarChar).Value = row["phone_number"].ToString();
                    db.dbConnect();
                    editCustomQwery.ExecuteNonQuery();
                    db.dbDisconnect();
                }
        }

        private void deleteCustom_Click(object sender, EventArgs e)
        {
            Button deleteCustom = sender as Button;
            DataGridView data = (DataGridView)deleteCustom.Tag;

            if(data.CurrentRow == null)
                return;

            dataBase db = new dataBase();
            MySqlCommand deleteCustomQwery = new MySqlCommand("DELETE FROM `customers` WHERE `id_custom` = @id_custom", db.getConnect());
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter.DeleteCommand = deleteCustomQwery;
            deleteCustomQwery.Parameters.Add("@id_custom", MySqlDbType.Int32).Value = data.Rows[data.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            db.dbConnect();
            deleteCustomQwery.ExecuteNonQuery();
            db.dbDisconnect();
            data.Rows.Remove(data.CurrentRow);
        }

        private void addMaker_Click(object sender, EventArgs e)
        {
            Button addMaker = sender as Button;
            string data = (string)addMaker.Tag;
            Second secondForm = new Second(data);
            secondForm.Show();
        }

        private void editMaker_Click(object sender, EventArgs e)
        {
            dataBase db = new dataBase();
            MySqlCommand editMakerQwery = new MySqlCommand("UPDATE `makers` SET `name` = @name WHERE `id_maker` = @id_maker", db.getConnect());
            Button editMaker = sender as Button;
            DataTable data = (DataTable)editMaker.Tag;

            foreach (DataRow row in data.Rows)
            {
                editMakerQwery.Parameters.Clear();
                editMakerQwery.Parameters.Add("@id_maker", MySqlDbType.Int32).Value = int.Parse(row["id_maker"].ToString());
                editMakerQwery.Parameters.Add("@name", MySqlDbType.VarChar).Value = row["name"].ToString();
                db.dbConnect();
                editMakerQwery.ExecuteNonQuery();
                db.dbDisconnect();
            }
        }

        private void deleteMaker_Click(object sender, EventArgs e)
        {
            Button deleteMaker = sender as Button;
            DataGridView data = (DataGridView)deleteMaker.Tag;

            if (data.CurrentRow == null)
                return;

            dataBase db = new dataBase();
            MySqlCommand deleteMakerQwery = new MySqlCommand("DELETE FROM `makers` WHERE `id_maker` = @id_maker", db.getConnect());
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            adapter.DeleteCommand = deleteMakerQwery;
            deleteMakerQwery.Parameters.Add("@id_maker", MySqlDbType.Int32).Value = data.Rows[data.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            db.dbConnect();
            deleteMakerQwery.ExecuteNonQuery();
            db.dbDisconnect();
            data.Rows.Remove(data.CurrentRow);
        }

        private void addAccess_Click(object sender, EventArgs e)
        {
            Button AddAccess = sender as Button;
            string data = (string)AddAccess.Tag;
            Second secondForm = new Second(data);
            secondForm.Show();
        }

        private void editAccess_Click(object sender, EventArgs e)
        {
            Button editAccess = sender as Button;
            string data = (string)editAccess.Tag;
            Second secondForm = new Second(data);
            secondForm.Show();
        }

        private void deleteAccess_Click(object sender, EventArgs e)
        {
            Button deleteAccess = sender as Button;
            string data = (string)deleteAccess.Tag;
            Second secondForm = new Second(data);
            secondForm.Show();
        }
    }
}