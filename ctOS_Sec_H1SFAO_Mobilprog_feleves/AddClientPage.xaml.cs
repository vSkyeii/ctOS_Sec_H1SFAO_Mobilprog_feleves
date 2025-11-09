namespace ctOS_Sec_H1SFAO_Mobilprog_feleves;

public partial class AddClientPage : ContentPage
{
    public AddClientPage(AddNewClientViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ClientDetailViewModel vm)
        {
            await vm.LoadFromContextAsync();
        }
    }
}