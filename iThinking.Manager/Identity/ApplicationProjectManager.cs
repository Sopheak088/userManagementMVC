using iThinking.Common.Helpers;
using iThinking.Manager.Common;
using iThinking.UserCenter.Identity;
using iThinking.ViewModel;
using iThinking.ViewModel.Identity;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iThinking.Manager.Identity
{
    public class ApplicationProjectManager : ObjectManager
    {
        public ApplicationProjectManager(IUnitOfWorkAsync unitOfWorkAsync) : base(unitOfWorkAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public IEnumerable<ApplicationProject> GetAll()
        {
            return _unitOfWorkAsync.Repository<ApplicationProject>().Query()
                .Select().AsEnumerable().OrderByDescending(t => t.CreatedDate);
        }

        public IEnumerable<ApplicationProject> GetAll(ApplicationProjectIndexViewModel applicationProjectIndexViewModel)
        {
            var _applicationProjects = GetAll(applicationProjectIndexViewModel.Keyword);
            
            if (!string.IsNullOrWhiteSpace(applicationProjectIndexViewModel.Id))
            {
                _applicationProjects = _applicationProjects.Where(m => m.Id == applicationProjectIndexViewModel.Id);
            }
            if (!string.IsNullOrWhiteSpace(applicationProjectIndexViewModel.Name))
            {
                _applicationProjects = _applicationProjects.Where(m => m.Name.ToLower().Contains(applicationProjectIndexViewModel.Name.ToLower()));
            }

            return _applicationProjects.OrderByDescending(t => t.CreatedDate);
        }

        public IEnumerable<ApplicationProject> GetInMonthByDate(DateTime date)
        {
            DateTime _firstDayOfMonth = DatetimeHelpers.FirstDayOfMonth(date);
            DateTime _lastDayOfMonth = DatetimeHelpers.LastDayOfMonth(date);

            var _projects = GetAll().Where(m => m.CreatedDate >= _firstDayOfMonth && m.CreatedDate <= _lastDayOfMonth).AsEnumerable().OrderByDescending(t => t.CreatedDate);
            return _projects;
        }

        public DashboardTopTitleViewModel UpdateDashboardTopTitleViewModel(DashboardTopTitleViewModel dashboardTopTitleViewModel)
        {
            dashboardTopTitleViewModel.TotalProject = GetAll().Count();
            dashboardTopTitleViewModel.TotalProjectInMonth = GetInMonthByDate(DateTime.Now).Count();

            return dashboardTopTitleViewModel;
        }

        public void Insert(ApplicationProject applicationProject)
        {
            _unitOfWorkAsync.Repository<ApplicationProject>().Insert(applicationProject);
            _unitOfWorkAsync.SaveChanges();
        }

        public void InsertRange(IEnumerable<ApplicationProject> applicationProjects)
        {
            _unitOfWorkAsync.Repository<ApplicationProject>().InsertRange(applicationProjects);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Update(ApplicationProject applicationProject)
        {
            _unitOfWorkAsync.Repository<ApplicationProject>().Update(applicationProject);
            _unitOfWorkAsync.SaveChanges();
        }

        public void Delete(ApplicationProject applicationProject)
        {
            _unitOfWorkAsync.Repository<ApplicationProject>().Delete(applicationProject);
            _unitOfWorkAsync.SaveChanges();
        }

        public void DeleteRange(IEnumerable<ApplicationProject> applicationProjects)
        {
            _unitOfWorkAsync.Repository<ApplicationProject>().DeleteRange(applicationProjects);
            _unitOfWorkAsync.SaveChanges();
        }

        public IEnumerable<ApplicationProject> GetAll(string keyword)
        {
            var _applicationProjects = GetAll();

            if (!string.IsNullOrEmpty(keyword))
                return _applicationProjects.Where(m => m.Description.Contains(keyword)).AsEnumerable().OrderByDescending(t => t.CreatedDate);
            else
                return _applicationProjects;
        }

        public ApplicationProject Find(string id)
        {
            return GetAll().FirstOrDefault(m => m.Id == id);
        }
    }
}