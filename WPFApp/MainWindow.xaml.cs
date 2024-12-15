using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateTask();
        }

        void AddMessage(string message)
        {
            int CurrentThreadId = Thread.CurrentThread.ManagedThreadId;
            this.Dispatcher.Invoke(() =>
            {
                Messages.Content +=
                    $"Mensaje: {message}, " +
                    $"Hilo Actual: {CurrentThreadId}\n";

            });
        }

        void CreateTask()
        {
            Task T;
            Action Code = new Action(ShowMessage);
            T = new Task(Code);
            Task T2 = new Task(
                delegate
                {
                    MessageBox.Show("Ejecutando una tarea en método Anónimo");
                }
            );

            Task T3A = new Task(ShowMessage);

            Task T3 = new Task(
                () => ShowMessage()
            );

            Task T4 = new Task(() => MessageBox.Show("Ejecutando la tarea 4"));

            Task T5 = new Task(() =>
                {
                    DateTime CurrentDate = DateTime.Today;
                    DateTime StartDate = CurrentDate.AddDays(30);
                    MessageBox.Show($"Tarea 5. Fecha Calculada: {StartDate}");
                }
            );

            Task T6 = new Task((message) => MessageBox.Show(message.ToString()), "Expresión Lambda con parámetros.");

            Task T7 = new Task(() => AddMessage("Ejecutando la tarea."));
            T7.Start();
            AddMessage("En el hilo principal");
        }

        

        void ShowMessage()
        {
            MessageBox.Show("Ejecutando el método ShowMessage");
        }

    }
}