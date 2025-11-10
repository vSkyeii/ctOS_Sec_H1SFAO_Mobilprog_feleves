namespace ctOS_Sec_H1SFAO_Mobilprog_feleves;

public partial class ClientPage : ContentPage
{
	public ClientPage(ClientViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ClientViewModel vm)
        {
            await vm.InitializeAsync();
        }
    }

}