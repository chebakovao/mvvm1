using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using MyLibrary.Model;
using System.Windows;
using System.Threading;

namespace MyLibrary.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private User selectedUser;
        private Book selectedBook;
        private Book selectedUserBook;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Book> Books { get; set; }


        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
                OnPropertyChanged("FilteredUserBooks");
            }
        }

        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }


        private RelayComand addCommand;
        public RelayComand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayComand(obj =>
                  {
                      if (selectedUser == null)
                      {
                          MessageBox.Show("Не выбран пользователь в меню слева, которому Вы собираетесь добавить выбранную книгу!", "Внимание!");
                      }
                      if (selectedBook == null)
                      {
                          MessageBox.Show("Не выбрана книга в меню справа, которую Вы собираетесь добавить выбранному пользователю!", "Внимание!");
                      }
                      else
                      {
                          if (selectedBook.Count == 0) MessageBox.Show("Эта книга закончилась на складе!", "Внимание!");
                          else
                          {                       
                              selectedUser.UserBooks.Insert(0, selectedBook);
                              selectedBook.Count--;
                          }
                      }
                  }));
            }
        }

        private RelayComand removeCommand;
        public RelayComand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayComand(obj =>
                  {
                      if (selectedUser == null)
                      {
                          MessageBox.Show("Не выбран пользователь в меню слева, у которого Вы собираетесь удалить выбранную книгу!", "Внимание!");
                      }
                 
                      else
                      {
                          selectedUser.UserBooks.Remove(selectedUserBook);
                          if (SelectedBook.Count < 15)
                          {
                              selectedBook.Count++;
                          }
                      }
                  }));
            }
        }


        public ObservableCollection<User> FilteredUsers
        {
            get
            {
                if (searchUserText != null)
                {
                    return new ObservableCollection<User>(Users.Where(x => x.Name.Contains(SearchUserText, StringComparison.OrdinalIgnoreCase) ||
                    x.Surname.Contains(SearchUserText, StringComparison.OrdinalIgnoreCase) || Convert.ToString(x.Id).Contains(SearchUserText)));
                }
                else
                {
                    return Users;
                }
            }
        }

        private string searchUserText;
        public string SearchUserText
        {
            get { return searchUserText; }
            set
            {
                searchUserText = value;
                OnPropertyChanged("SearchUserText");
                OnPropertyChanged("FilteredUsers");
            }
        }

        public ApplicationViewModel()
        {
            Books = new ObservableCollection<Book>
            {
                new Book { Article = 4532, Author = "Лев Толстой", Name = "Война и мир", Age = 1869, Count = 15 },
                new Book { Article = 9032, Author = "Антон Чехов", Name = "Дама с собачкой", Age = 1899, Count = 15 },
                new Book { Article = 3467, Author = "Михаил Булгаков", Name = "Мастер и Маргарита", Age = 1966, Count = 15 },
                new Book { Article = 7895, Author = "Владимир Набоков", Name = "Лолита", Age = 1955, Count = 15 },
                new Book { Article = 5682, Author = "Борис Пастернак", Name = "Доктор Живаго", Age = 1957, Count = 15 },
                new Book { Article = 9025, Author = "Александр Солженицын", Name = "Архипелаг ГУЛАГ", Age = 1973, Count = 15 },
                new Book { Article = 3455, Author = "Иван Тургенев", Name = "Отцы и дети", Age = 1862, Count = 15 },
                new Book { Article = 7892, Author = "Николай Гоголь", Name = "Мертвые души", Age = 1842, Count = 15 },
                new Book { Article = 3435, Author = "Александр Пушкин", Name = "Евгений Онегин", Age = 1830, Count = 15 },
                new Book { Article = 1360, Author = "Максим Горький", Name = "Мать", Age = 1906, Count = 15 }
            };

            Users = new ObservableCollection<User>
            {
                new User {Id = 0, Name = "Алексей", Surname = "Смирнов", UserBooks = {  } },
                new User {Id = 1, Name = "Мария", Surname = "Петрова", UserBooks = {  } },
                new User {Id = 2, Name = "Владимир", Surname = "Иванов", UserBooks = { } },
                new User {Id = 3, Name = "Анна", Surname = "Соколова", UserBooks = { } },
                new User {Id = 4, Name = "Сергей", Surname = "Попов", UserBooks = { } },
                new User {Id = 5, Name = "Екатерина", Surname = "Кузнецова", UserBooks = {  } },
                new User {Id = 6, Name = "Дмитрий", Surname = "Новиков", UserBooks = { } },
                new User {Id = 7, Name = "Наталья", Surname = "Волкова", UserBooks = { } }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
