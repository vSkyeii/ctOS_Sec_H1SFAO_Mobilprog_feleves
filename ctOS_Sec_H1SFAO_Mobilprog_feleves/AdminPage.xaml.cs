namespace ctOS_Sec_H1SFAO_Mobilprog_feleves;

public partial class AdminPage : ContentPage
{
	public AdminPage(AdminViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AdminViewModel vm)
        {
            await vm.LoadClientsAsync();
        }
    }
    
}