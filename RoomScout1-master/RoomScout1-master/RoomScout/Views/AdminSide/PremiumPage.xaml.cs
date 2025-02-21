namespace RoomScout.Views.AdminSide;

public partial class PremiumPage : ContentPage
{
    public PremiumPage()
    {
        InitializeComponent();
    }
    private async void OnUpgradeButtonClicked1(object sender, EventArgs e)
    {

        if (sender is Button button && button.CommandParameter is string amountStr)
        {
            if (decimal.TryParse(amountStr, out decimal amount))
            {
                // Navigate to PayPal with the selected amount
                await Shell.Current.GoToAsync($"paypal?amount={amount}");
            }
        }
    }
    private async void OnUpgradeButtonClicked2(object sender, EventArgs e)
    {

        if (sender is Button button && button.CommandParameter is string amountStr)
        {
            if (decimal.TryParse(amountStr, out decimal amount))
            {
                // Navigate to PayPal with the selected amount
                await Shell.Current.GoToAsync($"paypal?amount={amount}");
            }

        }
    }
    private async void OnUpgradeButtonClicked3(object sender, EventArgs e)
    {

        if (sender is Button button && button.CommandParameter is string amountStr)
        {
            if (decimal.TryParse(amountStr, out decimal amount))
            {
                // Navigate to PayPal with the selected amount
                await Shell.Current.GoToAsync($"paypal?amount={amount}");
            }


        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}