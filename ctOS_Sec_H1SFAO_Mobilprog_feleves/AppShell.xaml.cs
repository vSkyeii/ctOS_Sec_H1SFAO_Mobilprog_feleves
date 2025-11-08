namespace ctOS_Sec_H1SFAO_Mobilprog_feleves
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Client", typeof(ClientPage));
            Routing.RegisterRoute("Admin", typeof(AdminPage));
            Routing.RegisterRoute("ClientDetail", typeof(ClientDetailPage));
        }
    }
}
