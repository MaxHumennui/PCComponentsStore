using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCShop
{
    class basket
    {
        public Panel addComponent(int PanelWidth, string image, string name, string count, int width)
        {
            Panel component = new Panel()
            {
                Size = new Size(110, 160),
                Anchor = (AnchorStyles.Left | AnchorStyles.Top),
                Location = new Point(width, 30)
            };

            PictureBox picture = new PictureBox()
            {
                ImageLocation = image,
                Size = new Size(110, 100),
                Location = new Point(0, 0),
                ErrorImage = Image.FromFile(@"resources\errorimage\Error100x100.gif")
            };

            TextBox accessName = new TextBox()
            {
                ReadOnly = true,
                Text = name,
                Size = new Size(110, 20),
                Location = new Point(0, picture.Height + 5)
            };

            NumericUpDown accessCount = new NumericUpDown()
            {
                Name = "numb",
                ReadOnly = true,
                Text = count,
                Size = new Size(110, 20),
                Location = new Point(0, picture.Height + accessName.Height + 10)
            };

            component.Controls.Add(picture);
            component.Controls.Add(accessName);
            component.Controls.Add(accessCount);

            return component;
        }
    }
}
