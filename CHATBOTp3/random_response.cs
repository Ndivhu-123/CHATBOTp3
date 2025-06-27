
using System;
using System.Collections.Generic;

namespace CHATBOTp3
    {
        /// <summary>
        /// Generates cybersecurity tips and educational responses.
        /// </summary>
        public class random_response
        {
            public random_response() { }

            // List containing various cybersecurity tips for different security aspects
        private List<string> cybersecurityTips = new List<string>
{
    // --- Phishing Protection ---
    "Be cautious of emails that request sensitive information such as your ID number, login credentials, or bank details. Legitimate organizations will never ask for this kind of information via email.",
    "Avoid clicking on unfamiliar or shortened links—especially from messages claiming urgency. Always hover over the link to preview the real URL before visiting any page.",
    "Never provide your password, credit card number, or banking details through email or phone unless you've verified the request directly.",
    "Look for spelling and grammar mistakes in suspicious emails. Attackers often overlook small details that expose fraudulent messages.",
    "Use official contact details from a company’s website instead of trusting contact info from an unsolicited message.",

    // --- Password Security ---
    "Use strong, unique passwords that contain uppercase, lowercase, numbers, and special characters. A complex password drastically reduces the chances of being guessed.",
    "Never reuse passwords across different platforms. If one gets breached, all linked accounts are compromised.",
    "Use a reputable password manager to generate and store long, complex passwords securely.",
    "Avoid using personal information like birthdays or pet names as passwords. These are easily guessed from public profiles.",
    "Change your passwords regularly, especially for sensitive accounts like banking, email, or work platforms.",

    // --- Secure Browsing ---
    "Always check for 'HTTPS' and a padlock icon in the browser address bar before entering sensitive information. This confirms the site uses encryption.",
    "Enable two-factor authentication (2FA) on services you use. Even if your password is stolen, 2FA adds an extra barrier to access.",
    "Clear cookies and cached data from your browser regularly to avoid tracking and exposure of personal information.",
    "Avoid auto-filling personal data on public computers or shared devices. Disable form autofill in browser settings when needed.",
    "Use browser privacy extensions to block ads, stop trackers, and prevent malicious scripts from loading.",

    // --- Device & Network Security ---
    "Keep your software and operating systems up to date. Developers frequently issue security patches that fix vulnerabilities.",
    "Use a virtual private network (VPN) when connected to public Wi-Fi to encrypt your internet traffic and hide your activity.",
    "Turn off Bluetooth and Wi-Fi when not in use, especially in public spaces, to reduce exposure to unauthorized connections.",
    "Secure your devices with a lock screen, PIN, fingerprint, or facial recognition to prevent unauthorized access.",
    "Be cautious when connecting USB drives or unknown accessories — they might carry malware or spyware.",

    // --- Social Engineering Awareness ---
    "Always verify a sender's identity before responding to messages requesting sensitive info. Call back using verified contact numbers if in doubt.",
    "Don’t overshare personal details online. Cybercriminals often gather background information from public profiles for targeted attacks.",
    "Pause before reacting to high-pressure or urgent messages. Social engineers use fear or urgency to bypass your judgment.",
    "Educate yourself and coworkers about the latest scams. Awareness is your first line of defense.",
    "Don’t trust pop-ups or calls claiming your computer is infected — they often link to fake tech support scams."
};


        /// <summary>
        /// Retrieves a random cybersecurity tip from the list.
        /// </summary>
        /// <returns>One tip string randomly selected.</returns>
        public string GetRandomTip()
            {
                Random random = new Random();
                int index = random.Next(cybersecurityTips.Count);
                return cybersecurityTips[index];
            }
        }

    // Detects sentiment in a given input
    public class sentiment_handler
    {
        private readonly string[] positivekeywords = { "good", "great", "happy", "excited", "amazing", "fun", "love", "cool" };
        private readonly string[] negativekeywords = { "sad", "bad", "angry", "frustrated", "upset", "hate", "worried", "tired", "scared", "nervous" };

        // Returns detected sentiment as a string
        public string DetectSentiment(string input)
        {
            string lowerInput = input.ToLower();

            foreach (string word in positivekeywords)
                if (lowerInput.Contains(word)) return "positive";

            foreach (string word in negativekeywords)
                if (lowerInput.Contains(word)) return "negative";

            return "neutral";
        }
    }

}
    
