using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace RoomScout.Views.StudentSide
{
    public partial class ChatPage : ContentPage
    {
        public class ChatMessage
        {
            public string Message { get; set; }
            public bool IsUser { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private ObservableCollection<ChatMessage> Messages { get; } = new();
        private Dictionary<int, string> listingResponses = new();
        private Dictionary<int, string> bookingsResponses = new();
        private Dictionary<int, string> accountResponses = new();

        // State variable to track conversation context
        private string _currentContext = "Welcome";
        public ChatPage()
        {
            InitializeComponent();
            ChatHistoryList.ItemsSource = Messages;
            InitializeResponses();
            DisplayWelcomeMessage();

            // Attach a single event handler for user input
            UserInput.Completed += OnUserInputCompleted;
        }

        private void DisplayWelcomeMessage()
        {
            AddMessage("Hello, I'm Roomy, RoomScout's chatbot. Here for your every need. How may I assist you today?", false);
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

        private async void InitializeResponses()
        {
            listingResponses = new Dictionary<int, string>
            {
                { 1, "A listing is a room/accomodation that landlords add and advertise to you tenants." },
                { 2, "Yes, you can book to view as many listings as you wish. Remember, viewing rooms/accomodations is subject to whether the landlord accepts your booking requests or not." },
                { 3, "You can report a suspicious listing that will be reviewed to verify its legitimacy. As it stands all accomodations listed on RoomScout are verified to give ou tenants the best of the best." },
                { 4, "If a Landlord is subscribed to premium there will be an option for you to pay for your new accomodation through RoomScout." }
            };

            bookingsResponses = new Dictionary<int, string>
            {
                { 1, "You book a viewing by clicking on the Book to view button in your home page." },
                { 2, "You will get a notification with information wheather your booking request had been approved or rejected from the landlord." },
                { 3, "Your cannot canel once a booking request had been approved you can only contact the landlord directly if there are any changes." }
            };

            accountResponses = new Dictionary<int, string>
            {
                { 1, "Yes, by clicking 'Delete My Account' on your profile and confirming that you want to permanently delete your account. It will be scheduled for permanent deletion in 30 days and you can only create a new one after the 30 day period has passed." },
                { 2, "No, you cannot register as a landlord and also as a tenant as your information is captured in our database and you cannot register again once your information is captured." },
                { 3, "An email with a link to verify your account is sent to you." }
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
                    case "Bookings":
                    case "Account":
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
            if (int.TryParse(userInput, out int option) && option >= 1 && option <= 4)
            {
                switch (option)
                {
                    case 1:
                        _currentContext = "Listings";
                        await AskListingQuestions();
                        break;
                    case 2:
                        _currentContext = "Bookings";
                        await AskBookingsQuestions();
                        break;
                    case 3:
                        _currentContext = "Account";
                        await AskAccountQuestions();
                        break;
                    case 4:
                        _currentContext = "Other";
                        await HandleOtherOption();
                        break;
                }
            }
            else
            {
                AddMessage("Invalid input. Please type a number between 1 and 4.", false);
                ShowMainMenu();
            }
        }

        private void ShowMainMenu()
        {
            AddMessage("Please select an option by typing a number between 1 and 4:\n" +
                       "1. Listings\n" +
                       "2. Bookings\n" +
                       "3. Account\n" +
                       "4. Other", false);
            _currentContext = "MainMenu";
        }

        private async Task AskListingQuestions()
        {
            AddMessage("You have selected **Listings**. Please enter the number corresponding to your question:\n" +
                       "1. What is a listing?\n" +
                       "2. Can I view more than one listing?\n" +
                       "3. Can i report a listing?\n" +
                       "4. Can i pay for a room using RoomScout listings?", false);
        }

        private async Task AskBookingsQuestions()
        {
            AddMessage("You have selected **Required Documents**. Please enter the number corresponding to your question:\n" +
                       "1. How do i book a viewing?\n" +
                       "2. How will i know if my booking request has been approved or not?\n" +
                       "3. Can i cancel my booking once it has been approved?", false);
        }

        private async Task AskAccountQuestions()
        {
            AddMessage("You have selected **Premium Features**. Please enter the number corresponding to your question:\n" +
                       "1. Can i permanently delete my account?\n" +
                       "2. Can i register and a landlord and also as a tenant?\n" +
                       "3. How is my account verified?", false);
        }

        private async Task HandleSubQuestion(string userInput)
        {
            Dictionary<int, string> responses = _currentContext switch
            {
                "Listings" => listingResponses,
                "Bookings" => bookingsResponses,
                "Account" => accountResponses,
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
            await Task.Delay(2500);
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

        public async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}