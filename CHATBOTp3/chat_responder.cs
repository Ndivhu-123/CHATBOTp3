using System.Collections;

namespace CHATBOTp3
{
    public class chat_responder
    {

        // Creating an instance of the Key_wordn class
        private readonly memory_manager memory = new memory_manager();


     private string userQuery = string.Empty;
    private string username = string.Empty;
    private ArrayList replies = new ArrayList();
    // public chat_responder() { }


    // Stores user-specific info like favorite topic
    private Dictionary<string, string> userMemory = new Dictionary<string, string>();

    // Tracks response turns for favorite topic callback
    private int responseCount = 0;

    // Sample keyword-response map
    private readonly Dictionary<string, List<string>> keywordResponses =
       new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
   {
            //phishing 
            { "phishing", new List<string>
                {
                    "Cybercriminals use phishing to steal sensitive information by pretending to be trusted entities. Be cautious of emails that urge immediate action.",
                    "Watch out for suspicious emails asking for personal information. These often contain urgent requests such as \"Your account will be locked\" or \"Verify your identity now\".",
                    "Don't click on unfamiliar links or download attachments from unknown sources. Attackers embed malware in these files to compromise your system.",
                    "Verify senders before responding to sensitive requests. Scammers impersonate banks, government agencies, or even coworkers to gain trust.",
                    "Phishing can happen via emails, SMS (\"smishing\"), phone calls (\"vishing\"), or social media messages. If something feels off, verify before clicking."
                }
            },
            // password 
            { "password", new List<string>
                {
                    "Strong passwords are essential for protecting online accounts. Use long, complex passwords with a mix of uppercase, lowercase, numbers, and special characters.",
                    "Avoid using the same password across multiple sites. If one gets breached, all your accounts could be compromised.",
                    "Consider using a password manager to securely store your credentials. It simplifies managing strong passwords without needing to memorise them.",
                    "Enable multi-factor authentication (MFA) wherever possible. This adds an extra layer of security, even if your password is compromised.",
                    "Avoid common passwords like '123456' or 'password'. Hackers can easily guess these."
                }
            },
            //safe browsing
            { "safe browsing", new List<string>
                {
                    "Always look for HTTPS in the URL before submitting any sensitive info. The 'S' means your data is encrypted and secure.",
                    "Use updated browsers and antivirus software to prevent vulnerabilities. Regular updates patch security flaws that attackers exploit.",
                    "Avoid clicking on pop-up ads and suspicious websites. Many of these contain malware or lead to scams designed to steal your data.",
                    "Be cautious when downloading software. Only install apps from trusted sources to reduce the risk of malware infections.",
                    "Always use a secure Wi-Fi connection when browsing sensitive websites. Avoid logging in to your bank or email on public Wi-Fi networks."
                }
            },
            // scam 
            { "scam", new List<string>
                {
                    "Online scams come in many forms, including fake websites, phishing, and fraudulent investment opportunities. Always verify sources before engaging.",
                    "Scammers often use urgency to pressure victims into quick action. If an offer sounds too good to be true, it probably is.",
                    "Fraudsters create fake customer support lines, impersonating trusted brands to steal your personal or financial details.",
                    "Be sceptical of messages claiming you've won a prize or lottery—especially if they ask for upfront payments or personal information.",
                    "Scammers prey on emotions, using fear or excitement to manipulate victims into making rash decisions. Take your time to verify before acting."
                }
            },
            /* ——— privacy ——— */
            { "privacy", new List<string>
                {
                    "Privacy online is about protecting your personal data from unauthorised access. Be mindful of the information you share on social media.",
                    "Review your privacy settings on all accounts to ensure minimal exposure of personal details.",
                    "Avoid oversharing personal information like location or financial data on public platforms to reduce the risk of identity theft.",
                    "Use encrypted messaging apps and secure email services to safeguard your communications from unauthorised surveillance.",
                    "Regularly update your passwords and enable two-factor authentication to enhance the security of your personal data."
                }
            }
   };
    private Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
         {
             { "worried", "I understand your concern. Staying informed helps against online scams!" },
             { "confused", "No worries, cybersecurity can be complex. Let me break it down for you!" },
             { "nervous", "I get that! The online world can be scary, but knowledge is power." },
             { "overwhelmed", "Cybersecurity has many layers, but I’m here to guide you step by step!" },
             { "anxious", "It's completely normal to feel anxious about online threats. Staying proactive will keep you safe!" },
             { "excited", "That’s great energy! Learning more about cybersecurity makes your online world safer." },
             { "curious", "Curiosity is key! The more you know, the better prepared you’ll be." },
             { "motivated", "I love that spirit! Strengthening your cybersecurity skills will really pay off." },
             { "frustrated", "I understand how frustrating cybersecurity challenges can be. Let’s work through them together!" },
             { "stressed", "Cybersecurity can seem overwhelming, but small steps make a big difference. I can help!" }
         };

    private Key_word keywordRecognition = new Key_word();

    public void StartConversation()
    {
        StoreReplies();

        // Chatbot asks for the user's name
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Chatbot: Hey there! What’s your first name?");
        Console.ResetColor();
        Console.WriteLine("   ");

        // User enters their name in magenta
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("You: ");
        Console.WriteLine("   ");

        username = Console.ReadLine();
        Console.ResetColor();

        // Chatbot greets the user
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"Chatbot: Hey {username}, how can I assist you today?");
        Console.WriteLine("   ");

        Console.ResetColor();

        do
        {
            // User enters query in magenta
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(username + ": ");
            userQuery = Console.ReadLine();

            Console.ResetColor();

            if (userQuery.ToLower() != "exit")
            {
                ProcessQuery(userQuery);
            }

        } while (userQuery.ToLower() != "exit");

        // Chatbot says goodbye
        Console.ForegroundColor = ConsoleColor.Yellow;



        Console.WriteLine("   ");

        // Ensure string interpolation is used properly
        Console.WriteLine($"Chatbot: Bye for now! Don't hesitate to reach out again, {username}! Have a great day ahead!");

        Console.ForegroundColor = ConsoleColor.Magenta;

        // Properly formatted separator for visual effect
        Console.WriteLine("............................................................................................");

        Console.ResetColor(); // Reset colors to default
    }

    private void StoreReplies()
    {
        replies.Add("Strong passwords should include uppercase letters, lowercase letters, numbers, and special characters.");
        replies.Add("Phishing attempts often use fake links and urgent language. Be cautious.");
        replies.Add("Always use multi-factor authentication for sensitive accounts.");
        replies.Add("Public Wi-Fi can expose you to hackers. Use a VPN when possible.");
    }

    private void ProcessQuery(string query)
    {
        bool answered = false;

        foreach (string reply in replies)
        {
            if (query.ToLower().Contains("password") && reply.ToLower().Contains("password"))
            {
                Console.ForegroundColor = ConsoleColor.Gray;


                Console.WriteLine("Chatbot: " + reply);
                Console.ResetColor();
                answered = true;
                break;
            }
            else if (query.ToLower().Contains("phishing") && reply.ToLower().Contains("phishing"))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Chatbot: " + reply);


                Console.ResetColor();
                answered = true;
                break;
            }
        }

        // Check sentiment for an emotional response
        foreach (var sentiment in sentimentResponses.Keys)
        {
            if (query.ToLower().Contains(sentiment))
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Chatbot: " + sentimentResponses[sentiment]);
                Console.ResetColor();
                answered = true;
            }
        }

        // Check for keyword recognition
        string keywordResp = keywordRecognition.GetResponse(query);
        if (!string.IsNullOrEmpty(keywordResp))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Chatbot: " + keywordResp);
            Console.ResetColor();
            answered = true;
        }

        // Detect interest
        if (query.ToLower().Contains("interested in"))
        {
            foreach (var topic in keywordResponses.Keys)
            {
                if (query.ToLower().Contains(topic))
                {
                    userMemory["favoriteTopic"] = topic;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("    ");

                    //Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Great! I'll remember that you're interested in {topic}.");
                    // Console.WriteLine("          ");
                }
            }
        }

        // Flag to track if a keyword was found in the user's query
        bool found = false;

        foreach (var keyword in keywordResponses.Keys)
        {
            // Convert user input to lowercase and check if it contains a known keyword
            if (query.ToLower().Contains(keyword))
            {
                Random rand = new Random(); // Random object for selecting responses
                List<string> responses = keywordResponses[keyword];
                string reply = responses[rand.Next(responses.Count)]; // Select a random response

                // Set console text color for chatbot response
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.ResetColor();
                Console.WriteLine("    ");
                Console.WriteLine(reply); // Display chatbot reply

                // Track response count and mention favorite topic every 3 turns if stored in memory
                responseCount++;
                if (userMemory.ContainsKey("favoriteTopic") && responseCount % 3 == 0)
                {
                    Console.WriteLine($"Since you're interested in {userMemory["favoriteTopic"]}, here's something to keep in mind: {reply}");
                }

                found = true; // Mark keyword as found
                break; // Stop loop after first match
            }
        }

        // If no keyword is found, consider adding a fallback response mechanism

        if (!answered && !found)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Chatbot: I'm not sure I understand. Can you rephrase?remember  to ask questions related to cybersecurity");
            Console.ResetColor();

        }
    }
}

    internal class Key_word
    {
        internal string GetResponse(string query)
        {
            throw new NotImplementedException();
        }
    }

    internal class memory_manager
    {
        public memory_manager()
        {
        }
    }
}