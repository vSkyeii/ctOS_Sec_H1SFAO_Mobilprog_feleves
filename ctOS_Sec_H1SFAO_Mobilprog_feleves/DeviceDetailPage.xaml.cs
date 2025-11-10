namespace ctOS_Sec_H1SFAO_Mobilprog_feleves;

public partial class DeviceDetailPage : ContentPage
{
    public DeviceDetailPage(DeviceDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DeviceDetailViewModel vm)
        {
            await vm.LoadDeviceAsync();
        }
    }
}