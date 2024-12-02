using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace RoomScout.Views.AdminSide;

public partial class AddListingPage : ContentPage
{
    public ObservableCollection<Rule> Rules { get; set; }

    public AddListingPage()
	{
		InitializeComponent();
        Rules = new ObservableCollection<Rule>();
        RulesList.ItemsSource = Rules;
    }

    private void OnPriceTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            return;

        // Only allow numbers and decimal point
        string newText = new string(e.NewTextValue.Where(c => char.IsDigit(c) || c == '.').ToArray());

        // Ensure only one decimal point
        if (newText.Count(c => c == '.') > 1)
        {
            newText = newText.Replace(".", "");
            if (newText.Length > 0)
                newText = newText.Insert(newText.Length - 2, ".");
        }

        // Format to 2 decimal places if there's a decimal point
        if (newText.Contains('.'))
        {
            string[] parts = newText.Split('.');
            if (parts[1].Length > 2)
                newText = parts[0] + "." + parts[1].Substring(0, 2);
        }

        // Update the entry only if the text has changed
        if (newText != e.NewTextValue)
        {
            ((Entry)sender).Text = newText;
        }
    }


    private void OnAddRuleClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(RuleEntry.Text))
            return;

        Rules.Add(new Rule
        {
            Number = $"{Rules.Count + 1}.",
            Text = RuleEntry.Text
        });

        RuleEntry.Text = string.Empty;
    }

    private void OnDeleteRuleClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Rule ruleToDelete)
        {
            Rules.Remove(ruleToDelete);
            UpdateRuleNumbers();
        }
    }

    private void UpdateRuleNumbers()
    {
        for (int i = 0; i < Rules.Count; i++)
        {
            Rules[i].Number = $"{i + 1}.";
        }
    }

    private async void OnSaveRulesClicked(object sender, EventArgs e)
    {
        if (Rules.Count > 0)
        {
            // Here you would typically save the rules to your database or storage
            await DisplayAlert("Success", "Rules have been saved!", "OK");
        }
        else
        {
            await DisplayAlert("Info", "No rules to save", "OK");
        }
    }

    private async void OnUploadClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                FileNameLabel.Text = result.Count() == 1
                    ? Path.GetFileName(result.First().FullPath)
                    : $"{result.Count()} files selected";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to pick file: " + ex.Message, "OK");
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        // Add your submission logic here
        await DisplayAlert("Success", "Listing uploaded successfully!", "OK");
    }
}

public class Rule : INotifyPropertyChanged
{
    private string number;
    private string text;

    public string Number
    {
        get => number;
        set
        {
            if (number != value)
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
    }

    public string Text
    {
        get => text;
        set
        {
            if (text != value)
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}