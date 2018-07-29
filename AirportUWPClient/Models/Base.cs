using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AirportUWPClient.Models
{
    public class BaseModel : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get => id;
            set
            {
                if(id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
