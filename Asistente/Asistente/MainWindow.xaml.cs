using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace Asistente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine reconocedor = new SpeechRecognitionEngine();
        SpeechSynthesizer asistente = new SpeechSynthesizer();
        String speech;
        bool habilitarReconocimiento = true;

        public MainWindow()
        {
            InitializeComponent();
            asistente.SpeakAsync("Todos los sistemas cargados");
            cargarGramaticas();
        }

        void cargarGramaticas()
        {
            reconocedor.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"Comandos.txt")))));
            reconocedor.RequestRecognizerUpdate();
            reconocedor.SpeechRecognized += reconocedor_SpeechRecognized;
            //asistente.SpeakStarted += asistente_SpeakStarted;
            //asistente.SpeakCompleted += asistente_SpeakCompleted;
            reconocedor.SetInputToDefaultAudioDevice();
            reconocedor.RecognizeAsync(RecognizeMode.Multiple);
        }
        private void reconocedor_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            speech = e.Result.Text;
            if (habilitarReconocimiento)
            {
                switch (speech)
                {
                    case "hola":
                        asistente.Speak("Buenos dias señor" + ". . .");
                        lblreconocimiento.Content = "";
                        lblreconocimiento.Content = speech;
                        break;
                    case "navegar":
                        asistente.SpeakAsync("a la orden señor, abriendo el buscador" +
                       ". . .");
                        System.Diagnostics.Process.Start("https://www.google.com");
                        lblreconocimiento.Content = "";
                        lblreconocimiento.Content = speech;
                        break;

                    default:
                        break;
                }
            }//fin if
        }
    }
}
