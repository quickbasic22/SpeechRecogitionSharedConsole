using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecogitionSharedConsole
{
    public class ColorGrammar
    {
       public void GetColorGrammar()
        {
            Choices colorChoice = new System.Speech.Recognition.Choices();

            //red
            GrammarBuilder colorBuilder = new System.Speech.Recognition.GrammarBuilder("red");
            SemanticResultValue colorValue = new System.Speech.Recognition.SemanticResultValue(colorBuilder, "#FF0000");
            colorChoice.Add(new System.Speech.Recognition.GrammarBuilder(new SemanticResultValue(colorValue)));

            //green
            colorBuilder = new System.Speech.Recognition.GrammarBuilder("green");
            colorValue = new System.Speech.Recognition.SemanticResultValue(colorBuilder, "#00FF00");
            colorChoice.Add(new System.Speech.Recognition.GrammarBuilder(new SemanticResultValue(colorValue)));

            //blue
            colorBuilder = new System.Speech.Recognition.GrammarBuilder("blue");
            colorValue = new System.Speech.Recognition.SemanticResultValue(colorBuilder, "#0000FF");
            colorChoice.Add(new System.Speech.Recognition.GrammarBuilder(new SemanticResultValue(colorValue)));

            GrammarBuilder colorElement = new System.Speech.Recognition.GrammarBuilder(colorChoice);

            GrammarBuilder makePhrase = new System.Speech.Recognition.GrammarBuilder("Make background");
            makePhrase.Append(colorElement);
            GrammarBuilder setPhrase = new System.Speech.Recognition.GrammarBuilder("Set background to");
            setPhrase.Append(colorElement);

            Choices bothChoices = new System.Speech.Recognition.Choices();
            bothChoices.Add(makePhrase);
            bothChoices.Add(setPhrase);

            GrammarBuilder bothPhrases = new System.Speech.Recognition.GrammarBuilder(bothChoices);

            SemanticResultKey colorKey = new System.Speech.Recognition.SemanticResultKey("ColorCode", bothPhrases);
            bothPhrases = new System.Speech.Recognition.GrammarBuilder(colorKey);

            Grammar grammar = new System.Speech.Recognition.Grammar(bothPhrases);
            grammar.Name = "backgroundColor";

            SpeechRecognitionEngine recognizer = new System.Speech.Recognition.SpeechRecognitionEngine();
            recognizer.LoadGrammar(grammar);

           
                recognizer.SpeechRecognized += Recognizer_SpeechRecognized;

            recognizer.SetInputToDefaultAudioDevice();
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
                      

            Console.WriteLine("Begin Speaking now");

        }

        private void Recognizer_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            
                Console.WriteLine($"you said: {0} color hex is:", e.Result.Text);
            
           
        }
    }

}
