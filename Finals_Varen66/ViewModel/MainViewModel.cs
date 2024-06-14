using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Input;
using Finals_Varen66.Models;
using Finals_Varen66.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Finals_Varen66.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;
        private ICollectionView _usersView;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView UsersView
        {
            get => _usersView;
            private set
            {
                _usersView = value;
                OnPropertyChanged();
            }
        }

        public ICommand SortByNameCommand { get; }
        public ICommand SortByRoleCommand { get; }

        public MainViewModel()
        {
            SortByNameCommand = new RelayCommand(SortByName);
            SortByRoleCommand = new RelayCommand(SortByRole);


            LoadUsers();

            private void LoadUsers()
            {
                using (var dbContext = new AppDbContext())
                {
                    var users = dbContext.Users
                        .Include(u => u.Employee)
                        .Include(u => u.Role)
                        .ToList();

                    Users = new ObservableCollection<User>(users);
                    UsersView = CollectionViewSource.GetDefaultView(Users);
                }
            }



            private void SortByName()
            {
                UsersView.SortDescriptions.Clear();
                UsersView.SortDescriptions.Add(new SortDescription("Employee.FullName", ListSortDirection.Ascending));
                UsersView.Refresh();
            }


            private void SortByRole()
            {
                UsersView.SortDescriptions.Clear();
                UsersView.SortDescriptions.Add(new SortDescription("Role.RoleName", ListSortDirection.Ascending));
                UsersView.Refresh();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
