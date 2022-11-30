using iPatient.Managers;
using iPatient.Model;
using iPatient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPatient.ViewModels
{
    public class MyVisitsViewModel : BaseViewModel<MyVisitsPage>
    {
        public Command<UserVisit> VisitClickedCommand { get; set; }
        public ObservableCollection<UserVisit> Visits { get; set; }
        public MyVisitsViewModel(string title, MyVisitsPage viewPage) : base(title, viewPage)
        {
            Visits = new ObservableCollection<UserVisit>();
            VisitClickedCommand = new Command<UserVisit>(VisitClicked);
        }

        public void Load()
        {
            _viewPage.ShowPopupPage(new WaitingPopupPage(async delegate ()
            {
                var result = await APIManager.GetAllUserVisits();

                if (!result.ok)
                {
                    _viewPage.ShowPopupPage(new InfoPopupPage(result.errors));
                    return false;
                }

                Visits.Clear();

                foreach(var visit in result.visits)
                {
                    Visits.Add(visit);
                }
                

                return true;

            }, null, null, "Pobieranie danych..."));
        }

        private void VisitClicked(UserVisit userVisit)
        {
            _viewPage.ShowPopupPage(new InfoPopupPage(userVisit.FacilityInfoToString())); 
        }
    }
}
