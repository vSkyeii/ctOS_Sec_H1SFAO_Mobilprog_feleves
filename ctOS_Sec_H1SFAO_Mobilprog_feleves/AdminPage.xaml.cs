namespace ctOS_Sec_H1SFAO_Mobilprog_feleves;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}