using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace MyLibrary.Model
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string surname;
        ObservableCollection<Book> userbooks;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Surname
        {
            get { return surname; }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }

        public ObservableCollection<Book> UserBooks
        {
            get 
            {   if (userbooks == null)
                {
                    userbooks = new ObservableCollection<Book>();
                }
                return userbooks; 
            }
            set
            {
                userbooks = value;
                OnPropertyChanged("UserBooks");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
