using System;

namespace MPS
{
    static class Program
    {
        static Game1 game;

        [STAThread]
        static void Main()
        {
            formMain form = new formMain();
            form.Disposed += form_Disposed;
            using (game = new Game1(form))
            {
                form.Show();
                form.TopMost = true;
                game.Run();
            }
        }

        static void form_Disposed(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
}

