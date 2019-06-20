using iThinking.Common.Helpers;
using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationErrorManager : ObjectManager
    {
        public ApplicationErrorManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationError> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationError>().Query().Select().AsEnumerable().OrderByDescending(t => t.CreatedDate);
        }

        public IEnumerable<ApplicationError> GetInMonthByDate(DateTime date)
        {
            DateTime _firstDayOfMonth = DatetimeHelpers.FirstDayOfMonth(date);
            DateTime _lastDayOfMonth = DatetimeHelpers.LastDayOfMonth(date);

            var _errors = GetAll().Where(m => m.CreatedDate >= _firstDayOfMonth && m.CreatedDate <= _lastDayOfMonth).AsEnumerable().OrderByDescending(t => t.CreatedDate);
            return _errors;
        }

        public DashboardTopTitleViewModel UpdateDashboardTopTitleViewModel(DashboardTopTitleViewModel dashboardTopTitleViewModel)
        {
            dashboardTopTitleViewModel.TotalError = GetAll().Count();
            dashboardTopTitleViewModel.TotalErrorInMonth = GetInMonthByDate(DateTime.Now).Count();

            return dashboardTopTitleViewModel;
        }

        public void Insert(ApplicationError error)
        {
            _unitOfWorkAsync.Repository<ApplicationError>().Insert(error);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationError> errors)
        {
            _unitOfWorkAsync.Repository<ApplicationError>().InsertRange(errors);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationError error)
        {
            _unitOfWorkAsync.Repository<ApplicationError>().Update(error);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationError error)
        {
            _unitOfWorkAsync.Repository<ApplicationError>().Delete(error);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationError> errors)
        {
            _unitOfWorkAsync.Repository<ApplicationError>().DeleteRange(errors);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationError> GetAll(string keyword)
        {
            var _errors = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                _errors = _errors.Where(m => m.Message.Contains(keyword));

            return _errors;
        }

        public ApplicationError Find(int id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }
    }
}