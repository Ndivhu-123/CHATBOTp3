using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CHATBOTp3
{
    public partial class MainWindow : Window
    {

        private List<TaskItem> taskList = new();
        private List<string> activityLog = new();

        public MainWindow()
        {
            InitializeComponent();

            new random_response();
            new chat_responder();
        }

        //AddBot Message method
        private void AddBotMessage(string message)
        {
            show_chat.Items.Add("ChatBot: " + message);
        }

        // Task Model
        public class TaskItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? Reminder { get; set; }

            public override string ToString()
            {
                string reminderText = Reminder.HasValue ? $" (Reminder: {Reminder.Value.ToShortDateString()})" : "";
                return $"{Title} - {Description}{reminderText}";
            }
        }

        // Show intro prompt when entering chat
        private void SetInitialPrompt()
        {
            TextBlock prompt = new TextBlock
            {
                Text = "LETS GET STARTED",
                Foreground = Brushes.DarkGray,
                FontSize = 20,
                Margin = new Thickness(15)
            };
            show_chat.Items.Add(prompt);
        }

        // Update UI depending on which app mode is active
        private void UpdateAppContext(string contextName)
        {
            show_chat.Items.Clear();

            TextBlock sectionPrompt = new TextBlock
            {
                Text = $"You are now in {contextName}",
                Foreground = Brushes.Gray,
                FontStyle = FontStyles.Italic,
                Margin = new Thickness(5)
            };
            show_chat.Items.Add(sectionPrompt);

            show_chat.Items.Add($"Hey there, I'M CHATBOT ready to assist you, {userNameBox.Text.Trim()}!\nLet's get started — how can I help you today?");

            bool isTaskMode = contextName == "Manage Tasks";

            TaskColumn.Width = isTaskMode ? new GridLength(300) : new GridLength(0);
            Manage_Task.Visibility = isTaskMode ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(userNameBox.Text))
            {
                WelcomeGrid.Visibility = Visibility.Collapsed;
                ChatGrid.Visibility = Visibility.Visible;

                new voice_greeting();
                SetInitialPrompt();
            }
            else
            {
                MessageBox.Show("Please enter your first name to continue.",
                                "Name Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitleBox.Text.Trim();
            string desc = TaskDescBox.Text.Trim();
            DateTime? reminderDate = TaskReminderDate.SelectedDate;

            if (string.IsNullOrWhiteSpace(title)) return;

            TaskItem task = new() { Title = title, Description = desc, Reminder = reminderDate };
            taskList.Add(task);
            TaskListBox.Items.Add(task);

            AddBotMessage($"Task added: '{title}'. {(reminderDate != null ? $"Reminder set for {reminderDate:MMM dd}." : "")}");
            activityLog.Add($"Task added: {title} ({desc}){(reminderDate != null ? $" [Reminder: {reminderDate:MMM dd}]" : "")}");

            TaskTitleBox.Clear();
            TaskDescBox.Clear();
            TaskReminderDate.SelectedDate = null;
        }

        private void ToggleTaskPanel_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppContext("Manage Tasks");
        }

        private void AskQuestions_Click(object sender, RoutedEventArgs e)
        {
            UpdateAppContext("Ask Questions");

            var btn = sender as Button;
            btn.Background = Brushes.LightGreen;
            btn.Content = "Ask Questions";
            btn.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFD3D3D3"));
        }
        // Define question model class
        public class QuizQuestion
        {
            public string Question { get; set; } // Text of the quiz question
            public List<string> Answers { get; set; } // Answer choices (a, b, c)
            public int CorrectIndex { get; set; } // Index of the correct choice
            public string Explanation { get; set; } // Feedback explanation for answer
        }

        private List<QuizQuestion> quizQuestions = new(); // Complete quiz question list
        private int currentQuestionIndex = 0; // Tracks the active question
        private int quizScore = 0; // Tracks the user's total score


        // Triggered when "Start Quiz" is clicked
        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            LoadQuizQuestions(); // Populate questions
            currentQuestionIndex = 0;
            quizScore = 0;

            ShowNextQuestion(); // Show first question
            QuizPanel.Visibility = Visibility.Visible; // Show quiz UI
        }

        // Loads 20 cybersecurity-related questions into the quiz
        private void LoadQuizQuestions()
        {
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What is phishing?",
                    Answers = ["a) Online fishing hobby", "b) Malware", "c) Social engineering attack"],
                    CorrectIndex = 2,
                    Explanation = "Phishing tricks users into giving up personal info by impersonating legitimate sources."
                },
                new QuizQuestion
                {
                    Question = "Which is the best password?",
                    Answers = ["a) 123456", "b) Hello123", "c) T&k9#pL!r"],
                    CorrectIndex = 2,
                    Explanation = "Strong passwords use symbols, numbers, and uppercase/lowercase mix."
                },
                new QuizQuestion
                {
                    Question = "Should you reuse the same password across websites?",
                    Answers = ["a) Yes", "b) No", "c) Sometimes"],
                    CorrectIndex = 1,
                    Explanation = "Reusing passwords increases risk — if one leaks, others can be compromised."
                },
                new QuizQuestion
                {
                    Question = "What does HTTPS indicate?",
                    Answers = ["a) Secure connection", "b) Unsecured site", "c) High traffic site"],
                    CorrectIndex = 0,
                    Explanation = "HTTPS ensures encrypted communication between browser and website."
                },
                new QuizQuestion
                {
                    Question = "True or False: Antivirus protects against phishing.",
                    Answers = ["a) True", "b) False", "c) Depends on browser"],
                    CorrectIndex = 0,
                    Explanation = "Antivirus can help detect phishing but is not a complete solution."
                },
                new QuizQuestion
                {
                    Question = "What is 2FA (Two-Factor Authentication)?",
                    Answers = ["a) A password", "b) Backup device", "c) Extra login step for verification"],
                    CorrectIndex = 2,
                    Explanation = "2FA adds an extra layer of identity verification."
                },
                new QuizQuestion
                {
                    Question = "How often should you update your password?",
                    Answers = ["a) Never", "b) Every 2–3 months", "c) Only when prompted"],
                    CorrectIndex = 1,
                    Explanation = "Regular updates help prevent long-term vulnerabilities."
                },
                new QuizQuestion
                {
                    Question = "Social engineering targets...",
                    Answers = ["a) Computers", "b) Human psychology", "c) Routers"],
                    CorrectIndex = 1,
                    Explanation = "It manipulates people, not systems, into giving up secure data."
                },
                new QuizQuestion
                {
                    Question = "Which is a safe URL?",
                    Answers = ["a) http://bank.com", "b) https://secure.bank.com", "c) www-bank.com"],
                    CorrectIndex = 1,
                    Explanation = "Always prefer 'https' with a known domain structure."
                },
                new QuizQuestion
                {
                    Question = "What should you do after clicking a suspicious link?",
                    Answers = ["a) Share it", "b) Close tab", "c) Disconnect from the internet and scan system"],
                    CorrectIndex = 2,
                    Explanation = "Isolate the system to prevent spread of potential malware."
                },

                // Add 10 more similarly styled questions below for a full 20-question quiz...
                // You can repeat this pattern for a full professional quiz experience
            };
        }

        // Displays current question or finishes quiz if done
        private void ShowNextQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                int finalScore = quizScore * 5;
                string result = finalScore > 50
                    ? "Great job! You're a cybersecurity pro!"
                    : "Keep learning to stay safe online!";

                MessageBox.Show($"Quiz Complete!\nScore: {finalScore}/{quizQuestions.Count * 5}\n{result}", "Result");
                QuizPanel.Visibility = Visibility.Collapsed;
                return;
            }

            // Populate UI with current question text and answers
            var question = quizQuestions[currentQuestionIndex];
            QuizQuestionText.Text = $"{currentQuestionIndex + 1}. {question.Question}";
            QuizAnswersList.ItemsSource = question.Answers;
            QuizAnswersList.SelectedIndex = -1;
        }

        // Evaluates user's answer when "Submit" is clicked
        private void SubmitQuizAnswer_Click(object sender, RoutedEventArgs e)
        {
            int selected = QuizAnswersList.SelectedIndex;
            if (selected == -1)
            {
                MessageBox.Show("Please select an answer.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var question = quizQuestions[currentQuestionIndex];

            if (selected == question.CorrectIndex)
            {
                quizScore++;
                MessageBox.Show($"✔ Correct!\n{question.Explanation}", "Feedback", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"✖ Incorrect.\n{question.Explanation}", "Feedback", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            currentQuestionIndex++;
            ShowNextQuestion();
        }
    }
}

        
        // Core logic used by Ask and Send
        /*
        public void HandleQuestionInput()
        {
            string input = user_questions.Text.Trim();

            if (!ValidateInput(input)) return;

            SubmitMessage(input);

            sentiment_handler sentiment = new sentiment_handler();
            string mood = sentiment.DetectSentiment(input);
            show_chat.Items.Add($"(Detected mood: {mood})");

            random_response responder = new random_response();
            string tip = responder.GetRandomTip();
            show_chat.Items.Add("ChatBot: " + tip);

            user_questions.Clear();
            ScrollToLatest();
        }

        private void send_question(object sender, RoutedEventArgs e)
        {
            HandleQuestionInput();
        }

        public bool ValidateInput(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Oops! The question field can't be empty.\nPlease enter a question.",
                                "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void SubmitMessage(string input)
        {
            if (input.ToLower().Contains("add task"))
            {
                DateTime date = DateTime.Now.Date;
                DateTime time = DateTime.Now.ToLocalTime();
                string formattedDate = date.ToString("yyyy-MM-dd");

                show_chat.Items.Add("User: " + input +
                                    "\n" + formattedDate + " Time: " + time.ToShortTimeString());
            }
            else
            {
                show_chat.Items.Add("User: " + input);
            }
        }

        private void AddBotMessage(string message)
        {
            show_chat.Items.Add("ChatBot: " + message);
            ScrollToLatest();
        }

        private void ScrollToLatest()
        {
            if (show_chat.Items.Count > 0)
                show_chat.ScrollIntoView(show_chat.Items[show_chat.Items.Count - 1]);
        }

        private void show_chat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (show_chat.SelectedItem == null) return;

            int index = show_chat.SelectedIndex;

            if (show_chat.Items[index] is string text && !text.Contains("status done"))
            {
                MessageBox.Show(text);

                TextBlock styled = new TextBlock();
                styled.Inlines.Add(new Run(text)
                {
                    TextDecorations = TextDecorations.Strikethrough,
                    Foreground = Brushes.Black
                });
                styled.Inlines.Add(" ");
                styled.Inlines.Add(new Run("status done") { Foreground = Brushes.Green });

                show_chat.Items[index] = styled;
            }
            else if (show_chat.Items[index] is TextBlock)
            {
                show_chat.Items.RemoveAt(index);
            }
        }

        // Placeholder visibility toggle
        private void user_questions_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userPlaceholder != null)
            {
                userPlaceholder.Visibility = string.IsNullOrWhiteSpace(user_questions.Text)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Placeholder
        }
    
// Simulated NLP handler for flexible user phrasing
private void ProcessInput(string input)
        {
            input = input.ToLower().Trim();

            string[] taskKeywords = { "remind me", "add task", "set reminder", "remember to", "reminder" };
            string[] quizKeywords = { "quiz", "start quiz", "test me", "ask questions", "game" };
            string[] logKeywords = { "activity log", "what have you done", "recent actions", "show log" };
            string[] cyberKeywords = { "password", "phishing", "browsing", "malware", "security", "cyber" };

            if (taskKeywords.Any(k => input.Contains(k)))
            {
                AddBotMessage("Sure! You can enter a task title, description, and set a reminder.");
                activityLog.Add("User triggered a reminder/task request.");
            }
            else if (quizKeywords.Any(k => input.Contains(k)))
            {
                AddBotMessage("Launching the cybersecurity quiz now!");
                StartQuiz_Click(null, null);
            }
            else if (logKeywords.Any(k => input.Contains(k)))
            {
                ShowActivityLog();
            }
            else if (cyberKeywords.Any(k => input.Contains(k)))
            {
                AddBotMessage("I see you're interested in cybersecurity. Want to take a quiz or ask me a specific question?");
            }
            else
            {
                AddBotMessage("Try asking about tasks, quizzes, reminders, or anything related to cybersecurity!");
            }
        }

        //  Chat message handler
        private void send_question(object sender, RoutedEventArgs e)
        {
            string userText = user_questions.Text.Trim();

            if (!string.IsNullOrEmpty(userText))
            {
                show_chat.Items.Add($"{userName}: {userText}");
                ProcessInput(userText);
                activityLog.Add($"User said: {userText}");
                user_questions.Clear();
            }
        }

        //  Welcome screen transition
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            string name = userNameBox.Text.Trim();

            if (!string.IsNullOrEmpty(name))
            {
                userName = name;
                WelcomeGrid.Visibility = Visibility.Collapsed;
                ChatGrid.Visibility = Visibility.Visible;

                show_chat.Items.Add($"{userName}: Hi, I'm {userName}.");
                AddBotMessage($"Nice to meet you, {userName}! I’m here to help you stay safe online with quizzes, reminders, and friendly advice.");
            }
            else
            {
                MessageBox.Show("Please enter your name to continue.", "Name Required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //  Activity Log Display
        private void ShowActivityLog()
        {
            AddBotMessage("Here's a look at your recent actions:");
            foreach (var entry in activityLog.TakeLast(5))
            {
                show_chat.Items.Add($"• {entry}");
            }
        }

        // Utility to display bot responses
        private void AddBotMessage(string message)
        {
            show_chat.Items.Add($"Bot: {message}");
            activityLog.Add($"Bot: {message}");




        //Button for Showing activity log
        private void ShowActivityLog_Click(object sender, RoutedEventArgs e)
        {
            ShowActivityLog();
        }

        private void OpenActivityLog_Click(object sender, RoutedEventArgs e)
        {
            // Pass recent activity to the popup window
            var logWindow = new ActivityLogWindow(activityLog);
            logWindow.Show();
        }

        //Method for showing activity log
        private void ShowActivityLog()
        {
            ActivityLogList.Items.Clear();
            int count = 0;
            foreach (var entry in activityLog.Reverse<string>().Take(5))
            {
                ActivityLogList.Items.Add(entry);
                count++;
            }

            AddBotMessage($"Showing last {count} actions.");
        }

    }

}
*/