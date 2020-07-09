using System.ComponentModel;

namespace HomeBudget.Client.Model
{
    public class Message : INotifyPropertyChanged
    {
        private bool visibility;
        private int color;
        private string messageContent;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Visibility"));
            }
        }

        public int Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
            }
        }

        public string MessageContent
        {
            get
            {
                return messageContent;
            }
            set
            {
                messageContent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MessageContent"));
            }
        }
    }
}
