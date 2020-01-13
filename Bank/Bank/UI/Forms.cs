using System.Windows.Forms;

namespace Bank.Forms
{
    class Forms
    {
        public static void LoadForm(Form formToLoad, Form currentForm)
        {
            formToLoad.Location = currentForm.Location;
            formToLoad.StartPosition = FormStartPosition.Manual;
            formToLoad.FormClosing += delegate { currentForm.Show(); };
            formToLoad.Show();
            currentForm.Hide();
        }

        public static Button NormalizeBackButton(Button button)
        {
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            return button;
        }
    }
}
