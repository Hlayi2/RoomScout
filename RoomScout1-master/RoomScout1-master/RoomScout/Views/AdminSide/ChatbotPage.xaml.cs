using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace RoomScout.Views.AdminSide
{
    public partial class ChatbotPage : ContentPage
    {
        public class ChatMessage
        {
            public string Message { get; set; }
            public bool IsUser { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private ObservableCollection<ChatMessage> Messages { get; } = new();
        private Dictionary<int, string> listingResponses = new();
        private Dictionary<int, string> requiredDocumentsResponses = new();
        private Dictionary<int, string> accountResponses = new();
        private Dictionary<int, string> premiumResponses = new();

        // State variable to track conversation context
        private string _currentContext = "Welcome";

        public ChatbotPage()
        {
            InitializeComponent();
            ChatHistoryList.ItemsSource = Messages;
            InitializeResponses();

            // Display the welcome message when the page loads
            DisplayWelcomeMessage();

            // Attach a single event handler for user input
            UserInput.Completed += OnUserInputCompleted;
        }

        private void DisplayWelcomeMessage()
        {
            AddMessage("Hello, I'm Roomy, RoomScout's chatbot. How may I assist you today?", false);
        }

        private void AddMessage(string message, bool isUser)
        {
            Messages.Add(new ChatMessage
            {
                Message = message,
                IsUser = isUser,
                Timestamp = DateTime.Now
            });

            // Scroll to the latest message
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(100);
                await ChatHistoryScrollView.ScrollToAsync(0, ChatHistoryScrollView.ContentSize.Height, true);
            });
        }

        private void InitializeResponses()
        {
            listingResponses = new Dictionary<int, string>
            {
                { 1, "On Profile, navigate to 'Add new Listing'. That is where you can add your accommodation." },
                { 2, "Yes, you can edit your listing by selecting 'View Listings' and clicking 'Edit' next to the desired listing." },
                { 3, "You cannot remove a listing as yet, you can only select if it is available or not." },
                { 4, "On profile click on 'View my Listings' where you will find the listings you have added." }
            };

            requiredDocumentsResponses = new Dictionary<int, string>
            {
                { 1, "Yes you can do so by emailing: info@roomscout.co.za and requesting to update documents." },
                { 2, "Document verification can take up to 2 working days. An email will be sent to you regarding the next steps." },
                { 3, "Your account won't be verified. To avoid this make sure you upload the correct and valid documents" }
            };

            accountResponses = new Dictionary<int, string>
            {
                { 1, "Yes. If your account is reported a number of times it will get banned and you can no longer use RoomScout." },
                { 2, "You cannot necessarily report tenants, you are only allowed to accept or reject a tenant's booking request." },
                { 3, "You can interact with tenants through booking requests. They can also contact you based on the information you would have shared on RoomScout." },
                { 4, "Yes, you can retrieve your account within 30 days of deleting it. However, when 30 days have passed your account will be permanently deleted." }
            };

            premiumResponses = new Dictionary<int, string>
            {
                { 1, "RoomScout premium is a powerful yet convinient upgrade for landlords who want to maximiza their rental success.It provides more features including a verified bedge and lots more" },
                { 2, "You can simply go to the premium page scroll to your current plan and then click cancel subscription. " },
                { 3, "With RoomScout premium you can get listing ratings, teanants can pay directly to you on the app, you can control you listing availabilty and many more. Go to the premium page to find out more!" }
            };
        }

        private async void OnUserInputCompleted(object sender, EventArgs e)
        {
            await ProcessUserInput();
        }

        private async void OnSendMessageClicked(object sender, EventArgs e)
        {
            await ProcessUserInput();
        }

        private async Task ProcessUserInput()
        {
            string userMessage = UserInput.Text?.Trim();

            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                AddMessage(userMessage, true);
                UserInput.Text = string.Empty;

                // Show typing indicator
                TypingIndicator.IsVisible = true;
                await Task.Delay(1000); // Simulate typing delay

                // Handle the user's input based on the current context
                switch (_currentContext)
                {
                    case "Welcome":
                        // After the first user message, show the main menu
                        ShowMainMenu();
                        break;

                    case "MainMenu":
                        // Handle the user's selection from the main menu
                        await HandleMainMenuSelection(userMessage);
                        break;

                    case "Listings":
                    case "RequiredDocuments":
                    case "Account":
                    case "Premium":
                        // Handle sub-questions based on the current context
                        await HandleSubQuestion(userMessage);
                        break;

                    case "Other":
                        // Handle the "Other" option
                        await HandleOtherQuery(userMessage);
                        break;

                    default:
                        AddMessage("Invalid context. Please try again.", false);
                        break;
                }

                // Hide typing indicator
                TypingIndicator.IsVisible = false;
            }
        }

        private async Task HandleMainMenuSelection(string userInput)
        {
            if (int.TryParse(userInput, out int option) && option >= 1 && option <= 5)
            {
                switch (option)
                {
                    case 1:
                        _currentContext = "Listings";
                        await AskListingQuestions();
                        break;
                    case 2:
                        _currentContext = "RequiredDocuments";
                        await AskRequiredDocumentsQuestions();
                        break;
                    case 3:
                        _currentContext = "Account";
                        await AskAccountQuestions();
                        break;
                    case 4:
                        _currentContext = "Premium";
                        await AskPremiumQuestions();
                        break;
                    case 5:
                        _currentContext = "Other";
                        await HandleOtherOption();
                        break;
                }
            }
            else
            {
                AddMessage("Invalid input. Please type a number between 1 and 5.", false);
                ShowMainMenu();
            }
        }

        private void ShowMainMenu()
        {
            AddMessage("Please select an option by typing a number between 1 and 5:\n" +
                       "1. Listings\n" +
                       "2. Required Documents\n" +
                       "3. Account\n" +
                       "4. Premium Features\n" +
                       "5. Other", false);
            _currentContext = "MainMenu";
        }

        private async Task AskListingQuestions()
        {
            AddMessage("You have selected **Listings**. Please enter the number corresponding to your question:\n" +
                       "1. How do I add a listing?\n" +
                       "2. Can I edit my listing?\n" +
                       "3. How do I remove a listing?\n" +
                       "4. Where do I find my listings?", false);
        }

        private async Task AskRequiredDocumentsQuestions()
        {
            AddMessage("You have selected **Required Documents**. Please enter the number corresponding to your question:\n" +
                       "1. Can I update documents I have already uploaded?\n" +
                       "2. How long does it take to get verified?\n" +
                       "3. What happens if I upload invalid documents?", false);
        }

        private async Task AskAccountQuestions()
        {
            AddMessage("You have selected **Account**. Please enter the number corresponding to your question:\n" +
                       "1. Can my account be banned?\n" +
                       "2. Can I report tenants?\n" +
                       "3. How do I interact with tenants?\n" +
                       "4. Can I retrieve my account if it is permanently deleted?", false);
        }

        private async Task AskPremiumQuestions()
        {
            AddMessage("You have selected **Premium Features**. Please enter the number corresponding to your question:\n" +
                       "1. Why upgrade to premium?\n" +
                       "2. How do i cancel premium subscription?\n" +
                       "3. How is premium different from freemium?", false);
        }

        private async Task HandleSubQuestion(string userInput)
        {
            Dictionary<int, string> responses = _currentContext switch
            {
                "Listings" => listingResponses,
                "RequiredDocuments" => requiredDocumentsResponses,
                "Account" => accountResponses,
                "Premium" => premiumResponses,
                _ => null
            };

            if (responses != null && int.TryParse(userInput, out int questionNumber) && responses.ContainsKey(questionNumber))
            {
                AddMessage(responses[questionNumber], false);
            }
            else
            {
                AddMessage("Please enter a valid number from the list provided.", false);
            }

            // Ask if the user still needs help
            await AskIfUserNeedsHelp();
        }

        private async Task HandleOtherOption()
        {
            AddMessage("Send a query to management, and it will be attended to soon.", false);
        }

        private async Task HandleOtherQuery(string userInput)
        {
            AddMessage("Query recorded!", false);

            // Ask if the user still needs help
            await AskIfUserNeedsHelp();
        }

        private async Task AskIfUserNeedsHelp()
        {
            // Show Yes/No buttons
            YesNoButtons.IsVisible = true;

            // Add a message asking if the user needs further assistance
            AddMessage("Do you still need help?", false);
        }

        private async void OnYesClicked(object sender, EventArgs e)
        {
            // Hide Yes/No buttons
            YesNoButtons.IsVisible = false;

            // Show the main menu again
            ShowMainMenu();
        }

        private async void OnNoClicked(object sender, EventArgs e)
        {
            // Hide Yes/No buttons
            YesNoButtons.IsVisible = false;

            // Add a farewell message
            AddMessage("I am happy I could help. I am here if you need further assistance.", false);
        }
    }
}