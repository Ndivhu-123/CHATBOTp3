THE POE READMEFILE

- PART 3
Cybersecurity Chatbot – WPF Desktop Application
This chatbot serves as an interactive cybersecurity companion, designed with educational, task management, and natural language understanding capabilities. Built in WPF using C#, it simulates intelligent conversation, quizzes users on best practices, and helps manage reminders related to online safety.

Table of Contents
Project Overview

Features

Welcome Interface

Chat Input and Keyword Detection

Cybersecurity Quiz

Activity Log

Task and Reminder Panel (Optional)

Project Structure

How to Use

Technologies Used

Recommendations for Extension

Project Overview
This application is a desktop-based AI-style chatbot built in C# and WPF. It blends string-based natural language recognition with interactive UI components to provide cybersecurity education, task assistance, and basic chatbot conversation in a sleek, modern layout.

Features
1. Welcome Interface
When the application is launched, users are prompted to enter their name.

The chatbot greets them personally and transitions to the main interface.

2. Chat Input and Keyword Detection
A simulated NLP engine uses string.Contains() to detect user intent.

Recognized intent categories include:

Task creation: “remind me”, “add task”, “set reminder”

Starting a quiz: “quiz”, “test me”, “ask questions”

Checking logs: “activity log”, “what have you done”, “show history”

Cybersecurity terms: “phishing”, “password”, “malware”, etc.

3. Cybersecurity Quiz
Users can start a quiz either via text commands or by clicking "Start Quiz".

The quiz includes:

20 cybersecurity questions (multiple choice format)

Immediate feedback with explanations

Scoring system: 5 points per question

Final evaluation:

Above 50: “Great job! You're a cybersecurity pro!”

50 or below: “Keep learning to stay safe online!”

4. Activity Log
Tracks user inputs and bot responses.

Clicking “Activity Log” opens a separate window (ActivityLogWindow).

The log displays the five most recent actions for user review.

5. Task and Reminder Panel (Optional)
Architecture in place for adding custom tasks.

Includes UI elements for entering a title, description, and date.

Prepares the foundation for further task logic and scheduling integration.

Project Structure
File	Description
MainWindow.xaml	Defines the layout for the welcome, chat, quiz, and menu interface
MainWindow.xaml.cs	Contains logic for NLP recognition, chatbot response handling, quiz engine, and UI events
ActivityLogWindow.xaml	Layout for the popup activity log viewer
ActivityLogWindow.xaml.cs	Populates the activity log viewer with the latest interactions
QuizQuestion class	Represents a single quiz question with answers and feedback
How to Use
Launch the application.

Enter your name to begin.

Use the chatbot interface to:

Ask questions or start the quiz

Create reminders

View recent interactions via the Activity Log

Optionally manage tasks and reminders using the task panel.

Technologies Used
WPF (.NET) — UI development

C# — Logic and structure

String manipulation — Lightweight keyword-based NLP

Observable controls — ListView, ListBox, StackPanel for dynamic content

Modular XAML — Reusable layout components

Recommendations for Extension
Implement persistent data storage (SQLite, file-based) for tasks and logs

Add support for exporting quiz scores and logs

Enhance NLP using Regex or integrate a lightweight ML.NET model

Enable speech input and output for accessibility

Expand quiz content with more categories and difficulty levels
