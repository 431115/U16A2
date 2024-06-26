using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        // Collection of TaskItem objects that will be displayed in the UI
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent(); // Initializes the UI components
            Tasks = new ObservableCollection<TaskItem>(); // Initializes the Tasks collection
            TaskList.ItemsSource = Tasks; // Binds the Tasks collection to the TaskList UI element
        }

        // Event handler for adding a new task
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            // makes sure you select a date thats not before today
            if (DueDatePicker.SelectedDate < DateTime.Today)
            {
                MessageBox.Show("Please select a valid date.", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Creates a new TaskItem with input values
            var newTask = new TaskItem
            {
                Title = TitleBox.Text == "Task Name" ? string.Empty : TitleBox.Text,
                Description = DescriptionBox.Text == "Task Description" ? string.Empty : DescriptionBox.Text,
                DueDate = DueDatePicker.SelectedDate ?? DateTime.Now,
                IsCompleted = false
            };
            Tasks.Add(newTask); // Adds the new task to the Tasks collection
            ClearInputFields(); // Clears the input fields for the next entry
        }

        // Event handler for deleting a selected task
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem selectedTask) // Checks if a task is selected
            {
                Tasks.Remove(selectedTask); // Removes the selected task from the Tasks collection
            }
        }

        // Event handler for marking a selected task as completed
        private void MarkAsComplete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskList.SelectedItem is TaskItem selectedTask) // Checks if a task is selected
            {
                selectedTask.IsCompleted = true; // Marks the task as completed
                TaskList.Items.Refresh(); // Refreshes the TaskList UI to reflect changes
            }
        }

        // Event handler for showing or hiding completed tasks
        private void ShowCompletedTasks_Checked(object sender, RoutedEventArgs e)
        {
            if (ShowCompletedTasksCheckBox.IsChecked == true) // Checks if the checkbox is checked
            {
                TaskList.ItemsSource = Tasks.Where(t => t.IsCompleted); // Shows only completed tasks
            }
            else
            {
                TaskList.ItemsSource = Tasks.Where(t => !t.IsCompleted); // Shows only incomplete tasks
            }
        }

        // Clears the input fields in the UI
        private void ClearInputFields()
        {
            TitleBox.Text = "Task Name";
            TitleBox.Foreground = Brushes.Gray;
            DescriptionBox.Text = "Task Description";
            DescriptionBox.Foreground = Brushes.Gray;
            DueDatePicker.SelectedDate = null;
        }

        // Event handler for removing placeholder text
        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        // Event handler for adding placeholder text
        private void AddPlaceholderText(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = Brushes.Gray;
            }
        }
    }

    // Represents a task item with properties for title, description, due date, and completion status
    public class TaskItem
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        // Returns a string representation of the TaskItem
        public override string ToString()
        {
            return $"{Title} - {Description} (Due: {DueDate.ToShortDateString()}) - {(IsCompleted ? "Completed" : "Incomplete")}";
        }
    }
}
